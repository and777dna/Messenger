using System.Text.Json;
using Messenger.Caching;
using Messenger.DTOs.Requests;
using Messenger.DTOs.Responses;
using Messenger.Entities;
using Messenger.MapperDto;
using Messenger.MapperDto.Interfaces;
using Messenger.Repositories.Interfaces;
using Messenger.Services;
using Messenger.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Messenger.Tests.Services;

public class ChatServiceTests
{
    private readonly Mock<IMapper<Message, RequestMessageDto>> _mapper = new(MockBehavior.Strict);
    private readonly Mock<IMessageRepository> _messageRepository = new(MockBehavior.Strict);
    private readonly Mock<IDistributedCache> _cache = new(MockBehavior.Strict);
    private readonly Mock<IChatNotifier> _notifier = new(MockBehavior.Strict);
    private readonly ChatService _sut;

    public ChatServiceTests()
    {
        _sut = new ChatService(_mapper.Object, new ResponseMessageMapper(), _messageRepository.Object,
            _cache.Object, _notifier.Object, NullLogger<ChatService>.Instance);
    }

    private static RequestMessageDto CreateDto() => new()
    {
        ChatId = Guid.NewGuid(),
        SenderId = Guid.NewGuid(),
        Content = "hello",
        IdempotencyKey = Guid.NewGuid()
    };

    private static Message CreateMessage(RequestMessageDto dto) => new()
    {
        ChatId = dto.ChatId,
        SenderId = dto.SenderId,
        Content = dto.Content,
        IdempotencyKey = dto.IdempotencyKey
    };

    private static List<Message> CreateMessages(Guid chatId, int count) =>
        Enumerable.Range(1, count)
            .Select(i => new Message { ChatId = chatId, SenderId = Guid.NewGuid(), Content = $"message {i}" })
            .ToList();

    private void SetupHappyPath(RequestMessageDto dto, Message message)
    {
        _mapper.Setup(m => m.ToEntity(dto)).Returns(message);
        _cache.Setup(c => c.RemoveAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _messageRepository.Setup(r => r.AddAsync(It.IsAny<Message>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _notifier.Setup(n => n.MessagePostedAsync(It.IsAny<RequestMessageDto>()))
            .Returns(Task.CompletedTask);
    }

    [Fact]
    public async Task SendMessageAsync_AddsMappedMessageToRepository()
    {
        var dto = CreateDto();
        var message = CreateMessage(dto);
        SetupHappyPath(dto, message);

        await _sut.SendMessageAsync(dto, CancellationToken.None);

        _mapper.Verify(m => m.ToEntity(dto), Times.Once);
        _messageRepository.Verify(r => r.AddAsync(message, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SendMessageAsync_ValidDto_InvalidatesChatCache()
    {
        var dto = CreateDto();
        var message = CreateMessage(dto);
        SetupHappyPath(dto, message);

        await _sut.SendMessageAsync(dto, CancellationToken.None);

        var cacheKey = $"chat:{dto.ChatId}:messages";
        _cache.Verify(c => c.RemoveAsync(cacheKey, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SendMessageAsync_ValidDto_NotifiesClientsWithSameDto()
    {
        var dto = CreateDto();
        var message = CreateMessage(dto);
        SetupHappyPath(dto, message);

        await _sut.SendMessageAsync(dto, CancellationToken.None);

        _notifier.Verify(n => n.MessagePostedAsync(It.Is<RequestMessageDto>(d
            => ReferenceEquals(d, dto))), Times.Once);
    }

    [Fact]
    public async Task GetMessagesAsync_CacheMiss_CachesWholePageAndReturnsLimit()
    {
        var chatId = Guid.NewGuid();
        var cacheKey = $"chat:{chatId}:messages";
        var messages = CreateMessages(chatId, 3);
        byte[]? cachedBytes = null;
        _cache.Setup(c => c.GetAsync(cacheKey, It.IsAny<CancellationToken>()))
            .ReturnsAsync((byte[]?)null);
        _messageRepository.Setup(r => r.GetLatestAsync(chatId, 50, null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(messages);
        _cache.Setup(c => c.SetAsync(cacheKey, It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(),
                It.IsAny<CancellationToken>()))
            .Callback<string, byte[], DistributedCacheEntryOptions, CancellationToken>(
                (_, value, _, _) => cachedBytes = value)
            .Returns(Task.CompletedTask);

        var result = await _sut.GetMessagesAsync(chatId, limit: 2, CancellationToken.None);

        Assert.Equal(messages.Take(2).Select(m => m.Id), result.Select(m => m.Id));
        var cached = JsonSerializer.Deserialize<List<ResponseMessageDto>>(cachedBytes!);
        Assert.Equal(messages.Select(m => m.Id), cached!.Select(m => m.Id));
    }

    [Fact]
    public async Task GetMessagesAsync_CacheHit_ReturnsCachedMessagesWithoutDatabase()
    {
        var chatId = Guid.NewGuid();
        var cached = Enumerable.Range(1, 3)
            .Select(i => new ResponseMessageDto(Guid.NewGuid(), chatId, Guid.NewGuid(), $"message {i}",
                new DateTimeOffset(2026, 1, 1, 10, i, 0, TimeSpan.Zero), IsRead: true))
            .ToList();
        _cache.Setup(c => c.GetAsync($"chat:{chatId}:messages", It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonSerializer.SerializeToUtf8Bytes(cached));

        var result = await _sut.GetMessagesAsync(chatId, limit: 2, CancellationToken.None);

        // The repository mock is strict, so any database call would fail this test.
        Assert.Equal(cached.Take(2), result);
    }
}

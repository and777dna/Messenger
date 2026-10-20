using Messenger.Caching;
using Messenger.DTOs.Requests;
using Messenger.Entities;
using Messenger.MapperDto.Interfaces;
using Messenger.Repositories.Interfaces;
using Messenger.Services;
using Messenger.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
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
        _sut = new ChatService(_mapper.Object, _messageRepository.Object, _cache.Object, _notifier.Object);
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

    private void SetupHappyPath(RequestMessageDto dto, Message message)
    {
        _mapper.Setup(m => m.ToEntity(dto)).Returns(message);
        _cache.Setup(c => c.RefreshAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _messageRepository.Setup(r => r.AddAsync(It.IsAny<Message>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
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
}

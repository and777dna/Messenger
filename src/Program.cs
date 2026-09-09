using Messenger.Caching;
using Messenger.Data;
using Messenger.Hubs;
using Messenger.Integrations.Gif;
using Messenger.Integrations.PushNotifications;
using Messenger.Messaging;
using Messenger.Messaging.Consumers;
using Messenger.Middleware;
using Messenger.Repositories.Implementations;
using Messenger.Repositories.Interfaces;
using Messenger.Services.Implementations;
using Messenger.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Serilog;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .Enrich.FromLogContext());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSignalR();

var postgresConnectionString = builder.Configuration.GetConnectionString("MessengerDatabase");
builder.Services.AddDbContext<MessengerDbContext>(options =>
    options.UseNpgsql(postgresConnectionString));

var redisConnectionString = builder.Configuration.GetConnectionString("Redis")!;
builder.Services.AddSingleton<IConnectionMultiplexer>(
    _ => ConnectionMultiplexer.Connect(redisConnectionString));
builder.Services.AddSingleton<ICacheService, RedisCacheService>();

var rabbitMqConnectionString = builder.Configuration.GetConnectionString("RabbitMq")!;
builder.Services.AddSingleton<IConnection>(_ =>
{
    var factory = new ConnectionFactory { Uri = new Uri(rabbitMqConnectionString) };
    return factory.CreateConnectionAsync().GetAwaiter().GetResult();
});
builder.Services.AddSingleton<IEventPublisher, RabbitMqEventPublisher>();
builder.Services.AddHostedService<OfflineNotificationConsumer>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddSingleton<IPushNotificationService, FirebasePushNotificationService>();
builder.Services.AddHttpClient<IGifSearchService, GiphyClient>();

builder.Services.AddHealthChecks()
    .AddNpgSql(postgresConnectionString!)
    .AddRedis(redisConnectionString)
    .AddRabbitMQ();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapHub<ChatHub>("/hubs/chat");
app.MapHealthChecks("/health");

app.Run();

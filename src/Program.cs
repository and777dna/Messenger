using Messenger.Data;
using Messenger.Entities;
using Messenger.Hubs;
using Messenger.Middleware;
using Messenger.Repositories;
using Messenger.Repositories.Interfaces;
using Messenger.Services;
using Messenger.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("FulfilmentCenterDatabase");
builder.Services.AddDbContext<MessengerDbContext>(options =>
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        if (builder.Environment.IsDevelopment())
        {
            options.LogTo(
                msg =>
                {
                    var lines = msg.Split(Environment.NewLine);

                    var sqlLines = lines
                        .SkipWhile(l =>
                            !l.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase) &&
                            !l.TrimStart().StartsWith("INSERT", StringComparison.OrdinalIgnoreCase) &&
                            !l.TrimStart().StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase) &&
                            !l.TrimStart().StartsWith("DELETE", StringComparison.OrdinalIgnoreCase));

                    var sql = string.Join(Environment.NewLine, sqlLines);

                    if (!string.IsNullOrWhiteSpace(sql))
                    {
                        Console.WriteLine(sql);
                    }
                },
                LogLevel.Information,
                DbContextLoggerOptions.Id
            ).EnableSensitiveDataLogging();
        }
    }
);

builder.Services.AddScoped<IRepository<Chat>, SqlChatRepository>();
builder.Services.AddScoped<IMessageRepository, SqlMessageRepository>();
builder.Services.AddScoped<IRepository<User>, SqlUserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChatService, ChatService>();

builder.Services.AddSingleton<IChatNotifier, SignalRChatNotifier>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "SampleInstance";
});

builder.Services.AddSignalR();


builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapHub<ChatHub>("/hubs/chat");

app.MapControllers();
app.Run();
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Messenger.Messaging.Consumers;

public class OfflineNotificationConsumer(IConnection connection, ILogger<OfflineNotificationConsumer> logger)
    : BackgroundService
{
    private const string QueueName = "message.created";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);
        await channel.QueueDeclareAsync(QueueName, durable: true, exclusive: false, autoDelete: false,
            cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += (_, ea) =>
        {
            logger.LogInformation("Received {Bytes} bytes from {Queue}", ea.Body.Length, QueueName);
            return Task.CompletedTask;
        };

        await channel.BasicConsumeAsync(QueueName, autoAck: true, consumer, stoppingToken);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}

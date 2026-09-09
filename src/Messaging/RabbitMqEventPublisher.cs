using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Messenger.Messaging;

public class RabbitMqEventPublisher(IConnection connection) : IEventPublisher
{
    public async Task PublishAsync<T>(string queue, T @event)
    {
        using var channel = await connection.CreateChannelAsync();
        await channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queue, body: body);
    }
}

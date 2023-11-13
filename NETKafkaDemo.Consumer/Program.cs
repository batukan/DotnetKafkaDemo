using System.Text.Json;
using Confluent.Kafka;
using NETKafkaDemo.Consumer;

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:29092",
    GroupId = "product-consumer-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
{
    const string topic = "product-topic";
    consumer.Subscribe(topic);

    Console.WriteLine($"Consumer: Listening for messages on {topic}. Press Ctrl+C to exit.");

    while (true)
    {
        try
        {
            var consumeResult = consumer.Consume();
            var products = JsonSerializer.Deserialize<List<ConsumerProduct>>(consumeResult.Message.Value);

            Console.WriteLine($"Consumed message: {consumeResult.Message.Value}, {products.Count} products found.");
        }
        catch (ConsumeException e)
        {
            Console.WriteLine($"Error while consuming message: {e.Error.Reason}");
        }
    }
}
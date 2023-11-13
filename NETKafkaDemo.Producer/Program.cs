using System.Text.Json;
using Confluent.Kafka;
using NETKafkaDemo.Producer;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:29092",
    ApiVersionRequest = true
};

using (var producer = new ProducerBuilder<Null, string>(config).Build())
{
    const string topic = "product-topic";

    Console.WriteLine($"Producing messages to: {topic}. q to exit.");
    var message = Console.ReadLine();
    while (message?.ToLower() != "q")
    {
        try
        {
            var products = new Product().GenerateProducts();
            var convertedProducts = JsonSerializer.Serialize(products);
            var result = await producer.ProduceAsync(topic, new Message<Null, string> { Value = convertedProducts });
            Console.WriteLine($"Message produced to: {result.TopicPartitionOffset}, {products.Count} products generated.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
        }

        message = Console.ReadLine();
    }
}
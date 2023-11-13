namespace NETKafkaDemo.Producer;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string Name { get; set; }

    public int Stock { get; set; }
    
    private static readonly string[] Names = { "Laptop", "Phone", "Tablet", "Camera", "Headphones", "Printer", "Monitor", "Keyboard", "Mouse", "Speaker" };
    
    public List<Product> GenerateProducts()
    {
        var products = new List<Product>();
        var random = new Random();
        var count = random.Next(1, 10);
         
        for (var i = 0; i < count; i++)
        {
            products.Add(new Product
            {
                Name = Names[i % Names.Length],
                Stock = random.Next(1, 100)
            });
        }

        return products;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

var productService = new ProductService();

Console.WriteLine("Starting product loading...");

var products = await productService.GetProductsAsync();

// Add a new product
products.Add(new Product("Monitor", "Electronics", 8500m, 7));
products.Add(new Product("Cookies", "Grocery", 400m, 3));
//Adding two new products
products.Add(new Product("Headphones", "Electronics", 2200m, 12));
products.Add(new Product("Chicken", "Grocery", 350m, 0));
Console.WriteLine("\n=== All Products ===");
foreach (var product in products)
{
    Console.WriteLine($"{product.Name,-15} | {product.Category,-15} | {product.Price,10:C} | Stock: {product.Stock}");
}

// LINQ: Filter electronics
var electronics = products
    .Where(p => p.Category == "Electronics")
    .OrderBy(p => p.Name)
    .ToList();

Console.WriteLine("\n=== Electronics Products ===");
foreach (var product in electronics)
{
    Console.WriteLine(product.Name);
}

// LINQ: High-value products
var highValueProducts = products
    .Where(p => p.Price >= 1000)
    .Select(p => new
    {
        p.Name,
        p.Price
    })
    .ToList();

Console.WriteLine("\n=== High-Value Products ===");
foreach (var item in highValueProducts)
{
    Console.WriteLine($"{item.Name}: {item.Price:C}");
}

// LINQ: Out-of-stock products
var outOfStock = products
    .Where(p => p.Stock == 0)
    .Select(p => p.Name)
    .ToList();

Console.WriteLine("\n=== Out of Stock ===");
foreach (var productName in outOfStock)
{
    Console.WriteLine(productName);
}

// LINQ: Total inventory value
decimal totalInventoryValue = products.Sum(p => p.Price * p.Stock);

Console.WriteLine($"\nTotal inventory value: {totalInventoryValue:C}");

// LINQ: Group by category
var groupedProducts = products.GroupBy(p => p.Category);

Console.WriteLine("\n=== Products by Category ===");
foreach (var group in groupedProducts)
{
    Console.WriteLine($"Category: {group.Key}");
    Console.WriteLine($"Number of products: {group.Count()}");

    foreach (var product in group)
    {
        Console.WriteLine($" - {product.Name}");
    }
}

// Record with expression
var originalLaptop = products.First(p => p.Name == "Laptop");

var updatedLaptop = originalLaptop with
{
    Price = 45000m
};

Console.WriteLine("\n=== Record with Expression ===");
Console.WriteLine($"Original laptop price: {originalLaptop.Price:C}");
Console.WriteLine($"Updated laptop price: {updatedLaptop.Price:C}");

// Pattern matching discount
Console.WriteLine("\n=== Discounted Prices ===");
foreach (var product in products)
{
    decimal discountedPrice = GetDiscountedPrice(product);

    Console.WriteLine($"{product.Name,-15} Original: {product.Price,10:C} Discounted: {discountedPrice,10:C}");
}

static decimal GetDiscountedPrice(Product product)
{
    decimal discountRate = product switch
    {
        { Category: "Electronics", Price: > 1000 } => 0.10m,
        { Category: "Grocery", Stock: < 5 } => 0.02m,
        {Category: "School Supplies", Price: > 40 } => 0.05m,
        { Stock: 0 } => 0.00m,
        _ => 0.00m
    };

    return Math.Round(product.Price * (1 - discountRate), 2);
}

public record Product(string Name, string Category, decimal Price, int Stock);

public class ProductService
{
    public async Task<List<Product>> GetProductsAsync()
    {
        Console.WriteLine("Fetching products from simulated database...");

        await Task.Delay(1500);

        return new List<Product>
        {
            new("Laptop", "Electronics", 48000m, 5),
            new("Mouse", "Electronics", 750m, 20),
            new("Keyboard", "Electronics", 1500m, 0),
            new("Rice", "Grocery", 60m, 3),
            new("Coffee", "Grocery", 250m, 15),
            new("Notebook", "School Supplies", 45m, 100)
        };
    }
}
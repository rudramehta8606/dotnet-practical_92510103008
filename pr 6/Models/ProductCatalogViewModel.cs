namespace ProductCatalog.Models;

public class ProductCatalogViewModel
{
    public IReadOnlyList<Product> Products { get; init; } = [];
    public IReadOnlyList<string> Categories { get; init; } = [];
    public string? Search { get; init; }
    public string? Category { get; init; }
    public int ProductCount { get; init; }
}
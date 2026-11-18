using ProductCatalog.Models;

namespace ProductCatalog.Services;

public class ProductCatalogService
{
    private readonly IReadOnlyList<Product> products =
    [
        new Product { Id = 1, Name = "Arc Desk Lamp", Category = "Workspace", Description = "A sculptural lamp with a warm dimmable glow for late-night ideas.", Price = 129, ImageUrl = "https://images.unsplash.com/photo-1507473885765-e6ed057f782c?auto=format&fit=crop&w=900&q=85", IsFeatured = true },
        new Product { Id = 2, Name = "Field Notes Set", Category = "Stationery", Description = "Three pocket notebooks with heavyweight paper and a lay-flat binding.", Price = 24, ImageUrl = "https://images.unsplash.com/photo-1517842645767-c639042777db?auto=format&fit=crop&w=900&q=85", IsFeatured = true },
        new Product { Id = 3, Name = "Cove Ceramic Mug", Category = "Kitchen", Description = "Hand-finished stoneware with a comfortable thumb rest and matte glaze.", Price = 32, ImageUrl = "https://images.unsplash.com/photo-1514228742587-6b1558fcf93a?auto=format&fit=crop&w=900&q=85" },
        new Product { Id = 4, Name = "Ridge Carryall", Category = "Travel", Description = "A durable everyday tote cut from recycled canvas with leather handles.", Price = 86, ImageUrl = "https://images.unsplash.com/photo-1544816155-12df9643f363?auto=format&fit=crop&w=900&q=85" },
        new Product { Id = 5, Name = "Moss Throw", Category = "Living", Description = "A soft wool-blend throw in a quiet moss green for slow Sundays.", Price = 118, ImageUrl = "https://images.unsplash.com/photo-1584100936595-c0654b55a2e2?auto=format&fit=crop&w=900&q=85", IsFeatured = true },
        new Product { Id = 6, Name = "Daily Timer", Category = "Workspace", Description = "A tactile analog timer that helps you make room for focused work.", Price = 42, ImageUrl = "https://images.unsplash.com/photo-1508057198894-247b23fe5ade?auto=format&fit=crop&w=900&q=85" }
    ];

    public ProductCatalogViewModel GetCatalog(string? search, string? category)
    {
        var filtered = products.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            filtered = filtered.Where(product => product.Name.Contains(search, StringComparison.OrdinalIgnoreCase) || product.Description.Contains(search, StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrWhiteSpace(category))
        {
            filtered = filtered.Where(product => product.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }
        return new ProductCatalogViewModel
        {
            Products = filtered.ToList(),
            Categories = products.Select(product => product.Category).Distinct().Order().ToList(),
            Search = search,
            Category = category,
            ProductCount = products.Count
        };
    }

    public Product? GetById(int id) => products.FirstOrDefault(product => product.Id == id);
}
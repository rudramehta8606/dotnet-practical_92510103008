using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Services;

namespace ProductCatalog.Controllers;

public class CatalogController(ProductCatalogService catalogService) : Controller
{
    [HttpGet]
    public IActionResult Index(string? search, string? category) => View(catalogService.GetCatalog(search, category));

    [HttpGet]
    public IActionResult Details(int id)
    {
        var product = catalogService.GetById(id);
        return product is null ? NotFound() : View(product);
    }
}
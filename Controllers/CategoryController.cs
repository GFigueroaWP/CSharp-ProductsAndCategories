using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Products.Models;

namespace Products.Controllers;

public class CategoryController : Controller
{
    private readonly ILogger<CategoryController> _logger;

    private MyContext _context;

    public CategoryController(ILogger<CategoryController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("/categories")]
    public IActionResult Index()
    {
        ViewBag.Categories = _context.Categories.ToList();
        return View();
    }

    [HttpGet]
    [Route("/categories/{id}")]
    public IActionResult Show(int id)
    {
        ViewBag.Category = _context.Categories.Include(a => a.Products).ThenInclude(c => c.Product).FirstOrDefault(p => p.CategoryId == id);
        ViewBag.Products = _context.Products.ToList();
        return View();
    }

    [HttpPost]
    [Route("/categories/create")]
    public IActionResult Create(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost("AddProduct")]
    public IActionResult AddProduct(Association association)
    {
        _context.Associations.Add(association);
        _context.SaveChanges();
        return RedirectToAction("Show", new { id = association.ProductId });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

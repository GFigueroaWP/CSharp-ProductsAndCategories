using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Models;

namespace Products.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;

    private MyContext _context;

    public ProductController(ILogger<ProductController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        ViewBag.Products = _context.Products.ToList();
        return View();
    }

    [HttpGet]
    [Route("/products/{id}")]
    public IActionResult Show(int id)
    {
        ViewBag.Product = _context.Products.Include(a => a.Categories).ThenInclude(c => c.Category).FirstOrDefault(p => p.ProductId == id);
        ViewBag.Categories = _context.Categories.ToList();
        return View();
    }

    [HttpPost]
    [Route("/products/create")]
    public IActionResult Create(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost("AddCategory")]
    public IActionResult AddCategory(Association association)
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

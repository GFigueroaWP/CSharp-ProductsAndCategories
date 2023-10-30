using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Products.Models;

namespace Products.Controllers;

public class AssociationController : Controller
{
    private readonly ILogger<AssociationController> _logger;

    private MyContext _context;

    public AssociationController(ILogger<AssociationController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpPost]
    [Route("/associations/create")]
    public IActionResult Create(Association association)
    {
        _context.Associations.Add(association);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

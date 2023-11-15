using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using stock.Models;

namespace stock.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult versetatdestockformulaire()
    {
        Magasin magasin = new Magasin();
        List<Magasin> listemagasin  = magasin.getallmagasin(null);
        ViewBag.listemagasin = listemagasin;
        ViewData["Title"] = "Etat de Stock";
        return View("Etatdestockformulaire");
    }

    public IActionResult versetatdestocktable()
    {
        ViewData["Title"] = "Etat de Stock";
        return View("Etatdestocktable");
    }


    public IActionResult Privacy()
    {
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

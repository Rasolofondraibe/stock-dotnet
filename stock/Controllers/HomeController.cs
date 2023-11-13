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
        Mouvement mouvement = new Mouvement("2023-11-13","article1100",0,4,2300,"magasin1");
        List<Mouvement> listemouvement = mouvement.changeresteoflistemouvement(null);
        foreach(Mouvement m in listemouvement){
            Console.WriteLine(m.getIdmouvement());
            Console.WriteLine(m.getReste());
        }
        return View();
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

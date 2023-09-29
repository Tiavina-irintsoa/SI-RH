using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers;

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

    public ActionResult Test()
    {
        Service[] services = Service.GetAll(null);
        return View("~/Views/Home/Index.cshtml", services);
    }

    public ActionResult GetBesoin(int idService)
    {
        Console.WriteLine("vit");
        Besoin[] besoins = Besoin.GetAll(null, idService);
        // Console.WriteLine(besoins[0].accompli + " ok");
        ViewBag.IdService = idService;
        ViewBag.besoins = besoins;
        return View("~/Views/Home/listeBesoin.cshtml",besoins);
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

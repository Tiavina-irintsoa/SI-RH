using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers;

public class HomeController : SessionController
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if(!CookieIdAdminExists())
          return  RedirectToAction( "admin" , "login" );            
        return View();
    }
    public ActionResult Test()
    {
        Service[] services = Service.GetAll(null);
        return View("~/Views/Home/Index.cshtml", services);
    }

    public ActionResult GetBesoin(int idService)
    {
        if(!CookieIdAdminExists())
          return  RedirectToAction( "admin" , "login" );  
        Besoin[] besoins = Besoin.GetAll(null, idService);
        // Console.WriteLine(besoins[0].accompli + " ok");
        ViewBag.IdService = idService;
        ViewBag.besoins = besoins;
        ViewData["page"] = "service";
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

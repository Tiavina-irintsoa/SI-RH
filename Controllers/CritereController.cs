using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers;

public class CritereController : Controller
{
    private readonly ILogger<CritereController> _logger;

    public CritereController(ILogger<CritereController> logger){
        _logger = logger;
    }
    public IActionResult Edit()
    {
        var idposte = Request.Cookies["idposte"];
        var heuresemaine = Request.Cookies["heuresemaine"];
        var heuremploye = Request.Cookies["heuremploye"];
        List<TypeCritere> liste = TypeCritere.GetAll(null);

        //  foreach (var cookie in Request.Cookies.Keys)
        // {
        //     Response.Cookies.Delete(cookie);
        // }
        ViewData["page"] = "service";
        Console.WriteLine(idposte);
        ViewBag.Idposte=idposte;
        ViewBag.heuresemaine=heuresemaine;
        ViewBag.heuremploye=heuremploye;
        return View("Views/Home/Critere.cshtml",liste);        
    }
}
using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers;

public class ListeCvController : Controller
{
    private readonly ILogger<ListeCvController> _logger;

    public ListeCvController(ILogger<ListeCvController> logger){
        _logger = logger;
    }

    public IActionResult BesoinListe()
    {
        Besoin[] besoins = Besoin.GetAll(null, 1);
        return View("Views/Home/besoinListe.cshtml", besoins);        
    }

    public IActionResult Liste(int idbesoin)
    {
        Console.WriteLine(idbesoin);
        FicheCandidat[] fiches = FicheCandidat.GetAll(null, idbesoin);
        return View("Views/Home/listeCv.cshtml", fiches);        
    }

    public IActionResult Details()
    {
        return View("Views/Home/detailsCv.cshtml");        
    }
}
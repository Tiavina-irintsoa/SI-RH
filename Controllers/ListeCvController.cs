using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers;

public class ListeCvController : Controller
{
    private readonly ILogger<ListeCvController> _logger;

    public ListeCvController(ILogger<ListeCvController> logger){
        _logger = logger;
    }
    public IActionResult Liste()
    {
        FicheCandidat[] fiches = FicheCandidat.GetAll(null, 1);
        return View("Views/Home/listeCv.cshtml", fiches);        
    }

    public IActionResult Details()
    {
        return View("Views/Home/detailsCv.cshtml");        
    }
}
using Microsoft.AspNetCore.Mvc;
// using RH.Models;

namespace RH.Controllers;

public class ListeCvController : Controller
{
    private readonly ILogger<ListeCvController> _logger;

    public ListeCvController(ILogger<ListeCvController> logger){
        _logger = logger;
    }
    public IActionResult Liste()
    {
        return View("Views/Home/listeCv.cshtml");        
    }

    public IActionResult Details()
    {
        return View("Views/Home/detailsCv.cshtml");        
    }
}
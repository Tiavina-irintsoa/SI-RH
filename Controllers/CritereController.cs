using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RH.Models;
using System.Collections.Generic;


namespace RH.Controllers;

public class CritereController : Controller
{
    public IActionResult Edit()
    {
        List<TypeCritere> liste = TypeCritere.GetAll(null);
        foreach (var s in liste)
        {
            Console.WriteLine(" type critere "+s.idTypeCritere);
        }
        return View("Views/Home/Critere.cshtml",liste);
    }
}
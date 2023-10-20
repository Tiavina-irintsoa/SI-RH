using Microsoft.AspNetCore.Mvc;
using RH.Models;
using Npgsql;

namespace RH.Controllers;

public class EntretienController : Controller
{

    public IActionResult BesoinListe()
    {
        Besoin[] besoins = Besoin.GetAll(null, 0);
        return View("Views/Home/ListeEntretien.cshtml", besoins);        
    }
    [HttpGet]
    public IActionResult listeCandidat(int idbesoin){
        try{
            Entretien entretien = new Entretien(idbesoin);
            entretien.candidats = entretien.GetCandidatEntretienList();
            ViewBag.entretien = entretien;
            return View("~/Views/Home/CandidatEntretien.cshtml");
        }
        catch(Exception e){
            Console.WriteLine(e.StackTrace);
            throw e;
        }
    }
}
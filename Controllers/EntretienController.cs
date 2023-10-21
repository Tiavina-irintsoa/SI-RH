using Microsoft.AspNetCore.Mvc;
using RH.Models;
using Npgsql;

namespace RH.Controllers;

public class EntretienController : Controller
{

    public IActionResult BesoinListe()
    {
        Besoin[] besoins = Besoin.GetAllAccompli(null, 0);
        return View("Views/Home/ListeEntretien.cshtml", besoins);        
    }
    public IActionResult Embauche(int idbesoin){
        try{
            Besoin besoin = new Besoin(idbesoin);
            besoin.EmbaucherPremiers();
            return RedirectToAction("BesoinListe","entretien");
        }
        catch(Exception e){
            return  RedirectToAction( "listeCandidat" , "entretien",new{
                erreur = e.Message,
                idbesoin = idbesoin
            } );
        }
    }
    [HttpGet]
    public IActionResult listeCandidat(int idbesoin,string erreur){
        try{
            Entretien entretien = new Entretien(idbesoin);
            entretien.candidats = entretien.GetCandidatEntretienList(null);
            if(erreur!=null){
                ViewBag.erreur=erreur;
            }
            ViewBag.entretien = entretien;
            return View("~/Views/Home/CandidatEntretien.cshtml");
        }
        catch(Exception e){
            Console.WriteLine(e.StackTrace);
            throw e;
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using RH.Models;
using Npgsql;

namespace RH.Controllers;

public class ListeCvController : Controller
{
    private readonly ILogger<ListeCvController> _logger;

    public ListeCvController(ILogger<ListeCvController> logger){
        _logger = logger;
    }

    
    public IActionResult BesoinListe()
    {
        Besoin[] besoins = Besoin.GetAllAccompli(null, 0);
        return View("Views/Home/besoinListe.cshtml", besoins);        
    }

    public IActionResult Liste(int idbesoin)
    {
        ViewData["page"] = "cv";
        Console.WriteLine(idbesoin);
        FicheCandidat[] fiches = FicheCandidat.GetAll(null, idbesoin);
        ViewData["page"] = "cv";
        return View("Views/Home/listeCv.cshtml", fiches);        
    }

    public IActionResult Details(int idbesoin, int idcandidat)
    {
        ViewData["page"] = "cv";
        Console.WriteLine(idbesoin + " " + idcandidat);
        Connection connexion = new Connection();
        NpgsqlConnection npg = connexion.ConnectSante();

        Candidat candidat = Candidat.GetCandidat(npg, idbesoin, idcandidat);
        Dictionary< string, List<Choix> > choix = FicheCandidat.getChoixCandidat(npg, idbesoin, idcandidat);
        Besoin besoin = new Besoin{
            idBesoin = idbesoin
        };
        double point = FicheCandidat.getPoint(npg, idbesoin, idcandidat);
        FicheCandidat fiche = new FicheCandidat(candidat, choix, besoin, point);

        npg.Close();
        ViewData["page"] = "cv";
        return View("Views/Home/detailsCv.cshtml", fiche);        
    }
}
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
        Besoin[] besoins = Besoin.GetAll(null, 1);
        return View("Views/Home/besoinListe.cshtml", besoins);        
    }

    public IActionResult Liste(int idbesoin)
    {
        Console.WriteLine(idbesoin);
        FicheCandidat[] fiches = FicheCandidat.GetAll(null, idbesoin);
        return View("Views/Home/listeCv.cshtml", fiches);        
    }

    public IActionResult Details(int idbesoin, int idcandidat)
    {
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

        return View("Views/Home/detailsCv.cshtml", fiche);        
    }
}
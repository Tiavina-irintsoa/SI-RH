using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers{
    public class ContratController : SessionController{
        public IActionResult Essai()
        {
            return View("Views/Home/adminEssai.cshtml");
        }

        public IActionResult Travail()
        {
            return View("Views/Home/adminTravail.cshtml");
        }

        public IActionResult Contravail()
        {
            return View("Views/Home/contrat_essai.cshtml");
        }

        public IActionResult Accompli(){
            if(!CookieIdAdminExists())
                return  RedirectToAction( "admin" , "login" );  
            int idbesoin = int.Parse(Request.Query["idbesoin"]);
            Besoin.Update( idbesoin , null );
            return RedirectToAction("GetBesoin", "Home" , new { idservice = int.Parse(Request.Query["idservice"]) });
        }

        public IActionResult InsertBesoin()
        {
            if(!CookieIdAdminExists())
                return  RedirectToAction( "admin" , "login" );  
            int idposte = int.Parse(Request.Query["idposte"]);
            double heuresemaine = double.Parse(Request.Query["heuresemaine"]);
            double heuremploye = double.Parse(Request.Query["heuremploye"]);
            int idtypecontrat =  int.Parse(Request.Query["idtypecontrat"]);

            Besoin besoin = new Besoin {
                poste = new Poste{ idPoste = idposte},
                heureSemaine = heuresemaine, 
                heurePersonne = heuremploye,
            };

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };
            Response.Cookies.Append("idposte", idposte.ToString(), cookieOptions);
            Response.Cookies.Append("heuresemaine", heuresemaine.ToString(), cookieOptions);
            Response.Cookies.Append("heuremploye", heuremploye.ToString(), cookieOptions);
            Response.Cookies.Append("typecontrat", idtypecontrat.ToString(), cookieOptions);

            return RedirectToAction("Edit", "Critere");
        }
    }
}
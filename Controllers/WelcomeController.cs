using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers{
    public class WelcomeController : Controller{
        public ActionResult Index(){
            Annonce[] annonces = Annonce.GetAllAnnonces(null);
            return View("~/Views/Home/Annonces.cshtml", annonces);
        }
        public ActionResult DetailsOffre(){
            string idParam = Request.Query["besoin"];
            int idbesoin = int.Parse( idParam ); 
            Dictionary<string, Critere> critere =  Critere.GetCritereMapByBesoinId(null,idbesoin);
            return View("~/Views/Home/DetailsOffre.cshtml",critere);
        }
    }
}
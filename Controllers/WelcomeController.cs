using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers{
    public class WelcomeController : Controller{
        public ActionResult Index(){
            Console.WriteLine(" requeste : "+Request.Cookies.ContainsKey("idadmin"));
            Annonce[] annonces = Annonce.GetAllAnnonces(null);
            return View("~/Views/Home/Annonces.cshtml", annonces);
        }

        public ActionResult DetailsOffre(){
            if (Request.Query.ContainsKey("estadmin"))
                ViewBag.est_admin = true;
            string idParam = Request.Query["besoin"];
            ViewBag.idservice = Request.Query["idservice"];
            int idbesoin = int.Parse( idParam ); 
            ViewData["idbesoin"] = idbesoin; 
            Dictionary<string, Critere> critere =  Critere.GetCritereMapByBesoinId(null,idbesoin);
            return View("~/Views/Home/DetailsOffre.cshtml",critere);
        }
    }
}
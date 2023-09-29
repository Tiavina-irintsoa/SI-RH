using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers{
    public class WelcomeController : Controller{
        public ActionResult Index(){
            Annonce[] annonces = Annonce.GetAllAnnonces(null);
            return View("~/Views/Home/Annonces.cshtml", annonces);
        }
        public ActionResult DetailsOffre(){
            return View("~/Views/Home/DetailsOffre.cshtml");
        }
    }
}
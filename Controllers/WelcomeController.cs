using Microsoft.AspNetCore.Mvc;

namespace RH.Controllers{
    public class WelcomeController : Controller{
        public ActionResult Index(){
            return View("~/Views/Home/Annonces.cshtml");
        }
    }


}
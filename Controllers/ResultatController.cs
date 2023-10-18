using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers{
    public class ResultatController : Controller{
        public ActionResult Index(){
            string code = HttpContext.Request.Form["code"];
              Candidature candidature = Candidature.GetByCode( null , code );
              Console.WriteLine( "page candidature "+candidature.getPage() );
              ViewBag.idCandidature = candidature.idcandidature;
              return View( candidature.getPage() );
        }

        public ActionResult Code(){
            return View("~/Views/Home/code.cshtml");
        }
    }

}    
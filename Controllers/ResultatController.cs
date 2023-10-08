using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers{
    public class ResultatController : Controller{
        public ActionResult Index(){
            string code = HttpContext.Request.Form["code"];
              Candidature candidature = Candidature.GetByCode( null , code );
              return View( candidature.getPage() );
        }

        public ActionResult Code(){
            return View("~/Views/Home/code.cshtml");
        }
    }

}    
using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers{
    public class CVController : Controller{
        public ActionResult Index(){
            return View("~/Views/Home/CV.cshtml");
        }

        public ActionResult critere(){
            return View("~/Views/Home/CV_critere.cshtml");
        }
    }
}
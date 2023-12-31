using Microsoft.AspNetCore.Mvc;
using RH.Models;
using Newtonsoft.Json;
namespace RH.Controllers{
    public class HeureSuppController : SessionController{
        public IActionResult Demande(){
            return View("Views/Home/DemandeHeureSupp.cshtml");
        }
        public IActionResult  Validation(int validation, int iddemande){
            HeureSupp hs = new HeureSupp(iddemande);
            hs.setValidation(validation,null);
            return RedirectToAction("getNonConsulte","HeureSupp");
        }
        [HttpGet]
        public IActionResult Confirm(List<int> employe){
            Console.WriteLine(employe.Count);
            string cookieValue = Request.Cookies["heureSupp"];
            HeureSupp heureSupp = JsonConvert.DeserializeObject<HeureSupp>(cookieValue);
            heureSupp.setPersonnels(employe);
            heureSupp.InsererHeureSup();
            return RedirectToAction("Demande","HeureSupp");
        }
        [HttpGet]
        public IActionResult details(int iddemande){
            HeureSupp hs = new HeureSupp(iddemande);
            hs.complete();
            ViewBag.demande = hs;
            return View("Views/Home/DetailsDemandeHeureSupp.cshtml");
        }
        public IActionResult getNonConsulte(){
            ViewBag.liste = HeureSupp.getHeureSupp();
            return View("Views/Home/ListeHeureSupp.cshtml");
        }
        [HttpPost]
        public IActionResult SubmitFirst(){

            string? raison = HttpContext.Request.Form["raison"];
            string? date = HttpContext.Request.Form["date"];
            string? debut = HttpContext.Request.Form["debut"];
            string? fin = HttpContext.Request.Form["fin"];
            int idtypeuser = int.Parse(Request.Cookies["idtypeuser"]);
            HeureSupp  heuresupp = new HeureSupp(debut,fin,date,raison,idtypeuser);
            string heureSupAsJson = JsonConvert.SerializeObject(heuresupp);
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1) 
            };
            ViewBag.liste = heuresupp.GetPersonnelByServiceId();
            Response.Cookies.Append("heureSupp", heureSupAsJson, cookieOptions);

            return View("Views/Home/HeureSuppEmploye.cshtml");
        }

    }
}
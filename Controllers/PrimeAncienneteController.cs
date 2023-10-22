using Microsoft.AspNetCore.Mvc;
using RH.Models;
using Npgsql;

namespace RH.Controllers{
    public class PrimeAncienneteController : SessionController{
        public IActionResult Ajout(){
            return View("Views/Home/AjoutPrimeAnciennete.cshtml");
        }
        [HttpPost]
        public IActionResult Submit(PrimeAnciennete primeAnciennete)
        {
            if (ModelState.IsValid)
            {
                primeAnciennete.InsertPrimeAnciennete();
                return RedirectToAction("Index","PrimeAnciennete");
            }
            else
            {
                 return RedirectToAction("Ajout","PrimeAnciennete");
            }
        }
        public IActionResult Index(){
            List<PrimeAnciennete> liste = PrimeAnciennete.GetAllPrimeAnciennete();
            ViewBag.liste = liste;
            return View("Views/Home/listePrime.cshtml");
        }
    }
}
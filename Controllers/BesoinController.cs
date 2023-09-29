using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers{
    public class BesoinController : Controller{
        public IActionResult Liste(int idService)
        {
            Poste[] liste = Poste.GetAll(null, idService);
            foreach (var s in liste)
            {
                Console.WriteLine(s.nomPoste);
            }
            return View("Views/Home/ajoutBesoin.cshtml",liste);
        }

        public IActionResult InsertBesoin()
        {
            int idposte = int.Parse(Request.Query["idposte"]);
            double heuresemaine = double.Parse(Request.Query["heuresemaine"]);
            double heuremploye = double.Parse(Request.Query["heuremploye"]);
            
            Besoin besoin = new Besoin {
                poste = new Poste{ idPoste = idposte},
                heureSemaine = heuresemaine, 
                heurePersonne = heuremploye
            };

            besoin.Insert(null);
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            };
            Response.Cookies.Append("idposte", idposte.ToString(), cookieOptions);
            Response.Cookies.Append("heuresemaine", heuresemaine.ToString(), cookieOptions);
            Response.Cookies.Append("heuremploye", heuremploye.ToString(), cookieOptions);

            return RedirectToAction("Edit", "Critere");
        }
    }
}
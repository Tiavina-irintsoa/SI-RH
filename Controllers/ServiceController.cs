using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers{
    public class ServiceController : Controller{
        public IActionResult Liste()
        {
            Service[] liste = Service.GetAll(null);
            foreach (var s in liste)
            {
                Console.WriteLine(s.IconeService);
            }
            ViewData["page"] = "service";
            return View("Views/Home/liste_service.cshtml",liste);
        }
    }
}
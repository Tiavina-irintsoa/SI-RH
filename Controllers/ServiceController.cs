using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers{
public class ServiceController : SessionController {

        public IActionResult Liste()
        {
            if(!CookieIdAdminExists())
                return  RedirectToAction( "admin" , "login" );  
            int idadmin = int.Parse(Request.Cookies["idadmin"]);
            Service[] liste = Service.GetAll(null , idadmin);
            foreach (var s in liste)
            {
                Console.WriteLine(s.IconeService);
            }
            ViewData["page"] = "service";
            return View("Views/Home/liste_service.cshtml",liste);
        }
    }
}
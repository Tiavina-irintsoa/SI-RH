using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers{
    public class DeconnexionController : Controller{
        public IActionResult Index()
        {
            Response.Cookies.Delete("idadmin");
            Response.Cookies.Delete("idpersonnel");
            Response.Cookies.Delete("superieur");
            Response.Cookies.Delete("idtypeuser");
            Response.Cookies.Delete("nomadmin");
            return View("~/Views/Home/loginAdmin.cshtml");
        }      
    }
}
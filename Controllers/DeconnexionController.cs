using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers{
    public class DeconnexionController : Controller{
        public IActionResult Index()
        {
            Response.Cookies.Delete("idadmin");
            return View("~/Views/Home/loginAdmin.cshtml");
        }      
    }
}
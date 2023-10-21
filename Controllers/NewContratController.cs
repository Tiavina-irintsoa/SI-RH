using Microsoft.AspNetCore.Mvc;
using RH.Models;
using System.Collections.Generic;
using System.Web;namespace RH.Models{
    public class NewContratController : Controller{
        public IActionResult Index(){
            try
            {
                ViewBag.liste = Candidature.getAll(3,null);
                return View("Views/Home/NewContrat.cshtml");
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
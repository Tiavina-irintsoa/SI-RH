using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }
    public ActionResult Index()
    {
        return View("~/Views/Home/loginAdmin.cshtml");
    }

    public ActionResult Admin()
    {
        return View("~/Views/Home/loginAdmin.cshtml");
    }

    public ActionResult connexionAdmin()
    {
        string nom = Request.Form["nom"];
        string mdp = Request.Form["mdp"];
        UserAdmin admin = UserAdmin.GetUser( nom , mdp , null );
        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(1)
        };
        Response.Cookies.Append("idadmin",  admin.Idadmin.ToString()  , cookieOptions);
        Response.Cookies.Append("nomadmin", nom  , cookieOptions);
        return RedirectToAction("liste", "service");
    }
}
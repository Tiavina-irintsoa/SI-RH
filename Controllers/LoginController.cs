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
        UserAdmin admin = UserAdmin.GetUser( nom.Trim() , mdp.Trim() , null );
        Service service = new Service{
            IdService = admin.type.idtypeuser
        };
        service = service.GetById(null);
        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(1)
        };
        if( service != null ){
            if( service.Superieur.idpersonnel == admin.Personnel.idpersonnel )
                Response.Cookies.Append("superieur" , true.ToString() , cookieOptions );  
        }
        Response.Cookies.Append("idpersonnel" , admin.Personnel.idpersonnel
        .ToString() , cookieOptions );
        Console.WriteLine( " idpersonnel login  : "+admin.Personnel.idpersonnel );
        Response.Cookies.Append("idadmin",  admin.Idadmin.ToString()  , cookieOptions);
        Response.Cookies.Append("nomadmin", nom  , cookieOptions);
        Response.Cookies.Append("idtypeuser", admin.type.idtypeuser.ToString()  , cookieOptions);
        return RedirectToAction("liste", "service");
    }
}
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers;

public class PersonnelController : SessionController
{
        public ActionResult Index(){
            ViewBag.listePersonnel = Personnel.GetAll( null , null );
            if(!CookieIdAdminExists())
                return  RedirectToAction( "admin" , "login" );  
            return View("~/Views/Home/liste_personnel.cshtml");
        }

        public ActionResult search(){
            if(!CookieIdAdminExists())
                return  RedirectToAction( "admin" , "login" );  
            string annee = HttpContext.Request.Form["annee"];
            string nom_prenom = HttpContext.Request.Form["nom_prenom"];
            string genre = HttpContext.Request.Form["genre"];
            string min_age = HttpContext.Request.Form["min-age"];
            string max_age = HttpContext.Request.Form["max-age"];
            string adresse = HttpContext.Request.Form["adresse"];
            string nationalite = HttpContext.Request.Form["nationalite"];
            string matricule = HttpContext.Request.Form["matricule"];
            string idservice = HttpContext.Request.Form["idservice"];
            string brut_min = HttpContext.Request.Form["brut-min"];
            string brut_max = HttpContext.Request.Form["brut-max"];
            string net_min = HttpContext.Request.Form["net-min"];
            string net_max = HttpContext.Request.Form["brut-max"];
            
            ViewBag.Annee = annee;
            ViewBag.Genre = genre;
            ViewBag.MinAge = min_age;
            ViewBag.MaxAge = max_age;
            ViewBag.Adresse = adresse;
            ViewBag.Nationalite = nationalite;
            ViewBag.Matricule = matricule;
            ViewBag.IdService = idservice;
            ViewBag.BrutMin = brut_min;
            ViewBag.BrutMax = brut_max;
            ViewBag.NetMin = net_min;
            ViewBag.NetMax = net_max;
            ViewBag.nom_prenom = nom_prenom;

            string sql = Personnel.GetSql( annee ,genre ,min_age ,max_age ,adresse ,nationalite ,matricule ,idservice ,brut_min ,brut_max ,net_min ,net_max , nom_prenom);
            Console.WriteLine(sql);
            ViewBag.listePersonnel = Personnel.GetAll( null , sql );
            return View("~/Views/Home/liste_personnel.cshtml");
        }

}
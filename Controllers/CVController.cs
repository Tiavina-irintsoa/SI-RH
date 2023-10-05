using Microsoft.AspNetCore.Mvc;
using RH.Models;
using System.Collections.Generic;
using System.Web;

namespace RH.Controllers{
    public class CVController : Controller{
        public ActionResult Index(){
            return View("~/Views/Home/CV.cshtml");
        }

        // [HttpPost] 
        // public ActionResult Postuler(){
        //     int genre = int.Parse(HttpContext.Request.Form["genre"]);
        //     int nationalite = int.Parse(HttpContext.Request.Form["nationalite"]);
        //     int diplome = int.Parse(HttpContext.Request.Form["diplome"]);
        //     int experience = int.Parse(HttpContext.Request.Form["experience"]);
        //     int sm = int.Parse(HttpContext.Request.Form["sm"]);
  
        //     if (file != null && file.Length > 0)
        //         {
        //             var fileName = Path.GetFileName(file.FileName);
        //             var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

        //             using (var fileStream = new FileStream(path, FileMode.Create))
        //             {
        //                 await file.CopyToAsync(fileStream);
        //             }
        //         }


        //     return View("~/Views/Home/CV_critere.cshtml");
        // }

         [HttpPost]
        public async Task<IActionResult> Postuler(IFormFile fichierTelecharge)
        {
            if (fichierTelecharge != null && fichierTelecharge.Length > 0)
            {
                // Vous pouvez accéder aux propriétés du fichier comme son nom, sa taille, etc.
                string nomFichier = fichierTelecharge.FileName;
                long tailleFichier = fichierTelecharge.Length;

                Console.WriteLine("Nom du fichier : " + nomFichier);
                Console.WriteLine("Taille du fichier : " + tailleFichier);

                // Vous pouvez également enregistrer le fichier sur le serveur si nécessaire
                string cheminDeDestination = "chemin/vers/le/dossier/de/destination/" + nomFichier;
                using (var stream = new FileStream(cheminDeDestination, FileMode.Create))
                {
                    await fichierTelecharge.CopyToAsync(stream);
                }

                // Traitez le fichier téléchargé comme nécessaire
                // ...
                
                return RedirectToAction("ActionSuivante");
            }

            return View();
        }


        [HttpPost] 
        public ActionResult critere(){
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };

            string nom = HttpContext.Request.Form["nom"];  
            string prenom = HttpContext.Request.Form["prenom"];
            string dtn = HttpContext.Request.Form["dtn"];
            string contact = HttpContext.Request.Form["contact"];
            string email = HttpContext.Request.Form["email"];

            Response.Cookies.Append("nomCV",nom , cookieOptions);
            Response.Cookies.Append("prenomCV",  prenom , cookieOptions);
            Response.Cookies.Append("dtnCV",  dtn , cookieOptions);
            Response.Cookies.Append("contactCV",  contact , cookieOptions);
            Response.Cookies.Append("emailCV",  email , cookieOptions);

            return View("~/Views/Home/CV_critere.cshtml");
        }
    }
}
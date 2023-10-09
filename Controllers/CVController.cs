using Microsoft.AspNetCore.Mvc;
using RH.Models;
using System.Collections.Generic;
using System.Web;
using Npgsql;
namespace RH.Controllers{
    public class CVController : Controller{
        public ActionResult Index(){
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };
            Response.Cookies.Append("idbesoincv",HttpContext.Request.Query["idbesoin"] , cookieOptions);
            return View("~/Views/Home/CV.cshtml");
        }
    
        [HttpPost]
        public IActionResult Postuler(IFormFile file_diplome , IFormFile file_experience ) 
        {
            Upload.save(file_diplome);
            Upload.save(file_experience);
            int genre = int.Parse(HttpContext.Request.Form["genre"]);
            int nationalite = int.Parse(HttpContext.Request.Form["nationalite"]);
            int diplome = int.Parse(HttpContext.Request.Form["diplome"]);
            int experience = int.Parse(HttpContext.Request.Form["experience"]);
            int sm = int.Parse(HttpContext.Request.Form["sm"]);

            string nomcv = (Request.Cookies["nomCV"]);  
            string prenomcv = (Request.Cookies["prenomCV"]);
            string dtncv = (Request.Cookies["dtnCV"]);
            string contactcv = (Request.Cookies["contactCV"]);
            string emailcv = (Request.Cookies["emailCV"]);
            Console.WriteLine( "idbesoin : "+int.Parse( Request.Cookies["idbesoincv"]  ));
            Connection connexion = new Connection();
            NpgsqlConnection npg = connexion.ConnectSante();
            try{
                Candidat c = Candidat.GetByName( npg , nomcv , prenomcv , emailcv );
                Boolean new_candidat = false;
                if( c == null ){
                    c = new Candidat{
                    idcandidat = Utilitaire.GetNextSerialValue( npg  , "candidat_idcandidat_seq" ),
                    nom = nomcv,
                    prenom = prenomcv , 
                    mail = emailcv , 
                    contact =  contactcv , 
                    dtn = DateTime.Parse(dtncv)
                    };
                    new_candidat = true;
                }        
            
                Console.WriteLine( " insertion candidat " );
                if( new_candidat == true )
                    c.insert( npg );
                Console.WriteLine( " insertion candidat fin" );
                Candidature candidature = new Candidature{
                    idcandidature = Utilitaire.GetNextSerialValue( npg  , "candidature_idcanditature_seq" ),
                    idcandidat = c.idcandidat,
                    idbesoin = int.Parse( Request.Cookies["idbesoincv"] )
                };

                Fichier fichier = new Fichier{
                    idcandidature = candidature.idcandidature,
                    fichier_diplome = file_diplome.FileName,
                    fichier_experience = file_experience.FileName                    
                };
                candidature.fichier = fichier;

                candidature.code = Candidature.GenerateRandomCode( candidature.idcandidat );
                Console.WriteLine( " insertion candidature" );
                candidature.Insert( npg );
                Console.WriteLine( " insertion candidature fin " );
                choixCandidat choixCandidat = new choixCandidat{
                    Idchoix = genre ,
                    Idcandidature =  candidature.idcandidature            
                };
                Console.WriteLine( " insertion choixcandidat" );
                choixCandidat.Insert( npg );
                Console.WriteLine( " insertion nationalité" );                
                choixCandidat.Idchoix = nationalite;
                choixCandidat.Insert( npg );
                Console.WriteLine( " insertion diplome" );
                choixCandidat.Idchoix = diplome;
                choixCandidat.Insert( npg );
                Console.WriteLine( " insertion experience" );
                choixCandidat.Idchoix = experience;
                choixCandidat.Insert( npg ); 
                Console.WriteLine( " insertion SM" );
                choixCandidat.Idchoix = sm;
                choixCandidat.Insert( npg );
                Console.WriteLine( " insertion code" );
                ViewBag.code_cv =  candidature.code;
                npg.Close();
            }catch( Exception ex ){
                Console.WriteLine(ex.ToString()); 
                Console.WriteLine("Type d'exception : " + ex.GetType().Name);
                Console.WriteLine("Message : " + ex.Message);
                Console.WriteLine("Pile d'appels : " + ex.StackTrace);
                npg.Close();
                throw;
            }
            return View("~/Views/Home/finpostule.cshtml");
        }
        public ActionResult test(){
            return View("~/Views/Home/finpostule.cshtml");
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
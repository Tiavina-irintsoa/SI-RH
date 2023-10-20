using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers{
    public class CongeController : SessionController{

        public IActionResult accept(){
            if(!CookieIdAdminExists())
                return  RedirectToAction( "admin" , "login" );
            int idservice = Convert.ToInt32(Request.Cookies["idtypeuser"]);
            int accepte = 2;
            if( idservice == 7 ){
                accepte = 3;
            }
            try{
                
                int idconge = Convert.ToInt32( Request.Form["idconge"]);
                Conge.UpdateAccepte( idconge , accepte );  
                return RedirectToAction("liste", "conge");
            }catch (Exception e  ){
                Console.WriteLine( e.StackTrace );
                throw e;
            }
        }

        public IActionResult refusSuperieur(){
            if(!CookieIdAdminExists())
                return  RedirectToAction( "admin" , "login" );

            int idservice = Convert.ToInt32(Request.Cookies["idtypeuser"]);
            Console.WriteLine( "  test 1  " + Request.Form["idtypeuser"] );
            int accepte = -1;
            if( idservice == 7 ){
                accepte = -2;
            }
            try{
                Console.WriteLine( "  test 2  " + Request.Form["idconge"] );
                int idconge =   Convert.ToInt32( Request.Form["idconge"]) ;
                string raison =  Request.Form["raison"] ;

                Console.WriteLine(  idconge +" et " + raison  );

                Refus refus = new Refus{
                    IdConge = idconge,
                    RaisonRefus = raison
                };

                Conge.UpdateAccepte( idconge , accepte );  

                refus.InsertRefus();

                return RedirectToAction("liste", "conge");
            }catch (Exception e  ){
                Console.WriteLine( e.StackTrace );
                throw e;
            }
        }

        public IActionResult liste(){
            if(!CookieIdAdminExists())
                return  RedirectToAction( "admin" , "login" );
            int idservice = Convert.ToInt32(Request.Cookies["idtypeuser"]);
            
            int accepte = 1;
            //DRH
            if( idservice == 7 ){
                accepte = 2;
            }

            List<Conge> l_conge = Conge.GetCongesSuperieur( null , idservice , accepte );

            ViewBag.l_conge = l_conge;

            return View("~/Views/Home/liste_conge.cshtml");  
        }    

        public IActionResult Index(){
            if(!CookieIdAdminExists())
                return  RedirectToAction( "admin" , "login" );  
            int idtypeuser =   Convert.ToInt32(Request.Cookies["idtypeuser"]);
            string sql = " select * from v_personnel_information where idservice = "+idtypeuser;
            List<Personnel> liste_personnel = Personnel.GetAll(null , sql );
            ViewBag.personnel =  liste_personnel; 
            return View("~/Views/Home/congeAjout.cshtml");
        }
        public IActionResult Demande()
        {
            if (!CookieIdAdminExists())
                return RedirectToAction("admin", "login");

                // Récupérez les valeurs du formulaire ici
            string personnelId = Request.Cookies["idpersonnel"];
            string debut = Request.Form["debut"];
            string debutTime = Request.Form["debut_time"];
            string fin = Request.Form["fin"];
            string finTime = Request.Form["fin_time"];
            string raison = Request.Form["raison"];

            Console.WriteLine( " personnal id :  " + personnelId );

            // Utilisez la fonction toDateTime pour créer les objets DateTime
            DateTime debutDateTime = Utilitaire.toDateTime(debut, debutTime);
            DateTime finDateTime = Utilitaire.toDateTime(fin, finTime);

            // Créez une instance de Conge avec les données récupérées
            Conge conge = new Conge
            {
                DateDebut = debutDateTime,
                DateFin = finDateTime,
                Personnel = new Personnel{
                    idpersonnel = Convert.ToInt32(personnelId)
                },
                Raison = new Raison{
                    idraison = Convert.ToInt32(raison)
                }
            };

            Console.WriteLine( debut  + "  " +debutTime );
            Console.WriteLine( fin  + "  " +finTime );
            int idtypeuser =   Convert.ToInt32(Request.Cookies["idtypeuser"]);

            string sql = " select * from v_personnel_information where idservice = "+idtypeuser;
            List<Personnel> liste_personnel = Personnel.GetAll(null , sql );
            ViewBag.personnel =  liste_personnel; 
            try{
                Console.WriteLine( "insert" );
                Conge.isValid( personnelId , debut , debutTime , fin , finTime );
                ViewBag.Succes =  "votre demande a été envoyé" ;
                conge.InsertConge();
            }catch( Exception e ){
                Console.WriteLine( e.ToString() );
                ViewBag.ErreurMessage = e.Message;          
            }
            return View("~/Views/Home/congeAjout.cshtml");  
        }
    }
}

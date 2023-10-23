using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RH.Models;

namespace RH.Controllers{
    public class CongeController : SessionController{
        public IActionResult modif(){
            string idconge =  Request.Form["idconge"] ;
            string new_fin = Request.Form["new_fin"];
            Console.WriteLine( new_fin );
            Conge conge = new Conge{
                IdConge = Convert.ToInt32(idconge),
                ReelDateFin = Convert.ToDateTime( new_fin )
            };

            conge.updateFin();

            return RedirectToAction( "planningModif" , "conge" );
        }

        public IActionResult planningModif(){
            int idservice = Convert.ToInt32(Request.Cookies["idtypeuser"]);
            List<Conge> l_conge  =  Conge.getPlanning( null , idservice );
           ViewBag.l_conge = l_conge;
            return View("~/Views/Home/planningMoModif.cshtml"); 
        }
        public IActionResult planning(){
            int idservice = Convert.ToInt32(Request.Cookies["idtypeuser"]);

            List<Conge> l_conge  =  Conge.getPlanning( null , idservice );
            List<Personnel> l_personnel = Personnel.GetPersonnelByService(null , idservice);

            List<Event> events = l_conge.Select((conge, index) => Utilitaire.ConvertCongeToEvent(conge, index)).ToList();

            List<Ressource> ressources = l_personnel.Select((p, index) => Utilitaire.ConvertCongeToRessource(p, index)).ToList();

            string evenement = JsonSerializer.Serialize(events);
            string ressource = JsonSerializer.Serialize( ressources ) ;

            Console.WriteLine( evenement );

            Console.WriteLine( ressource );

            ViewBag.conges = l_conge;
            ViewBag.Evenements = evenement;
            ViewBag.Ressources = ressource;
            return View("~/Views/Home/planning.cshtml"); 
        }

        public IActionResult Categorie(){
            return View("~/Views/Home/conge_categorie.cshtml"); 
        }

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


        public IActionResult listeDemande(){
            if(!CookieIdAdminExists())
                return  RedirectToAction( "admin" , "login" );
            int idpersonnel =   Convert.ToInt32(Request.Cookies["idpersonnel"]);
            List<Conge> l_conge = Conge.GetCongesSuperieur( null , 0 , 0 , idpersonnel );

            ViewBag.l_conge = l_conge;

            return View("~/Views/Home/liste_demande.cshtml"); 
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
                    RaisonRefus = raison , 
                    service = new Service{
                        IdService = idservice
                    }
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

            List<Conge> l_conge = Conge.GetCongesSuperieur( null , idservice , accepte  );

            ViewBag.l_conge = l_conge;

            return View("~/Views/Home/liste_conge.cshtml");  
        }    

        public IActionResult Index(){
            if(!CookieIdAdminExists())
                return  RedirectToAction( "admin" , "login" );
             
            Personnel p = Personnel.GetPersonnelByID(null, Convert.ToInt32( Request.Cookies["idpersonnel"] ) );

            List<Choix> choix =  new List<Choix>() ;

            choix.Add( new Choix{ idChoix = 0 , intitule = "Aucun" } );

            if( p.genre == 2 ){
                choix.Add( new Choix{ idChoix = 1 , intitule = "Maternité" } );
            }
            else{
            choix.Add( new Choix{ idChoix = 2 , intitule = "Paternité" } );
            }
            choix.Add( new Choix{ idChoix = 3 , intitule = "Maladie" } );
            ViewBag.choix = choix;
            int idtypeuser =   Convert.ToInt32(Request.Cookies["idtypeuser"]);
            string sql = " select * from v_personnel_information where idservice = "+idtypeuser;
            List<Personnel> liste_personnel = Personnel.GetAll(null , sql );
            DateTime dateDuJour = DateTime.Now;
            DateTime dateDans15Jours = dateDuJour.AddDays(15);
            ViewBag.DateDans15Jours = dateDans15Jours.ToString("yyyy-MM-dd"); 
            ViewBag.personnel =  liste_personnel; 
            return View("~/Views/Home/congeAjout.cshtml");
        }

        public IActionResult Demande()
        {
            if (!CookieIdAdminExists())
                return RedirectToAction("admin", "login");

            string personnelId = Request.Cookies["idpersonnel"];
            string debut = Request.Form["debut"];
            string debutTime = Request.Form["debut_time"];
            string fin = Request.Form["fin"];
            string finTime = Request.Form["fin_time"];
            string raison = Request.Form["raison"];
            string raison_autre = Request.Form["raison_autre"];
            Console.WriteLine( " personnal id :  " + raison+ "  "+string.IsNullOrEmpty( raison_autre.Trim() ) );
            if( string.IsNullOrEmpty( raison_autre.Trim() ) == false ){
                Console.WriteLine("mankato");
                raison = "0";
                raison_autre = raison_autre.Trim();
            }            

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
                },
                autre_raison = raison_autre
            };

            Console.WriteLine( debut  + "  " +debutTime );
            Console.WriteLine( fin  + "  " +finTime );
            int idtypeuser =   Convert.ToInt32(Request.Cookies["idtypeuser"]);

            string sql = " select * from v_personnel_information where idservice = "+idtypeuser;
            List<Personnel> liste_personnel = Personnel.GetAll(null , sql );
            ViewBag.personnel =  liste_personnel; 
            try{
                Console.WriteLine( "insert" );
                Conge.isValid( personnelId , debut , debutTime , fin , finTime , raison );
                ViewBag.Succes =  "votre demande a été envoyé" ;
                conge.InsertConge();
            }catch( Exception e ){
                Console.WriteLine( e.ToString() );
                ViewBag.ErreurMessage = e.Message;          
            }
            Personnel p = Personnel.GetPersonnelByID(null, Convert.ToInt32( Request.Cookies["idpersonnel"] ) );

            List<Choix> choix =  new List<Choix>() ;
            
            choix.Add( new Choix{ idChoix = 0 , intitule = "Aucun" } );

            if( p.genre == 2 ){
                choix.Add( new Choix{ idChoix = 1 , intitule = "Maternité" } );
            }
            else{
            choix.Add( new Choix{ idChoix = 2 , intitule = "Paternité" } );
            }
            choix.Add( new Choix{ idChoix = 3 , intitule = "Maladie" } );
            ViewBag.choix = choix;
            DateTime dateDuJour = DateTime.Now;
            DateTime dateDans15Jours = dateDuJour.AddDays(15);
            ViewBag.DateDans15Jours = dateDans15Jours.ToString("yyyy-MM-dd"); 
            return View("~/Views/Home/congeAjout.cshtml");  
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using RH.Models;
using Npgsql;

namespace RH.Controllers{
    public class ContratController : SessionController{
        public IActionResult Essai( int idcandidature) {            
            Connection connexion = new Connection();
            NpgsqlConnection npg = connexion.ConnectSante();

            Candidature candidature = Candidature.GetCandidature(npg , idcandidature );

            int idbesoin = candidature.idbesoin;
            int idcand = candidature.idcandidat;

            Candidat candidat = Candidat.GetById(npg , idcand );
            Avantage[] avantages = Avantage.GetAllAvantages(npg);

            npg.Close();

            ViewBag.nom = candidat.nom;
            ViewBag.prenom = candidat.prenom;
            ViewBag.id= candidat.idcandidat;
            ViewBag.idbesoin= 1;
            return View("Views/Home/adminEssai.cshtml", avantages);
        }

        public IActionResult saveEssai()
        {
            Connection connexion = new Connection();
            NpgsqlConnection npg = connexion.ConnectSante();
            int idcandidat = int.Parse(Request.Query["idcandidat"]);
            int idbesoin = int.Parse(Request.Query["idbesoin"]);
            double duree = double.Parse(Request.Query["duree"]);
            string debut = Request.Query["debut"];
            double brut = double.Parse(Request.Query["brut"]);

            Essai essai = new Essai(idbesoin, idcandidat, duree, debut, brut);
            int idessai = essai.InsertEssai(npg);

            string[] avantage = Request.Query["avantage"];
            foreach (string av in avantage)
            {
                int idavantage = int.Parse(av);
                Avantage.InsertAvantage(npg, idessai, idavantage);
            }

            npg.Close();

            return View("Views/Home/index.cshtml");
        }

        public IActionResult Travail()
        {
            return View("Views/Home/adminTravail.cshtml");
        }

        public IActionResult saveTravail()
        {
            Connection connexion = new Connection();
            NpgsqlConnection npg = connexion.ConnectSante();

            int idcontrat_essai = int.Parse(Request.Query["idcontrat_essai"]);
            double duree = double.Parse(Request.Query["duree"]);
            string debut = Request.Query["debut"];

            Travail travail = new Travail(idcontrat_essai, duree, debut);
            travail.InsertTravail(npg);

            npg.Close();

            return View("Views/Home/index.cshtml");
        }

        public IActionResult Contressai(int idessai, int idcand)
        {
            Connection connexion = new Connection();
            NpgsqlConnection npg = connexion.ConnectSante();

            Candidat candidat = Candidat.GetById(npg , idcand );
            
            npg.Close();

            ViewBag.candidat = candidat;
            ViewBag.idessai = idessai;
            return View("Views/Home/contrat_essai.cshtml");
        }

        public IActionResult Contravail(int contrat_travail)
        {            
            Connection connexion = new Connection();
            NpgsqlConnection npg = connexion.ConnectSante();

            Sante[] santes = Sante.GetAllSantes(npg);
            ViewBag.idt = contrat_travail;

            npg.Close();

            return View("Views/Home/contrat_travail.cshtml", santes);
        }

        public IActionResult Traitement_Contrat(){
            Connection connexion = new Connection();
            NpgsqlConnection npg = connexion.ConnectSante();

            int idcand = int.Parse(Request.Query["idcand"]);
            string cin = Request.Query["cin"];
            string adresse = Request.Query["adresse"];
            string pere = Request.Query["pere"];
            string mere = Request.Query["mere"];
            int nbenf = int.Parse(Request.Query["enfant"]);

            double net = double.Parse(Request.Query["net"]);
            string signature = Request.Query["signature"];

            int idessai = int.Parse(Request.Query["idessai"]);

            Info info = new Info(idcand, cin, adresse, pere, mere, nbenf);
            info.InsertInfo(npg);

            Contrat contrat = new Contrat(idessai, signature, net);
            contrat.InsertContratEssai(npg);

            npg.Close();
            return View("Views/Home/index.cshtml");
        }

        public IActionResult Save_Contrat(){
            Connection connexion = new Connection();
            NpgsqlConnection npg = connexion.ConnectSante();

            int sante = int.Parse(Request.Query["sante"]);
            string sign = Request.Query["sign"];
            int idtravail = int.Parse(Request.Query["idtravail"]);

            Sante.InsertSante(npg, idtravail, sante);

            Contrat contrat = new Contrat(idtravail, sign);
            contrat.InsertContratTravail(npg);

            npg.Close();
            return View("Views/Home/index.cshtml");
        }

        public IActionResult Accompli(){
            if(!CookieIdAdminExists())
                return  RedirectToAction( "admin" , "login" );  
            int idbesoin = int.Parse(Request.Query["idbesoin"]);
            Besoin.Update( idbesoin , null );
            return RedirectToAction("GetBesoin", "Home" , new { idservice = int.Parse(Request.Query["idservice"]) });
        }

        public IActionResult InsertBesoin()
        {
            if(!CookieIdAdminExists())
                return  RedirectToAction( "admin" , "login" );  
            int idposte = int.Parse(Request.Query["idposte"]);
            double heuresemaine = double.Parse(Request.Query["heuresemaine"]);
            double heuremploye = double.Parse(Request.Query["heuremploye"]);
            int idtypecontrat =  int.Parse(Request.Query["idtypecontrat"]);

            Besoin besoin = new Besoin {
                poste = new Poste{ idPoste = idposte},
                heureSemaine = heuresemaine, 
                heurePersonne = heuremploye,
            };

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };
            Response.Cookies.Append("idposte", idposte.ToString(), cookieOptions);
            Response.Cookies.Append("heuresemaine", heuresemaine.ToString(), cookieOptions);
            Response.Cookies.Append("heuremploye", heuremploye.ToString(), cookieOptions);
            Response.Cookies.Append("typecontrat", idtypecontrat.ToString(), cookieOptions);

            return RedirectToAction("Edit", "Critere");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using RH.Models;
using Npgsql;

namespace RH.Controllers{
    public class ContratController : SessionController{
        public IActionResult EssaiAdmin( int idcandidature) {            
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
            double basesalaire = double.Parse(Request.Query["base"]);

            Essai essai = new Essai(idbesoin, idcandidat, duree, debut, basesalaire);
            int idessai = essai.InsertEssai(npg);

            string[] avantage = Request.Query["avantage"];
            foreach (string av in avantage)
            {
                int idavantage = int.Parse(av);
                Avantage.InsertAvantage(npg, idessai, idavantage);
            }

            Candidat candidat = Candidat.GetById(npg , idcandidat );
            
            npg.Close();

            ViewBag.candidat = candidat;
            ViewBag.idessai = idessai;
            ViewBag.basesal = basesalaire;
            return View("Views/Home/contrat_essai.cshtml");
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

            string signature = Request.Query["signature"];

            int idessai = int.Parse(Request.Query["idessai"]);

            Info info = new Info(idcand, cin, adresse, pere, mere, nbenf);
            info.InsertInfo(npg);

            Contrat contrat = new Contrat(idessai, signature);
            contrat.InsertContratEssai(npg);

            npg.Close();
            return View("Views/Home/index.cshtml");
        }

        public IActionResult Liste_essai(){  
            Connection connexion = new Connection();
            NpgsqlConnection npg = connexion.ConnectSante();

            Essai[] essais = Essai.GetAll(npg);

            return View("Views/Home/listeEssai.cshtml", essais);
        }

        public IActionResult Travail(int id, int idc, double salaire)
        {          
            Connection connexion = new Connection();
            NpgsqlConnection npg = connexion.ConnectSante();

            Candidat candidat = Candidat.GetById(npg , idc );
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };
            Response.Cookies.Append("idcandidat", idc.ToString(), cookieOptions);

            ViewBag.id = id;
            ViewBag.salaire = salaire;
            ViewBag.candidat = candidat;

            npg.Close();
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

            int idtravail = 0;

            Sante[] santes = Sante.GetAllSantes(npg);
            ViewBag.idt = idtravail;

            npg.Close();

            return View("Views/Home/contrat_travail.cshtml", santes);
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

            int idcandidat = int.Parse( Request.Cookies["idcandidat"] );
            double salaire = Essai.getSalaire(npg, idcandidat);
            Response.Cookies.Delete("idcandidat");
            Candidat candidat = Candidat.GetById(npg , idcandidat );

            int idbesoin = Essai.getIdbesoin(npg, idcandidat);
            Dictionary< string, List<Choix> > choix = FicheCandidat.getChoixCandidat(npg, idbesoin, idcandidat);

            int[] id = Essai.getIdservice_Idposte(npg, idbesoin);
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };
            Response.Cookies.Append("idservice", id[0].ToString(), cookieOptions);
            
            int idposte = id[1];

            Info info = Info.GetInfoId(npg, idcandidat);

            Personnel personnel = new Personnel{
                nom = candidat.nom,
                prenom = candidat.prenom,
                mail = candidat.mail,
                matricule = "12345",
                nationalite =  Convert.ToInt32(choix["nationalite"][0].idChoix),
                adresse = info.adresse,
                genre = Convert.ToInt32(choix["genre"][0].idChoix),
                travailleur = 1,
                dtn = candidat.dtn
            };
            int idpersonnel = personnel.insert(npg);
            Personnel.insertPoste(npg, idposte, idpersonnel);
            Personnel.insertEmbauche(npg, idpersonnel, sign);
            Personnel.insertSalaire(npg, idpersonnel, salaire);

            ViewBag.idpersonnel = idpersonnel;

            npg.Close();
            return View("Views/Home/loguser.cshtml");
        }

        public IActionResult InsertUser(){
            Connection connexion = new Connection();
            NpgsqlConnection npg = connexion.ConnectSante();
            string nom = Request.Form["nom"];
            string mdp = Request.Form["mdp"];
            int idpersonnel = int.Parse( Request.Form["idperso"] );
            int idtypeuser = int.Parse( Request.Cookies["idservice"] );
            Response.Cookies.Delete("idservice");
            User user = new User(nom, mdp, idtypeuser, idpersonnel);
            user.InsertUser(npg);
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
using Npgsql;
// using Npgsql.Replication;

namespace RH.Models
{
    public class Essai{
        int _idessai;
        public int idessai {
            get { return _idessai; }
            set { _idessai = value; }
        }
        int _idbesoin;
        public int idbesoin {
            get { return _idbesoin; }
            set { _idbesoin = value; }
        }
        int _idcandidat;
        public int idcandidat {
            get { return _idcandidat; }
            set { _idcandidat = value; }
        }
        double _duree;
        public double duree {
            get { return _duree; }
            set { _duree = value; }
        }
        string _debut;
        public string debut {
            get { return _debut; }
            set { _debut = value; }
        }
        double _salaire_base;
        public double salaire_base {
            get { return _salaire_base; }
            set { _salaire_base = value; }
        }
        Candidat _candidat;
        public Candidat candidat {
            get { return _candidat; }
            set { _candidat = value; }
        }

        public Essai(int idb, int idc, double duree, string date, double salaire_base){
            idbesoin = idb;
            idcandidat = idc;
            _duree = duree;
            _debut = date;
            _salaire_base = salaire_base;
            
        }

        public Essai(int idb, int idc, double duree, string date, double salaire_base, Candidat candidat,int idessai){
            idbesoin = idb;
            idcandidat = idc;
            _duree = duree;
            _debut = date;
            _salaire_base = salaire_base;
            _candidat = candidat;
            _idessai = idessai;
        }

        public static int getIdbesoin(NpgsqlConnection npg, int idc){
            bool estOuvert = false;
            int idbesoin = 0;
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try{
                string sql = "select idbesoin from essai where idcandidat=" + idc;
                Console.WriteLine( sql );
                using (NpgsqlCommand command2 = new NpgsqlCommand(sql, npg))
                {
                    using (NpgsqlDataReader reader = command2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idbesoin = reader.GetInt32(0);
                        }
                    }
                }
            }
            catch (Exception e)        {
                Console.WriteLine(e.ToString());
            }
            finally        {
                if (estOuvert)            {
                    npg.Close();
                }
            }   
            return idbesoin ; 
        }

        public static double getSalaire(NpgsqlConnection npg, int idc){
            bool estOuvert = false;
            double salaire = 0;
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try{
                string sql = "select salaire_base from essai where idcandidat=" + idc;
                Console.WriteLine( sql );
                using (NpgsqlCommand command2 = new NpgsqlCommand(sql, npg))
                {
                    using (NpgsqlDataReader reader = command2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salaire = reader.GetDouble(0);
                        }
                    }
                }
            }
            catch (Exception e)        {
                Console.WriteLine(e.ToString());
            }
            finally        {
                if (estOuvert)            {
                    npg.Close();
                }
            }   
            return salaire ; 
        }

        public static int[] getIdservice_Idposte(NpgsqlConnection npg, int idbesoin){
            bool estOuvert = false;
            int[] rep = new int[2];
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try{
                string sql = "select idservice, idposte from v_poste_besoin where idbesoin=" + idbesoin;
                Console.WriteLine( sql );
                using (NpgsqlCommand command2 = new NpgsqlCommand(sql, npg))
                {
                    using (NpgsqlDataReader reader = command2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rep[0] = reader.GetInt32(0);
                            rep[1] = reader.GetInt32(1);
                        }
                    }
                }
            }
            catch (Exception e)        {
                Console.WriteLine(e.ToString());
            }
            finally        {
                if (estOuvert)            {
                    npg.Close();
                }
            }   
            return rep ; 
        }

        public int InsertEssai(NpgsqlConnection npg) {
            bool estOuvert = false;
            int idessai = 0;
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try{
                string sql = "insert into essai (idbesoin, idcandidat, duree, debut, salaire_base) values (@idbesoin, @idcandidat, @duree, @debut, @salaire_base)";
                Console.WriteLine(sql);
                
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    command.Parameters.AddWithValue("@idbesoin", this.idbesoin);
                    command.Parameters.AddWithValue("@idcandidat", this.idcandidat);
                    command.Parameters.AddWithValue("@duree", this.duree);
                    command.Parameters.AddWithValue("@debut", Convert.ToDateTime(this.debut));
                    command.Parameters.AddWithValue("@salaire_base", this.salaire_base);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        sql = "SELECT idessai FROM essai order by idessai desc";
                        Console.WriteLine( sql );
                        using (NpgsqlCommand command2 = new NpgsqlCommand(sql, npg))
                        {
                            using (NpgsqlDataReader reader = command2.ExecuteReader())
                            {
                                if(reader.Read())
                                {
                                    idessai = reader.GetInt32(0);
                                }
                            }
                        }
                    }                                
                    else
                    {
                        Console.WriteLine("Aucune ligne insérée.");
                    }
                }
            }
            catch (Exception e)        {
                Console.WriteLine(e.ToString());
            }
            finally        {
                if (estOuvert)            {
                    npg.Close();
                }
            }   
            Console.WriteLine("idessai " + idessai);
            return idessai ; 
        }

        public static Essai[] GetAll(NpgsqlConnection npg) {
        Essai[] essais = null;
        bool estOuvert = false;
        
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }        
        try        
        {
            string sql = "select * from v_essai_fin";
            Console.WriteLine(sql);         
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))            {
                using (NpgsqlDataReader reader = command.ExecuteReader())                {
                    List<Essai> EssaiList = new List<Essai>();
                    while (reader.Read())                    {
                        int idcandidat = Convert.ToInt32(reader["idcandidat"]);
                        int idessai = Convert.ToInt32(reader["idcontrat_essai"]);
                        string signessai = Convert.ToString(reader["signessai"]);
                        int idbesoin = Convert.ToInt32(reader["idbesoin"]);
                        double duree = Convert.ToDouble(reader["duree"]);
                        DateTime debut = Convert.ToDateTime(reader["debut"]);
                        TimeSpan dureeEnMois = TimeSpan.FromDays(duree * 30.44); // Approximation de la durée en jours
                        // Date actuelle
                        DateTime currentDate = DateTime.Now;
                        // Calcul de la date finale
                        DateTime dateFinale = debut.Add(dureeEnMois);
                        string datefin = dateFinale.ToString();
                        double salaire_base = Convert.ToDouble(reader["salaire_base"]);

                        Candidat candidat = new Candidat{
                            idcandidat = Convert.ToInt32(reader["idcandidat"]),
                            nom = Convert.ToString(reader["nomcandidat"]),
                            prenom = Convert.ToString(reader["prenomcandidat"]),
                            dtn = Convert.ToDateTime(reader["dtn"]),
                            mail = Convert.ToString(reader["mail"]),
                            contact = Convert.ToString(reader["contact"])
                        };
                        
                        Essai Essai = new  Essai(idbesoin, idcandidat, duree, datefin, salaire_base, candidat,idessai);
                        EssaiList.Add(Essai);
                    }
                    essais = EssaiList.ToArray();
                }
            }
        }
        catch (Exception e)        {
            Console.WriteLine("erreurrrr");
            Console.WriteLine(e.ToString());
        }
        finally        {
            if (estOuvert)            {
                npg.Close();
            }
        }      
        return essais;
    }

    }
}
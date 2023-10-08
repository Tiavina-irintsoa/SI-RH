using Npgsql;
namespace RH.Models
{
    public class Annonce{
        Besoin? _besoin { get; set; } 
        int? _nombre { get; set; } 
        public Annonce(int idbesoin, int nb_personne, string nomService, string iconeservice, string nomposte){
            Service service = new Service(nomService, iconeservice);
            Poste p = new Poste(service, nomposte);
            _besoin = new Besoin(idbesoin,p);
            _nombre = nb_personne;
        }

        
        public static Annonce[] GetAllAnnonces(NpgsqlConnection npg){
            Annonce[] annonces = null;
            bool estOuvert = false;
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try        {
                string sql = "SELECT * FROM v_annonce";
                Console.WriteLine(sql);            
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))            {
                    using (NpgsqlDataReader reader = command.ExecuteReader())                {
                        List<Annonce> AnnonceList = new List<Annonce>();
                        while (reader.Read())                    
                        {
                            int idbesoin = reader.GetInt32(0);
                            int nbpersonne = reader.GetInt32(1);
                            string nomservice = reader.GetString(2);
                            string iconeservice = reader.GetString(3);
                            string nomposte = reader.GetString(4);

                            Annonce Annonce = new Annonce(idbesoin, nbpersonne,nomservice,iconeservice,nomposte);
                            AnnonceList.Add(Annonce);
                        }
                        annonces = AnnonceList.ToArray();
                    }
                }
            }
            catch (Exception e)        {
                Console.WriteLine(e.ToString());
                throw e;
            }
            finally        {
                if (estOuvert)            {
                    npg.Close();
                }
            }        
            return annonces;
        }
        public Besoin? Besoin {
            get { return _besoin; }
            set { _besoin = value; }
        }
        public int? Nombre {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }
    
}
using Npgsql;
namespace RH.Models
{
    public class Avantage{
        int _idavantage;

        public int idavantage {
            get { return _idavantage; }
            set { _idavantage = value; }
        }

        string _nomavantage;

        public string nomavantage {
            get { return _nomavantage; }
            set { _nomavantage = value; }
        }

        public Avantage(int id, string nom){
            idavantage = id;
            nomavantage = nom;
        }

        public static void InsertAvantage(NpgsqlConnection npg, int idessai, int idavantage) {
            bool estOuvert = false;
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try{
                string sql = "insert into essai_avantage values (@idessai, @idavantage)";
                Console.WriteLine(sql);
                
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    command.Parameters.AddWithValue("@idessai", idessai);
                    command.Parameters.AddWithValue("@idavantage", idavantage);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine( "Insertion reussie" );
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
        }
        
        public static Avantage[] GetAllAvantages(NpgsqlConnection npg){
            Avantage[] Avantages = null;
            bool estOuvert = false;
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try        {
                string sql = "SELECT * FROM avantage";
                Console.WriteLine(sql);            
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))            {
                    using (NpgsqlDataReader reader = command.ExecuteReader())                {
                        List<Avantage> AvantageList = new List<Avantage>();
                        while (reader.Read())                    
                        {
                            int id = reader.GetInt32(0);
                            string nom = reader.GetString(1);

                            Avantage Avantage = new Avantage(id, nom);
                            AvantageList.Add(Avantage);
                        }
                        Avantages = AvantageList.ToArray();
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
            return Avantages;
        }
    
    }
}
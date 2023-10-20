using Npgsql;
namespace RH.Models
{
    public class Sante{
        int _idSante;

        public int idSante {
            get { return _idSante; }
            set { _idSante = value; }
        }

        string _nomSante;

        public string nomSante {
            get { return _nomSante; }
            set { _nomSante = value; }
        }

        public Sante(int id, string nom){
            idSante = id;
            nomSante = nom;
        }

        public static void InsertSante(NpgsqlConnection npg, int idcontrat_travail, int idsante) {
            bool estOuvert = false;
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try{
                string sql = "insert into travail_sante values (@idcontrat_travail, @idsante)";
                Console.WriteLine(sql);
                
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    command.Parameters.AddWithValue("@idcontrat_travail", idcontrat_travail);
                    command.Parameters.AddWithValue("@idsante", idsante);
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
        
        public static Sante[] GetAllSantes(NpgsqlConnection npg){
            Sante[] Santes = null;
            bool estOuvert = false;
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try        {
                string sql = "SELECT * FROM Sante";
                Console.WriteLine(sql);            
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))            {
                    using (NpgsqlDataReader reader = command.ExecuteReader())                {
                        List<Sante> SanteList = new List<Sante>();
                        while (reader.Read())                    
                        {
                            int id = reader.GetInt32(0);
                            string nom = reader.GetString(1);

                            Sante Sante = new Sante(id, nom);
                            SanteList.Add(Sante);
                        }
                        Santes = SanteList.ToArray();
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
            return Santes;
        }
    
    }
}
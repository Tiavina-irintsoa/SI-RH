using Npgsql;
using Npgsql.Replication;

namespace RH.Models
{
    public class Contrat{
        int _id;
        public int id {
            get { return _id; }
            set { _id = value; }
        }
        string _sign;
        public string sign {
            get { return _sign; }
            set { _sign = value; }
        }

        public Contrat(int idb, string date){
            _id = idb;
            _sign = date;
        }

        public void InsertContratEssai(NpgsqlConnection npg) {
            bool estOuvert = false;
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try{
                string sql = "insert into contrat_essai (idessai, signessai) values (@idessai, @signessai)";
                Console.WriteLine(sql);
                
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    command.Parameters.AddWithValue("@idessai", this.id);
                    command.Parameters.AddWithValue("@signessai", this.sign);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Insertion contrat_essai");
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

        public void InsertContratTravail(NpgsqlConnection npg) {
            bool estOuvert = false;
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try{
                string sql = "insert into contrat_travail (idtravail, signetravail) values (@idtravail, @signetravail)";
                Console.WriteLine(sql);
                
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    command.Parameters.AddWithValue("@idtravail", this.id);
                    command.Parameters.AddWithValue("@signetravail", this.sign);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Insertion contrat_travail");
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

    }
}
using Npgsql;
using Npgsql.Replication;

namespace RH.Models
{
    public class Travail{
        int _idtravail;
        public int idtravail {
            get { return _idtravail; }
            set { _idtravail = value; }
        }
        int _idcontrat_essai;
        public int idcontrat_essai {
            get { return _idcontrat_essai; }
            set { _idcontrat_essai = value; }
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

        public Travail(int idc, double duree, string debut){
            _idcontrat_essai = idc;
            _duree = duree;
            _debut = debut;
        }

        public void InsertTravail(NpgsqlConnection npg) {
            bool estOuvert = false;
            int idessai = 0;
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try{
                string sql = "insert into travail (idcontrat_essai, duree, debut) values (@idcontrat_essai, @duree, @debut)";
                Console.WriteLine(sql);
                
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    command.Parameters.AddWithValue("@idcontrat_essai", this.idcontrat_essai);
                    command.Parameters.AddWithValue("@duree", this.duree);
                    command.Parameters.AddWithValue("@debut", this.debut);
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

    }
}
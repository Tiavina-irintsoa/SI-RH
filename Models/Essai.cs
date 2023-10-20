using Npgsql;
using Npgsql.Replication;

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

        public Essai(int idb, int idc, double duree, string date, double salaire_base){
            idbesoin = idb;
            idcandidat = idc;
            _duree = duree;
            _debut = date;
            _salaire_base = salaire_base;
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
                    command.Parameters.AddWithValue("@debut", this.debut);
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
                                while (reader.Read())
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

    }
}
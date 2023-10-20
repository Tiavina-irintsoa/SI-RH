using Npgsql;
using Npgsql.Replication;

namespace RH.Models
{
    public class Contrat{
        int _idessai;
        public int idessai {
            get { return _idessai; }
            set { _idessai = value; }
        }
        string _signessai;
        public string signessai {
            get { return _signessai; }
            set { _signessai = value; }
        }
        double _net;
        public double net {
            get { return _net; }
            set { _net = value; }
        }

        int _idtravail;
        public int idtravail {
            get { return _idtravail; }
            set { _idtravail = value; }
        }
        string _signetravail;
        public string signetravail {
            get { return _signetravail; }
            set { _signetravail = value; }
        }

        public Contrat(int idb, string date, double net){
            _idessai = idb;
            _signessai = date;
            _net = net;
        }

        public Contrat(int idt, string signetravail){
            _idtravail = idt;
            _signetravail = signetravail;
        }

        public void InsertContratEssai(NpgsqlConnection npg) {
            bool estOuvert = false;
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try{
                string sql = "insert into contrat_essai (idessai, net, signessai) values (@idessai, @net, @signessai)";
                Console.WriteLine(sql);
                
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    command.Parameters.AddWithValue("@idessai", this.idessai);
                    command.Parameters.AddWithValue("@net", this.net);
                    command.Parameters.AddWithValue("@signessai", this.signessai);
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
                    command.Parameters.AddWithValue("@idtravail", this.idtravail);
                    command.Parameters.AddWithValue("@signetravail", this.signetravail);
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
using Npgsql;
using Npgsql.Replication;

namespace RH.Models
{
    public class Info{
        int _idinfo;
        public int idinfo {
            get { return _idinfo; }
            set { _idinfo = value; }
        }
        int _idcandidat;
        public int idcandidat {
            get { return _idcandidat; }
            set { _idcandidat = value; }
        }
        string _cin;
        public string cin {
            get { return _cin; }
            set { _cin = value; }
        }
        string _adresse;
        public string adresse {
            get { return _adresse; }
            set { _adresse = value; }
        }
        string _pere;
        public string pere {
            get { return _pere; }
            set { _pere = value; }
        }
        string _mere;
        public string mere {
            get { return _mere; }
            set { _mere = value; }
        }
        int _nbenfant;
        public int nbenfant {
            get { return _nbenfant; }
            set { _nbenfant = value; }
        }

        public Info(int idcandidat, string cin, string adr, string pr, string mr, int nbe){
            _idcandidat = idcandidat;
            _cin = cin;
            _adresse = adr;
            _pere = pr;
            _mere = mr;
            _nbenfant = nbe;
        }

        public static Info GetInfoId(NpgsqlConnection npg, int idcandidat)
        {
            bool estOuvert = false;
            if (npg == null)
            {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }
            try
            {
                string sql = "SELECT * FROM info where idcandidat=@idcandidat";
                Console.WriteLine(sql);
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    command.Parameters.AddWithValue("@idcandidat", idcandidat);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string cin = Convert.ToString(reader["cin"]);
                            string adresse = Convert.ToString(reader["cin"]);
                            string pere = Convert.ToString(reader["pere"]);
                            string mere = Convert.ToString(reader["mere"]);
                            int nbenfant = Convert.ToInt32(reader["nbenfant"]);
                            
                            Info info = new Info(idcandidat, cin, adresse, pere, mere, nbenfant);
                            return info;
                        }
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (estOuvert)
                {
                    npg.Close();
                }
            }
        }

        public void InsertInfo(NpgsqlConnection npg) {
            bool estOuvert = false;
            int idessai = 0;
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try{
                string sql = "insert into info (idcandidat, cin, adresse, pere, mere, nbenfant) values (@idcandidat, @cin, @adresse, @pere, @mere, @nbenfant)";
                Console.WriteLine(sql);
                
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    command.Parameters.AddWithValue("@idcandidat", this.idcandidat);
                    command.Parameters.AddWithValue("@cin", this.cin);
                    command.Parameters.AddWithValue("@adresse", this.adresse);
                    command.Parameters.AddWithValue("@pere", this.pere);
                    command.Parameters.AddWithValue("@mere", this.mere);
                    command.Parameters.AddWithValue("@nbenfant", this.nbenfant);
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
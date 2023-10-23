using Npgsql;
using Npgsql.Replication;

namespace RH.Models
{
    public class User{
        int _idadmin;
        public int idadmin {
            get { return _idadmin; }
            set { _idadmin = value; }
        }
        string _nom;
        public string nom {
            get { return _nom; }
            set { _nom = value; }
        }
        string  _mdp;
        public string mdp {
            get { return _mdp; }
            set { _mdp = value; }
        }
        int _idtypeuser;
        public int idtypeuser {
            get { return _idtypeuser; }
            set { _idtypeuser = value; }
        }
        int _idpersonnel;
        public int idpersonnel {
            get { return _idpersonnel; }
            set { _idpersonnel = value; }
        }

        public User(string nom, string mdp, int idtype, int idperso){
            _nom = nom;
            _mdp = mdp;
            _idtypeuser =idtype;
            _idpersonnel = idperso;
        }

        public void InsertUser(NpgsqlConnection npg) {
            bool estOuvert = false;
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try{
                string sql = "insert into useradmin ( nom , mdp , idtypeuser, idpersonnel ) values ( @nom , @mdp , @idtypeuser, @idpersonnel )";
                Console.WriteLine(sql);
                
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    command.Parameters.AddWithValue("@nom", this.nom);
                    command.Parameters.AddWithValue("@mdp", this.mdp);
                    command.Parameters.AddWithValue("@idtypeuser", this.idtypeuser);
                    command.Parameters.AddWithValue("@idpersonnel", this.idpersonnel);
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
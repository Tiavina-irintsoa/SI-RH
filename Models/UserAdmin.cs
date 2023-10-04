using Npgsql;

namespace RH.Models
{
    public class UserAdmin{
        int? _idadmin;
        string? _nom;
        string? _mdp;
        TypeUSer _type; 
        public int? Idadmin
        {
            get { return _idadmin; }
            set { _idadmin = value; }
        }
        public string? Nom{
            get{ return _nom; }
            set{  _nom = value; }
        }
        public string? Mdp{
            get{ return _mdp; }
            set{  _mdp = value; }
        }
        
        public TypeUSer type { get => type; set => type = value; }


        public static UserAdmin GetUser(string nom, string mdp, NpgsqlConnection npg)
        {
            UserAdmin user = null;
            bool estOuvert = false;
            
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }  
            try
            {
                string sql = "SELECT * FROM useradmin WHERE nom = @nom AND mdp = @mdp";
                Console.WriteLine(sql);
                Console.WriteLine( nom+" / "+mdp );
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    command.Parameters.AddWithValue("@nom", nom);
                    command.Parameters.AddWithValue("@mdp", mdp);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new UserAdmin
                            {
                                Idadmin = reader.IsDBNull(0) ? (int?)null : reader.GetInt32(0),
                                Nom = reader.IsDBNull(1) ? null : reader.GetString(1),
                                Mdp = reader.IsDBNull(2) ? null : reader.GetString(2),
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally{
                if (estOuvert)
                {
                    npg.Close();
                }
            }
            return user;
        }
            

    }
}
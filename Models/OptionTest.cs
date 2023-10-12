using Npgsql;

namespace RH.Models
{
    public class OptionTest{
        public int? idOption { get; set; }
        public string? option { get; set; }
        public int? note { get; set; }
        
        public void InsertOption(NpgsqlConnection npg, int? idquestion) {
            Console.WriteLine("insert option");
            bool estOuvert = false;
            string sql = "";
            if (npg == null){
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try{
                sql = "INSERT INTO option (idquestion, option,points) VALUES (@idquestion, @option,@points) ";                
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    command.Parameters.AddWithValue("@idquestion", idquestion);
                    command.Parameters.AddWithValue("@option",option);
                    command.Parameters.AddWithValue("@points",note);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected < 0)
                    {
                        throw new Exception("Erreur lors de l'insertion de l'option"+option);
                    } 
                }
            }
            catch (Exception e){
                Console.WriteLine(e.ToString());
                throw e; 
            }
            finally{
                if (estOuvert){
                    npg.Close();
                }
            }      
        }
 
    }
}
using Npgsql;
namespace RH.Models{
    
public class QuestionData
{
    public string? Question { get; set; }
    public List<OptionTest>? Options { get; set; }
    public int? idQuestion { get; set; }
    public int? note { get; set; }

        public void Insert(NpgsqlConnection npg,int? idQuestionnaire){
            Console.WriteLine("insert questiondata");
            bool estOuvert = false;
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }
            try{
                this.InsertQuestion(npg,idQuestionnaire);
                foreach(var option in Options){
                    option.InsertOption(npg,idQuestion);
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
      public void InsertQuestion(NpgsqlConnection npg, int?idquestionnaire) {
            Console.WriteLine("insert question");
            bool estOuvert = false;
            string sql = "";
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try{
                sql = "INSERT INTO question (idquestionnaire, question,points) VALUES (@idquestionnaire, @question,@points) returning idquestion";                
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    command.Parameters.AddWithValue("@idquestionnaire", idquestionnaire);
                    command.Parameters.AddWithValue("@question",Question);
                    command.Parameters.AddWithValue("@points",note);
                    idQuestion = (int)command.ExecuteScalar();
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
        public void InsertOption(NpgsqlConnection npg, int? idquestion,string option) {
            Console.WriteLine("insert option");
            bool estOuvert = false;
            string sql = "";
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try{
                sql = "INSERT INTO option (idquestion, option) VALUES (@idquestion, @option) ";                
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    command.Parameters.AddWithValue("@idquestion", idquestion);
                    command.Parameters.AddWithValue("@option",option);
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
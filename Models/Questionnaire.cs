using Npgsql;
namespace RH.Models{
    public class Questionnaire{
        int? idQuestionnaire { get; set; }
        public Besoin Besoin { get; set; }
        public List<QuestionData> questions { get; set; }

        public void Insert(NpgsqlConnection npg){
            Console.WriteLine("insert questionnaire 1");
            bool estOuvert = false;
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }
            try{
                InsertQuestionnaire(npg);
                foreach(var question in questions){
                    Console.WriteLine("one");
                    question.Insert(npg,idQuestionnaire);
                }
            }   
            catch (Exception e){
                Console.WriteLine("2 erreur");
                Console.WriteLine(e.ToString());
                throw e;
            }
            finally{
                if (estOuvert){
                    npg.Close();
                }
            }   
        }
         public void InsertQuestionnaire(NpgsqlConnection npg) {
            Console.WriteLine("insert questionnaire 2");
            bool estOuvert = false;
            string sql = "";
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }        
            try{
                sql = "INSERT INTO questionnaire (idbesoin ) VALUES (@idbesoin) returning idquestionnaire";                
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    command.Parameters.AddWithValue("@idbesoin", this.Besoin.idBesoin);
                    this.idQuestionnaire = (int)command.ExecuteScalar();
                    Console.WriteLine("idquestionnaire = "+idQuestionnaire);
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
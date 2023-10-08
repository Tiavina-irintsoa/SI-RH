using Npgsql;
namespace RH.Models{
    public class Questionnaire{
        int? idQuestionnaire { get; set; }
        Besoin Besoin { get; set; }
        public List<QuestionData> questions { get; set; }

        public void Insert(NpgsqlConnection npg){
            bool estOuvert = false;
            if (npg == null)        {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }
            try{
                InsertQuestionnaire(npg);
                foreach(var question in questions){
                    question.Insert(npg,idQuestionnaire);
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
         public void InsertQuestionnaire(NpgsqlConnection npg) {
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
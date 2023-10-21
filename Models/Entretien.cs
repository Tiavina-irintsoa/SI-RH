using Npgsql;

namespace RH.Models{
    public class Entretien{
        public Besoin besoin {get;set;}
        public List<QuestionEntretien> questions {get;set;}
        public List<CandidatEntretien> candidats {get;set;}
        public Entretien(int idbesoin){
            besoin = new Besoin(idbesoin);
        }

        
        public List<CandidatEntretien> GetCandidatEntretienList(NpgsqlConnection connection)
        {
            Boolean estOuvert = false;
            try
            {
                
                var candidatEntretiens = new List<CandidatEntretien>();
                if (connection == null)
                {
                    Connection connect = new Connection();
                    connection = connect.ConnectSante();
                    estOuvert = true;
                }

                besoin.complete(connection);

                using (var cmd = new NpgsqlCommand("SELECT * FROM v_points_entretien_candidat WHERE idbesoin = @BesoinId order by points desc", connection))
                {
                    cmd.Parameters.AddWithValue("BesoinId", besoin.idBesoin);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var candidatEntretien = new CandidatEntretien(Convert.ToInt32(reader["idcandidature"]), Convert.ToDouble(reader["points"]), Convert.ToString(reader["nomcandidat"]), Convert.ToString(reader["prenomcandidat"]));
                            candidatEntretiens.Add(candidatEntretien);
                           
                        }
                    }
                }

                return candidatEntretiens;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw e;
            }
            finally{
                if(estOuvert){
                    connection.Close();
                }
            }
        }

    }
}
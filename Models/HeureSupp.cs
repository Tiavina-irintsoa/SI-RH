using Npgsql;

namespace RH.Models{
    public class HeureSupp{
        public int? IdHeureSup {get;set;}
        public DateTime debut {get;set;}
        public DateTime fin {get;set;}
        public Service? Service {get;set;}
        public List<Personnel>? Personnels {get;set;}
        public string? Raison {get;set;}

        public void setPersonnels(List<int> ids){
            Personnels = new List<Personnel>();
            foreach (var id in ids)
            {   
                Console.WriteLine(id);
                Personnels.Add(new Personnel(id));
            }
        }
        public void InsererHeureSup(){
        using (NpgsqlConnection connection =new Connection(). ConnectSante())
        {
            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    string insertHeureSupQuery = "INSERT INTO demande_heure_sup (date_heure_sup, heure_debut, heure_fin, idservice, validation,raison) " +
                        "VALUES (@DateHeureSup, @HeureDebut, @HeureFin, @IdService, @Validation,@raison) RETURNING iddemande_heure_sup";
                    Console.WriteLine(insertHeureSupQuery);
                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertHeureSupQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("DateHeureSup", debut.Date);
                        cmd.Parameters.AddWithValue("HeureDebut", debut.TimeOfDay);
                        cmd.Parameters.AddWithValue("HeureFin", fin.TimeOfDay);
                        cmd.Parameters.AddWithValue("IdService", Service.IdService);
                        cmd.Parameters.AddWithValue("Validation", 0);
                        cmd.Parameters.AddWithValue("raison", Raison);

                        int demandeHeureSupId = (int)cmd.ExecuteScalar();

                        string insertEmployeHeureSupQuery = "INSERT INTO employe_heure_sup (iddemande_heure_sup, idemploye) " +
                            "VALUES (@IdDemandeHeureSup, @IdEmploye)";
                    Console.WriteLine(insertEmployeHeureSupQuery);

                        foreach (Personnel employe in Personnels)
                        {
                            using (NpgsqlCommand employeCmd = new NpgsqlCommand(insertEmployeHeureSupQuery, connection))
                            {
                                employeCmd.Parameters.AddWithValue("IdDemandeHeureSup", demandeHeureSupId);
                                employeCmd.Parameters.AddWithValue("IdEmploye", employe.idpersonnel);
                                employeCmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de l'insertion : " + ex.Message);
                    transaction.Rollback();
                }
            }
        }
    }

        public List<Personnel> GetPersonnelByServiceId()
        {
            List<Personnel> personnelList = new List<Personnel>();

            using (NpgsqlConnection connection = new Connection().ConnectSante())
            {
                try
                {
                    string sql = "SELECT * FROM v_personnel_information WHERE idservice = @ServiceId order by nom asc, nomposte asc";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                    {

                        cmd.Parameters.AddWithValue("ServiceId", this.Service.IdService);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Personnel personnel = new Personnel
                                (
                                    Convert.ToString(reader["nom"]),
                                    Convert.ToString(reader["prenom"]),
                                    Convert.ToInt32(reader["idpersonnel"]),
                                    Convert.ToString(reader["nomposte"])
                                );
                                personnelList.Add(personnel);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de l'exécution de la requête : " + ex.Message);
                    throw ex;
                }
            }

            return personnelList;
        }
        public HeureSupp(string heuredebut, string heurefin, string date,string raison,int idtypeuser)
        {
            // Parsez les chaînes de caractères en objets DateTime
            if (DateTime.TryParse(date, out DateTime parsedDate) &&
                DateTime.TryParse(heuredebut, out DateTime parsedDebut) &&
                DateTime.TryParse(heurefin, out DateTime parsedFin))
            {
                debut = parsedDebut;
                fin = parsedFin;
            }
            else
            {
                debut = DateTime.MinValue;
                fin = DateTime.MinValue;
            }
            Raison = raison;
            Service = new Service(idtypeuser);
        }
    }
}
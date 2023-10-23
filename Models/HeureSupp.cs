using Npgsql;

namespace RH.Models{
    public class HeureSupp{
        public int? IdHeureSup {get;set;}
        public DateTime debut {get;set;}
        public DateTime fin {get;set;}
        public Service? Service {get;set;}

        public List<Personnel>? Personnels {get;set;}
        public string? Raison {get;set;}
        public HeureSupp(int idservice, string nomservice, string icone,TimeSpan debut, TimeSpan fin, DateTime date, int idHeureSupp ,string raison){   
            Service = new Service(idservice,nomservice,icone);
            this.debut = date + debut;
            this.fin = date + fin; 
            this.IdHeureSup = idHeureSupp;
            Raison =raison;
        }
        public void setPersonnels(List<int> ids){
            Personnels = new List<Personnel>();
            foreach (var id in ids)
            {   
                Console.WriteLine(id);
                Personnels.Add(new Personnel(id));
            }
        }
        public HeureSupp(){

        }
        public void setValidation(int newvalidation,NpgsqlConnection? npg){
            bool estOuvert = false;

        if (npg == null)
        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }
        try
        {
            string sql = "UPDATE demande_heure_sup SET validation = @newValidation WHERE iddemande_heure_sup = @idd";
            Console.WriteLine(sql);
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
            {
                command.Parameters.AddWithValue("@newValidation", newvalidation);
                command.Parameters.AddWithValue("@idd", this.IdHeureSup);
                int row = command.ExecuteNonQuery();
                Console.WriteLine(row);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
        finally
        {
            if (estOuvert)
            {
                npg.Close();
            }
        }
        }
        public string getDate(){
            return debut.Date.ToString("yyyy-MM-dd");
        }
        public string getDebut(){
            return debut.ToString("HH:mm");
        }
        public string getFin(){
            return fin.ToString("HH:mm");
        }
        public HeureSupp(int id){
            IdHeureSup = id;
        }
        public void complete(){
            Personnels  = this.GetPersonnelHeureSup();
            this.getHeureSuppById();
        }
        public List<Personnel> GetPersonnelHeureSup(){
             List<Personnel> PersonnelList = new List<Personnel>();
            using (NpgsqlConnection connection = new Connection().ConnectSante())
            {
                try
                {
                    string sql = "SELECT * FROM employe_demande_heure_sup where iddemande_heure_sup  = @id order by nom,nomposte";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("id", this.IdHeureSup);
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Personnel personnel = new Personnel
                                (
                                    Convert.ToString(reader["nom"]),
                                    Convert.ToString(reader["prenom"]),
                                    Convert.ToInt32(reader["idemploye"]),
                                    Convert.ToString(reader["nomposte"])
                                );
                                PersonnelList.Add(personnel);
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
            return PersonnelList;
        }
        public  void getHeureSuppById(){
            
            using (NpgsqlConnection connection = new Connection().ConnectSante())
            {
                try
                {
                    string sql = "SELECT * FROM v_heure_supp_non_consulte_avec_service where iddemande_heure_sup = @id order by date_heure_sup desc";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("id",IdHeureSup);
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            
                            Service = new Service(Convert.ToInt32(reader["idservice"]), Convert.ToString(reader["nomservice"]),Convert.ToString(reader["iconeservice"]));
                            DateTime date = Convert.ToDateTime(reader["date_heure_sup"]);

                            
                            debut = date + TimeSpan.Parse(Convert.ToString(reader["heure_debut"]));
                            fin = date + TimeSpan.Parse(Convert.ToString(reader["heure_fin"]));
                                
                            Raison  = Convert.ToString(reader["raison"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de l'exécution de la requête : " + ex.Message);
                    throw ex;
                }
            }
            
        }
        public static List<HeureSupp> getHeureSupp(){
            List<HeureSupp> HeureSuppList = new List<HeureSupp>();
            using (NpgsqlConnection connection = new Connection().ConnectSante())
            {
                try
                {
                    string sql = "SELECT * FROM v_heure_supp_non_consulte_avec_service order by date_heure_sup desc";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                HeureSupp HeureSupp = new HeureSupp
                                (
                                    Convert.ToInt32(reader["idservice"]),
                                    Convert.ToString(reader["nomservice"]),
                                    Convert.ToString(reader["iconeservice"]),
                                    TimeSpan.Parse(Convert.ToString(reader["heure_debut"])),
                                    TimeSpan.Parse(Convert.ToString(reader["heure_fin"])),
                                    Convert.ToDateTime(reader["date_heure_sup"]),
                                    Convert.ToInt32(reader["iddemande_heure_sup"]),
                                    Convert.ToString(reader["raison"])
                                );
                                HeureSuppList.Add(HeureSupp);
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
            return HeureSuppList;
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
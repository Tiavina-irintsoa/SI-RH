using Npgsql;
using System;
using System.Collections.Generic;
namespace RH.Models
{
    public class PrimeAnciennete
    {
        public int? IdPrimeAnciennete { get; set; }
        public double? Pourcentage { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }
        public DateTime? Date {get;set;}
    
        
        public PrimeAnciennete(){

        }
        public PrimeAnciennete(int? idPrimeAnciennete, double? pourcentage, int? min, int? max, DateTime? date)
        {
            IdPrimeAnciennete = idPrimeAnciennete;
            Pourcentage = pourcentage;
            Min = min;
            Max = max;
            Date = date;
        }

        public void InsertPrimeAnciennete()
        {
            using (NpgsqlConnection connection = new Connection().ConnectSante())
            using (NpgsqlCommand cmd = new NpgsqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "INSERT INTO prime_anciennete (annee_min, annee_max, pourcentage) VALUES (@Min, @Max, @Pourcentage)";

                cmd.Parameters.AddWithValue("@Min", Min);
                cmd.Parameters.AddWithValue("@Max", Max);
                cmd.Parameters.AddWithValue("@Pourcentage", Pourcentage);

                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine(rows);
            }
        }

        public static List<PrimeAnciennete> GetAllPrimeAnciennete()
        {
            List<PrimeAnciennete> primeAncienneteList = new List<PrimeAnciennete>();

            using (NpgsqlConnection connection = new Connection().ConnectSante())
            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM prime_anciennete order by annee_max desc", connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("idprime_anciennete"));
                        int min = reader.GetInt32(reader.GetOrdinal("annee_min"));
                        int max = reader.GetInt32(reader.GetOrdinal("annee_max"));
                        double pourcentage = reader.GetDouble(reader.GetOrdinal("pourcentage"));
                        DateTime date = Convert.ToDateTime(
                            reader["date_insertion"]
                        );

                        PrimeAnciennete primeAnciennete = new PrimeAnciennete(id, pourcentage, min, max,date);
                        primeAncienneteList.Add(primeAnciennete);
                    }
                }
            }

            return primeAncienneteList;
        }
    }

}
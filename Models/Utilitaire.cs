using Npgsql;
namespace RH.Models
{
    public class Utilitaire{


        static List<string> colors = new List<string>
        {
            "#e20000",
            "#e63cad",
            "#ffcff9",
            "#ae6bb6",
            "#d896e0",
            "#38b5fd"
        };

        public static Ressource ConvertCongeToRessource( Personnel p , int index ){
            return new Ressource{
                id = p.idpersonnel ,
                name = p.nom + " " +p.prenom ,
                color = colors[index % colors.Count]
            };
        }
        public static Event ConvertCongeToEvent(Conge conge , int index )
        {
            return new Event
            {
                start = conge.DateDebut,
                end = conge.ReelDateFin,
                title =  "Congé" + conge.Raison == "Aucune" ? conge.autre_raison : conge.Raison.nomRaison ,
                resource = conge.Personnel.idpersonnel,
            };
        }

        public static double  CalculateHoursDifference( string dt1 , string tm1 , string dt2 , string tm2 ){
            return CalculateHoursDifference( toDateTime( dt1 , tm1 ) , toDateTime( dt2 , tm2 )  );
        }
        
        public static double CalculateHoursDifference(DateTime dateTime1, DateTime dateTime2)
        {
            TimeSpan timeDifference = dateTime2 - dateTime1;

            int days = timeDifference.Days;

            int workHoursPerDay = 24 - 8; 

            double hoursDifference = timeDifference.TotalHours - (days * workHoursPerDay);

            return hoursDifference;
        }

        public static DateTime toDateTime( string dateStr1 , string timeStr1  )
        {
            DateTime dateTime1 = DateTime.Parse(dateStr1 + " " + timeStr1);
            return dateTime1;
        }
        public static bool IsDateGreaterThan15DaysFromToday(string dateString)
        {
            if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime inputDate))
            {
                DateTime today = DateTime.Today;
                Console.WriteLine( inputDate+ " - " +today );
                TimeSpan difference = inputDate - today;
                Console.WriteLine( "la difference est : "+difference );
                if( difference.TotalDays < 0 ){
                    throw new Exception( " cette date n'est plus disponible " );
                }
                if (difference.TotalDays >= 15)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<string> ConvertStringToList(string input)
        {
            string[] parts = input.Split(',');
            return new List<string>(parts);
        }

        public  static string GenerateCondition(string fieldName, List<string> values)
        {
            if (values.Count == 0)
            {
                return ""; // Si la liste est vide, retourner une chaîne vide
            }

            string condition = string.Join(" or ", values.Select(value => $"{fieldName} = {value}"));
            return condition;
        }
        public static int getLast(NpgsqlConnection npg, string colonne , string table)
        {
            int nextSerialValue = 0;
            bool estOuvert = false;

            if (npg == null)
            {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }

            try
            {
                string sql = $"SELECT max('{colonne}') from ('{table}')";
                Console.WriteLine(sql);

                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    nextSerialValue = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw e;
            }
            finally
            {
                if (estOuvert)
                {
                    npg.Close();
                }
            }

            return nextSerialValue;
        }
        public static int GetNextSerialValue(NpgsqlConnection npg, string sequenceName)
        {
            int nextSerialValue = 0;
            bool estOuvert = false;

            if (npg == null)
            {
                estOuvert = true;
                Connection connexion = new Connection();
                npg = connexion.ConnectSante();
            }

            try
            {
                string sql = $"SELECT nextval('{sequenceName}')";
                Console.WriteLine(sql);

                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    nextSerialValue = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw e;
            }
            finally
            {
                if (estOuvert)
                {
                    npg.Close();
                }
            }

            return nextSerialValue;
        }


    }

}
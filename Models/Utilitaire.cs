using Npgsql;
namespace RH.Models
{
    public class Utilitaire{
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
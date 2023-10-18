using Npgsql;
using System;

namespace RH.Models
{
    public class choixCandidat
    {
        private int _idchoix;
        private int _idcandidature;

        public int Idcandidature
        {
            get => _idcandidature;
            set => _idcandidature = value;
        }
        public int Idchoix
        {
            get => _idchoix;
            set => _idchoix = value;
        }

        public void Insert(NpgsqlConnection npg)
        {
            bool estOuvert = false;

            if (npg == null)
            {
                estOuvert = true;
                Connection connexion = new Connection(); // Remplacez par votre logique de connexion
                npg = connexion.ConnectSante();
            }
            
            try
            {
                string sql = "INSERT INTO choixcandidature (idcandidature, idchoix) VALUES (@idcandidature, @idchoix)";
                Console.WriteLine(sql+ " : " + Idcandidature  + "  / " + Idchoix );
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    command.Parameters.AddWithValue("@idcandidature", Idcandidature);
                    command.Parameters.AddWithValue("@idchoix", Idchoix);

                    string sqlWithValues = command.CommandText;
                    foreach (NpgsqlParameter parameter in command.Parameters)
                    {
                        sqlWithValues = sqlWithValues.Replace(parameter.ParameterName, parameter.Value.ToString());
                    }

                    Console.WriteLine(sqlWithValues);

                    int rowsAffected = command.ExecuteNonQuery();


                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Insertion réussie.");
                    }
                    else
                    {
                        Console.WriteLine("Aucune ligne insérée.");
                    }
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
        }
    }
}

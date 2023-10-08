using System;
using Npgsql;
using System.Text;
namespace RH.Models;
public class Fichier
{
    public int idcandidature { get;set; }
    public string ? fichier_diplome { get;set; }
    public string ? fichier_experience { get;set; }

    
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
            string sql = "INSERT INTO fichier ( idcandidature , lienfichierdiplome, lienfichierexperience) " +
                         "VALUES ( @idcandidature , @diplome , @experience)";


            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
            {
                command.Parameters.AddWithValue("@idcandidature", idcandidature);
                command.Parameters.AddWithValue("@diplome", fichier_diplome);
                command.Parameters.AddWithValue("@experience", fichier_experience);

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
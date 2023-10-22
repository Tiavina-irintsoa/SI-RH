namespace RH.Models;
using System;
using System.Collections.Generic;
using Npgsql;
public class Refus
{
    public int IdRefus { get; set; }
    public int IdConge { get; set; }
    public string ? RaisonRefus { get; set; }

    public Service ? service {get;set;}
    public void InsertRefus()
    {
        Connection connexion = new Connection();
        using (NpgsqlConnection connection = connexion.ConnectSante())
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand())
            {
                cmd.Connection = connection;

                cmd.CommandText = "INSERT INTO refus (idconge, raison_refus , idservice) VALUES (@idconge, @raison_refus)";
                cmd.Parameters.AddWithValue("@idconge", IdConge);
                cmd.Parameters.AddWithValue("@raison_refus", RaisonRefus);
                cmd.Parameters.AddWithValue("@raison_refus", service.IdService);
                Console.WriteLine( cmd.CommandText );
                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} ligne(s) insérée(s) avec succès.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de l'insertion : " + ex.Message);
                }
            }
        }
    }
}

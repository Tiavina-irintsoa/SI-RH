namespace RH.Models;

using System;
using System.Collections.Generic;
using Npgsql;

public class TypeContrat
{
    int? _idtypecontrat;
    string? _nomtypecontrat;

    public int? Idtypecontrat { get => _idtypecontrat; set => _idtypecontrat = value; }
    public string? Nomtypecontrat { get => _nomtypecontrat; set => _nomtypecontrat = value; }

    public static TypeContrat[] GetAll(NpgsqlConnection npg)
    {
        TypeContrat[] typeContrats = null;
        bool estOuvert = false;

        if (npg == null)
        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }
        try
        {
            string sql = "SELECT * FROM type_contrat";
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    List<TypeContrat> typeContratList = new List<TypeContrat>();
                    while (reader.Read())
                    {
                        int? idtypecontrat = reader.IsDBNull(0) ? (int?)null : reader.GetInt32(0);
                        string? nomtypecontrat = reader.IsDBNull(1) ? null : reader.GetString(1);

                        TypeContrat typeContrat = new TypeContrat
                        {
                            Idtypecontrat = idtypecontrat,
                            Nomtypecontrat = nomtypecontrat
                        };

                        typeContratList.Add(typeContrat);
                    }
                    typeContrats = typeContratList.ToArray();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Erreur : " + e.ToString());
        }
        finally
        {
            if (estOuvert)
            {
                npg.Close();
            }
        }

        return typeContrats;
    }
}

using System;
using Npgsql;
using System.Text;
namespace RH.Models;
public class Candidature
{
    public int idcandidature { get; set; }
    public int idcandidat { get; set; }
    public DateTime datecandidature { get; set; }
    public int validation { get; set; }
    public string code { get; set; }
    public int idbesoin { get; set; }
    public Fichier fichier { get;set; }

    public Candidat Candidat { get; set; }

    private static Random random = new Random();

    public static string GenerateRandomCode(int id)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        StringBuilder randomString = new StringBuilder(14);

        for (int i = 0; i < 14; i++)
        {
            randomString.Append(chars[random.Next(chars.Length)]);
        }

        string code = randomString.ToString() + id.ToString();
        return code;
    }

    public void setCode( int id ){
        this.code  = GenerateRandomCode( id );
    }

    public static Candidature GetCandidature(NpgsqlConnection npg , int idcandidature ){
        bool estOuvert = false;
        
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }        
        try
        {
            string sql = "SELECT * FROM candidature where idcandidature = @idc ";
            Console.WriteLine(sql);            
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))           
            {

                command.Parameters.AddWithValue("@idc", idcandidature);
                string sqlWithValues = command.CommandText;
                foreach (NpgsqlParameter parameter in command.Parameters)
                {
                  sqlWithValues = sqlWithValues.Replace(parameter.ParameterName, parameter.Value.ToString());
                }
                Console.WriteLine( sqlWithValues );
                
                using (NpgsqlDataReader reader = command.ExecuteReader())                {
                    if (reader.Read())                    
                    {
                        Candidature candidature = new Candidature{
                            idcandidature = idcandidature,
                            idcandidat = Convert.ToInt32(reader["idcandidat"]),
                            validation = Convert.ToInt32(reader["validation"]),
                            code = Convert.ToString( reader["code"] ),
                            idbesoin = Convert.ToInt32(reader["idbesoin"]),
                        };
                        return candidature;
                    }
                }
            }
        }
        catch (Exception e)        {
            Console.WriteLine(e.ToString());
            throw;
        }
        finally        {
            if (estOuvert)
            {
                npg.Close();
            }
        }        
        return null;
    }


    public string getPage(){
        string path = "~/Views/Home/";
        if( validation == 0 )
            return path+"Encours.cshtml";
        if( validation == 1 )
            return path+"Refus.cshtml";
        return path+"Accept.cshtml";
    }

    
    public static Candidature GetByCode(NpgsqlConnection npg , string code ){
        bool estOuvert = false;
        
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }        
        try
        {
            string sql = "SELECT * FROM v_candidat_candidature where code = @code ";
            Console.WriteLine(sql);            
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))           
            {

                command.Parameters.AddWithValue("@code", code.Trim());
                string sqlWithValues = command.CommandText;
                foreach (NpgsqlParameter parameter in command.Parameters)
                {
                  sqlWithValues = sqlWithValues.Replace(parameter.ParameterName, parameter.Value.ToString());
                }
                Console.WriteLine( sqlWithValues );
                Console.WriteLine( " niditra 1" );
                using (NpgsqlDataReader reader = command.ExecuteReader())                {
                    while (reader.Read())                    
                    {
                        Console.WriteLine( " niditra " );
                        int idcandidat = Convert.ToInt32(reader["idcandidat"]);
                        string nomcandidat = reader["nomcandidat"].ToString();
                        string prenomcandidat = reader["prenomcandidat"].ToString();
                        Candidat c = new Candidat{
                            idcandidat = idcandidat,
                            nom = nomcandidat,
                            prenom = prenomcandidat
                        };
                        Candidature candidature = new Candidature{
                            idcandidature = Convert.ToInt32(reader["idcandidature"]),
                            validation = Convert.ToInt32(reader["validation"]),
                            Candidat = c
                        };
                        return candidature;
                    }
                }
            }
        }
        catch (Exception e)        {
            Console.WriteLine(e.ToString());
            throw;
        }
        finally        {
            if (estOuvert)
            {
                npg.Close();
            }
        }        
        return null;
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
            string sql = "INSERT INTO candidature ( idcandidature , idcandidat , datecandidature, code, idbesoin) " +
                         "VALUES ( @idcandidature , @idcandidat , now()::timestamp(0) ,  @code  , @idbesoin)";

            Console.WriteLine( " idbesoin : "+idbesoin );
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
            {
                command.Parameters.AddWithValue("@idcandidature", idcandidature);
                command.Parameters.AddWithValue("@idcandidat", idcandidat);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@idbesoin", idbesoin);

                int rowsAffected = command.ExecuteNonQuery();

                string sqlWithValues = command.CommandText;
                foreach (NpgsqlParameter parameter in command.Parameters)
                {
                    sqlWithValues = sqlWithValues.Replace(parameter.ParameterName, parameter.Value.ToString());
                }

                Console.WriteLine(sqlWithValues);

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Insertion réussie.");
                }
                else
                {
                    Console.WriteLine("Aucune ligne insérée.");
                }
            }
            fichier.Insert(npg);
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

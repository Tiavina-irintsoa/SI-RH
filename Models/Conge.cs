namespace RH.Models;
using System;
using System.Collections.Generic;
using Npgsql;
public class Conge{
    public int IdConge { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
    public DateTime ReelDateFin { get; set; }
    public int Accepte { get; set; }
    public Personnel? Personnel { get; set; } 
    public Raison ? Raison { get; set; }

    public static void UpdateAccepte(int idConge, int accepte)
    {
        Connection connexion = new Connection();
        using (NpgsqlConnection connection = connexion.ConnectSante())
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand())
            {
                cmd.Connection = connection;

                cmd.CommandText = "UPDATE conge SET accepte = @accepte WHERE idconge = @idconge";
                cmd.Parameters.AddWithValue("@accepte", accepte);
                cmd.Parameters.AddWithValue("@idconge", idConge);
                Console.WriteLine( cmd.CommandText );
                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} ligne(s) mise(s) à jour avec succès.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la mise à jour : " + ex.Message);
                }
            }
            connection.Close();
        }
    }

    public static List<Conge> GetCongesSuperieur(NpgsqlConnection npg , int  idservice , int accepte )
    {
        bool estOuvert = false;
        if (npg == null)
        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }
        try
        {
            string sql = "select * from v_conge_service  where idservice = "+idservice+" and accepte  = "+accepte;
            if( idservice == 7 ){
                sql = "select * from v_conge_service  where  accepte  = "+accepte;
            }
            Console.WriteLine(sql);
            List<Conge> conges = new List<Conge>();
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Conge conge = new Conge
                        {
                            IdConge =  Convert.IsDBNull(reader["idconge"]) ? 0 : Convert.ToInt32(reader["idconge"]),
                            DateDebut = Convert.ToDateTime(reader["datedebut"]),
                            DateFin = Convert.ToDateTime(reader["datefin"]),
                            ReelDateFin =  Convert.IsDBNull(reader["reeldatefin"]) ? DateTime.MinValue : Convert.ToDateTime(reader["reeldatefin"]),
                            Accepte = Convert.ToInt32(reader["accepte"]),
                            Personnel = Personnel.GetPersonnelByID( null ,  Convert.ToInt32(reader["idpersonnel"]) )
                            ,
                            Raison = new Raison
                            {
                                idraison = Convert.IsDBNull(reader["idraison"]) ? 0 : Convert.ToInt32(reader["idraison"]),
                            }
                        };
                        
                        conges.Add(conge);
                    }
                }
            }
            return conges;
        }
        catch (Exception e)
        {
            Console.WriteLine( e.StackTrace );
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



    public void InsertConge()
    {
        Connection connexion = new Connection();
        using (NpgsqlConnection connection = connexion.ConnectSante())
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand())
            {
                cmd.Connection = connection;

                cmd.CommandText = "INSERT INTO conge (idpersonnel, datedebut, datefin , reeldatefin , idraison) VALUES (@idpersonnel, @datedebut, @datefin , @datefin , @idraison)";
                if(  Raison.idraison == 0 ){
                    cmd.CommandText = "INSERT INTO conge (idpersonnel, datedebut, datefin , reeldatefin ) VALUES (@idpersonnel, @datedebut, @datefin , @datefin )";
                }

                Console.WriteLine( cmd.CommandText );

                cmd.Parameters.AddWithValue("@idpersonnel", Personnel.idpersonnel);
                cmd.Parameters.AddWithValue("@datedebut", DateDebut);
                cmd.Parameters.AddWithValue("@datefin", DateFin);
                if(  Raison.idraison == 0 ){
                    cmd.Parameters.AddWithValue("@idraison", Raison.idraison); 
                }
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
            connection.Close();
        }
    }

    public static double NbConge( NpgsqlConnection npg , string idpersonnel ){
        bool estOuvert = false;
        if (npg == null)        
        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }
        try
        {
            string sql = " select * from v_nbheure_conge_personnel where idpersonnel =  "+idpersonnel;
            Console.WriteLine( sql );
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))            
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())   
                {
                    while (reader.Read()) {
                        return Convert.ToDouble(reader["nbheure"] );
                    }
                }
            }
        }catch (Exception e )
        {
            throw e;
        }finally    
        {
            if (estOuvert)            
            {
                npg.Close();
            }
        } 
        return 0;
    }

    public static void isValid(  string idpersonnel  , string dt1 , string tm1 , string dt2 , string tm2 ) {
        if (Utilitaire.IsDateGreaterThan15DaysFromToday( dt1 ) == false  )
            throw new Exception( " vous devez envoyer la demande 15 jours avant " );
        
        double nb_heure = Conge.NbConge( null , idpersonnel );
        Console.WriteLine( " huhu :  "+ Utilitaire.CalculateHoursDifference(dt1 , tm1 , dt2 , tm2  ) + " et  " + nb_heure );
        if( Utilitaire.CalculateHoursDifference(dt1 , tm1 , dt2 , tm2  ) > nb_heure  ){
            throw new Exception( " vous n'avez pas assez de conge " );
        }
    }


}



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


    public void InsertConge()
    {
        Connection connexion = new Connection();
        using (NpgsqlConnection connection = connexion.ConnectSante())
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand())
            {
                cmd.Connection = connection;

                cmd.CommandText = "INSERT INTO conge (idpersonnel, datedebut, datefin , idraison) VALUES (@idpersonnel, @datedebut, @datefin , @idraison)";
                if(  Raison.idraison == 0 ){
                    cmd.CommandText = "INSERT INTO conge (idpersonnel, datedebut, datefin ) VALUES (@idpersonnel, @datedebut, @datefin )";
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



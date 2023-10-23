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
    public Refus ? Refus {get;set;}

    public string  autre_raison {get;set;}

    public string getColor(){
        if( Accepte == -1  )
            return "yellow-td";
        if( Accepte == -2 )
            return "red-td";
        if( Accepte == 3 )
            return "green-td";
        return "blue-td";
    }

    public void updateFin()
    {
        Connection connexion = new Connection();
        using (NpgsqlConnection connection = connexion.ConnectSante())
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand())
            {
                cmd.Connection = connection;

                cmd.CommandText = "update conge set  reeldatefin = @datefin where idconge = @idconge";

                cmd.Parameters.AddWithValue("@datefin", ReelDateFin);
                cmd.Parameters.AddWithValue("@idconge", IdConge); 
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
            connection.Close();
        }
    }

    public static List<Conge> getPlanning(NpgsqlConnection npg , int  idservice  )
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
            string sql = " "+
           " select * "+
            "from v_conge_refus as  cs "+
             "   where "+
              "  accepte = 3 and"+
               " idservice in ( select idvisible"+
               " from  planning_visible as pv "+
               " where idservice = "+idservice+" )"+
             "order by datedebut  desc";
            
            Console.WriteLine(sql);
            List<Conge> conges = new List<Conge>();
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine( "yaya : "+ Convert.ToString(reader["autre_raison"] ) );
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
                                nomRaison = Convert.IsDBNull(reader["nomraison"]) ? "Aucune" : Convert.ToString(reader["nomraison"])
                            },
                            autre_raison = Convert.IsDBNull(reader["autre_raison"]) ? "Aucune" : Convert.ToString(reader["autre_raison"]),
                            Refus = new Refus{
                                IdConge = Convert.IsDBNull(reader["idconge"]) ? 0 : Convert.ToInt32(reader["idconge"]) , 
                                RaisonRefus = Convert.IsDBNull(reader["raison_refus"]) ? "Aucune" : Convert.ToString(reader["raison_refus"]),
                                service = new Service{
                                    IdService =  Convert.IsDBNull(reader["superieur"]) ? 0 : Convert.ToInt32(reader["superieur"])
                                }
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

    public static List<Conge> GetCongesSuperieur(NpgsqlConnection npg , int  idservice , int accepte , int idpersonnel = 0 )
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
            string sql = "select * from v_conge_refus  where idservice = "+idservice+" and accepte  = "+accepte;
            if( idservice == 7 ){
                sql = "select * from v_conge_refus  where  accepte  = "+accepte;
            }
            if( idpersonnel != 0 ){
                sql = " select * from v_conge_refus  where idpersonnel = "+idpersonnel;
            }

            sql += " order by datedebut  desc";
            
            Console.WriteLine(sql);
            List<Conge> conges = new List<Conge>();
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine( "yaya : "+ Convert.ToString(reader["autre_raison"] ) );
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
                                nomRaison = Convert.IsDBNull(reader["nomraison"]) ? "Aucune" : Convert.ToString(reader["nomraison"])
                            },
                            autre_raison = Convert.IsDBNull(reader["autre_raison"]) ? "Aucune" : Convert.ToString(reader["autre_raison"]),
                            Refus = new Refus{
                                IdConge = Convert.IsDBNull(reader["idconge"]) ? 0 : Convert.ToInt32(reader["idconge"]) , 
                                RaisonRefus = Convert.IsDBNull(reader["raison_refus"]) ? "Aucune" : Convert.ToString(reader["raison_refus"]),
                                service = new Service{
                                    IdService =  Convert.IsDBNull(reader["superieur"]) ? 0 : Convert.ToInt32(reader["superieur"])
                                }
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

                cmd.CommandText = "INSERT INTO conge (idpersonnel, datedebut, datefin , reeldatefin , idraison ) VALUES (@idpersonnel, @datedebut, @datefin , @datefin , @idraison )";
                if(  Raison.idraison == 0 ){
                    cmd.CommandText = "INSERT INTO conge (idpersonnel, datedebut, datefin , reeldatefin , autre_raison  ) VALUES (@idpersonnel, @datedebut, @datefin , @datefin , @autre_raison )";
                }

                cmd.Parameters.AddWithValue("@idpersonnel", Personnel.idpersonnel);
                cmd.Parameters.AddWithValue("@datedebut", DateDebut);
                cmd.Parameters.AddWithValue("@datefin", DateFin);
                if(  Raison.idraison != 0 ){
                    cmd.Parameters.AddWithValue("@idraison", Raison.idraison); 
                }
                else{
                    cmd.Parameters.AddWithValue("@autre_raison", autre_raison); 
                }
                Console.WriteLine( cmd.CommandText );
                try
                {
                    // int rowsAffected = cmd.ExecuteNonQuery();
                    // Console.WriteLine($"{rowsAffected} ligne(s) insérée(s) avec succès.");
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

    public static void isValid(  string idpersonnel  , string dt1 , string tm1 , string dt2 , string tm2 , string raison ) {
        if (Utilitaire.IsDateGreaterThan15DaysFromToday( dt1 ) == false  ){
            if( raison == "0" )
                throw new Exception( " vous devez envoyer la demande 15 jours avant " );
        }

        if( raison != "0" ) return ;

        double nb_heure = Conge.NbConge( null , idpersonnel );
        Console.WriteLine( " huhu :  "+ Utilitaire.CalculateHoursDifference(dt1 , tm1 , dt2 , tm2  ) + " et  " + nb_heure );
        if( Utilitaire.CalculateHoursDifference(dt1 , tm1 , dt2 , tm2  ) > nb_heure  ){
            throw new Exception( " vous n'avez pas assez de conge " );
        }
    }
}
namespace RH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

public class Candidat{
    int _idcandidat;
    string _nom;
    string _prenom;
    string _mail;
    string _contact;
    DateTime _dtn;
    DateTime _datecandidature;
    string _nomposte;
    public string nomposte {
        get { return _nomposte; }
        set { _nomposte = value; }
    }

   
    public DateTime datecandidature {
        get { return _datecandidature; }
        set { _datecandidature = value; }
    }
    public string prenom {
        get { return _prenom; }
        set { _prenom = value; }
    }
    public string nom {
        get { return _nom; }
        set { _nom = value; }
    }
    public DateTime dtn {
        get { return _dtn; }
        set { _dtn = value; }
    }    
    public int idcandidat {
        get { return _idcandidat; }
        set { _idcandidat = value; }
    }
    public string contact {
        get { return _contact; }
        set { _contact = value; }
    }
    public string mail {
        get { return _mail; }
        set { _mail = value; }
    }

    public Candidat() {}

    public Candidat(string nomCandidat){
        nom = nomCandidat;
    }
    public Candidat(string nomCandidat,string prenomCandidat){
        nom = nomCandidat;
        prenom = prenomCandidat;
    }
    public Candidat(string nomCandidat,string prenomCandidat,string contact){
        nom = nomCandidat;
        prenom = prenomCandidat;
        this.contact = contact;
    }
    

    public static Candidat GetById(NpgsqlConnection npg , int idcand ){
        bool estOuvert = false;
        
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }        
        try
        {
            string sql = "SELECT * FROM candidat where idcandidat = @idcand";
            Console.WriteLine(sql);            
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))           
            {

                command.Parameters.AddWithValue("@idcand", idcand);
                string sqlWithValues = command.CommandText;
                foreach (NpgsqlParameter parameter in command.Parameters)
                {
                    sqlWithValues = sqlWithValues.Replace(parameter.ParameterName, parameter.Value.ToString());
                }
                using (NpgsqlDataReader reader = command.ExecuteReader())                {
                    while (reader.Read())                    
                    {
                        int idcandidat = reader.GetInt32(0);
                        string nomcandidat = reader.GetString(1);
                        string prenomcandidat = reader.GetString(2);
                        DateTime dtn = reader.GetDateTime(3);
                        string mailcandidat = reader.GetString(4);  
                        string contact = reader.GetString(5);  
                        Candidat c = new Candidat{
                            idcandidat = idcandidat,
                            nom = nomcandidat,
                            prenom = prenomcandidat,
                            dtn = dtn,
                            mail = mailcandidat,
                            contact = contact
                        };
                        return c;
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

    public static Candidat GetByName(NpgsqlConnection npg , string nom , string prenom , string mail ){
        bool estOuvert = false;
        
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }        
        try
        {
            string sql = "SELECT * FROM candidat where nomcandidat = @nom and prenomcandidat = @prenom and mail = @mail";
            Console.WriteLine(sql);            
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))           
            {

                command.Parameters.AddWithValue("@nom", nom);
                command.Parameters.AddWithValue("@prenom", prenom);
                command.Parameters.AddWithValue("@mail", mail);
                string sqlWithValues = command.CommandText;
                foreach (NpgsqlParameter parameter in command.Parameters)
                {
                    sqlWithValues = sqlWithValues.Replace(parameter.ParameterName, parameter.Value.ToString());
                }
                using (NpgsqlDataReader reader = command.ExecuteReader())                {
                    while (reader.Read())                    
                    {
                        int idcandidat = reader.GetInt32(0);
                        string nomcandidat = reader.GetString(1);
                        string prenomcandidat = reader.GetString(2);
                        DateTime dtn = reader.GetDateTime(3);
                        string mailcandidat = reader.GetString(4);  
                        Candidat c = new Candidat{
                            idcandidat = idcandidat,
                            nom = nomcandidat,
                            prenom = prenomcandidat,
                            dtn = dtn,
                            mail = mailcandidat
                        };
                        return c;
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

    public void insert( NpgsqlConnection npg)
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
            string sql = "insert into candidat (nomcandidat, prenomcandidat , dtn ,mail, contact)values( @nom , @prenom , @dtn , @mail , @contact )  ";
            if (idcandidat != 0)
            {
                sql = "insert into candidat (idcandidat, nomcandidat, prenomcandidat, dtn, mail, contact) values (@idcandidat, @nom, @prenom, @dtn, @mail, @contact)";
            }
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
            {
               if (idcandidat != 0)
                {
                    command.Parameters.AddWithValue("@idcandidat", idcandidat);
                }
                command.Parameters.AddWithValue("@nom", nom);
                command.Parameters.AddWithValue("@prenom", prenom);
                command.Parameters.AddWithValue("@dtn", dtn);
                command.Parameters.AddWithValue("@mail", mail);
                command.Parameters.AddWithValue("@contact", contact);

                string sqlWithValues = command.CommandText;
                foreach (NpgsqlParameter parameter in command.Parameters)
                {
                    sqlWithValues = sqlWithValues.Replace(parameter.ParameterName, parameter.Value.ToString());
                }

                Console.WriteLine(sqlWithValues);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Mise à jour réussie.");
                }
                else
                {
                    Console.WriteLine("Aucune ligne mise à jour.");
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

    public Candidat(int idcandidat, string nom, string prenom, string mail, string contact, DateTime dtn, DateTime dtm, string nomp){
        this._idcandidat = idcandidat;
        this._nom = nom;
        this._prenom = prenom;
        this._mail = mail;
        this._contact = contact;
        this._dtn = dtn;
        this._datecandidature = dtm;
        this.nomposte = nomp;
    }

    public static Candidat[] GetAll(NpgsqlConnection npg, int idbesoin) {
        Candidat[] candidats = null;
        bool estOuvert = false;
        
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }        
        try        
        {
            string sql = "SELECT * FROM v_candidat_candidature where idbesoin=" + idbesoin;
            Console.WriteLine(sql);         
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))            {
                using (NpgsqlDataReader reader = command.ExecuteReader())                {
                    List<Candidat> candidatList = new List<Candidat>();
                    while (reader.Read())                    {
                        int idcandidat = reader.GetInt32(1);
                        string nom = reader.GetString(2);
                        string prenom = reader.GetString(3);
                        string mail = reader.GetString(5);
                        string contact = reader.GetString(6);
                        DateTime dtn = reader.GetDateTime(4);
                        DateTime datecandidature = reader.GetDateTime(8);
                        string nomposte = reader.GetString(13);
                        
                        Candidat candidat = new Candidat(idcandidat, nom, prenom, mail, contact, dtn, datecandidature, nomposte);
                        candidatList.Add(candidat);
                    }
                    candidats = candidatList.ToArray();
                }
            }
        }
        catch (Exception e)        {
            Console.WriteLine("erreurrrr");
            Console.WriteLine(e.ToString());
        }
        finally        {
            if (estOuvert)            {
                npg.Close();
            }
        }      
        return candidats;
    }

    public static Candidat GetCandidat(NpgsqlConnection npg, int idbesoin, int idcandidat) {
        Candidat candidat = null;
        bool estOuvert = false;
        
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }        
        try        {
            string sql = "SELECT * FROM v_candidat_candidature where idbesoin=" + idbesoin + " and idcandidat=" + idcandidat;
            Console.WriteLine(sql);         
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))            {
                using (NpgsqlDataReader reader = command.ExecuteReader())                {
                    while (reader.Read())                    {
                        int idcand = reader.GetInt32(1);
                        string nom = reader.GetString(2);
                        string prenom = reader.GetString(3);
                        string mail = reader.GetString(5);
                        string contact = reader.GetString(6);
                        DateTime dtn = reader.GetDateTime(4);
                        DateTime datecandidature = reader.GetDateTime(8);
                        string nomposte = reader.GetString(13);
                        
                        candidat = new Candidat(idcand, nom, prenom, mail, contact, dtn, datecandidature, nomposte);
                    }
                }
            }
        }
        catch (Exception e)        {
            Console.WriteLine("erreurrrr");
            Console.WriteLine(e.ToString());
        }
        finally        {
            if (estOuvert)            {
                npg.Close();
            }
        }      
        return candidat;
    }
}
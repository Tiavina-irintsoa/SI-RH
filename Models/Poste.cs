namespace RH.Models;
using System;
using System.Collections.Generic;
using Npgsql;

public class Poste{
    int _idPoste;
    Service _service;
    string _nomPoste;
    public int idPoste {
        get { return _idPoste; }
        set { _idPoste = value; }
    }
    public Service service {
        get { return _service; }
        set { _service = value; }
    }
    public string nomPoste {
        get { return _nomPoste; }
        set { _nomPoste = value; }
    }
    public Poste(string nom){
        nomPoste = nom;
    }

    public static Service getService( NpgsqlConnection npg,  int id ){
        bool estOuvert = false;
        
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }        
        try        {
            string sql = "SELECT * FROM v_service_poste where idposte=" + id;
            Console.WriteLine(sql);         
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))            {
                using (NpgsqlDataReader reader = command.ExecuteReader())                {
                    List<Poste> posteList = new List<Poste>();
                    if (reader.Read())                    {
                        Service service = new Service {
                            IdService = Convert.ToInt32( reader["idservice"] )  ,
                            nomService =  Convert.ToString( reader["nomservice"] )
                        };
                        return service;
                    }
                }
            }
        }
        catch (Exception e)        {
            Console.WriteLine(e.ToString());
            throw e;
        }
        finally        {
            if (estOuvert)            {
                npg.Close();
            }
        }   
        return null;   
    }

    public static Poste[] GetAll(NpgsqlConnection npg, int ids) {
        Poste[] postes = null;
        bool estOuvert = false;
        
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }        
        try        {
            string sql = "SELECT * FROM poste where idservice=" + ids;
            Console.WriteLine(sql);         
            Service service = new(ids);
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))            {
                using (NpgsqlDataReader reader = command.ExecuteReader())                {
                    List<Poste> posteList = new List<Poste>();
                    while (reader.Read())                    {
                        Poste poste = new(reader.GetInt32(0), service, reader.GetString(2));
                        posteList.Add(poste);
                    }
                    postes = posteList.ToArray();
                }
            }
        }
        catch (Exception e)        {
            Console.WriteLine(e.ToString());
            throw e;
        }
        finally        {
            if (estOuvert)            {
                npg.Close();
            }
        }      
        Console.WriteLine(postes);  
        return postes;
    }
   
    public Poste() {}

    public Poste(int id, Service serv, string np) {
        this._idPoste = id;
        this._service = serv;
        this._nomPoste = np;
    }   

    public Poste(Service serv, string np){
        this._service = serv;
        this._nomPoste = np;
    } 
}

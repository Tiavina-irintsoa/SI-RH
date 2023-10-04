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

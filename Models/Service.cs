namespace RH.Models;
using System;
using System.Collections.Generic;
using Npgsql;

public class Service
{
    int _idService;
    string _nomService ;
    string _iconeService;

    public string nomService{
        get { return _nomService; }
        set{ _nomService = value; }
    }

    public Service() {}

    public Service(int id, string nom, string icone) {
        this._idService = id;
        this._nomService = nom;
        this._iconeService = icone;
    } 
    public Service(string nom, string icone) {
        this._nomService = nom;
        this._iconeService = icone;
    } 
    public Service(int id) {
        this._idService = id;
    }  
    
    
    public static Service[] GetAll(NpgsqlConnection npg , int user) {
        Service[] services = null;
        bool estOuvert = false;
        
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }        
        try
        {
            string sql = "SELECT * FROM v_admin_service where idadmin = @idamin ";
            Console.WriteLine(sql);            
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))           
            {

                command.Parameters.AddWithValue("@idamin", user);
                using (NpgsqlDataReader reader = command.ExecuteReader())                {
                    List<Service> serviceList = new List<Service>();
                    while (reader.Read())                    
                    {
                        int  idadmin      = reader.GetInt32(0);
                        int idtypeuser    = reader.GetInt32(1);
                        string description   = reader.GetString(2);
                        string nom           = reader.GetString(3);
                        string mdp           = reader.GetString(4);
                        int idservice     = reader.GetInt32(5);
                        string nomservice    = reader.GetString(6);
                        string iconeservice  = reader.GetString(7);
                        Service service = new Service(idservice, nomservice, iconeservice);
                        serviceList.Add(service);
                    }
                    services = serviceList.ToArray();
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
        return services;
    }
      

    public static Service[] GetAll(NpgsqlConnection npg) {
        Service[] services = null;
        bool estOuvert = false;
        
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }        
        try        {
            string sql = "SELECT * FROM service";
            Console.WriteLine(sql);            
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))            {
                using (NpgsqlDataReader reader = command.ExecuteReader())                {
                    List<Service> serviceList = new List<Service>();
                    while (reader.Read())                    {
                        int id = reader.GetInt32(0);
                        string nom = reader.GetString(1);
                        string icone = reader.GetString(2);
                        Service service = new Service(id, nom, icone);
                        serviceList.Add(service);
                    }
                    services = serviceList.ToArray();
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
        return services;
    }


    public int IdService {
        get { return _idService; }
        set { _idService = value; }
     }

    public string NomService {
        get { return _nomService; }
        set { _nomService = value; }
    }

    public string IconeService {
        get { return _iconeService; }
        set { _iconeService = value; }
    }

    public string NomService1 { get => _nomService; set => _nomService = value; }

}

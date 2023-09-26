namespace RH.Models;
using System;
using System.Collections.Generic;
using Npgsql;

public class Service
{
    private string _nomService ;
    private string _iconeService;

    public Service() {}

    public Service(int id, string nom, string icone) {
        this.IdService = id;
        this._nomService = nom;
        this._iconeService = icone;
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
        }
        finally        {
            if (estOuvert)            {
                npg.Close();
            }
        }        
        return services;
    }


    public int IdService { get; set; }

    public string NomService {
        get { return _nomService; }
        set { _nomService = value; }
    }

    public string IconeService {
        get { return _iconeService; }
        set { _iconeService = value; }
    }
}
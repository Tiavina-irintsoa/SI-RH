namespace RH.Models;
using System;
using System.Collections.Generic;
using Npgsql;

public class Besoin{
    int _idBesoin;
    Poste _poste;
    double _heureSemaine;
    double _heurePersonne;
    double _accompli;

    public int idBesoin {
        get { return _idBesoin; }
        set { _idBesoin = value; }
    }
    public Poste poste {
        get { return _poste; }
        set { _poste = value; }
    }
    public double heureSemaine {
        get { return _heureSemaine; }
        set { _heureSemaine = value; }
    }
    public double heurePersonne {
        get { return _heurePersonne; }
        set { _heurePersonne = value; }
    }
    public double accompli {
        get { return _accompli; }
        set { _accompli = value; }
    }

    public Besoin() {}

    public Besoin(int id, Poste post, double hs, double hp, double ac) {
        this._idBesoin = id;
        this._poste = post;
        this._heureSemaine = hs;
        this._heurePersonne = hp;
        this._accompli = ac;
    }   

    public static Besoin[] GetAll(NpgsqlConnection npg, int ids) {
        Besoin[] besoins = null;
        bool estOuvert = false;
        
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }        
        try        {
            string sql = "SELECT * FROM v_poste_besoin where idservice=" + ids;
            Console.WriteLine(sql);         
            Service service = new Service(ids);
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))            {
                using (NpgsqlDataReader reader = command.ExecuteReader())                {
                    List<Besoin> besoinList = new List<Besoin>();
                    while (reader.Read())                    {
                        int idBesoin = reader.GetInt32(3);
                        Poste poste = new Poste(reader.GetInt32(0), service, reader.GetString(2));
                        double heureSemaine = reader.GetInt32(4);
                        double heurePersonne = reader.GetInt32(5);
                        double accompli = reader.GetInt32(6);
                        Besoin besoin = new Besoin(idBesoin, poste, heureSemaine, heurePersonne, accompli);
                        besoinList.Add(besoin);
                    }
                    besoins = besoinList.ToArray();
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
        return besoins;
    }
}

namespace RH.Models;
using System;
using System.Collections.Generic;
using Npgsql;

public class Besoin{
    int _idBesoin;
    Poste _poste;
    double _heureSemaine;
    double _heurePersonne;
    DateTime? _accompli;
    string _stringaccompli;
    int _nbpersonne;
    
    public int nbpersonne {
        get { return _nbpersonne; }
        set { _nbpersonne = value; }
    }
    public string stringaccompli {
        get { return _stringaccompli; }
        set { _stringaccompli = value; }
    }

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
    public DateTime? accompli {
        get { return _accompli; }
        set { _accompli = value; }
    }

    public Besoin() {}


    public Besoin(int id, Poste post, double hs, double hp, DateTime? ac , int nbp ) {
        this._idBesoin = id;
        this._poste = post;
        this._heureSemaine = hs;
        this._heurePersonne = hp;
        this._accompli = ac;
        this.nbpersonne = nbp;
    }   
    public string StringAccompli(){
        if (accompli == null)
        {
            return "completed";
        }
        return "not-completed";
    }
    public Besoin(int idbesoin, Poste poste){
        _idBesoin = idbesoin;
        this._poste = poste;
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
            Service service = new(ids);
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))            {
                using (NpgsqlDataReader reader = command.ExecuteReader())                {
                    List<Besoin> besoinList = new List<Besoin>();
                    while (reader.Read())                    {
                        int idBesoin = reader.GetInt32(3);
                        Poste poste = new(reader.GetInt32(0), service, reader.GetString(2));
                        double heureSemaine = reader.GetInt32(4);
                        double heurePersonne = reader.GetInt32(5);
                        DateTime? accompli = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6);
                        int nbp = reader.GetInt32(7);
                       
                        Besoin besoin = new Besoin(idBesoin, poste, heureSemaine, heurePersonne, accompli , stringaccompli , nbp );
                        besoinList.Add(besoin);
                        Console.WriteLine("vita");
                    }
                    besoins = besoinList.ToArray();
                        Console.WriteLine("vita1");
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
       Console.WriteLine(besoins.Length); 
        return besoins;
    }
    
    public void Insert(NpgsqlConnection npg) {
    bool estOuvert = false;
    
    if (npg == null) {
        estOuvert = true;
        Connection connexion = new Connection();
        npg = connexion.ConnectSante();
    }        

    try {
        string sql = "INSERT INTO besoin (idposte, heurepersonne, heuresemaine";
        if (this.idBesoin != 0) { // Vérifiez si idbesoin n'est pas égal à la valeur par défaut (0 pour int)
            sql += ", idbesoin";
        }
        sql += ") VALUES (@idPoste, @heurePersonne, @heureSemaine";
        if (this.idBesoin != 0) { // Vérifiez à nouveau ici
            sql += ", @idBesoin";
        }
        sql += ")";
        Console.WriteLine("ajout besoin");
        
        using (NpgsqlCommand command = new NpgsqlCommand(sql, npg)) {
            command.Parameters.AddWithValue("@idPoste", poste.idPoste);
            command.Parameters.AddWithValue("@heurePersonne", heurePersonne);
            command.Parameters.AddWithValue("@heureSemaine", heureSemaine);

            if (this.idBesoin != 0) {
                command.Parameters.AddWithValue("@idBesoin", this.idBesoin);
            }
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0) {
                Console.WriteLine("Insertion réussie.");
            } else {
                Console.WriteLine("Aucune ligne insérée.");
            }
        }
    } catch (Exception e) {
        Console.WriteLine(e.ToString());
    } finally {
        if (estOuvert) {
            npg.Close();
        }
    }      
}


}

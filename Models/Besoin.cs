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
    TypeContrat type_contrat;
    
    public int nbpersonne {
        get { return _nbpersonne; }
        set { _nbpersonne = value; }
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

    public TypeContrat Type_contrat { get => type_contrat; set => type_contrat = value; }


    public Besoin() {}


    public Besoin(int id, Poste post, double hs, double hp, DateTime? ac , int nbp ) {
        this._idBesoin = id;
        this._poste = post;
        this._heureSemaine = hs;
        this._heurePersonne = hp;
        this._accompli = ac;
        this.nbpersonne = nbp;
    }   
    public int IntAccompli(){
        if (accompli == null)
        {
            return 0;
        }
        return 1;
    }
    public string StringAccompli(){
        if (accompli == null)
        {
            return "not-completed";
        }
        return "completed";
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
        try{
            string sql = "SELECT * FROM v_poste_besoin where idservice=" + ids;
            if( ids == 0 ){
                sql = "SELECT * FROM v_poste_besoin";
            }
            Console.WriteLine(sql);
            Service service = new(ids);
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))            {
                using (NpgsqlDataReader reader = command.ExecuteReader())   
                {
                    List<Besoin> besoinList = new List<Besoin>();
                    while (reader.Read()) {
                        int idBesoin = reader.GetInt32(3);
                        Poste poste = new(reader.GetInt32(0), service, reader.GetString(2));
                        double heureSemaine = reader.GetInt32(4);
                        double heurePersonne = reader.GetInt32(5);
                        DateTime? accompli = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6);
                        int nbp = reader.GetInt32(8);
                       
                        Besoin besoin = new Besoin(idBesoin, poste, heureSemaine, heurePersonne, accompli , nbp );
                        besoinList.Add(besoin);
                    }
                    besoins = besoinList.ToArray();
                }
            }
        }
        catch (Exception e)        
        {
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
    
    public static void Update(int idbesoin, NpgsqlConnection npg)
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
            string sql = "UPDATE besoin SET accompli = NOW() WHERE idbesoin = @idbesoin";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
            {
                command.Parameters.AddWithValue("@idbesoin", idbesoin);

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
        }
        finally
        {
            if (estOuvert)
            {
                npg.Close();
            }
        }
    }

    public void Insert(NpgsqlConnection npg) {
    bool estOuvert = false;
    
    if (npg == null) {
        estOuvert = true;
        Connection connexion = new Connection();
        npg = connexion.ConnectSante();
    }        

    try {
        string sql = "INSERT INTO besoin (idposte, heurepersonne, heuresemaine , idtypecontrat ";
        if (this.idBesoin != 0) { // Vérifiez si idbesoin n'est pas égal à la valeur par défaut (0 pour int)
            sql += ", idbesoin";
        }
        sql += ") VALUES (@idPoste, @heurePersonne, @heureSemaine , @idtypecontrat";
        if (this.idBesoin != 0) { // Vérifiez à nouveau ici
            sql += ", @idBesoin";
        }
        sql += ")";
        Console.WriteLine("ajout besoin");
        
        using (NpgsqlCommand command = new NpgsqlCommand(sql, npg)) {
            command.Parameters.AddWithValue("@idPoste", poste.idPoste);
            command.Parameters.AddWithValue("@heurePersonne", heurePersonne);
            command.Parameters.AddWithValue("@heureSemaine", heureSemaine);
            command.Parameters.AddWithValue("@idtypecontrat", this.type_contrat.Idtypecontrat);


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

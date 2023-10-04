namespace RH.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing.Matching;
using Npgsql;

public class FicheCandidat{
    Candidat _candidat;
    Dictionary<string, List<Choix> > _choixCandidat;
    Besoin _besoin;
    double _point;
        
    public Dictionary<string, List<Choix> > choixCandidat {
        get { return _choixCandidat; }
        set { _choixCandidat = value; }
    }
    public double point {
        get { return _point; }
        set { _point = value; }
    }
    public Candidat candidat {
        get { return _candidat; }
        set { _candidat = value; }
    }
    public Besoin besoin {
        get { return _besoin; }
        set { _besoin = value; }
    }

    public FicheCandidat() {}

    public FicheCandidat(Candidat cand, Dictionary<string, List<Choix> > choix, Besoin bes, double pt){
        this._candidat = cand;
        this._choixCandidat = choix;
        this._besoin = bes;
        this._point = pt;
    }

    public static Dictionary<string, List<Choix> > getChoixCandidat(NpgsqlConnection npg, int idbesoin, int idcandidat){
        Dictionary<string, List<Choix> > choix = new Dictionary<string, List<Choix> >();
        bool estOuvert = false;
        
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }        
        try        {
            string sql = "SELECT * FROM v_choix_candidature_type WHERE idbesoin = @idbesoin and idcandidat = @idcandidat";
            Console.WriteLine( " typecritere :  "+sql );
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))            {
                command.Parameters.AddWithValue("@idbesoin", idbesoin);
                command.Parameters.AddWithValue("@idcandidat", idcandidat);

                using (NpgsqlDataReader reader = command.ExecuteReader())                {
                    while (reader.Read())                    {
                        int idchoixindex = 0;
                        int idcanditature  = 1;
                        int idcandidatindex = 2;
                        int datecandidature = 3;
                        int validation = 4;
                        int code = 5;
                        int idbesoinindex = 6;
                        int idtypecritere = 7; 
                        int intitulechoix =8; //homme
                        int valeurchoix = 9;
                        int nomtypecritere = 10; //genre
                        
                        int idCritere = reader.GetInt32(idtypecritere);
                        string intituleTypeCritere = reader.GetString(nomtypecritere);
                        Console.WriteLine( " typecritere :  "+intituleTypeCritere );

                        if (!choix.ContainsKey(intituleTypeCritere))
                        {
                            List<Choix> listeChoix = new List<Choix>();
                            choix[intituleTypeCritere] = listeChoix;
                        }
                        List<Choix> l_choix = choix[intituleTypeCritere];
                        l_choix.Add(new Choix{
                            idChoix = reader.GetInt32(idchoixindex),
                            intitule = reader.GetString(intitulechoix)
                        });
                    }
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
        return choix;
    }

    public static double getPoint(NpgsqlConnection npg, int idbesoin, int idcandidat){
        double point = 0;
        int coefficient = 0;
        bool estOuvert = false;
        
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }        
        try        {
            Dictionary<string, List<Choix> > choix = getChoixCandidat(npg, idbesoin, idcandidat);
            Dictionary<string, Critere> criteres = Critere.GetCritereMapByBesoinId(npg, idbesoin);
            
            foreach (var kvp in choix) {
                string cleCritere = kvp.Key;
                Critere critere = criteres[cleCritere];

                if (Enumerable.SequenceEqual(kvp.Value, critere.listeChoix)) {
                    coefficient = CritereBesoin.getCoefficient(npg, idbesoin, critere.idcritere);
                    point = point + 1 * coefficient;
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
        return point;
    }
   
    public static FicheCandidat[] GetAll(NpgsqlConnection npg, int idbesoin) {
        FicheCandidat[] fiches = null;
        bool estOuvert = false;

        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }
        try        
        {
            Candidat[] candidats = Candidat.GetAll(npg, idbesoin);
            FicheCandidat fiche = null;
            List<FicheCandidat> listeFiche = new List<FicheCandidat>();
            foreach (Candidat candidat in candidats){
                Dictionary< string, List<Choix> > choix = getChoixCandidat(npg, idbesoin, candidat.idcandidat);
                Besoin besoin = new Besoin{
                    idBesoin = idbesoin
                };
                double point = getPoint(npg, idbesoin, candidat.idcandidat);
                fiche = new FicheCandidat(candidat, choix, besoin, point);
                listeFiche.Add(fiche);
            }
            fiches = listeFiche.ToArray();
        }
        catch (Exception e)        {
        Console.WriteLine("erreur");
            Console.WriteLine(e.ToString());
        }
        finally        {
            if (estOuvert)            {
                npg.Close();
            }
        }      
        return fiches;
    }
}
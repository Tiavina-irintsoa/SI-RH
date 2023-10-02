using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RH.Models;
using System.Collections.Generic;
using Npgsql;

namespace RH.Controllers;

public class AjoutCritereController : Controller
{
    public IActionResult save()
    {
        Connection connexion = new Connection();
        NpgsqlConnection npg = connexion.ConnectSante();
        List<CritereBesoin> l_c = new List<CritereBesoin>();
        var idposte = Request.Cookies["idposte"];
        var heuresemaine = Request.Cookies["heuresemaine"];
        var heuremploye = Request.Cookies["heuremploye"];
        Besoin  ajoutBesoin = new Besoin{
            idBesoin = Utilitaire.GetNextSerialValue( npg , "besoin_idbesoin_seq" ),
            poste = new Poste{ idPoste = int.Parse(idposte) },
            heurePersonne = int.Parse(heuremploye),
            heureSemaine = int.Parse(heuresemaine)
        };
        ajoutBesoin.Insert(npg);
        Console.WriteLine(" idposte : "+idposte);
        try{
            List<TypeCritere> liste = TypeCritere.GetAll(npg);
            foreach (var t in liste)
            {   
                CritereBesoin c = new CritereBesoin{
                    idcritere = Utilitaire.GetNextSerialValue( npg , "critere_idcritere_seq" ),
                    Besoin = ajoutBesoin,
                    _coefficient = int.Parse(HttpContext.Request.Form[@t.idTypeCritere+"-coeff"]),
                    typeCritere = new TypeCritere{ idTypeCritere = t.idTypeCritere  }
                };  
                var choix = Request.Form[@t.idTypeCritere+"-choix"];
                List<Choix> lchoix = new();
                foreach( var ch in choix ){
                    Choix tempo_choix = new Choix{
                        idChoix = int.Parse(ch)
                    };
                    lchoix.Add( tempo_choix );
                }
                c.listeChoix = lchoix;
                Console.WriteLine( "value = "+string.Join(", ", c.listeChoix) );
                l_c.Add(c);
            }
            foreach( var critere in l_c  ){
                critere.Insert( npg );
                critere.InsertChoixCritere(npg);
                Console.WriteLine( "new" );
            }
        }
        catch (Exception e)        {
            Console.WriteLine(e.ToString());
        }
        finally        {
            npg.Close();
        }
        return RedirectToAction("service/liste");
    }
}
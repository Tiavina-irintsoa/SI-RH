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
        int id = 1;
        Connection connexion = new Connection();
        NpgsqlConnection npg = connexion.ConnectSante();
        List<CritereBesoin> l_c = new List<CritereBesoin>();
        try{
            List<TypeCritere> liste = TypeCritere.GetAll(npg);
            foreach (var t in liste)
            {   
                CritereBesoin c = new CritereBesoin{
                    idcritere = Utilitaire.GetNextSerialValue( npg , "critere_idcritere_seq" ),
                    Besoin = new Besoin{
                        idBesoin =  id
                    },
                    _coefficient = int.Parse(HttpContext.Request.Form[@t.idTypeCritere+"-coeff"])
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
        }
        catch (Exception e)        {
            Console.WriteLine(e.ToString());
        }
        finally        {
            npg.Close();
        }
        return View("Views/Home/Critere.cshtml",new List<TypeCritere>() );
    }
}
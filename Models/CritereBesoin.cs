namespace RH.Models;
public class CritereBesoin : Critere {
    int _coefficient { get; set; } 
    
    public CritereBesoin(TypeCritere type, List<Choix> choix){
        typeCritere = type; 
        listeChoix = choix;     
    }
}
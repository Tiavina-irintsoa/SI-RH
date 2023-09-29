namespace RH.Models;
public class CritereBesoin : Critere {
    public int _coefficient;

    int Get_coefficient()
    {
        return _coefficient;
    }

    void Set_coefficient(int value)
    {
        _coefficient = value;
    }


    // public CritereBesoin(TypeCritere type, List<Choix> choix){
    //     typeCritere = type; 
    //     listeChoix = choix;     
    // }
}
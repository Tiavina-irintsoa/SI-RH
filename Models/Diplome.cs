namespace RH.Models
{
    public class Diplome : Critere{
        public override string getStringDetails(){
            string result = base.getStringDetails();
            Choix choixAvecPlusPetitId = listeChoix.OrderBy(c => c.idChoix).FirstOrDefault();
            result = choixAvecPlusPetitId.intitule +" ou plus ";
            return result;
        }

    }

}
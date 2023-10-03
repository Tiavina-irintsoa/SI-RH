namespace RH.Models
{
    public  class Experience : Critere{
        public override string getStringDetails(){
            string result = base.getStringDetails();
            Choix choixAvecPlusPetitId = listeChoix.OrderBy(c => c.valeurchoix).FirstOrDefault();
            result = choixAvecPlusPetitId.intitule +" ou + ";
            return result;
        }
        
    }

}
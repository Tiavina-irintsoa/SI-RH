namespace RH.Models
{
    public class Nationalite : Critere{
        public override string getStringDetails(){
            string result = base.getStringDetails();
            result = string.Join(" / ", listeChoix.Select(c => c.intitule));
            return result;
        }
        
    }

}
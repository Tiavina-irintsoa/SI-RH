namespace RH.Models
{
    public class SM : Critere{
        public override string getStringDetails(){
            string result = base.getStringDetails();
            result = string.Join(" ou ", listeChoix.Select(c => c.intitule));
            return result;
        }
    }

}
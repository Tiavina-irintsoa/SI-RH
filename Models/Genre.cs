namespace RH.Models
{
    public class Genre : Critere{
        public override  string getStringDetails(){
            string result = base.getStringDetails();
            
            foreach( var choix in listeChoix ){
            }
            result = string.Join(" / ", listeChoix.Select(c => c.intitule));
            return result;
        }
    }

}
namespace RH.Models
{
    public class Choix{
        int? _idChoix; 
        public int? idChoix {
            get { return _idChoix; }
            set { _idChoix = value; }
        }
        string? _intitule ;
        public string? intitule {
            get { return _intitule; }
            set { _intitule = value; }
        }
    }
}
namespace RH.Models{
    public class TypeCritere{
        int? _idTypeCritere;
        public int? idTypeCritere {
            get { return _idTypeCritere; }
            set { _idTypeCritere = value; }
        }
        string? _nomcritere; 
        public string? nomcritere {
            get { return _nomcritere; }
            set { _nomcritere = value; }
        }
        List<Choix> _listeChoix; 
        public List<Choix> listeChoix {
            get { return _listeChoix; }
            set { _listeChoix = value; }
        }
    }
}
using Npgsql;

namespace RH.Models
{
    public class Critere{
        int? _idcritere; 
        public int? idcritere {
            get { return _idcritere; }
            set { _idcritere = value; }
        }
        Besoin besoin;
        TypeCritere _typeCritere;
        public TypeCritere typeCritere {
            get { return _typeCritere; }
            set { _typeCritere = value; }
        }
        List<Choix> _listeChoix;
        
        public List<Choix> listeChoix {
            get { return _listeChoix; }
            set { _listeChoix = value; }
        }



        public Besoin Besoin { get => besoin; set => besoin = value; }

    }
}
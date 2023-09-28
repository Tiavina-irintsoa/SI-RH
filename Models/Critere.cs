


using Npgsql;

namespace RH.Models
{
    public class Critere{
        int? _idcritere; 
        public int? idcritere {
            get { return _idcritere; }
            set { _idcritere = value; }
        }
        Besoin _besoin; 
        public Besoin besoin {
            get { return _besoin; }
            set { _besoin = value; }
        }
        TypeCritere _typeCritere;
        public TypeCritere typeCritere {
            get { return _typeCritere; }
            set { _typeCritere = value; }
        }
        int _coefficient; 
        List<Choix> _listeChoix;
        public List<Choix> listeChoix {
            get { return _listeChoix; }
            set { _listeChoix = value; }
        }


        

    }
}
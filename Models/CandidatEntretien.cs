namespace RH.Models{
    public class CandidatEntretien {
        public Candidature candidature {get;set;}
        public double points {get;set;}

        public CandidatEntretien(int idcandidature,double points,string nomCandidat,string prenomcandidat){
            this.points = points;
            Console.WriteLine("idcan"+idcandidature);
            this.candidature = new Candidature(idcandidature,nomCandidat,prenomcandidat);
        }
    }
}
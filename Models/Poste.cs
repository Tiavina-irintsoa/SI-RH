namespace RH.Models;

public class Poste{
    int _idPoste;
    Service _service;
    string _nomPoste;
    public int idPoste {
        get { return _idPoste; }
        set { _idPoste = value; }
    }
    public Service service {
        get { return _service; }
        set { _service = value; }
    }
    public string nomPoste {
        get { return _nomPoste; }
        set { _nomPoste = value; }
    }

   
    public Poste() {}

    public Poste(int id, Service serv, string np) {
        this._idPoste = id;
        this._service = serv;
        this._nomPoste = np;
    }   
}

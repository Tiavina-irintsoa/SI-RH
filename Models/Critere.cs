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

        public virtual  string getStringDetails(){
            return "huhu";
        }


    public static Dictionary<string, Critere> InstantiateCritereObjects(Dictionary<string, Critere> data)
    {
        Dictionary<string, Critere> critereDictionary = new Dictionary<string, Critere>();
        foreach (var entry in data)
        {
            string critereType = entry.Key;
            Critere critereData = entry.Value;
            Critere critere = CreateCritereInstance(critereType);
            critere._idcritere = critereData.idcritere;
            critere._listeChoix = critereData.listeChoix;
            critere._typeCritere = critereData._typeCritere;
            critere.besoin = critereData.besoin;
            critereDictionary[critereType] = critere;
            Console.WriteLine( "type : "+critereDictionary[critereType].GetType()+"  "+critereDictionary[critereType].getStringDetails()   );
        }

        return critereDictionary;
    }

    public static Critere CreateCritereInstance(string critereType)
    {
        switch (critereType.ToLower())
        {
            case "genre":
                return new Genre();
            case "diplome":
                return new Diplome();
            case "situation matrimoniale":
                return new SM();
            case "experience":
                return new Experience();
            case "nationalite":
                return new Nationalite();
            default:
                throw new ArgumentException("Type de critère non pris en charge : " + critereType);
        }
    }
    public static Dictionary<string, Critere> GetCritereMapByBesoinId(NpgsqlConnection npg, int besoinId)
    {
        Dictionary<string, Critere> critereMap = new Dictionary<string, Critere>();
        bool estOuvert = false;

        if (npg == null)
        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }

        try
        {
            string sql = "SELECT * FROM v_critere_service WHERE idbesoin = @idbesoin";
            Console.WriteLine( " typecritere :  "+sql );
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
            {
                command.Parameters.AddWithValue("@idbesoin", besoinId);

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idServiceIndex = 0;
                        int nomServiceIndex = 1;
                        int iconservice = 2;
                        int idTypeCritereIndex = 3;
                        int idCritereIndex = 4;
                        int idChoixIndex = 5;
                        int intituleChoixIndex = 6;
                        int valeurChoixIndex = 7;
                        int nomTypeCritereIndex =8;
                        int idBesoinIndex = 9;
                        int idPosteIndex = 10;
                        int nomPosteIndex = 11;
                        int heureSemaineIndex = 12;
                        int heurePersonneIndex = 13;
                        int accompliIndex = 14;
                        int nbPersonneIndex = 15;
                        int coefficientIndex = 16;
                        int idCritere = reader.GetInt32(idCritereIndex);
                        string intituleTypeCritere = reader.GetString(nomTypeCritereIndex);
                        Console.WriteLine( " typecritere :  "+intituleTypeCritere );
                        if (!critereMap.ContainsKey(intituleTypeCritere))
                        {
                            Critere critere = new Critere
                            {
                                idcritere = reader.GetInt32(idCritereIndex),
                                besoin = new Besoin
                                {
                                    idBesoin = reader.GetInt32(idBesoinIndex),
                                    poste = new Poste
                                    {
                                        nomPoste = reader.GetString(nomPosteIndex),
                                        service = new Service
                                            {
                                            nomService = reader.GetString(nomServiceIndex)
                                        }
                                    }
                                },
                                typeCritere = new TypeCritere
                                {
                                    idTypeCritere = reader.GetInt32(idTypeCritereIndex),
                                    nomcritere = reader.GetString(nomTypeCritereIndex),
                                    listeChoix = new List<Choix>()
                                },
                                listeChoix = new List<Choix>()
                            };
                            critereMap[intituleTypeCritere] = critere;
                        }

                        // Ajoutez le choix actuel à la liste des choix du type de critère.
                        Critere critereExist = critereMap[intituleTypeCritere];
                        critereExist.listeChoix.Add(new Choix
                        {
                            idChoix = reader.GetInt32(5),
                            intitule = reader.GetString(6)
                        });
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        finally
        {
            if (estOuvert)
            {
                npg.Close();
            }
        }

        //instanciation
        return InstantiateCritereObjects(critereMap);
    }



    }
}
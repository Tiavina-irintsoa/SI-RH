using Npgsql;
using System;
using System.Collections.Generic;
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

        public static List<TypeCritere> GetAll(NpgsqlConnection npg)
            {
                List<TypeCritere> typeCriteres = new List<TypeCritere>();
                bool estOuvert = false;

                if (npg == null)
                {
                    estOuvert = true;
                    Connection connexion = new Connection();
                    npg = connexion.ConnectSante();
                }

                try
                {
                    string sql = " select * from v_criter_choix ";

                    Console.WriteLine(sql);

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            int currentTypeId = -1;
                            TypeCritere currentTypeCritere = null;

                            while (reader.Read())
                            {
                                int idtypecritere = reader.GetInt32(0);
                                string nomtypecritere = reader.GetString(1);

                                if (idtypecritere != currentTypeId)
                                {
                                    currentTypeId = idtypecritere;
                                    currentTypeCritere = new TypeCritere
                                    {
                                        idTypeCritere = idtypecritere,
                                        nomcritere = nomtypecritere,
                                        listeChoix = new List<Choix>()
                                    };
                                    typeCriteres.Add(currentTypeCritere);
                                }

                                int idchoix = reader.GetInt32(2);
                                string intitulechoix = reader.GetString(3);
                                int valeur = reader.GetInt32( 4 );
                                currentTypeCritere.listeChoix.Add(new Choix
                                {
                                    idChoix = idchoix,
                                    intitule = intitulechoix,
                                    valeurchoix = valeur
                                });
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    throw e;
                }
                finally
                {
                    if (estOuvert)
                    {
                        npg.Close();
                    }
                }

                return typeCriteres;
            }
    
    }
}
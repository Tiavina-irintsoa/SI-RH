namespace RH.Models;
using System;
using System.Collections.Generic;
using Npgsql;

public class Personnel{
    public int idpersonnel { get; set; } 
    public int idposte { get; set; }
    public int idservice { get; set; }
    public string ? nomposte { get; set; }
    public string ? nom { get; set; }
    public string ? prenom { get; set; }
    public string ? mail { get; set; }
    public string ? matricule { get; set; }
    public int nationalite { get; set; }
    public string ? adresse { get; set; }
    public int genre { get; set; }
    public int travailleur { get; set; }
    public string ? contact { get ; set; }
    public DateTime ? dtn { get; set; }
    public int age { get;set; }
    public double latest_salary_brut { get; set; }
    public double latest_salary_net { get; set; }
    public DateTime ? latest_salary_date { get; set; }
    public DateTime ? latest_hire_date { get; set; }

    public static List<Personnel> GetAll( NpgsqlConnection npg, string sql  ){
        bool estOuvert = false;
        List<Personnel> PersonnelList = new List<Personnel>();
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }
        try
        {
            if( sql == null )
                sql = " select * from v_personnel_information";
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))            
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())   
                {

                    while (reader.Read()) {
                        Personnel p = new Personnel{
                            idpersonnel = Convert.ToInt32(reader["idpersonnel"]),
                            idposte = Convert.ToInt32(reader["idposte"]),
                            idservice = Convert.ToInt32(reader["idservice"]),
                            nomposte = reader["nomposte"].ToString(),
                            nom = reader["nom"].ToString(),
                            prenom = reader["prenom"].ToString(),
                            mail = reader["mail"].ToString(),
                            matricule = reader["matricule"].ToString(),
                            nationalite = Convert.ToInt32(reader["nationalite"]),
                            adresse = reader["adresse"].ToString(),
                            genre = Convert.ToInt32(reader["genre"]),
                            travailleur = Convert.ToInt32(reader["travailleur"]),
                            dtn =  Convert.ToDateTime(reader["dtn"]),
                            latest_salary_brut = Convert.ToDouble(reader["latest_salary_brut"]),
                            latest_salary_net = Convert.ToDouble(reader["latest_salary_net"]),
                            latest_salary_date =  Convert.ToDateTime(reader["latest_salary_date"]),
                            latest_hire_date = Convert.ToDateTime(reader["latest_hire_date"]),
                            contact = reader["contact"].ToString(),
                            age = Convert.ToInt32(reader["age"])
                        };
                        PersonnelList.Add( p );
                    }
                }
            }
            return PersonnelList;
        }catch (Exception e )
        {
            throw;
        }finally    
        {
            if (estOuvert)            
            {
                npg.Close();
            }
        }     
    }

    public static string GetSql( string annee, string genre, string min_age, string max_age, string adresse, string nationalite, string matricule, string idservice, string brut_min, string brut_max, string net_min, string net_max , string nom_prenom){
            List<string> where = new();
            if(! string.IsNullOrEmpty( nom_prenom ) ){
                where.Add( " ( upper(nom) like upper('%"+nom_prenom.Trim()+"%') or upper(prenom) like upper('%"+nom_prenom.Trim()+"%')  ) " );
            }
            if( ! string.IsNullOrEmpty(annee) ){
                where.Add( "(travailleur is not null and extract( year from latest_hire_date ) <=  "+annee+")" );
            }if( ! string.IsNullOrEmpty(min_age) ||  ! string.IsNullOrEmpty(max_age) ){
                List<string> agesql = new();
                if( ! string.IsNullOrEmpty(min_age) ){
                    agesql.Add("extract ( year from age( now() , dtn )  ) >= "+min_age);
                } 
                if( ! string.IsNullOrEmpty(max_age) ){
                   agesql.Add("extract ( year from age( now() , dtn )  ) <= "+max_age); 
                }
                string joinedString = string.Join(" and ", agesql);
                string result = "( "+joinedString  +" )"  ;
                where.Add( result );
            } 
            if( int.Parse(idservice) != 0 ){
                where.Add( "( idservice =  "+idservice+")" );
            }
            if( ! string.IsNullOrEmpty(nationalite) ){
                List<string> value_nationalite = Utilitaire.ConvertStringToList( nationalite );
                string sql_nationalite = Utilitaire.GenerateCondition( "nationalite" , value_nationalite );
                where.Add( "("+sql_nationalite+")" );
            }
            if( ! string.IsNullOrEmpty(adresse) ){
                where.Add(" (upper(adresse) like upper('%"+adresse.Trim()+"%')) ");
            }
            if( ! string.IsNullOrEmpty(matricule) ){
                where.Add( "( upper(matricule) like upper('%"+matricule.Trim()+"%') )" ); 
            }
            if( ! string.IsNullOrEmpty(genre) ){
                List<string> value_genre = Utilitaire.ConvertStringToList( genre );
                string sql_nationalite = Utilitaire.GenerateCondition( "genre" , value_genre );
                where.Add( "("+sql_nationalite+")" );
            }
            if ( ! string.IsNullOrEmpty(brut_min) || ! string.IsNullOrEmpty(brut_max)  ){
                List<string> brutsql = new();
                if( ! string.IsNullOrEmpty(brut_min) ){
                    brutsql.Add("latest_salary_brut >= "+brut_min);
                } 
                if( ! string.IsNullOrEmpty(brut_max) ){
                   brutsql.Add("latest_salary_brut <= "+brut_max); 
                }
                string joinedString = string.Join(" and ", brutsql);
                string result = "( "+joinedString  +" )"  ;
                where.Add( result );
            } 
            if ( ! string.IsNullOrEmpty(net_min) || ! string.IsNullOrEmpty(net_max)  ){
                List<string> netsql = new();
                if( ! string.IsNullOrEmpty(net_min) ){
                    netsql.Add("latest_salary_net >= "+net_min);
                } 
                if( ! string.IsNullOrEmpty(net_max) ){
                   netsql.Add("latest_salary_net <= "+net_max); 
                }
                string joinedString = string.Join(" and ", netsql);
                string result = "( "+joinedString  +" )"  ;
                where.Add( result );
            } 

            string sql_result= "select * from v_personnel_information"  ;
            if( where.Count != 0 ){
                string joinedString = string.Join(" and ", where);
                sql_result += " where " + joinedString ;
            // Console.WriteLine( sql_result );
            }
            return sql_result;
    }
}
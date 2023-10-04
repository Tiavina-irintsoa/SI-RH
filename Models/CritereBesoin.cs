using Npgsql;

namespace RH.Models;
public class CritereBesoin : Critere {
    public int _coefficient;

    int Get_coefficient()
    {
        return _coefficient;
    }

    void Set_coefficient(int value)
    {
        _coefficient = value;
    }

    public static int getCoefficient(NpgsqlConnection npg, int idbesoin, int? idcritere){
        int coeff = 0;
        bool estOuvert = false;
        
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }        
        try        {
            string sql = "SELECT coefficient FROM v_critere_service WHERE idbesoin = @idbesoin and idcritere = @idcritere";
            Console.WriteLine( " sql :  "+sql );
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))            {
                command.Parameters.AddWithValue("@idbesoin", idbesoin);
                command.Parameters.AddWithValue("@idcritere", idcritere);

                using (NpgsqlDataReader reader = command.ExecuteReader())                {
                    if (reader.Read())                    {
                        coeff = reader.GetInt32(0);
                    }
                }
            }
        }
        catch (Exception e)        {
            Console.WriteLine(e.ToString());
        }
        finally        {
            if (estOuvert)            {
                npg.Close();
            }
        }    
        return coeff;
    }

    public void InsertChoixCritere(NpgsqlConnection npg) {
        bool estOuvert = false;
        string sql = "";
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }        
        try{
            foreach( var c in this.listeChoix ){
                sql = "INSERT INTO criterechoix (idcritere,idchoix ) VALUES (@idcritere,@idchoix)";
                Console.WriteLine(sql);
                
                using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
                {
                    command.Parameters.AddWithValue("@idcritere", this.idcritere);
                    command.Parameters.AddWithValue("@idchoix", c.idChoix);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Insertion réussie.");
                    }                                
                    else
                    {
                        Console.WriteLine("Aucune ligne insérée.");
                    }
                }
            }
        }
        catch (Exception e)        {
            Console.WriteLine(e.ToString());
        }
        finally        {
            if (estOuvert)            {
                npg.Close();
            }
        }      
    }

    public void Insert(NpgsqlConnection npg) {
        bool estOuvert = false;
        
        if (npg == null)        {
            estOuvert = true;
            Connection connexion = new Connection();
            npg = connexion.ConnectSante();
        }        
        try        {
            string sql = 
            "INSERT INTO critere (idcritere, idbesoin, idtypecritere,coefficient) VALUES (@idcritere, @idbesoin, @idtypecritere, @coefficient)";
            Console.WriteLine(sql);
            using (NpgsqlCommand command = new NpgsqlCommand(sql, npg))
            {
                command.Parameters.AddWithValue("@idcritere", this.idcritere);
                command.Parameters.AddWithValue("@idbesoin", this.Besoin.idBesoin);
                command.Parameters.AddWithValue("@idtypecritere", this.typeCritere.idTypeCritere);
                command.Parameters.AddWithValue("@coefficient", this._coefficient);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Insertion réussie.");
                }
                else
                {
                    Console.WriteLine("Aucune ligne insérée.");
                }
            }
        }
        catch (Exception e)        {
            Console.WriteLine(e.ToString());
        }
        finally        {
            if (estOuvert)            {
                npg.Close();
            }
        }      
    }

    // public CritereBesoin(TypeCritere type, List<Choix> choix){
    //     typeCritere = type; 
    //     listeChoix = choix;     
    // }
}
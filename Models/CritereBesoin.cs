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
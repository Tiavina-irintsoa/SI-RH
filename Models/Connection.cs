namespace RH.Models;
using Npgsql;

public class Connection {    
    public NpgsqlConnection ConnectSante()
    {
        NpgsqlConnection connection = null;
        try
        {
            string connectionString = "Host=localhost;Port=5432;Database=rh;Username=postgres;Password=root";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Connexion à PostgreSQL réussie !");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur de connexion à PostgreSQL : " + ex.Message);
        }
        return connection;
    }

}

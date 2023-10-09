using Npgsql;
using System.Web;
namespace RH.Models
{
    public class Upload{

        public async static void save( IFormFile file  ){
            if (file != null && file.Length > 0)
            {
                string nomFichier = file.FileName;
                long tailleFichier = file.Length;

                Console.WriteLine("Nom du fichier : " + nomFichier);
                Console.WriteLine("Taille du fichier : " + tailleFichier);

                string cheminDeDestination = "wwwroot/upload/" + nomFichier;
                using (var stream = new FileStream(cheminDeDestination, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
        }

    }
}
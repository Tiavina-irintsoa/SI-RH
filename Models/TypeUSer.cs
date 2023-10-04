using Npgsql;

namespace RH.Models
{
    public class TypeUSer{
        int? _idtypeuser;
        string? _description;

        public int? idtypeuser{
            get{ return _idtypeuser; }
            set{  _idtypeuser = value; }
        }

        public string? description{
            get{ return _description; }
            set{  _description = value; }
        }
    }
}
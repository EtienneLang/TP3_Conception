using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Data
{
    public class Abonne
    {
        private ObjectId _id;
        private string _username;
        private DateTime _dateJoined;
        private string _password;
        private string _role;
        private List<ObjectId> _reservations;
        private string[] _categoriesFav;
        private List<ObjectId> _idFilmsOfferts;
        
        public ObjectId Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Le nom d'utilisateur ne peut pas être vide.");
                }

                _username = value;
            }
        }
        
        public string Password
        {
            get { return _password; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Le mot de passe ne peut pas être vide.");
                }
                _password = value;
            }
        }

        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }
        
        public DateTime DateJoined
        {
            get { return _dateJoined; }
            set { _dateJoined = value; }
        }
        
        public List<ObjectId> Reservations
        {
            get { return _reservations; }
            set { _reservations = value; }
        }
        
        public string[] CategoriesFav
        {
            get { return _categoriesFav; }
            set { _categoriesFav = value; }
        }

        public List<ObjectId> IdFilmsOfferts
        {
            get { return _idFilmsOfferts; }
            set { _idFilmsOfferts = value; }
        }
        
        public bool ListFilmOffertContientDejaFilm(ObjectId idFilm)
        {
            return IdFilmsOfferts.Contains(idFilm);
        }
        
        public override string ToString()
        {
            return $"{Username} - Membre depuis {DateJoined.Year}/{DateJoined.Month}/{DateJoined.Day}";
        }
        
    }
}
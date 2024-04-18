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

        public DateTime DateJoined
        {
            get { return _dateJoined; }
            set { _dateJoined = value; }
        }

        public override string ToString()
        {
            return $"{Username} - Membre depuis {DateJoined.Year}/{DateJoined.Month}/{DateJoined.Day}";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Data
{
    public class Film
    {
        private ObjectId _id;
        private string _titre;
        private List<List<string>> _projections;


        public ObjectId Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Titre
        {
            get { return _titre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Le titre ne peut pas être vide.");
                }

                _titre = value;
            }
        }

        public List<List<string>> Projections
        {
            get { return _projections; }
            set { _projections = value; }
        }

        public override string ToString()
        {
            return $"{Titre}";
        }
    }
}
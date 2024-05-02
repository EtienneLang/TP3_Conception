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
        private string _categorie;
        private List<ObjectId> _idRealisateurs;
        private List<ObjectId> _idActeurs;
        

        public ObjectId Id
        {
            get { return _id; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("L'ID ne peut pas être vide.");
                _id = value;
            }
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
        
        public string Categorie
        {
            get { return _categorie; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("La catégorie ne peut pas être vide.");
                _categorie = value;
            }
        }

        public List<ObjectId> IdRealisateurs
        {
            get { return _idRealisateurs; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Le réalisateur ne peut pas être vide.");
                _idRealisateurs = value;
            }
        }
        
        public List<ObjectId> IdActeurs
        {
            get { return _idActeurs; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("L'acteur ne peut pas être vide.");
                _idActeurs = value;
            }
        }
        
        public override string ToString()
        {
            return $"{Titre}";
        }
    }
}
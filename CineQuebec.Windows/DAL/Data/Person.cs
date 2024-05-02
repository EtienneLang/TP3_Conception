using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Data;

public abstract class Person
{
    private ObjectId _id;
    private string _nom;
    
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
    
    public string Nom
    {
        get { return _nom; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("Le nom ne peut pas être vide.");
            }

            _nom = value;
        }
    }
    
    public override string ToString()
    {
        return $"{Nom}";
    }
}
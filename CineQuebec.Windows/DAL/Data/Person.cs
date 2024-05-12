using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Data;

public abstract class Person
{
    private ObjectId _id;
    private string _nom;
    
    public ObjectId Id
    {
        get { return _id; }
        set { _id = value; }
    }
    
    public string Nom
    {
        get { return _nom; }
        set
        {
            _nom = value;
        }
    }
    
    public override string ToString()
    {
        return $"{Nom}";
    }
}
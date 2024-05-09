using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Data;

public class Categorie
{
    private ObjectId _id;
    private string _nomCategorie;

    public ObjectId Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string NomCategorie
    {
        get { return _nomCategorie; }
        set { _nomCategorie = value; }
    }

    public override string ToString()
    {
        return NomCategorie;
    }
}
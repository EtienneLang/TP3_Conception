using System.Windows.Documents;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Data;

public class Preference
{
    private ObjectId _id;
    private ObjectId _userId;
    private List<ObjectId> _realisateurs;
    private List<ObjectId> _acteurs;
    private List<ObjectId> _categories;

    public ObjectId Id
    {
        get { return _id; }
        set { _id = value; }
    }
    
    public ObjectId UserId
    {
        get { return _userId; }
        set { _userId = value; }
    }
    
    public List<ObjectId> Realisateurs
    {
        get { return _realisateurs; }
        set { _realisateurs = value; }
    }
    
    public List<ObjectId> Acteurs
    {
        get { return _acteurs; }
        set { _acteurs = value; }
    }
    
    public List<ObjectId> Categories
    {
        get { return _categories; }
        set { _categories = value; }
    }
}
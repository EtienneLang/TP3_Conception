using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Data;

public class Projection
{
    private ObjectId _id;
    private ObjectId _idFilmProjection;
    private DateTime _dateProjection;

    public ObjectId Id
    {
        get { return _id; }
        set { _id = value; }
    }
    
    public ObjectId IdFilmProjection
    {
        get { return _idFilmProjection; }
        set { _idFilmProjection = value; }
    }
    
    public DateTime DateProjection
    {
        get { return _dateProjection; }
        set { _dateProjection = value; }
    }

    // public override string ToString()
    // {
    //     
    // }
}
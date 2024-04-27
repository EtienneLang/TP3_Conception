using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Data;

public class Projection
{
    private ObjectId _id;
    private ObjectId _idFilmProjection;
    private DateTime _dateProjection;
    private bool _isReprojection;

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
    
    public bool IsReprojection
    {
        get { return _isReprojection; }
        set { _isReprojection = value; }
    }

    public override string ToString()
    {
        return $"{_dateProjection.Day}/{_dateProjection.Month}/{_dateProjection.Year} à {_dateProjection.Hour}h{_dateProjection.Minute:d2}";
    }
}
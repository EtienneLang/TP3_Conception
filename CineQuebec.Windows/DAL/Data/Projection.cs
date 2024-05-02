using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Data;

public class Projection
{
    private ObjectId _id;
    private ObjectId _idFilm;
    private DateTime _date;
    private bool _avantPremiere;

    public ObjectId Id
    {
        get { return _id; }
        set
        {
            if (value == ObjectId.Empty)
                throw new Exception("L'Id de la projection ne peut pas être vide.");
            _id = value;
        }
    }
    
    public ObjectId IdFilm
    {
        get { return _idFilm; }
        set
        {
            if (value == ObjectId.Empty)
                throw new Exception("L'Id du film ne peut pas être vide.");
            _idFilm = value;
        }
    }
    
    public DateTime Date
    {
        get { return _date; }
        set
        {
            if (value < DateTime.Now)
                throw new Exception("La date de projection ne peut pas être antérieure à la date actuelle.");
            if (value == null)
                throw new Exception("La date de projection ne peut pas être vide.");
            _date = value;
        }
    }
    
    public bool AvantPremiere
    {
        get { return _avantPremiere; }
        set
        {
            _avantPremiere = value;
        }
    }
    
    public override string ToString()
    {
        return $"{_date.Day}/{_date.Month}/{_date.Year} à {_date.Hour}h{_date.Minute:d2}";
    }
}
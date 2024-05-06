using MongoDB.Bson;
namespace CineQuebec.Windows.DAL.Data;

public class Note
{
    private ObjectId _id;
    private ObjectId _idFilm;
    private ObjectId _idUser;
    private int _noteSurCinq;
    private string _commentaire;

    public ObjectId Id
    {
        get { return _id;}
        set { _id = value; }
    }

    public ObjectId IdFilm
    {
        get { return _idFilm; }
        set { _idFilm = value; }
    }

    public ObjectId IdUser
    {
        get { return _idUser;}
        set { _idUser = value; }
    }

    public int NoteSurCinq
    {
        get { return _noteSurCinq; }
        set { _noteSurCinq = value; }
    }

    public string Commentaire
    {
        get { return _commentaire; }
        set { _commentaire = value; }
    }
}
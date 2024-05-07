using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories;

public class NoteRepository : ModelRepository, INoteRepository
{
    private IMongoCollection<Note> _collection;
    public NoteRepository()
    {
        _collection = _database.GetCollection<Note>("Notes");
    }
    public List<Note> ReadNotes()
    {
        try
        {
            return _collection.Find(abonne => true).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Note? ReadNoteByUserOnFilm(ObjectId userId, ObjectId filmId)
    {
        try
        {
            var filter = Builders<Note>.Filter.Eq(note => note.IdUser, userId) &
                         Builders<Note>.Filter.Eq(note => note.IdFilm, filmId);
            return _collection.Find(filter).FirstOrDefault();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Note ReadNoteById(ObjectId idNote)
    {
        try
        {
            var filter = Builders<Note>.Filter.Eq(film => film.Id, idNote);
            return _collection.Find(filter).FirstOrDefault();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public void CreateNote(Note note)
    {
        try
        {
            _collection.InsertOne(note);
        }
        catch (ArgumentNullException ex)
        {
            throw new ArgumentNullException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
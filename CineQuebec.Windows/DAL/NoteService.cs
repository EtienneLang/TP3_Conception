using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL;

public class NoteService : IDatabaseNote
{
    private readonly IMongoClient _mongoDBClient;
    private readonly IMongoDatabase _database;

    public NoteService()
    {
        _mongoDBClient = GetClient();
        _database = GetDatabase(_mongoDBClient);
    }


    public IMongoDatabase GetDatabase(IMongoClient client)
    {
        return client.GetDatabase("TP2DB");
    }

    public IMongoClient GetClient()
    {
        return new MongoClient("mongodb://localhost:27017/");
    }
    
    virtual public void CreateNote(Note note)
    {
        try
        {
            var collection = _database.GetCollection<Note>("Notes");
            collection.InsertOne(note);
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
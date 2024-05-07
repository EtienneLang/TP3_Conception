using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories;

public class ModelRepository
{
    private IMongoClient _mongoDBClient;
    protected IMongoDatabase _database;

    public ModelRepository(IMongoClient client = null)
    {
        if (client == null)
        {
            _mongoDBClient = GetClient();
        }
        else
        {
            _mongoDBClient = client;
        }
        _database = GetDatabase();
    }
    
    public IMongoDatabase GetDatabase()
    {
        try
        {
            IMongoDatabase database = _mongoDBClient.GetDatabase("TP2DB");
            return database;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public IMongoClient GetClient()
    {
        try
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017/");
            return client;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
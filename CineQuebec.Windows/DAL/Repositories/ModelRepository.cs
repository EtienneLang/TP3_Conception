using MongoDB.Driver;

namespace CineQuebec.Windows.DAL;

public class ModelRepository
{
    private IMongoClient _mongoDBClient;
    protected IMongoDatabase _database;

    public ModelRepository()
    {
        _mongoDBClient = GetClient();
        _database = GetDatabase();
    }
    
    public IMongoDatabase GetDatabase()
    {
        
        try
        {
            IMongoDatabase _databaseIni;
            _databaseIni = _mongoDBClient.GetDatabase("TP2DB");
            return _databaseIni;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public IMongoClient GetClient()
    {
        MongoClient client;
        try
        {
            client = new MongoClient("mongodb://localhost:27017/");
            return client;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
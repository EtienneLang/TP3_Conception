using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories;

public class AuthRepository : ModelRepository, IAuthRepository
{
    private IMongoCollection<Abonne> _collection;

    public AuthRepository()
    {
        _collection = _database.GetCollection<Abonne>("Abonnes");
    }
    public bool AbonneExiste(string username)
    {
        try
        {
            var filter = Builders<Abonne>.Filter.Eq(abonne => abonne.Username, username);
            var count = _collection.CountDocuments(filter);
            return count > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Abonne Login(string username, string password)
    {
        try
        {
            var filter = Builders<Abonne>.Filter.And(
                Builders<Abonne>.Filter.Eq(abonne => abonne.Username, username),
                Builders<Abonne>.Filter.Eq(abonne => abonne.Password, password)
            );
            return _collection.Find(filter).FirstOrDefault();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories;

public class AbonneRepository : ModelRepository, IAbonneRepository
{
    private readonly IMongoCollection<Abonne> _abonnes;

    public AbonneRepository(IMongoDatabase database)
    {
        _abonnes = database.GetCollection<Abonne>("Abonnes");
    }

    public List<Abonne> ReadAbonnes()
    {
        return _abonnes.Find(abonne => true).ToList();
    }

    public Abonne GetAbonneByUsername(string username)
    {
        return _abonnes.Find(abonne => abonne.Username == username).FirstOrDefault();
    }
    
    public void OffrirBillet(ObjectId idAbonne, ObjectId idFilm)
    {
        try
        {
            var collection = _database.GetCollection<Abonne>("Abonnes");
            var filter = Builders<Abonne>.Filter.Eq("_id", idAbonne);
            var update = Builders<Abonne>.Update.Push("IdFilmsOfferts", idFilm);
            collection.UpdateOne(filter, update);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
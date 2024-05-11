using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories;

public class RealisateurRepository : ModelRepository , IRealisateurRepository
{
    private IMongoCollection<Realisateur> _collection;

    public RealisateurRepository()
    {
        _collection = _database.GetCollection<Realisateur>("Realisateurs");
    }
    
    public List<Realisateur> ReadRealisateurs()
    {
        try
        {
            return _collection.Find(_ => true).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Realisateur ReadRealisateurFromId(ObjectId realisateurId)
    {
        try
        {
            return _collection.Find(r => r.Id == realisateurId).FirstOrDefault();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void CreateRealisateur(Realisateur realisateur)
    {
        try
        {
            _collection.InsertOne(realisateur);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateRealisateur(Realisateur realisateur)
    {
        try
        {
            var filter = Builders<Realisateur>.Filter.Eq(r => r.Id, realisateur.Id);
            var update = Builders<Realisateur>.Update
                .Set(r => r.Nom, realisateur.Nom);
            _collection.UpdateOne(filter, update);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteRealisateur(ObjectId realisateurId)
    {
        try
        {
            var filter = Builders<Realisateur>.Filter.Eq(r => r.Id, realisateurId);
            _collection.DeleteOne(filter);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
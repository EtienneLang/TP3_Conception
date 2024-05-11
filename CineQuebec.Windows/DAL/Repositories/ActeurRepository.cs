using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories;

public class ActeurRepository : ModelRepository, IActeurRepository
{
    private IMongoCollection<Acteur> _collection;

    public ActeurRepository()
    {
        _collection = _database.GetCollection<Acteur>("Acteurs");
    }
    
    public List<Acteur> ReadActeurs()
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

    public Acteur ReadActeurFromId(ObjectId acteurId)
    {
        try
        {
            return _collection.Find(a => a.Id == acteurId).FirstOrDefault();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void CreateActeur(Acteur acteur)
    {
        try
        {
            _collection.InsertOne(acteur);
        }
        catch (Exception e)
        {
            // Log or handle the exception
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateActeur(Acteur newActeur)
    {
        try
        {
            var filter = Builders<Acteur>.Filter.Eq(a => a.Id, newActeur.Id);
            var update = Builders<Acteur>.Update
                .Set(a => a.Nom, newActeur.Nom);
            _collection.UpdateOne(filter, update);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteActeur(ObjectId acteurId)
    {
        try
        {
            var filter = Builders<Acteur>.Filter.Eq(a => a.Id, acteurId);
            _collection.DeleteOne(filter);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
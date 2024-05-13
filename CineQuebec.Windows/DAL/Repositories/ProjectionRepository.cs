using System.Windows;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories;

public class ProjectionRepository : ModelRepository, IProjectionRepository
{
    private IMongoCollection<Projection> _collection;
    public ProjectionRepository()
    {
        _collection = _database.GetCollection<Projection>("Projections");
    }
    
    public List<Projection> ReadProjections()
    {
        var projections = new List<Projection>();
        try
        {
            projections = _collection.Find(new BsonDocument()).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return projections;
    }
    
    public void ReserverPlace(Projection projection, ObjectId idAbonne)
    {
        try
        {
            var collection = _database.GetCollection<Abonne>("Abonnes");
            var filter = Builders<Abonne>.Filter.Eq("Id", idAbonne);
            var abonne = collection.Find(filter).FirstOrDefault();
            var update = Builders<Abonne>.Update.Push("Reservations", projection.Id);
            collection.UpdateOne(filter, update);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void CreateProjection(Projection projection)
    {
        try
        {
            _collection.InsertOne(projection);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<Projection> ReadProjectionByFilmId(ObjectId idFilm)
    {
        try
        {
            var filter = Builders<Projection>.Filter.Eq("IdFilmProjection", idFilm);
            return _collection.Find(filter).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Projection GetProjectionById(ObjectId id)
    {
        try
        {
            var filter = Builders<Projection>.Filter.Eq("Id", id);
            Projection projection = _collection.Find(filter).FirstOrDefault();
            return projection;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

    public List<Projection> ReadAvantPremieres()
    {
        try
        {

            var filter = Builders<Projection>.Filter.Eq("AvantPremiere", true);
            return _collection.Find(filter).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
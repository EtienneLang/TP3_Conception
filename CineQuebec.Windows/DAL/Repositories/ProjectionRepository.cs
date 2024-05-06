using System.Windows;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories;

public class ProjectionRepository : ModelRepository, IProjectionRepository
{
    public List<Projection> ReadProjections()
    {
        var projections = new List<Projection>();
        try
        {
            var collection = _database.GetCollection<Projection>("Projections");
            projections = collection.Find(new BsonDocument()).ToList();
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
            //A CHANGER, ON DOIT METTRE LA LOGIQUE DANS LE SERVICE
            var collection = _database.GetCollection<Abonne>("Abonnes");
            var filter = Builders<Abonne>.Filter.Eq("Id", idAbonne);
            var abonne = collection.Find(filter).FirstOrDefault();
            // if (abonne.Reservations != null && abonne.Reservations.Contains(projection.Id))
            // {
            //     MessageBox.Show("Vous avez déjà réservé votre place pour cette projection", "Erreur",
            //         MessageBoxButton.OK, MessageBoxImage.Error);
            // }
            // else
            // {
                var update = Builders<Abonne>.Update.Push("Reservations", projection.Id);
                collection.UpdateOne(filter, update);
            // }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Projection GetProjectionById(ObjectId id)
    {
        try
        {
            var collection = _database.GetCollection<Projection>("Projections");
            var filter = Builders<Projection>.Filter.Eq("Id", id);
            Projection projection = collection.Find(filter).FirstOrDefault();
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
        var projections = new List<Projection>();
        try
        {
            var collection = _database.GetCollection<Projection>("Projections");
            var filter = Builders<Projection>.Filter.Eq("AvantPremiere", true);
            projections = collection.Find(filter).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return projections;
    }
}
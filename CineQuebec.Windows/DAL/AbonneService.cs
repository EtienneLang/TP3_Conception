using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;


namespace CineQuebec.Windows.DAL;

public class AbonneService : IDatabaseAbonnes
{
    private readonly IMongoClient _mongoDBClient;
    private readonly IMongoDatabase _database;
    
    public AbonneService()
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
    
    public Abonne GetAbonneByUsername(string username)
    {
        var collection = _database.GetCollection<Abonne>("Abonnes");
        var filter = Builders<Abonne>.Filter.Eq("Username", username);
        Abonne abonne = collection.Find(filter).FirstOrDefault();
        return abonne;
    }

    public List<Abonne> ReadAbonnes()
    {
        var abonnes = new List<Abonne>();
        try
        {
            var collection = _database.GetCollection<Abonne>("Abonnes");
            abonnes = collection.Aggregate().ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
        }

        var realisateurs = new Realisateur();
        return abonnes;
    }

    public void OffrirBillet(ObjectId id, ObjectId objectId)
    {
        try
        {
            var collection = _database.GetCollection<Abonne>("Abonnes");
            var filter = Builders<Abonne>.Filter.Eq("_id", id);
            var update = Builders<Abonne>.Update.Push("IdFilmsOfferts", objectId);
            collection.UpdateOne(filter, update);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
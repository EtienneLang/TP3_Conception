using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL
{
    public class ProjectionService : IDatabaseProjection
    {
        private readonly IMongoClient _mongoDBClient;
        private readonly IMongoDatabase _database;

        public ProjectionService()
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

        public void ReserverPlace(Projection projection, ObjectId idAbonne)
        {
            try
            {
                var collection = _database.GetCollection<Projection>("Abonnes");
                var filter = Builders<Projection>.Filter.Eq("Id", idAbonne);
                var update = Builders<Projection>.Update.Push("Reservations", projection.Id);
                collection.UpdateOne(filter, update);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public Projection GetProjectionById(ObjectId id)
        {
            var collection = _database.GetCollection<Projection>("Projections");
            var filter = Builders<Projection>.Filter.Eq("Id", id);
            Projection projection = collection.Find(filter).FirstOrDefault();
            return projection;
        }
        
    }
}
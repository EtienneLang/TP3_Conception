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



        virtual public List<List<string>> GetAllProjections()
        {
            var projections = new List<List<string>>();
            try
            {
                var collection = _database.GetCollection<Film>("Films");
                var films = collection.Aggregate().ToList();
                foreach (var film in films)
                {
                    projections.AddRange(film.Projections);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return projections;
        }
    }
}
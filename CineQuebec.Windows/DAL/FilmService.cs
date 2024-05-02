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
    public class FilmService : IDatabaseFilms
    {
        private readonly IMongoClient _mongoDBClient;
        private readonly IMongoDatabase _database;

        public FilmService()
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

        virtual public List<Film> ReadFilms()
        {
            var films = new List<Film>();
            try
            {
                var collection = _database.GetCollection<Film>("Films");
                films = collection.Aggregate().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return films;
        }

        virtual public void CreateFilm(Film film)
        {
            try
            {
                var collection = _database.GetCollection<Film>("Films");
                collection.InsertOne(film);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        virtual public void UpdateFilm(Film film)
        {
            try
            {
                var collection = _database.GetCollection<Film>("Films");
                var filter = Builders<Film>.Filter.Eq("Id", film.Id);
                var update = Builders<Film>.Update.Set("Projections", film.Projections);
                collection.UpdateOne(filter, update);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        virtual public void DeleteFilmById(ObjectId id)
        {
            try
            {
                var collection = _database.GetCollection<Film>("Films");
                var filter = Builders<Film>.Filter.Eq("Id", id);
                collection.FindOneAndDelete(filter);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        virtual public List<Projection> GetProjectionsOfFilm(Film film)
        {
            var projections = new List<Projection>();
            try
            {
                var collectionProjections = _database.GetCollection<Projection>("Projections");
                foreach (var projection in collectionProjections.Aggregate().ToList())
                {
                    if (projection.IdFilm == film.Id)
                    {
                        projections.Add(projection);
                    }
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
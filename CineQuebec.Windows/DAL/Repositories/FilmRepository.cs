using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories;

public class FilmRepository : ModelRepository, IFilmRepository
{
    public List<Film> ReadFilms()
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
    
    public void CreateFilm(Film film)
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
    public void UpdateFilm(Film film)
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
    public void DeleteFilmById(ObjectId id)
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

    public Film ReadFilmById(ObjectId idFilm)
    {
        try
        {
            var collection = _database.GetCollection<Film>("Films");
            var filter = Builders<Film>.Filter.Eq(film => film.Id, idFilm);
            return collection.Find(filter).FirstOrDefault();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<Film> ReadFilmFromIdList(List<ObjectId> idFilms)
    {
        throw new NotImplementedException();
    }
}
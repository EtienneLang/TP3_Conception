using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories;

public class FilmRepository : ModelRepository, IFilmRepository
{
    private IMongoCollection<Film> _collection;
    public FilmRepository()
    {
        _collection = _database.GetCollection<Film>("Films");
    }
    public List<Film> ReadFilms()
    {
        var films = new List<Film>();
        try
        {
            films = _collection.Aggregate().ToList();
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
            _collection.InsertOne(film);
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
            var filter = Builders<Film>.Filter.Eq("Id", film.Id);
            var update = Builders<Film>.Update.Set("Projections", film.Projections);
            _collection.UpdateOne(filter, update);
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
            var filter = Builders<Film>.Filter.Eq("Id", id);
            _collection.FindOneAndDelete(filter);
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
            var filter = Builders<Film>.Filter.Eq(film => film.Id, idFilm);
            return _collection.Find(filter).FirstOrDefault();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<Film> ReadFilmFromIdList(List<ObjectId> idFilms)
    {
        try
        {
            var filter = Builders<Film>.Filter.In(film => film.Id, idFilms);
            return _collection.Find(filter).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
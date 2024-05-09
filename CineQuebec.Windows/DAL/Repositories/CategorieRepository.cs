using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories;

public class CategorieRepository : ModelRepository, ICategorieRepository
{
    private IMongoCollection<Categorie> _collection;

    public CategorieRepository()
    {
        _collection = _database.GetCollection<Categorie>("Categories");
    }
    
    public List<Categorie> ReadCategories()
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

    public Categorie ReadCategorieFromId(ObjectId categorieId)
    {
        try
        {
            return _collection.Find(c => c.Id == categorieId).FirstOrDefault();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void CreateCategorie(Categorie categorie)
    {
        try
        {
            _collection.InsertOne(categorie);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateCategorie(Categorie newCategorie)
    {
        try
        {
            var filter = Builders<Categorie>.Filter.Eq(c => c.Id, newCategorie.Id);
            var update = Builders<Categorie>.Update
                .Set(c => c.NomCategorie, newCategorie.NomCategorie);
            _collection.UpdateOne(filter, update);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteCategorie(ObjectId categorieId)
    {
        try
        {
            var filter = Builders<Categorie>.Filter.Eq(c => c.Id, categorieId);
            _collection.DeleteOne(filter);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
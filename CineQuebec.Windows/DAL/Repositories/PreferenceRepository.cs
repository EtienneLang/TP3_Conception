using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories;

public class PreferenceRepository : ModelRepository, IPreferencesRepository
{
    private IMongoCollection<Preference> _collection;

    public PreferenceRepository()
    {
        _collection = _database.GetCollection<Preference>("Preferences");
    }
    public List<Preference> ReadPreferences()
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

    public Preference ReadPreferenceFromUserId(ObjectId userId)
    {
        try
        {
            return _collection.Find(p => p.UserId == userId).FirstOrDefault();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void CreatePreference(Preference preference)
    {
        try
        {
            _collection.InsertOne(preference);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeletePreference(ObjectId preferenceId)
    {
        try
        {
            var filter = Builders<Preference>.Filter.Eq(p => p.Id, preferenceId);
            _collection.DeleteOne(filter);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdatePreference(Preference newPreference)
    {
        try
        {
            var filter = Builders<Preference>.Filter.Eq(p => p.Id, newPreference.Id);
            var update = Builders<Preference>.Update
                .Set(p => p.Realisateurs, newPreference.Realisateurs)
                .Set(p => p.Acteurs, newPreference.Acteurs)
                .Set(p => p.Categories, newPreference.Categories);
            _collection.UpdateOne(filter, update);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
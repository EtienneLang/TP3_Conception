using System.Windows.Documents;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Interfaces;

public interface IPreferencesRepository
{
    List<Preference> ReadPreferences();
    Preference ReadPreferenceFromUserId(ObjectId userId);
    void CreatePreference(Preference preference);
    void DeletePreference(ObjectId preferenceId);
    void UpdatePreference(Preference newPreference);
}
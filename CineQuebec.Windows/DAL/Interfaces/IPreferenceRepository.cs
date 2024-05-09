using System.Windows.Documents;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Interfaces;

public interface IPreferenceRepository
{
    List<Preference> ReadPreferences();
    Preference ReadPreferenceFromId(ObjectId preferenceId);
    Preference ReadPreferenceFromUserId(ObjectId userId);
    void CreatePreference(Preference preference);
    void DeletePreference(ObjectId preferenceId);
    void UpdatePreference(Preference newPreference);
}
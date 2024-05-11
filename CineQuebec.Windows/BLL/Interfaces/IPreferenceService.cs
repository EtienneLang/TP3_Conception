using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Interfaces;

public interface IPreferenceService
{
    List<Preference> ReadPreference();
    Preference ReadPreferenceFromId(ObjectId preferenceId);
    Preference ReadPreferenceFromUserId(ObjectId userId);
    void CreatePreference(Preference preference);
    void UpdatePreference(Preference newPreference);
    void DeletePreference(ObjectId preferenceId);
}
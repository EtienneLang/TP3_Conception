using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Interfaces;

public interface IActeurRepository
{
    List<Acteur> ReadActeurs();
    Acteur ReadActeurFromId(ObjectId acteurId);
    void CreateActeur(Acteur acteur);
    void UpdateActeur(Acteur newActeur);
    void DeleteActeur(ObjectId acteurId);
}
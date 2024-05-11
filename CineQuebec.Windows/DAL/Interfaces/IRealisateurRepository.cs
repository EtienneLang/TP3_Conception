using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Interfaces;

public interface IRealisateurRepository
{
    List<Realisateur> ReadRealisateurs();
    Realisateur ReadRealisateurFromId(ObjectId realisateurId);
    void CreateRealisateur(Realisateur realisateur);
    void UpdateRealisateur(Realisateur realisateur);
    void DeleteRealisateur(ObjectId realisateurId);
}
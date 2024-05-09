using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Interfaces;

public interface IRealisateurService
{
    List<Realisateur> ReadRealisateurs();
    Realisateur ReadRealisateurFromId(ObjectId realisateurId);
    void CreateRealisateur(Realisateur realisateur);
    void UpdateRealisateur(Realisateur realisateur);
    void DeleteRealisateur(ObjectId realisateurId);
}
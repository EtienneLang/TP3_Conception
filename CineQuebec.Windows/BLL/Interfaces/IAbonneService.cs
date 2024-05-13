using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Interfaces;

public interface IAbonneService
{
    List<Abonne> ReadAbonnes();
    Abonne GetAbonneByUsername(string username);
    void CreateAbonne(Abonne abonne);
    void OffrirBillet(ObjectId idAbonne, ObjectId idFilm);
    List<Abonne> ReadAbonnesInterestedInCategorie(ObjectId categorieId);
    List<Abonne> ReadAbonnesInterestedInActeurAndCategorie(List<ObjectId> acteursIds, List<ObjectId> realisateursIds);
}
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Interfaces;

public interface IAbonneService
{
    List<Abonne> ReadAbonnes();
    Abonne GetAbonneByUsername(string username);
    void CreateAbonne(Abonne abonne);
    void OffrirBillet(ObjectId idAbonne, ObjectId idFilm);
}
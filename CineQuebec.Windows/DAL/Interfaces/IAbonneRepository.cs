using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.InterfacesForRepositories;

public interface IAbonneRepository
{
    List<Abonne>  ReadAbonnes();
    Abonne GetAbonneByUsername(string username);
    void OffrirBillet(ObjectId idAbonne, ObjectId idFilm);
}
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Interfaces;

public interface IAbonneRepository
{
    void CreateAbonne(Abonne abonne);
    List<Abonne>  ReadAbonnes();
    Abonne GetAbonneByUsername(string username);
    void OffrirBillet(ObjectId idAbonne, ObjectId idFilm);
    Abonne ReadAbonneById(ObjectId idAbonne);
}
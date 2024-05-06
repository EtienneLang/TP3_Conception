using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.InterfacesForRepositories;

public interface IProjectionRepository
{
    void ReserverPlace(Projection projection, ObjectId idAbonne);   
    Projection GetProjectionById(ObjectId id);
    List<Projection> ReadAvantPremieres();
    List<Projection> ReadProjections();
}
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Interfaces;

public interface IProjectionService
{
    void ReserverPlace(Projection projection, ObjectId idAbonne);
    Projection GetProjectionById(ObjectId id);
    List<Projection> ReadAvantPremieres();
}
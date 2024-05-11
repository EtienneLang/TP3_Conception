using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Interfaces;

public interface IProjectionService
{
    void ReserverPlace(Projection projection, ObjectId idAbonne);
    void CreateProjection(Projection projection);
    List<Projection> ReadProjectionByFilmId(ObjectId idFilm);
    Projection GetProjectionById(ObjectId id);
    List<Projection> ReadAvantPremieres();
}
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.InterfacesForRepositories;

public interface IFilmRepository
{
     List<Film> ReadFilms();
     void CreateFilm(Film film);
     void UpdateFilm(Film film);
     void DeleteFilmById(ObjectId film);
     Film ReadFilmById(ObjectId idFilm);
     List<Film> ReadFilmFromIdList(List<ObjectId> idFilms);

}
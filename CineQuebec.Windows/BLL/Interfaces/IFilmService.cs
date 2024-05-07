using System.Windows.Documents;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Interfaces;

public interface IFilmService
{
    List<Film> ReadFilms();
    void CreateFilm(Film film);
    void UpdateFilm(Film film);
    void DeleteFilmById(ObjectId film);
    Film ReadFilmById(ObjectId idFilm);
    List<Projection> GetProjectionsOfFilm(Film film);
    List<Film> GetFilmsToRate(ObjectId userId);
}
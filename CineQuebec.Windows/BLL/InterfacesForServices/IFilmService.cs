using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Interfaces;

public interface IFilmService
{
    List<Film> ReadFilms();
    void CreateFilm(Film film);
    void UpdateFilm(Film film);
    void DeleteFilmById(ObjectId film);
    List<Projection> GetProjectionsOfFilm(Film film);
}
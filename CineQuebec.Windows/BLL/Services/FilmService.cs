using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepo;
        private readonly IProjectionRepository _projectionRepo;

        public FilmService(IFilmRepository filmRepo, IProjectionRepository projectionRepo)
        {
            _filmRepo = filmRepo;
            _projectionRepo = projectionRepo;
        }
        

        public List<Film> ReadFilms()
        {
            try
            {
                return _filmRepo.ReadFilms();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateFilm(Film film)
        {
            try
            {
                _filmRepo.CreateFilm(film);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateFilm(Film film)
        {
            try
            {
                _filmRepo.UpdateFilm(film);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteFilmById(ObjectId id)
        {
            try
            {
               _filmRepo.DeleteFilmById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Projection> GetProjectionsOfFilm(Film film)
        {
            var projections = new List<Projection>();
            try
            {
                var collectionProjections = _projectionRepo.ReadProjections();
                foreach (var projection in collectionProjections)
                {
                    if (projection.IdFilmProjection == film.Id)
                    {
                        projections.Add(projection);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return projections;
        }
    }
}
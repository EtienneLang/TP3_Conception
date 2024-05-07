using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Services
{
    public class FilmService : IFilmService
    {
        
        private readonly IFilmRepository _filmRepo;
        private readonly IProjectionRepository _projectionRepository;
        private readonly IAbonneRepository _abonneRepository;
        private readonly INoteRepository _noteRepository;

        public FilmService(IFilmRepository filmRepo, IProjectionRepository projectionRepository, IAbonneRepository abonneRepository, INoteRepository noteRepository)
        {
            _filmRepo = filmRepo;
            _projectionRepository = projectionRepository;
            _abonneRepository = abonneRepository;
            _noteRepository = noteRepository;
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

        public Film ReadFilmById(ObjectId idFilm)
        {
            try
            {
                Film film = _filmRepo.ReadFilmById(idFilm);
                return film;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Projection> GetProjectionsOfFilm(Film film)
        {
            var projections = new List<Projection>();
            try
            {
                var collectionProjections = _projectionRepository.ReadProjections();
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

        public List<Film> GetFilmsToRate(ObjectId userId)
        {
            Abonne abonne = _abonneRepository.ReadAbonneById(userId);
            List<Projection> lstProjections = new List<Projection>();
            foreach (ObjectId idReservation in abonne.Reservations)
            {
                Projection projection = _projectionRepository.GetProjectionById(idReservation);
                if (projection.DateProjection < DateTime.Today)
                {
                    lstProjections.Add(projection);   
                }
            }
            List<Film> films = new List<Film>();
            foreach (Projection projection in lstProjections)
            {
                Note? note = _noteRepository.ReadNoteByUserOnFilm(abonne.Id, projection.IdFilmProjection);
                if (note == null)
                {
                    films.Add(_filmRepo.ReadFilmById(projection.IdFilmProjection)); 
                }
            }
            return films;
        }
    }
}
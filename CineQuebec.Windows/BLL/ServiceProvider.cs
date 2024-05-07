using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using CineQuebec.Windows.DAL.Repositories;

namespace CineQuebec.Windows.BLL;

public class ServiceProvider
{
    private IFilmService _filmService;
    private IAbonneService _abonneService;
    private IProjectionService _projectionService;
    private INoteService _noteService;
    
    public IFilmService FilmService
    {
        get { return _filmService; }
        private set { _filmService = value; }
    }

    public IAbonneService AbonneService
    {
        get { return _abonneService; }
        private set { _abonneService = value; }
    }

    public IProjectionService ProjectionService
    {
        get { return _projectionService; }
        private set { _projectionService = value; }
    }

    public INoteService NoteService
    {
        get { return _noteService; }
        private set { _noteService = value; }
    }
    public ServiceProvider()
    {
        IFilmRepository filmRepository = new FilmRepository();
        IAbonneRepository abonneRepository = new AbonneRepository();
        IProjectionRepository projectionRepository = new ProjectionRepository();
        INoteRepository noteRepository = new NoteRepository();

        _filmService = new FilmService(filmRepository, projectionRepository, abonneRepository, noteRepository);
        _abonneService = new AbonneService(abonneRepository);
        _projectionService = new ProjectionService(projectionRepository);
        _noteService = new NoteService(noteRepository);
    }
}
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
    private IAuthService _authService;
    private IPreferenceService _preferenceService;
    private ICategorieService _categorieService;
    private IRealisateurService _realisateurService;
    private IActeurService _acteurService;
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

    public IAuthService AuthService
    {
        get { return _authService; }
        private set { _authService = value; }
    }

    public ICategorieService CategorieService
    {
        get { return _categorieService; }
        private set { _categorieService = value; }
    }

    public IPreferenceService PreferenceService
    {
        get { return _preferenceService; }
        private set { _preferenceService = value; }
    }

    public IRealisateurService RealisateurService
    {
        get { return _realisateurService; }
        private set { _realisateurService = value; }
    }

    public IActeurService ActeurService
    {
        get { return _acteurService; }
        private set { _acteurService = value; }
    }
    
    public ServiceProvider()
    {
        IFilmRepository filmRepository = new FilmRepository();
        IAbonneRepository abonneRepository = new AbonneRepository();
        IProjectionRepository projectionRepository = new ProjectionRepository();
        INoteRepository noteRepository = new NoteRepository();
        IAuthRepository authRepository = new AuthRepository();
        IActeurRepository acteurRepository = new ActeurRepository();
        IRealisateurRepository realisateurRepository = new RealisateurRepository();
        ICategorieRepository categorieRepository = new CategorieRepository();
        IPreferenceRepository preferenceRepository = new PreferenceRepository();
        
        _filmService = new FilmService(filmRepository, projectionRepository, abonneRepository, noteRepository);
        _abonneService = new AbonneService(abonneRepository, preferenceRepository);
        _projectionService = new ProjectionService(projectionRepository);
        _noteService = new NoteService(noteRepository);
        _authService = new AuthService(authRepository);
        _acteurService = new ActeurService(acteurRepository);
        _realisateurService = new RealisateurService(realisateurRepository);
        _categorieService = new CategorieService(categorieRepository);
        _preferenceService = new PreferenceService(preferenceRepository, abonneRepository);
    }
}
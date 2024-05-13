using System.Windows;
using CineQuebec.Windows.BLL;
using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.View.AbonneViews;
using CineQuebec.Windows.View.AdminViews;
using MongoDB.Bson;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private INoteService _noteService;
        private readonly IAbonneService _abonneService;
        private IFilmService _filmService;
        private IProjectionService _projectionService;
        private ICategorieService _categorieService;
        private IAuthService _authService;
        private IActeurService _acteurService;
        private IRealisateurService _realisateurService;
        private IPreferenceService _preferenceService;

        public MainWindow()
        {
            InitializeComponent();
            
            //injection de dépendance fait à la mitaine
            ServiceProvider serviceProvider = new ServiceProvider();
            
            _noteService = serviceProvider.NoteService;
            _abonneService = serviceProvider.AbonneService;
            _filmService = serviceProvider.FilmService;
            _projectionService = serviceProvider.ProjectionService;
            _authService = serviceProvider.AuthService;
            _categorieService = serviceProvider.CategorieService;
            _acteurService = serviceProvider.ActeurService;
            _realisateurService = serviceProvider.RealisateurService;
            _preferenceService = serviceProvider.PreferenceService;

            /*
            Preference preference = new Preference();
            preference.UserId = new ObjectId("66425c1e2d33d2c7d6379ee1");
            List<ObjectId> categories = new List<ObjectId>();
            categories.Add(new ObjectId("663d53660f663456305392c0"));
            categories.Add(new ObjectId("663d53660f663456305392bd"));
            List<ObjectId> acteurs = new List<ObjectId>();
            acteurs.Add(new ObjectId("663e6d4738732b69bc2e9d81"));
            acteurs.Add(new ObjectId("663e6d4738732b69bc2e9d83"));
            List<ObjectId> realisateurs = new List<ObjectId>();
            realisateurs.Add(new ObjectId("663e6d9605586b9bceb4baa6"));
            realisateurs.Add(new ObjectId("663e6d9605586b9bceb4baa7"));
            preference.Acteurs = acteurs;
            preference.Categories = categories;
            preference.Realisateurs = realisateurs;
            preference.Id = new ObjectId();
            serviceProvider.PreferenceService.CreatePreference(preference);
            */
            mainContentControl.Content = new ConnexionControl(_authService);
            
        }

        public void AdminHomeControl()
        {
            mainContentControl.Content = new AdminHomeControl();
        }

        public void UserListControl()
        {
            mainContentControl.Content = new UserListControl(_abonneService);
        }

        public void FilmListControl()
        {
            mainContentControl.Content = new FilmListControl(_filmService, _categorieService,_acteurService,_realisateurService, _projectionService);
        }
        
        public void AbonneHomeControl(Abonne abonne)
        {
            mainContentControl.Content = new AbonneHomeControl(abonne);
        }
        
        public void FilmListForUser(Abonne abonne)
        {
            mainContentControl.Content = new FilmListForUser(abonne, _filmService, _projectionService);
        }
        
        public void GiftHomeControl()
        {
            mainContentControl.Content = new GiftHomeControl();
        }
        
        public void TicketGratuitProjection()
        {
            mainContentControl.Content = new TicketGratuitProjection(_abonneService, _filmService, _projectionService);
        }
        
        public void InvitationAvantPremiere()
        {
            mainContentControl.Content = new InvitationAvantPremiere(_filmService, _abonneService, _projectionService);
        }
        

        public void AbonneListeFilmNoteControl(Abonne abonne)
        {
            mainContentControl.Content = new AbonneListeFilmNoteControl(abonne, _projectionService, _filmService, _noteService);
        }

    }
}
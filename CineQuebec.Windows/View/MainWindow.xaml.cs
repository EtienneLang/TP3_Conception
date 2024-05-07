using CineQuebec.Windows.View;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories;
using CineQuebec.Windows.View.AbonneViews;
using CineQuebec.Windows.View.AdminViews;

namespace CineQuebec.Windows
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

        public MainWindow()
        {
            InitializeComponent();
            var projectionRepo = new ProjectionRepository();
            var filmRepo = new FilmRepository();
            var abonneRepo = new AbonneRepository();
            var noteRepo = new NoteRepository();
            _projectionService = new ProjectionService(projectionRepo);
            _filmService = new FilmService(filmRepo, projectionRepo);
            _abonneService = new AbonneService(abonneRepo);
            _noteService = new NoteService(noteRepo);
            
            mainContentControl.Content = new ConnexionControl(_abonneService);
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
            mainContentControl.Content = new FilmListControl(_filmService);
        }
        
        public void AbonneHomeControl(Abonne abonne)
        {
            mainContentControl.Content = new AbonneHomeControl(abonne);
        }
        
        public void FilmListForUser(Abonne abonne)
        {
            mainContentControl.Content = new FilmListForUser(abonne);
        }
        
        public void GiftHomeControl()
        {
            mainContentControl.Content = new GiftHomeControl();
        }
        
        public void TicketGratuitProjection()
        {
            mainContentControl.Content = new TicketGratuitProjection(_abonneService, _filmService);
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
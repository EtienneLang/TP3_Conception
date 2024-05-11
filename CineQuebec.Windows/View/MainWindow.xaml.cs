﻿using System.Windows;
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


            for (int i = 1; i < 10; i++)
            {
                Abonne abonne = new Abonne();
                abonne.Username = $"User {i}";
                abonne.Password = "U2VjcmV0U2FsdA==:9z9EkBnWaHbxnjdrJouevsufnnL3cjVtsWKCGfPjqdA=";
                abonne.DateJoined = DateTime.Now;
                abonne.Reservations = new List<ObjectId>();
                abonne.Role = "User";
                abonne.IdFilmsOfferts = new List<ObjectId>();
                _abonneService.CreateAbonne(abonne);
            }
            
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
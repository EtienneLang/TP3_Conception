using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.View.AdminViews
{
    public partial class InvitationAvantPremiere : UserControl
    {
        private readonly IFilmService _filmService;
        private readonly IAbonneService _abonneService;
        private readonly IProjectionService _projectionService;
        private bool _isFilmList;
        private bool _isUserList;
        private Film _selectedFilm;
        private List<Projection> _projections;
        private int _filmSelectedIndex;

        public InvitationAvantPremiere(IFilmService filmService, IAbonneService abonneService, IProjectionService projectionService)
        {
            InitializeComponent();
            _filmService = filmService;
            _abonneService = abonneService;
            _projectionService = projectionService;
            GenerateAvantPremiereList();
        }

        private void GenerateAvantPremiereList()
        {
            ButtonOffrir.IsEnabled = false;
            _isFilmList = true;
            _isUserList = false;
            
            _projections = _projectionService.ReadAvantPremieres();
            foreach (Projection projection in _projections)
            {
                ListeBoxItemsAvantPremiere.Items.Add(_filmService.ReadFilmById(projection.IdFilmProjection));
            }
            
        }

        private void ButtonRetourVersGiftHomeControl_OnClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GiftHomeControl();
        }

        private void ListeBoxItemsAvantPremiere_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListeBoxItemsAvantPremiere.SelectedIndex == -1)
                return;
            if (_isFilmList)
            {
                _selectedFilm = (Film)ListeBoxItemsAvantPremiere.SelectedItem;
                _filmSelectedIndex = ListeBoxItemsAvantPremiere.SelectedIndex;
                GenerateUserList();
            }
            if (_isUserList)
            {
                ButtonOffrir.IsEnabled = true;
            }
        }

        private void GenerateUserList()
        {
            ListeBoxItemsAvantPremiere.Items.Clear();
            _isFilmList = false;
            _isUserList = true;
            
            List<Abonne> abonnes = _abonneService.ReadAbonnesInterestedInActeurAndCategorie(_selectedFilm.IdActeurs,
                _selectedFilm.IdRealisateurs);
            foreach (Abonne abonne in abonnes)
            {
                ListeBoxItemsAvantPremiere.Items.Add(abonne);
            }
        }

        private void ButtonOffrir_OnClick(object sender, RoutedEventArgs e)
        {
            Projection projection = _projections[_filmSelectedIndex];
            Abonne abonne = (Abonne)ListeBoxItemsAvantPremiere.SelectedItem;
            _projectionService.ReserverPlace(projection, abonne.Id);
            MessageBox.Show("Invitation à l'avant première éffectué avec succès");
        }
    }
}
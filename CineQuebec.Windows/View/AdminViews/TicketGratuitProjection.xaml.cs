using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View.AdminViews
{
    public partial class TicketGratuitProjection : UserControl
    {
        private IFilmService _filmService;
        private IAbonneService _abonneService;
        private IProjectionService _projectionService;
        private List<Film> _films;
        private List<Abonne> _abonnes;
        private int _selectedIndex = -1;
        private bool _isFilmList;
        private bool _isUserList;
        private bool _isProjectionList;
        private Film _selectedFilm;
        private Abonne _selectedAbonne;
        private List<Abonne> _abonnesInteresseParFilm = new List<Abonne>();

        public TicketGratuitProjection(IAbonneService abonneService, IFilmService filmService, IProjectionService projectionService)
        {
            InitializeComponent();
            _abonneService = abonneService;
            _filmService = filmService;
            _projectionService = projectionService;
            GenerateFilmList();
        }

        private void GetFilms()
        {
            _films = _filmService.ReadFilms();
        }

        private void GetAbonnes()
        {
            _abonnes = _abonneService.ReadAbonnes();
        }

        private void ClearInterface()
        {
            ListeBoxReprojection.Items.Clear();
            //lstReprojection.SelectedIndex = -1;
        }

        private void GenerateFilmList()
        {
            ClearInterface();
            GetFilms();
            LabelTitre.Content = "Sélectionnez un film";
            _isFilmList = true;
            _isProjectionList = false;
            _isUserList = false;
            _selectedFilm = null;
            _selectedAbonne = null;
            foreach (Film film in _films)
            {
                ListeBoxReprojection.Items.Add(film);
            }
        }

        private void GenerateAbonneList()
        {
            ClearInterface();
            List<Abonne> abonnes = _abonneService.ReadAbonnesInterestedInCategorie(_selectedFilm.IdCategorie);
            LabelTitre.Content = "À qui offrir un billet gratuit pour le film " + _selectedFilm.Titre + " ?";
            _isFilmList = false;
            _isUserList = true;
            _isProjectionList = false;
            foreach (Abonne abonne in abonnes)
            {
                ListeBoxReprojection.Items.Add(abonne);
            }
        }
        
        private void LstReprojection_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListeBoxReprojection.SelectedIndex == -1)
                return;
            if (_isFilmList)
            {
                Film? selectedFilm = (Film)ListeBoxReprojection.SelectedItem;
                if (selectedFilm != null)
                {
                    _selectedFilm = selectedFilm;
                    GenerateAbonneList();
                    return;
                }    
            }
            else if (_isUserList)
            {
                Abonne? selectedAbonne = (Abonne)ListeBoxReprojection.SelectedItem;
                if (selectedAbonne != null)
                {
                    _selectedAbonne = selectedAbonne;
                    GenerateProjectionList();
                }    
            }
            else
            {
                Projection? selectedProjection = (Projection)ListeBoxReprojection.SelectedItem;
                if (selectedProjection != null)
                {
                    if (AfficherMessageBoxConfirmation(_selectedAbonne.Username, _selectedFilm.Titre))
                    {
                        _projectionService.ReserverPlace(selectedProjection, _selectedAbonne.Id);
                        MessageBox.Show("Billet offert avec succès");
                        GenerateFilmList();
                    }
                }
            }
        }

        private void GenerateProjectionList()
        {
            ListeBoxReprojection.Items.Clear();
            LabelTitre.Content = "Sélectionnez la projection que vous voulez offrir";
            _isFilmList = false;
            _isUserList = false;
            _isProjectionList = true;
            List<Projection> projections = _projectionService.ReadProjectionByFilmId(_selectedFilm.Id);
            foreach (Projection projection in projections)
            {
                ListeBoxReprojection.Items.Add(projection);
            }
        }

        private void BtnRetourVersGiftHomeControl_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isUserList)
            {
                GenerateFilmList();
                _selectedFilm = null;
                _isFilmList = true;
                _isProjectionList = false;
                _isUserList = false;
            }
            else if (_isProjectionList)
            {
                GenerateAbonneList();
                _isFilmList = false;
                _isProjectionList = false;
                _isUserList = true; 
                _selectedAbonne = null;
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow).GiftHomeControl();
            }
        }
        
        private bool AfficherMessageBoxConfirmation(string nomAbonne, string titreFilm)
        {
            MessageBoxResult result =
                MessageBox.Show(
                    "Voulez-vous offrir un billet gratuit pour le film " + titreFilm + " à " +
                    nomAbonne + " ?", "Offrir un billet gratuit",
                    MessageBoxButton.YesNo);
            return result == MessageBoxResult.Yes;
        }
        
    }
}
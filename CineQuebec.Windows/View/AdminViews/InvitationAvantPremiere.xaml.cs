using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View.AdminViews
{
    public partial class InvitationAvantPremiere : UserControl
    {
        private FilmService _dbFilms;
        private AbonneService _dbAbonnes;
        private ProjectionService _dbProjections;
        private List<Film> _films;
        private List<Abonne> _abonnes;
        private List<Projection> _projections;
        private int _selectedIndex = -1;
        private bool _isFilmSelection = true;
        private List<Abonne> _abonnesInteresseParFilm = new List<Abonne>();

        public InvitationAvantPremiere()
        {
            InitializeComponent();
            _dbFilms = new FilmService();
            _dbAbonnes = new AbonneService();
            _dbProjections = new ProjectionService();
            GenerateAvantPremiereList();
        }

        private void GetAvantPremieres()
        {
            _projections = _dbProjections.ReadAvantPremieres();
        }

        private void GetAbonnes()
        {
            _abonnes = _dbAbonnes.ReadAbonnes();
        }

        private void GetFilms()
        {
            _films = _dbFilms.ReadFilms();
        }
        
        private void ClearInterface()
        {
            lstReprojection.Items.Clear();
            lstReprojection.SelectedIndex = -1;
            _selectedIndex = lstReprojection.SelectedIndex;
        }

        private void GenerateAvantPremiereList()
        {
            ClearInterface();
            GetAvantPremieres();
            GetFilms();
           
            foreach (Projection projection in _projections)
            {
                foreach (Film film in _films)
                {
                    if (film.Id == projection.IdFilmProjection)
                    {
                        ListBoxItem item = new ListBoxItem();
                        item.Content = film + " " + projection;
                        lstReprojection.Items.Add(item);
                    }
                }
            }
        }

        private void GenerateAbonneList(string categorie)
        {
            ClearInterface();
            GetAbonnes();
            foreach (Abonne abonne in _abonnes)
            {
                if (abonne.CategoriesFav != null && abonne.CategoriesFav.Contains(categorie))
                {
                    _abonnesInteresseParFilm.Clear();
                    _abonnesInteresseParFilm.Add(abonne);
                    ListBoxItem itemAbonne = new ListBoxItem();
                    itemAbonne.Content = abonne;
                    lstReprojection.Items.Add(itemAbonne);
                }
            }
        }

        private void Btn_retour_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isFilmSelection)
            {
                GenerateAvantPremiereList();
                _isFilmSelection = true;
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow).GiftHomeControl();
            }
        }


        int indexMovie;

        private void LstReprojection_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedIndex = lstReprojection.SelectedIndex;
            if (_selectedIndex == -1)
                return;

            indexMovie = _selectedIndex;
            if (_isFilmSelection)
            {
                if (_abonnesInteresseParFilm.Count == 0)
                {
                    MessageBox.Show("Aucun abonné n'est intéressé par ce film.");
                    return;
                }

                lstReprojection.Items.Clear();
                lblTitre.Content = "À qui offrir un billet gratuit pour le film " + _films[indexMovie].Titre + " ?";
                GenerateAbonneList(_films[indexMovie].Categorie);
            }
            else
            {
                if (_abonnesInteresseParFilm[_selectedIndex].ListFilmOffertContientDejaFilm(_films[indexMovie].Id))
                {
                    MessageBox.Show("L'abonné a déjà reçu un billet gratuit pour ce film.");
                    return;
                }

                MessageBoxResult result =
                    MessageBox.Show(
                        "Voulez-vous offrir un billet gratuit pour le film " + _films[indexMovie].Titre + " à " +
                        _abonnesInteresseParFilm[_selectedIndex].Username + " ?", "Offrir un billet gratuit",
                        MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _dbAbonnes.OffrirBillet(_abonnesInteresseParFilm[_selectedIndex].Id, _films[indexMovie].Id);
                    lstReprojection.Items.Clear();
                    lblTitre.Content = "Sélectionnez un film à offir";
                    GenerateAvantPremiereList();
                }
                else
                    return;
            }

            _isFilmSelection = !_isFilmSelection;
        }
    }
}
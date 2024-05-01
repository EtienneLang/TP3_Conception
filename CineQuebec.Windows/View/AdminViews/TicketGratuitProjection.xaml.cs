using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View.AdminViews
{
    public partial class TicketGratuitProjection : UserControl
    {
        private FilmService _dbFilms;
        private AbonneService _dbAbonnes;
        private List<Film> _films;
        private List<Abonne> _abonnes;
        private int _selectedIndex = -1;
        private bool _isFilmSelection = true;
        private List<Abonne> _abonnesInteresseParFilm = new List<Abonne>();

        public TicketGratuitProjection()
        {
            InitializeComponent();
            _dbFilms = new FilmService();
            _dbAbonnes = new AbonneService();
            GenerateFilmList();
        }

        private void GetFilms()
        {
            _films = _dbFilms.ReadFilms();
        }

        private void GetAbonnes()
        {
            _abonnes = _dbAbonnes.ReadAbonnes();
        }

        private void ClearInterface()
        {
            lstReprojection.Items.Clear();
            //lstReprojection.SelectedIndex = -1;
            _selectedIndex = lstReprojection.SelectedIndex;
        }

        private void GenerateFilmList()
        {
            ClearInterface();
            GetFilms();
            foreach (Film film in _films)
            {
                ListBoxItem itemFilm = new ListBoxItem();
                itemFilm.Content = film;
                lstReprojection.Items.Add(itemFilm);
            }
        }

        private void GenerateAbonneList(string categorie)
        {
            ClearInterface();
            GetAbonnes();
            foreach (Abonne abonne in _abonnes)
            {
                if (abonne.CategoriesFav == null || !abonne.CategoriesFav.Contains(categorie)) continue;
                _abonnesInteresseParFilm.Clear();
                _abonnesInteresseParFilm.Add(abonne);
                ListBoxItem itemAbonne = new ListBoxItem();
                itemAbonne.Content = abonne;
                lstReprojection.Items.Add(itemAbonne);
            }
        }

        private void Btn_retour_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isFilmSelection)
            {
                GenerateFilmList();
                _isFilmSelection = true;
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow).GiftHomeControl();
            }
        }


        private int _indexMovie;

        private void LstReprojection_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedIndex = lstReprojection.SelectedIndex;
            if (_selectedIndex == -1)
                return;

            _indexMovie = _selectedIndex;
            if (_isFilmSelection)
            {
                lstReprojection.Items.Clear();
                GenerateAbonneList(_films[_indexMovie].Categorie);
                if (_abonnesInteresseParFilm.Count == 0)
                {
                    MessageBox.Show("Aucun abonné n'est intéressé par ce film.");
                    return;
                }
                lblTitre.Content = "À qui offrir un billet gratuit pour le film " + _films[_indexMovie].Titre + " ?";

            }
            else
            {
                if (_abonnesInteresseParFilm[_selectedIndex].ListeFilmOffertContientDejaFilm(_films[_indexMovie].Id))
                {
                    MessageBox.Show("L'abonné a déjà reçu un billet gratuit pour ce film.");
                    return;
                }

                MessageBoxResult result =
                    MessageBox.Show(
                        "Voulez-vous offrir un billet gratuit pour le film " + _films[_indexMovie].Titre + " à " +
                        _abonnesInteresseParFilm[_selectedIndex].Username + " ?", "Offrir un billet gratuit",
                        MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _dbAbonnes.OffrirBillet(_abonnesInteresseParFilm[_selectedIndex].Id, _films[_indexMovie].Id);
                    lstReprojection.Items.Clear();
                    lblTitre.Content = "Sélectionnez un film à offir";
                    GenerateFilmList();
                }
                else
                    return;
            }

            _isFilmSelection = !_isFilmSelection;
        }
    }
}
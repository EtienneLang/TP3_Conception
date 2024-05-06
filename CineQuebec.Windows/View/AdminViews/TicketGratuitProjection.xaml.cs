using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;

namespace CineQuebec.Windows.View.AdminViews
{
    public partial class TicketGratuitProjection : UserControl
    {
        private IFilmService _dbFilms;
        private IAbonneService _dbAbonnes;
        private List<Film> _films;
        private List<Abonne> _abonnes;
        private int _selectedIndex = -1;
        private bool _isFilmSelection = true;
        private List<Abonne> _abonnesInteresseParFilm = new List<Abonne>();

        public TicketGratuitProjection()
        {
            InitializeComponent();
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
            ListeBoxReprojection.Items.Clear();
            //lstReprojection.SelectedIndex = -1;
            _selectedIndex = ListeBoxReprojection.SelectedIndex;
        }

        private void GenerateFilmList()
        {
            ClearInterface();
            GetFilms();
            foreach (Film film in _films)
            {
                ListBoxItem itemFilm = new ListBoxItem();
                itemFilm.Content = film;
                ListeBoxReprojection.Items.Add(itemFilm);
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
                ListeBoxReprojection.Items.Add(itemAbonne);
            }
        }
        
        private int _indexMovie;

        private void LstReprojection_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedIndex = ListeBoxReprojection.SelectedIndex;
            if (_selectedIndex == -1)
                return;

            _indexMovie = _selectedIndex;
            if (_isFilmSelection)
            {
                ChangeListeFilmVersListeAbonnes(_films[_indexMovie]);
            }
            else
            {
                if (_abonnesInteresseParFilm[_selectedIndex].ListeFilmOffertContientDejaFilm(_films[_indexMovie].Id))
                {
                    MessageBox.Show("L'abonné a déjà reçu un billet gratuit pour ce film.");
                    return;
                }

                bool reponseUser = AfficherMessageBoxConfirmation(_abonnesInteresseParFilm[_selectedIndex].Username,
                    _films[_indexMovie].Titre);
                if (reponseUser)
                {
                    _dbAbonnes.OffrirBillet(_abonnesInteresseParFilm[_selectedIndex].Id, _films[_indexMovie].Id);
                    ChangeListeAbonnesVersListeFilm();
                }
                else
                    return;
            }

            _isFilmSelection = !_isFilmSelection;
        }

        private void BtnRetourVersGiftHomeControl_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_isFilmSelection)
            {
                GenerateFilmList();
                _isFilmSelection = true;
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
        
        private void ChangeListeAbonnesVersListeFilm()
        {
            ListeBoxReprojection.Items.Clear();
            LabelTitre.Content = "Sélectionnez un film à offir";
            GenerateFilmList();
        }
        
        private void ChangeListeFilmVersListeAbonnes(Film filmSelectionne)
        {
            ListeBoxReprojection.Items.Clear();
            GenerateAbonneList(filmSelectionne.Categorie);
            if (_abonnesInteresseParFilm.Count == 0)
            {
                MessageBox.Show("Aucun abonné n'est intéressé par ce film.");
                return;
            }
            LabelTitre.Content = "À qui offrir un billet gratuit pour le film " + filmSelectionne.Titre + " ?";
        }

    }
}
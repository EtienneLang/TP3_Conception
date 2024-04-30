using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

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
        private bool _isProjectionSelection = true;
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
            lstItemsAvantPremiere.Items.Clear();
            lstItemsAvantPremiere.SelectedIndex = -1;
            _selectedIndex = lstItemsAvantPremiere.SelectedIndex;
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
                        item.Tag = film;
                        lstItemsAvantPremiere.Items.Add(item);
                    }
                }
            }
        }

        private void GenerateAbonneList(List<ObjectId> idActeursFavorits, List<ObjectId> idRealisateursFavorits)
        {
            ClearInterface();
            GetAbonnes();

            foreach (Abonne abonne in _abonnes)
            {
                bool abonneEstInteresse = false;
                if (idActeursFavorits == null || idRealisateursFavorits == null)
                    return;
                foreach (ObjectId idActeur in idActeursFavorits)
                {
                    if (!abonneEstInteresse && abonne.IdActeursFavorits != null &&
                        abonne.IdActeursFavorits.Contains(idActeur))
                    {
                        _abonnesInteresseParFilm.Add(abonne);
                        ListBoxItem itemAbonne = new ListBoxItem();
                        itemAbonne.Content = abonne;
                        lstItemsAvantPremiere.Items.Add(itemAbonne);
                        abonneEstInteresse = true;
                    }
                }

                foreach (ObjectId idRealisateur in idRealisateursFavorits)
                {
                    if (!abonneEstInteresse && abonne.IdRealisateursFavorits != null &&
                        abonne.IdRealisateursFavorits.Contains(idRealisateur))
                    {
                        _abonnesInteresseParFilm.Add(abonne);
                        ListBoxItem itemAbonne = new ListBoxItem();
                        itemAbonne.Content = abonne;
                        lstItemsAvantPremiere.Items.Add(itemAbonne);
                        abonneEstInteresse = true;
                    }
                }
            }
        }
        

        private void Btn_retour_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isProjectionSelection)
            {
                GenerateAvantPremiereList();
                _isProjectionSelection = true;
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow).GiftHomeControl();
            }
        }


        int indexMovie;

        private void LstItemsAvantPremiere_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedIndex = lstItemsAvantPremiere.SelectedIndex;
            if (_selectedIndex == -1)
                return;

            indexMovie = _selectedIndex;
            if (_isProjectionSelection)
            {
                Film filmSelectionne = new Film();
                if (lstItemsAvantPremiere.SelectedItem is ListBoxItem selectedItem)
                {
                    filmSelectionne = selectedItem.Tag as Film;
                }
                lstItemsAvantPremiere.Items.Clear();
                GenerateAbonneList(filmSelectionne.IdActeurs, filmSelectionne.IdRealisateurs);
                
                if (_abonnesInteresseParFilm.Count == 0)
                {
                    MessageBox.Show("Aucun abonné n'est intéressé par ce film.");
                    return;
                }

                lblTitre.Content = "À qui offrir un billet gratuit pour le film " + _films[indexMovie].Titre + " ?";
            }
            else
            {
                if (_abonnesInteresseParFilm[_selectedIndex].ListeFilmOffertContientDejaFilm(_films[indexMovie].Id))
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
                    lstItemsAvantPremiere.Items.Clear();
                    lblTitre.Content = "Sélectionnez un film à offir";
                    GenerateAvantPremiereList();
                }
                else
                    return;
            }

            _isProjectionSelection = !_isProjectionSelection;
        }
    }
}
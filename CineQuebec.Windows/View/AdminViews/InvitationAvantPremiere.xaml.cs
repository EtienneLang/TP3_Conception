using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.View.AdminViews
{
    public partial class InvitationAvantPremiere : UserControl
    {
        private readonly FilmService _dbFilms;
        private readonly AbonneService _dbAbonnes;
        private readonly ProjectionService _dbProjections;
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
            ListeBoxItemsAvantPremiere.Items.Clear();
            ListeBoxItemsAvantPremiere.SelectedIndex = -1;
            _selectedIndex = ListeBoxItemsAvantPremiere.SelectedIndex;
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
                    if (film.Id != projection.IdFilmProjection) continue;
                    ListBoxItem item = new ListBoxItem();
                    item.Content = film + " " + projection;
                    item.Tag = film;
                    ListeBoxItemsAvantPremiere.Items.Add(item);
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
                    if (abonneEstInteresse || abonne.IdActeursFavorits == null ||
                        !abonne.IdActeursFavorits.Contains(idActeur)) continue;
                    _abonnesInteresseParFilm.Add(abonne);
                    ListBoxItem itemAbonne = new ListBoxItem();
                    itemAbonne.Content = abonne;
                    ListeBoxItemsAvantPremiere.Items.Add(itemAbonne);
                    abonneEstInteresse = true;
                }

                foreach (ObjectId idRealisateur in idRealisateursFavorits)
                {
                    if (abonneEstInteresse || abonne.IdRealisateursFavorits == null ||
                        !abonne.IdRealisateursFavorits.Contains(idRealisateur)) continue;
                    _abonnesInteresseParFilm.Add(abonne);
                    ListBoxItem itemAbonne = new ListBoxItem();
                    itemAbonne.Content = abonne;
                    ListeBoxItemsAvantPremiere.Items.Add(itemAbonne);
                    abonneEstInteresse = true;
                }
            }
        }
        

        private void ButtonRetour_OnClick(object sender, RoutedEventArgs e)
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

        private void ListeBoxItemsAvantPremiere_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedIndex = ListeBoxItemsAvantPremiere.SelectedIndex;
            if (_selectedIndex == -1)
                return;
            Film filmSelectionne = new Film();
            indexMovie = _selectedIndex;
            if (_isProjectionSelection)
            {
                if (ListeBoxItemsAvantPremiere.SelectedItem is ListBoxItem selectedItem)
                {
                    filmSelectionne = selectedItem.Tag as Film;
                }
                ListeBoxItemsAvantPremiere.Items.Clear();
                GenerateAbonneList(filmSelectionne.IdActeurs, filmSelectionne.IdRealisateurs);
                
                if (_abonnesInteresseParFilm.Count == 0)
                {
                    MessageBox.Show("Aucun abonné n'est intéressé par ce film.");
                    return;
                }

                LabelTitre.Content = "Qui voulez-vous inviter à l'avant première du film " + filmSelectionne.Titre + " ?";
            }
            else
            {
                if (_abonnesInteresseParFilm[_selectedIndex].ListeReservationContientDejaProjection(_projections[indexMovie].Id))
                {
                    MessageBox.Show("L'abonné a déjà reçu une invitation pour cette avant-première.");
                    return;
                }

                MessageBoxResult result =
                    MessageBox.Show(
                        "Voulez-vous inviter " + _abonnesInteresseParFilm[_selectedIndex].Username + " à l'avant première du film " +
                         filmSelectionne.Titre + " ?", "Invitation à l'avant première",
                        MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _dbProjections.ReserverPlace(_projections[indexMovie], _abonnesInteresseParFilm[_selectedIndex].Id);
                    ListeBoxItemsAvantPremiere.Items.Clear();
                    LabelTitre.Content = "Sélectionnez une avant-première à offrir.";
                    GenerateAvantPremiereList();
                }
                else
                {
                    ListeBoxItemsAvantPremiere.SelectedIndex = -1;
                    return;
                }
            }

            _isProjectionSelection = !_isProjectionSelection;
        }
    }
}
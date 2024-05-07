using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.View.AbonneViews
{
    public partial class FilmListForUser : UserControl
    {
        private IFilmService _filmService;
        private IProjectionService _projectionService;
        private List<Film> _films;
        private int _selectedIndex = -1;
        private bool _isProjectionList = false;
        private Abonne abonneConnecte;
        Dictionary<string, ObjectId> projectionIds = new Dictionary<string, ObjectId>();

        public FilmListForUser(Abonne abonne, IFilmService filmService, IProjectionService projectionService)
        {
            abonneConnecte = abonne;
            _filmService = filmService;
            _projectionService = projectionService;
            InitializeComponent();
            GenerateFilmList();
        }

        private void GetFilms()
        {
            _films = _filmService.ReadFilms();
        }

        private void GenerateFilmList()
        {
            GetFilms();
            ClearInterface();
            foreach (Film film in _films)
            {
                ListBoxItem itemFilm = new ListBoxItem();
                itemFilm.Content = film;
                ListBoxProjections.Items.Add(itemFilm);
            }
        }

        private void ClearInterface()
        {
            ListBoxProjections.SelectedIndex = -1;
            ListBoxProjections.Items.Clear();
            _selectedIndex = ListBoxProjections.SelectedIndex;
            ButtonReserverPlace.IsEnabled = false;
        }

        private void ButtonRetourVersAccueil_Click(object sender, RoutedEventArgs e)
        {
            if (_isProjectionList)
            {
                GenerateFilmList();
                _isProjectionList = !_isProjectionList;
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow).AbonneHomeControl(abonneConnecte);
            }
        }

        private void Btn_reserverPlace_OnClick(object sender, RoutedEventArgs e)
        {
            Projection selectedProjection = GetSelectedProjection();
            _projectionService.ReserverPlace(selectedProjection, abonneConnecte.Id);
        }

        private void ListBoxProjections_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedIndex = ListBoxProjections.SelectedIndex;
            if (_selectedIndex == -1)
                return;
            if (_isProjectionList)
            {
                Projection selectedProjection = GetSelectedProjection();
                MessageBoxResult resultat = MessageBox.Show(
                    $"Voulez vous réserver votre place pour le film {_films[_selectedIndex].Titre} ?",
                    "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultat == MessageBoxResult.Yes)
                {
                    _projectionService.ReserverPlace(selectedProjection, abonneConnecte.Id);
                }
            }
            else
            {
                GenerateProjectionList(_films[_selectedIndex]);
                _isProjectionList = !_isProjectionList;
            }
        }

        private Projection GetSelectedProjection()
        {
            if (_selectedIndex == -1)
                return null;

            string selectedItem = ListBoxProjections.SelectedItem.ToString();
            ObjectId id = projectionIds[selectedItem];
            Projection projection = _projectionService.GetProjectionById(id);

            return projection;
        }

        private void GenerateProjectionList(Film film)
        {
            GetFilms();
            ListBoxProjections.Items.Clear();
            List<Projection> projections = _filmService.GetProjectionsOfFilm(film);

            for (int i = 0; i < projections.Count; i++)
            {
                string affichage = $"{film.Titre} - {projections[i].ToString()}";
                ListBoxItem itemProjection = new ListBoxItem();
                itemProjection.Content = affichage;
                ListBoxProjections.Items.Add(affichage);
                projectionIds[affichage] = projections[i].Id;
            }
        }
    }
}
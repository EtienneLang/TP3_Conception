using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autofac;
using Autofac.Core;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using WpfTutorialSamples.Dialogs;

namespace CineQuebec.Windows.View
{
    public partial class FilmListForUser : UserControl
    {
        private FilmService _dbFilmService;
        private ProjectionService _dbProjectionService;
        private List<Film> _films;
        private int _selectedIndex = -1;
        private bool _isProjectionList = false;
        private Abonne abonneConnecte;
        Dictionary<string, ObjectId> projectionIds = new Dictionary<string, ObjectId>();

        public FilmListForUser(Abonne abonne)
        {
            _dbFilmService = new FilmService();
            _dbProjectionService = new ProjectionService();
            abonneConnecte = abonne;
            InitializeComponent();
            GenerateFilmList();
        }

        private void GetFilms()
        {
            _films = _dbFilmService.ReadFilms();
        }

        private void GenerateFilmList()
        {
            GetFilms();
            ClearInterface();
            foreach (Film film in _films)
            {
                ListBoxItem itemFilm = new ListBoxItem();
                itemFilm.Content = film;
                lstProjections.Items.Add(itemFilm);
            }
        }

        private void ClearInterface()
        {
            lstProjections.SelectedIndex = -1;
            lstProjections.Items.Clear();
            _selectedIndex = lstProjections.SelectedIndex;
            btn_reserverPlace.IsEnabled = false;
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
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
            _dbProjectionService.ReserverPlace(selectedProjection, abonneConnecte.Id);
        }

        private void lstProjections_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedIndex = lstProjections.SelectedIndex;
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
                    _dbProjectionService.ReserverPlace(selectedProjection, abonneConnecte.Id);
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

            string selectedItem = lstProjections.SelectedItem.ToString();
            ObjectId id = projectionIds[selectedItem];
            Projection projection = _dbProjectionService.GetProjectionById(id);

            return projection;
        }

        private void GenerateProjectionList(Film film)
        {
            GetFilms();
            lstProjections.Items.Clear();
            List<Projection> projections = _dbFilmService.GetProjectionsOfFilm(film);

            for (int i = 0; i < projections.Count; i++)
            {
                string affichage = $"{film.Titre} - {projections[i].ToString()}";
                ListBoxItem itemProjection = new ListBoxItem();
                itemProjection.Content = affichage;
                lstProjections.Items.Add(affichage);
                projectionIds[affichage] = projections[i].Id; // Assuming projections[i] has an Id property
            }
        }
    }
}
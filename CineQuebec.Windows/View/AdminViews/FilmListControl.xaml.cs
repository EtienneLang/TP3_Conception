using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.View.AdminViews
{
    public partial class FilmListControl : UserControl
    {
        private IFilmService _filmService;
        private ICategorieService _categorieService;
        private IActeurService _acteurService;
        private IRealisateurService _realisateurService;
        private List<Film> _films;
        private int _selectedIndex = -1;
        private bool _isProjectionList = false;

        public FilmListControl(IFilmService filmService, ICategorieService categorieService, IActeurService acteurService, IRealisateurService realisateurService)
        {
            InitializeComponent();
            _filmService = filmService;
            _categorieService = categorieService;
            _acteurService = acteurService;
            _realisateurService = realisateurService;
            ButtonDelete.IsEnabled = false;
            ButtonAddProjection.IsEnabled = false;
            GenerateFilmList();
        }

        private void GetFilms()
        {
            _films = _filmService.ReadFilms();
        }

        private void ClearInterface()
        {
            ListBoxFilms.Items.Clear();
            ListBoxFilms.SelectedIndex = -1;
            _selectedIndex = ListBoxFilms.SelectedIndex;
            ButtonDelete.IsEnabled = false;
            ButtonAddProjection.IsEnabled = false;
        }

        private void GenerateFilmList()
        {
            ClearInterface();
            GetFilms();
            ButtonChangerListe.Content = "Afficher les projections";
            foreach (Film film in _films)
            {
                ListBoxItem itemFilm = new ListBoxItem();
                itemFilm.Content = film;
                ListBoxFilms.Items.Add(itemFilm);
            }
        }

        private void GenerateProjectionList()
        {
            ListBoxFilms.Items.Clear();
            ButtonChangerListe.Content = "Afficher les films";

            //Meilleur essai pour afficher les projections
            foreach (Film film in _films)
            {
                List<Projection> projections = _filmService.GetProjectionsOfFilm(film);
                for (int i = 0; i < projections.Count; i++) {
                    ListBoxItem itemProjection = new ListBoxItem();
                    string affichage = $"{film.Titre} - {projections[i]}";
                    itemProjection.Content = affichage;
                    ListBoxFilms.Items.Add(affichage);
                }
            }
        }

        private void ButtonAjouterFilm_Click(object sender, RoutedEventArgs e)
        {
            PopUpAjoutFilm inputDialog = new PopUpAjoutFilm(_categorieService,_acteurService, _realisateurService, _filmService);
            inputDialog.ShowDialog();
            if (inputDialog.DialogResult == true)
            {
                MessageBox.Show("Film ajouté avec succès");
                GenerateFilmList();
            }
        }

        private void ListBoxFilms_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedIndex = ListBoxFilms.SelectedIndex;
            if (_selectedIndex != -1)
            {
                ButtonDelete.IsEnabled = true;
                ButtonAddProjection.IsEnabled = true;
            }
        }

        private Film? GetSelectedFilm()
        {
            if (_selectedIndex == -1)
                return null;
            ListBoxItem selectedItem = (ListBoxItem)ListBoxFilms.SelectedItem;
            Film selectedFilm = (Film)selectedItem.Content;
            return selectedFilm;
        }

        private void ButtonDeleteFilm_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Film? film = GetSelectedFilm();
                if (film == null)
                    return;
                _filmService.DeleteFilmById(film.Id);
                GenerateFilmList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private void ButtonAjouterProjection_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Film? film = GetSelectedFilm();
                if (film == null)
                    return;
                ProgramProjectionFilm programProjectionFilm = new ProgramProjectionFilm(film.Titre);
                /*
                if (programProjectionFilm.ShowDialog() == true)
                {
                    List<string> result = programProjectionFilm.Answer;
                    film.Projections.Add(result);
                    _filmService.UpdateFilm(film);
                    GenerateFilmList();
                }
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private void ButtonChangerListe_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _isProjectionList = !_isProjectionList;
                if (!_isProjectionList)
                    GenerateFilmList();
                else
                    GenerateProjectionList();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void ButtonRetourVersAccueil_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).AdminHomeControl();
        }
    }
}
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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
        private IProjectionService _projectionService;
        private List<Film> _films;
        private int _selectedIndex = -1;
        private bool _isProjectionList = false;
        private Film? _selectedFilm;
        public FilmListControl(IFilmService filmService, ICategorieService categorieService, IActeurService acteurService, IRealisateurService realisateurService, IProjectionService projectionService)
        {
            InitializeComponent();
            _filmService = filmService;
            _categorieService = categorieService;
            _acteurService = acteurService;
            _realisateurService = realisateurService;
            _projectionService = projectionService;
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
            ButtonChangerListe.IsEnabled = false;
        }

        private void GenerateFilmList()
        {
            ClearInterface();
            GetFilms();
            ButtonChangerListe.Content = "Afficher les projections";
            foreach (Film film in _films)
            {
                ListBoxFilms.Items.Add(film);
            }
        }

        private void GenerateProjectionList()
        {
            if (_selectedFilm == null)
                return;
            ListBoxFilms.Items.Clear();
            ButtonChangerListe.Content = "Afficher les films";
            try
            {
                List<Projection> projections = _projectionService.ReadProjectionByFilmId(_selectedFilm.Id);
                foreach (Projection projection in projections)
                {
                    ListBoxFilms.Items.Add(projection);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
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
                if (!_isProjectionList)
                {
                    _selectedFilm = (Film)ListBoxFilms.SelectedItem;
                }
                ButtonDelete.IsEnabled = true;
                ButtonAddProjection.IsEnabled = true;
                ButtonChangerListe.IsEnabled = true;
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
                if (_selectedFilm == null)
                    return;
                ProgramProjectionFilm programProjectionFilm = new ProgramProjectionFilm(_selectedFilm, _projectionService);
                programProjectionFilm.ShowDialog();
                if (programProjectionFilm.DialogResult == true)
                {
                    MessageBox.Show("Projection ajouté avec succès");
                    if (_isProjectionList)
                        GenerateProjectionList();
                    GenerateFilmList();
                }
                
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
                {
                    _selectedFilm = null;
                    ListBoxFilms.SelectedIndex = -1;
                    ButtonChangerListe.Content = "Afficher les projections de ce film";
                    GenerateFilmList();
                }
                else
                {
                    _selectedFilm = (Film)ListBoxFilms.SelectedItem;
                    ButtonChangerListe.Content = "Afficher les films";
                    GenerateProjectionList();   
                }
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
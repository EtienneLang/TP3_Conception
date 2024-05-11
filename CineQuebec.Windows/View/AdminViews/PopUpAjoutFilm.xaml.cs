using System.Windows;
using System.Windows.Documents;
using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.View.AdminViews
{
    public partial class PopUpAjoutFilm : Window
    {
        private ICategorieService _categorieService;
        private IActeurService _acteurService;
        private IRealisateurService _realisateurService;
        private IFilmService _filmService;
        public PopUpAjoutFilm(ICategorieService categorieService, IActeurService acteurService, IRealisateurService realisateurService, IFilmService filmService)
        {
            InitializeComponent();
            _categorieService = categorieService;
            _acteurService = acteurService;
            _realisateurService = realisateurService;
            _filmService = filmService;
            GenerateInterface();
        }

        private void GenerateInterface()
        {
            GenerateCategories();
            GenerateActeurs();
            GenerateRealisateur();
        }

        private void GenerateRealisateur()
        {
            List<Realisateur> realisateurs = _realisateurService.ReadRealisateurs();
            foreach (Realisateur realisateur in realisateurs)
            {
                LstRealisateurs.Items.Add(realisateur);
            }
        }

        private void GenerateActeurs()
        {
            List<Acteur> acteurs = _acteurService.ReadActeurs();
            foreach (Acteur acteur in acteurs)
            {
                LstActeurs.Items.Add(acteur);
            }
        }

        private void GenerateCategories()
        {
            List<Categorie> categories = _categorieService.ReadCategories();
            foreach (Categorie categorie in categories)
            {
                LstCategories.Items.Add(categorie);
            }
        }

        private void ButtonDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateForm())
                return;
            Film film = new Film();
            film.Titre = TextBoxTitre.Text.Trim();
            film.IdCategorie = ((Categorie)LstCategories.SelectedItem).Id;
            film.IdRealisateurs = new List<ObjectId>();
            film.IdActeurs = new List<ObjectId>();
            foreach (var lstActeursSelectedItem in LstActeurs.SelectedItems)
            {
                film.IdActeurs.Add(((Acteur)lstActeursSelectedItem).Id);
            }
            foreach (object lstRealisateursSelectedItem in LstRealisateurs.SelectedItems)
            {
                film.IdRealisateurs.Add(((Realisateur)lstRealisateursSelectedItem).Id);
            }
            film.Projections = new List<ObjectId>();
            film.Id = new ObjectId();
            CreateFilm(film);
        }

        private void CreateFilm(Film film)
        {
            _filmService.CreateFilm(film);
            DialogResult = true;
        }

        private bool ValidateForm()
        {
            if (String.IsNullOrEmpty(TextBoxTitre.Text))
            {
                MessageBox.Show("Veuillez entrer le titre du film");
                return false;
            }
            if (LstCategories.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez choisir la catégorie du film");
                return false;
            }

            if (LstActeurs.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Veuillez choisir au moins 1 acteur");
                return false;
            }

            if (LstRealisateurs.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Veuillez choisir au moins 1 réalisateur");
                return false;
            }
            
            return true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            TextBoxTitre.SelectAll();
            TextBoxTitre.Focus();
        }
        
    }
}
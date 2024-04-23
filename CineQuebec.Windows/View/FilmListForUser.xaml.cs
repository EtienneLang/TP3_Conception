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
        private FilmService _db;
        private List<Film> _films;
        private int _selectedIndex = -1;
        private bool _isProjectionList = false;

        public FilmListForUser()
        {
            _db = new FilmService();
            InitializeComponent();
            GenerateProjectionList();
        }

        private void GetFilms()
        {
            _films = _db.ReadFilms();
        }

        private void ClearInterface()
        {
            lstProjections.Items.Clear();
            lstProjections.SelectedIndex = -1;
            _selectedIndex = lstProjections.SelectedIndex;
            btn_reserverPlace.IsEnabled = false;
        }
        
        Dictionary<string, ObjectId> projectionIds = new Dictionary<string, ObjectId>();
        private void GenerateProjectionList()
        {
            GetFilms();
            ClearInterface();
            //Meilleur essai pour afficher les projections
            foreach (Film film in _films)
            {
                List<Projection> projections = _db.GetProjectionsOfFilm(film);
                for (int i = 0; i < projections.Count; i++) {
                    
                    string affichage = $"{film.Titre} - {projections[i].ToString()}";
                    ListBoxItem itemProjection = new ListBoxItem();
                    itemProjection.Content = affichage;
                    lstProjections.Items.Add(affichage);
                    projectionIds[affichage] = projections[i].Id; // Assuming projections[i] has an Id property

                }
            }
        }

        private Projection GetSelectedProjection()
        {
            if (_selectedIndex == -1)
            {
                return null;
            }
            string selectedItem = lstProjections.SelectedItem.ToString();
            ObjectId id = projectionIds[selectedItem.ToString()];
            // Perform operations with the ID
            
            Projection selectedProjection = lstProjections.SelectedItem as Projection;
            
            return selectedProjection;

        }
        
        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).AdminHomeControl();
        }

        private void Btn_reserverPlace_OnClick(object sender, RoutedEventArgs e)
        {  
            Projection selectedFilm = GetSelectedProjection();
            if (selectedFilm == null)
            {
                MessageBox.Show("Veuillez sélectionner un film.");
                return;
            }
            
        }

        private void lstProjections_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedIndex = lstProjections.SelectedIndex;
            if (_selectedIndex != -1)
            {
                btn_reserverPlace.IsEnabled = true;
            }
        }
    }
}
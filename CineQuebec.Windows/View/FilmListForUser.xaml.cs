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
            lstFilms.Items.Clear();
            lstFilms.SelectedIndex = -1;
            _selectedIndex = lstFilms.SelectedIndex;
            btn_reserverPlace.IsEnabled = false;
        }

        private void GenerateProjectionList()
        {
            GetFilms();
            ClearInterface();
            //Meilleur essai pour afficher les projections
            foreach (Film film in _films)
            {
                for (int i = 0; i < film.Projections.Count; i++)
                {
                    ListBoxItem itemProjection = new ListBoxItem();
                    string affichage = $"{film.Titre} - {film.Projections[i][0]} à {film.Projections[i][1]}";
                    itemProjection.Content = affichage;
                    lstFilms.Items.Add(affichage);
                }
            }
        }
        
        private void LstFilms_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedIndex = lstFilms.SelectedIndex;
            if (_selectedIndex != -1)
            {
                btn_reserverPlace.IsEnabled = true;
            }
        }

        private Film? GetSelectedFilm()
        {
            if (_selectedIndex == -1)
                return null;
            ListBoxItem selectedItem = (ListBoxItem)lstFilms.SelectedItem;
            Film selectedFilm = (Film)selectedItem.Content;
            return selectedFilm;
        }
        
        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).AdminHomeControl();
        }

        private void Btn_reserverPlace_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
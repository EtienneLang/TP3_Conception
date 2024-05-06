using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;

namespace CineQuebec.Windows.View;

public partial class AbonneListeFilmNoteControl : UserControl
{
    private Abonne _abonne;
    private IDatabaseProjection _databaseProjection;
    private IDatabaseFilms _databaseFilms;
    private IDatabaseNote _databaseNote;
    private List<Film> _lstFilms;
    public AbonneListeFilmNoteControl(Abonne abonne, IDatabaseProjection databaseProjection, IDatabaseFilms databaseFilms, IDatabaseNote databaseNote)
    {
        InitializeComponent();
        _abonne = abonne;
        _databaseProjection = databaseProjection;
        _databaseFilms = databaseFilms;
        _databaseNote = databaseNote;
        BtnNoter.IsEnabled = false;
        GetFilms();
        DataContext = _lstFilms;
    }

    private void GetFilms()
    {
        List<Projection> lstProjections = new List<Projection>();
        foreach (ObjectId id in _abonne.Reservations)
        {
            Projection projection = _databaseProjection.GetProjectionById(id);
            lstProjections.Add(projection);
        }

        List<Film> lstFilms = new List<Film>();
        foreach (Projection projection in lstProjections)
        {
            if (projection.DateProjection < DateTime.Today)
            {
                Film film = _databaseFilms.ReadFilmById(projection.IdFilmProjection);
                lstFilms.Add(film);   
            }
        }
        _lstFilms = lstFilms;
    }

    private void BtnRetourClick(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).AbonneHomeControl(_abonne);
    }

    private void LstFilms_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (LstFilms.SelectedIndex != -1)
        {
            BtnNoter.IsEnabled = true;
        }
    }

    private void BtnNoterClick(object sender, RoutedEventArgs e)
    {
        if ((Film)LstFilms.SelectedItem == null)
            return;
        var popUpAjoutNote = new PopUpAjoutNote((Film)LstFilms.SelectedItem, _databaseNote, _abonne);
        popUpAjoutNote.ShowDialog();
        if (popUpAjoutNote.DialogResult == true)
        {
            MessageBox.Show("Note ajouté avec succès");
        }
    }
}
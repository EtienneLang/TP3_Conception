using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.View.AbonneViews;

public partial class AbonneListeFilmNoteControl : UserControl
{
    private Abonne _abonne;
    private IProjectionService _projectionService;
    private IFilmService _filmService;
    private INoteService _noteServiceService;
    private List<Film> _lstFilms;
    public AbonneListeFilmNoteControl(Abonne abonne, IProjectionService projectionService, IFilmService filmService, INoteService noteService)
    {
        InitializeComponent();
        _abonne = abonne;
        _projectionService = projectionService;
        _filmService = filmService;
        _noteServiceService = noteService;
        BtnNoter.IsEnabled = false;
        GetFilms();
        DataContext = _lstFilms;
    }

    private void GetFilms()
    {
        List<Projection> lstProjections = new List<Projection>();
        foreach (ObjectId id in _abonne.Reservations)
        {
            Projection projection = _projectionService.GetProjectionById(id);
            lstProjections.Add(projection);
        }

        List<Film> lstFilms = new List<Film>();
        foreach (Projection projection in lstProjections)
        {
            if (projection.DateProjection < DateTime.Today)
            {
                Film film = _filmService.ReadFilmById(projection.IdFilmProjection);
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
        var popUpAjoutNote = new PopUpAjoutNote((Film)LstFilms.SelectedItem, _noteServiceService, _abonne);
        popUpAjoutNote.ShowDialog();
        if (popUpAjoutNote.DialogResult == true)
        {
            MessageBox.Show("Note ajouté avec succès");
        }
    }
}
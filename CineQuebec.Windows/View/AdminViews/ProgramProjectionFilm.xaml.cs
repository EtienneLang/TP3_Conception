using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.View.AdminViews;

public partial class ProgramProjectionFilm : Window
{
    private Film _film;
    private IProjectionService _projectionService;
    public ProgramProjectionFilm(Film film, IProjectionService projectionService)
    {
        InitializeComponent();
        _film = film;
        _projectionService = projectionService;
        LabelTitrePage.Content = film.Titre;
    }

    private void BtnDialogOk_OnClick(object sender, RoutedEventArgs e)
    {
        DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.MinValue;
        int selectedHour = int.Parse(((ComboBoxItem)HourComboBox.SelectedItem).Content.ToString());
        int selectedMinute = int.Parse(((ComboBoxItem)MinuteComboBox.SelectedItem).Content.ToString());
        DateTime dateProjection = new DateTime(
            selectedDate.Year, 
            selectedDate.Month, 
            selectedDate.Day, 
            selectedHour, 
            selectedMinute, 
            0);
        Projection projection = new Projection();
        projection.DateProjection = dateProjection;
        projection.NbPlace = int.Parse(NombrePlaceText.Text);
        projection.AvantPremiere = (bool)CheckBoxAvantPremiere.IsChecked!;
        projection.IdFilmProjection = _film.Id;
        projection.Id = new ObjectId();
        try
        {
            _projectionService.CreateProjection(projection);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
        
        DialogResult = true;
    }
    
}
using System.Windows;

namespace CineQuebec.Windows.View.AdminViews;

public partial class ProgramProjectionFilm : Window
{
    List<string> _projections = new List<string>();

    public ProgramProjectionFilm(string titreFilm)
    {
        InitializeComponent();
        lblTitrePage.Content = titreFilm;
    }

    private void BtnDialogOk_OnClick(object sender, RoutedEventArgs e)
    {
        _projections.Add(txtDate.Text);
        _projections.Add(txtHeure.Text);
        DialogResult = true;
    }

    public List<string> Answer
    {
        get { return _projections; }
    }
}
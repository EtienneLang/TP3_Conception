using System.Windows;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;

namespace CineQuebec.Windows.View;

public partial class PopUpAjoutNote : Window
{
    private IDatabaseNote _databaseNote;
    private Abonne _abonne;
    public PopUpAjoutNote(Film film, IDatabaseNote databaseNote, Abonne abonne)
    {
        InitializeComponent();
        _databaseNote = databaseNote;
        _abonne = abonne;
        DataContext = film;
    }

    private void BtnNoterClick(object sender, RoutedEventArgs e)
    {
        Note note = new Note();
        note.NoteSurCinq = int.Parse(ComboBoxNote.Text);
        note.Commentaire = textBoxCommentaire.Text.Trim();
        note.IdUser = _abonne.Id;
        note.Id = new ObjectId();
        _databaseNote.CreateNote(note);
        DialogResult = true;
    }

    private void BtnRetourClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}
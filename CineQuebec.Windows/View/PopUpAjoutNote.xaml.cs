using System.Windows;
using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.View;

public partial class PopUpAjoutNote : Window
{
    private INoteService _noteService;
    private Abonne _abonne;
    private Film _film;
    public PopUpAjoutNote(Film film, INoteService noteService, Abonne abonne)
    {
        InitializeComponent();
        _noteService = noteService;
        _abonne = abonne;
        _film = film;
        DataContext = _film;
    }

    private void BtnNoterClick(object sender, RoutedEventArgs e)
    {
        Note note = new Note();
        note.NoteSurCinq = int.Parse(ComboBoxNote.Text);
        note.Commentaire = textBoxCommentaire.Text.Trim();
        note.IdUser = _abonne.Id;
        note.IdFilm = _film.Id;
        note.Id = new ObjectId();
        _noteService.CreateNote(note, _abonne);
        DialogResult = true;
    }

    private void BtnRetourClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Interfaces;

public interface INoteRepository
{
    public List<Note> ReadNotes();
    public Note? ReadNoteByUserOnFilm(ObjectId userId, ObjectId filmId);
    public Note ReadNoteById(ObjectId idNote);
    public void CreateNote(Note note);
}
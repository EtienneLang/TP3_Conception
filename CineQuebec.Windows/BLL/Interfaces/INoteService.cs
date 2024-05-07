using System.Windows.Documents;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.BLL.Interfaces;

public interface INoteService
{
    public void CreateNote(Note note, Abonne abonne);
    public List<Note> ReadNotes();
}
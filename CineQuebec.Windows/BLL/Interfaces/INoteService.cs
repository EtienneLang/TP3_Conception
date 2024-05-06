using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.BLL.Interfaces;

public interface INoteService
{
    public void CreateNote(Note note);
}
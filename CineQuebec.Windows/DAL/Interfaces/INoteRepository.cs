using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.DAL.InterfacesForRepositories;

public interface INoteRepository
{
    public void CreateNote(Note note);
}
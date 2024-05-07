using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesForRepositories;

namespace CineQuebec.Windows.DAL.Repositories;

public class NoteRepository : ModelRepository, INoteRepository
{
    public void CreateNote(Note note)
    {
        try
        {
            var collection = _database.GetCollection<Note>("Notes");
            collection.InsertOne(note);
        }
        catch (ArgumentNullException ex)
        {
            throw new ArgumentNullException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
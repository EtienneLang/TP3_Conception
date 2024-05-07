using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesForRepositories;

namespace CineQuebec.Windows.BLL.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;

    public NoteService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    
    virtual public void CreateNote(Note note)
    {
        try
        {
            _noteRepository.CreateNote(note);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}
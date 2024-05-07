using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.InterfacesForRepositories;

namespace CineQuebec.Windows.BLL.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;

    public NoteService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    
    virtual public void CreateNote(Note note, Abonne abonne)
    {
        try
        {
            Note? noteExistante = _noteRepository.ReadNoteByUserOnFilm(abonne.Id, note.IdFilm);
            if (noteExistante != null)
                throw new NoteAlreadyExistException("Vous avez déjà noter ce film");
            if (note.NoteSurCinq < 1 || note.NoteSurCinq > 5)
                throw new InvalidNoteValueException("La note doit être entre 1 et 5");
            _noteRepository.CreateNote(note);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<Note> ReadNotes()
    {
        return _noteRepository.ReadNotes();
    }
}
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;
using Moq;

namespace CineQuebec.Tests.Tests;

public class TestsNote
{
    [Fact]
    public void ReadNotes_RetourneUneListeNotes()
    {
        // Arrange
        Mock<INoteRepository> noteRepoMock = new Mock<INoteRepository>();
        noteRepoMock.Setup(x => x.ReadNotes()).Returns(new List<Note>() { new Note(), new Note() });
        NoteService noteService = new NoteService(noteRepoMock.Object);
        
        // Act
        List<Note> notes = noteService.ReadNotes();
        
        // Assert
        Assert.Equal(2, notes.Count);
    }
    
    [Fact]
    public void CreateNote_CreerUneNote()
    {
        // Arrange
        Mock<INoteRepository> noteRepoMock = new Mock<INoteRepository>();
        noteRepoMock.Setup(x => x.CreateNote(It.IsAny<Note>()));
        Note note = new Note() { NoteSurCinq = 5};
        Abonne abonne = new Abonne();
        NoteService noteService = new NoteService(noteRepoMock.Object);
        
        // Act
        noteService.CreateNote(note, abonne);
        
        // Assert
        noteRepoMock.Verify(x => x.CreateNote(note), Times.Once);
    }
    
    [Fact]
    public void CreateNote_ThrowInvalidNoteValueException()
    {
        // Arrange
        Mock<INoteRepository> noteRepoMock = new Mock<INoteRepository>();
        Note note = new Note() { NoteSurCinq = 6};
        Abonne abonne = new Abonne();
        NoteService noteService = new NoteService(noteRepoMock.Object);
        
        // Act & Assert
        Assert.Throws<InvalidNoteValueException>(() => noteService.CreateNote(note, abonne));
    }
    
    [Fact]
    public void CreateNote_ThrowNoteAlreadyExistsException()
    {
        // Arrange
        var mockNoteRepository = new Mock<INoteRepository>();
        var abonneId = new ObjectId(); 
        var filmId = new ObjectId(); 
        var noteId = new ObjectId(); 
        var noteValue = 3; 
    
        var existingNote = new Note { Id = noteId, NoteSurCinq = noteValue };
        mockNoteRepository.Setup(repo => repo.ReadNoteByUserOnFilm(abonneId, filmId)).Returns(existingNote);
    
        var noteService = new NoteService(mockNoteRepository.Object);
    
        // Act & Assert
        var exception = Assert.Throws<NoteAlreadyExistException>(() => noteService.CreateNote(new Note { IdFilm = filmId, NoteSurCinq = noteValue }, new Abonne { Id = abonneId }));
        Assert.Equal("Vous avez déjà noter ce film", exception.Message);
        mockNoteRepository.Verify(repo => repo.ReadNoteByUserOnFilm(abonneId, filmId), Times.Once);
    }
}
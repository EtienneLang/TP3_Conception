using Xunit;
using Moq;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;

public class FilmServiceTests
{
    [Fact]
    public void ReadFilms_RetourneUneListeFilms()
    {
        // Arrange
        List<Film> films = new List<Film>() { new Film(), new Film() };
        Mock<IDatabaseFilms> dbMock = new Mock<IDatabaseFilms>();
        dbMock.Setup(x => x.ReadFilms()).Returns(films);

        // Act
        var result = dbMock.Object.ReadFilms();

        // Assert
        Assert.True(films.SequenceEqual(result));
    }

    [Fact]
    public void CreateFilm_CreerUnFilm()
    {
        // Arrange
        var newFilm = new Film
        {
            Id = ObjectId.GenerateNewId(),
            Titre = "La Matrix",
        };

        Mock<IDatabaseFilms> dbMock = new Mock<IDatabaseFilms>();
        dbMock.Setup(x => x.CreateFilm(It.IsAny<Film>())).Verifiable();

        // Act
        dbMock.Object.CreateFilm(newFilm);

        // Assert
        dbMock.Verify(x => x.CreateFilm(newFilm), Times.Once);
    }

    [Fact]
    public void CreateFilm_ThrowsArgumentNullException_WhenFilmIsNull()
    {
        // Arrange
        Mock<IDatabaseFilms> dbMock = new Mock<IDatabaseFilms>();
        dbMock.Setup(x => x.CreateFilm(It.IsAny<Film>())).Throws(new ArgumentNullException());

        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => dbMock.Object.CreateFilm(null));
    }

    [Fact]
    public void UpdateFilm_UpdatesAFilm()
    {
        // Arrange
        var filmId = ObjectId.GenerateNewId();
        var updatedFilm = new Film
        {
            Id = filmId,
            Titre = "Ze Matrix",
        };

        Mock<IDatabaseFilms> dbMock = new Mock<IDatabaseFilms>();
        dbMock.Setup(x => x.UpdateFilm(It.IsAny<Film>())).Verifiable();

        // Act
        dbMock.Object.UpdateFilm(updatedFilm);

        // Assert
        dbMock.Verify(x => x.UpdateFilm(updatedFilm), Times.Once);
    }

    [Fact]
    public void UpdateFilm_ThrowsArgumentNullException_WhenFilmIsNull()
    {
        // Arrange
        Mock<IDatabaseFilms> dbMock = new Mock<IDatabaseFilms>();
        dbMock.Setup(x => x.UpdateFilm(It.IsAny<Film>())).Throws(new ArgumentNullException());

        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => dbMock.Object.UpdateFilm(null));
    }

    [Fact]
    public void DeleteFilmById_DeletesAFilm()
    {
        // Arrange
        var film = new Film
        {
            Id = ObjectId.GenerateNewId(),
            Titre = "Le Matrix",
        };
        Mock<IDatabaseFilms> dbMock = new Mock<IDatabaseFilms>();
        dbMock.Setup(x => x.DeleteFilmById(It.IsAny<ObjectId>())).Verifiable();

        // Act
        dbMock.Object.DeleteFilmById(film.Id);

        // Assert
        dbMock.Verify(x => x.DeleteFilmById(film.Id), Times.Once);
    }

    [Fact]
    public void DeleteFilmById_ThrowsArgumentNullException_WhenIdIsNull()
    {
        // Arrange
        var filmId = ObjectId.GenerateNewId();
        Mock<IDatabaseFilms> dbMock = new Mock<IDatabaseFilms>();
        dbMock.Setup(x => x.DeleteFilmById(It.IsAny<ObjectId>())).Throws(new ArgumentNullException());

        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => dbMock.Object.DeleteFilmById(filmId));
    }
}
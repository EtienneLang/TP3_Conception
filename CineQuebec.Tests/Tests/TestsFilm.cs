using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using MongoDB.Bson;
using Moq;

namespace CineQuebec.Tests.Tests;

public class TestsFilm
{
    [Fact]
    public void ReadFilms_RetourneUneListeFilms()
    {
        // Arrange
        Mock<IFilmRepository> filmRepoMock = new Mock<IFilmRepository>();
        Mock<IProjectionRepository> projectionRepositoryMock = new Mock<IProjectionRepository>();
        Mock<IAbonneRepository> abonneRepositoryMock = new Mock<IAbonneRepository>();
        Mock<INoteRepository> noteRepositoryMock = new Mock<INoteRepository>();
        filmRepoMock.Setup(x => x.ReadFilms()).Returns(new List<Film>() { new Film(), new Film() });
        FilmService filmService = new FilmService(filmRepoMock.Object, projectionRepositoryMock.Object, abonneRepositoryMock.Object, noteRepositoryMock.Object);
        
        // Act
        List<Film> films = filmService.ReadFilms();
        
        // Assert
        Assert.Equal(2, films.Count);
    }
    
    [Fact]
    public void CreateFilm_CreerUnFilm()
    {
        // Arrange
        Mock<IFilmRepository> filmRepoMock = new Mock<IFilmRepository>();
        filmRepoMock.Setup(x => x.CreateFilm(It.IsAny<Film>()));
        Film film = new Film();
        Mock<IProjectionRepository> projectionRepositoryMock = new Mock<IProjectionRepository>();
        Mock<IAbonneRepository> abonneRepositoryMock = new Mock<IAbonneRepository>();
        Mock<INoteRepository> noteRepositoryMock = new Mock<INoteRepository>();
        FilmService filmService = new FilmService(filmRepoMock.Object, projectionRepositoryMock.Object, abonneRepositoryMock.Object, noteRepositoryMock.Object);
        
        // Act
        filmService.CreateFilm(film);
        
        // Assert
        filmRepoMock.Verify(x => x.CreateFilm(film), Times.Once);
    }
    
    [Fact]
    public void UpdateFilm_ModifierUnFilm()
    {
        // Arrange
        Mock<IFilmRepository> filmRepoMock = new Mock<IFilmRepository>();
        filmRepoMock.Setup(x => x.UpdateFilm(It.IsAny<Film>()));
        Film film = new Film();
        Mock<IProjectionRepository> projectionRepositoryMock = new Mock<IProjectionRepository>();
        Mock<IAbonneRepository> abonneRepositoryMock = new Mock<IAbonneRepository>();
        Mock<INoteRepository> noteRepositoryMock = new Mock<INoteRepository>();
        FilmService filmService = new FilmService(filmRepoMock.Object, projectionRepositoryMock.Object, abonneRepositoryMock.Object, noteRepositoryMock.Object);
        
        // Act
        filmService.UpdateFilm(film);
        
        // Assert
        filmRepoMock.Verify(x => x.UpdateFilm(film), Times.Once);
    }
    
    [Fact]
    public void DeleteFilmById_SupprimerUnFilm()
    {
        // Arrange
        Mock<IFilmRepository> filmRepoMock = new Mock<IFilmRepository>();
        filmRepoMock.Setup(x => x.DeleteFilmById(It.IsAny<MongoDB.Bson.ObjectId>()));
        Mock<IProjectionRepository> projectionRepositoryMock = new Mock<IProjectionRepository>();
        Mock<IAbonneRepository> abonneRepositoryMock = new Mock<IAbonneRepository>();
        Mock<INoteRepository> noteRepositoryMock = new Mock<INoteRepository>();
        FilmService filmService = new FilmService(filmRepoMock.Object, projectionRepositoryMock.Object, abonneRepositoryMock.Object, noteRepositoryMock.Object);
        
        // Act
        filmService.DeleteFilmById(new MongoDB.Bson.ObjectId());
        
        // Assert
        filmRepoMock.Verify(x => x.DeleteFilmById(It.IsAny<MongoDB.Bson.ObjectId>()), Times.Once);
    }

    [Fact]
    public void ReadFilmById_RetourneUnFilm()
    {
        // Arrange
        Mock<IFilmRepository> filmRepoMock = new Mock<IFilmRepository>();
        Film film = new Film();
        filmRepoMock.Setup(x => x.ReadFilmById(It.IsAny<MongoDB.Bson.ObjectId>())).Returns(film);
        Mock<IProjectionRepository> projectionRepositoryMock = new Mock<IProjectionRepository>();
        Mock<IAbonneRepository> abonneRepositoryMock = new Mock<IAbonneRepository>();
        Mock<INoteRepository> noteRepositoryMock = new Mock<INoteRepository>();
        FilmService filmService = new FilmService(filmRepoMock.Object, projectionRepositoryMock.Object,
            abonneRepositoryMock.Object, noteRepositoryMock.Object);

        // Act
        Film filmResult = filmService.ReadFilmById(new ObjectId());
        
        // Assert
        filmRepoMock.Verify(x => x.ReadFilmById(It.IsAny<MongoDB.Bson.ObjectId>()), Times.Once);
    }
}
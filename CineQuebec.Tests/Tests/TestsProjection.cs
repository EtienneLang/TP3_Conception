using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using MongoDB.Bson;
using Moq;

namespace CineQuebec.Tests.Tests;

public class TestsProjection
{
    [Fact]
    public void ReserverPlace_SuccessfulReservation_ReservesPlace()
    {
        // Arrange
        var mockProjectionRepo = new Mock<IProjectionRepository>();
        var projection = new Projection();
        var idAbonne = ObjectId.GenerateNewId();

        mockProjectionRepo.Setup(repo => repo.ReserverPlace(It.IsAny<Projection>(), It.IsAny<ObjectId>()))
            .Verifiable();

        var service = new ProjectionService(mockProjectionRepo.Object);
        // Act
        service.ReserverPlace(projection, idAbonne);

        // Assert
        mockProjectionRepo.Verify(repo => repo.ReserverPlace(It.IsAny<Projection>(), It.IsAny<ObjectId>()), Times.Once);
    }

    [Fact]
    public void CreateProjection_SuccessfulCreation_CreatesProjection()
    {
        // Arrange
        var mockProjectionRepo = new Mock<IProjectionRepository>();
        var projection = new Projection();

        mockProjectionRepo.Setup(repo => repo.CreateProjection(It.IsAny<Projection>())).Verifiable();

        var service = new ProjectionService(mockProjectionRepo.Object);
        // Act
        service.CreateProjection(projection);

        // Assert
        mockProjectionRepo.Verify(repo => repo.CreateProjection(It.IsAny<Projection>()), Times.Once);
    }

    [Fact]
    public void ReadProjectionByFilmId_SuccessfulReading_ReadsProjections()
    {
        // Arrange
        var mockProjectionRepo = new Mock<IProjectionRepository>();
        var idFilm = ObjectId.GenerateNewId();
        var projections = new List<Projection>();
        mockProjectionRepo.Setup(repo => repo.ReadProjectionByFilmId(idFilm)).Returns(projections);

        var service = new ProjectionService(mockProjectionRepo.Object);

        // Act
        var result = service.ReadProjectionByFilmId(idFilm);

        // Assert
        Assert.Equal(projections, result);
        mockProjectionRepo.Verify(repo => repo.ReadProjectionByFilmId(idFilm), Times.Once);
    }

    [Fact]
    public void GetProjectionById_SuccessfulRetrieval_ReturnsProjection()
    {
        // Arrange
        var mockProjectionRepo = new Mock<IProjectionRepository>();
        var id = ObjectId.GenerateNewId();
        var projection = new Projection();

        mockProjectionRepo.Setup(repo => repo.GetProjectionById(id)).Returns(projection);
        var service = new ProjectionService(mockProjectionRepo.Object);

        // Act
        var result = service.GetProjectionById(id);

        // Assert
        Assert.Equal(projection, result);
        mockProjectionRepo.Verify(repo => repo.GetProjectionById(id), Times.Once);
    }

    [Fact]
    public void ReadAvantPremieres_SuccessfulReading_ReturnsProjections()
    {
        // Arrange
        var mockProjectionRepo = new Mock<IProjectionRepository>();
        var projections = new List<Projection>();

        mockProjectionRepo.Setup(repo => repo.ReadAvantPremieres()).Returns(projections);

        var service =
            new ProjectionService(mockProjectionRepo
                .Object);

        // Act
        var result = service.ReadAvantPremieres();

        // Assert
        Assert.Equal(projections, result);
        mockProjectionRepo.Verify(repo => repo.ReadAvantPremieres(), Times.Once);
    }
}
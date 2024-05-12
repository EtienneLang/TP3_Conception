using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;
using Moq;

namespace CineQuebec.Tests.Tests;

public class RealisateurTests
{
    [Fact]
    public void ReadRealisateurs_SuccessfulReading_ReturnsRealisateurs()
    {
        // Arrange
        var mockRealisateurRepository = new Mock<IRealisateurRepository>();
        var realisateurs = new List<Realisateur>(); 
        
        mockRealisateurRepository.Setup(repo => repo.ReadRealisateurs()).Returns(realisateurs);

        var service = new RealisateurService(mockRealisateurRepository.Object); 

        // Act
        var result = service.ReadRealisateurs();

        // Assert
        Assert.Equal(realisateurs, result);
        mockRealisateurRepository.Verify(repo => repo.ReadRealisateurs(), Times.Once);
    }

    [Fact]
    public void ReadRealisateurs_ExceptionThrown_RethrowsException()
    {
        // Arrange
        var mockRealisateurRepository = new Mock<IRealisateurRepository>();

        mockRealisateurRepository.Setup(repo => repo.ReadRealisateurs())
            .Throws(new Exception("An error occurred"));

        var service = new RealisateurService(mockRealisateurRepository.Object);

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => service.ReadRealisateurs());
        Assert.Equal("An error occurred", exception.Message);
    }
    
    [Fact]
    public void ReadRealisateurFromId_SuccessfulReading_ReturnsRealisateur()
    {
        // Arrange
        var mockRealisateurRepository = new Mock<IRealisateurRepository>();
        var realisateur = new Realisateur();
        var realisateurId = ObjectId.GenerateNewId();

        mockRealisateurRepository.Setup(repo => repo.ReadRealisateurFromId(realisateurId)).Returns(realisateur);

        var service = new RealisateurService(mockRealisateurRepository.Object);

        // Act
        var result = service.ReadRealisateurFromId(realisateurId);

        // Assert
        Assert.Equal(realisateur, result);
        mockRealisateurRepository.Verify(repo => repo.ReadRealisateurFromId(realisateurId), Times.Once);
    }
    
    [Fact]
    public void ReadRealisateurFromId_NullRealisateur_ThrowsInexistingEntityException()
    {
        // Arrange
        var mockRealisateurRepository = new Mock<IRealisateurRepository>();
        var realisateurId = ObjectId.GenerateNewId();

        mockRealisateurRepository.Setup(repo => repo.ReadRealisateurFromId(realisateurId)).Returns((Realisateur)null);

        var service = new RealisateurService(mockRealisateurRepository.Object);

        // Act & Assert
        var exception = Assert.Throws<InexistingEntityException>(() => service.ReadRealisateurFromId(realisateurId));
        Assert.Equal("Le réalisateur n'existe pas", exception.Message);
    }
    
    [Fact]
    public void CreateRealisateur_SuccessfulCreation_CreatesRealisateur()
    {
        // Arrange
        var mockRealisateurRepository = new Mock<IRealisateurRepository>();
        var realisateur = new Realisateur { Nom = "Christopher Nolan" };

        mockRealisateurRepository.Setup(repo => repo.CreateRealisateur(realisateur));

        var service = new RealisateurService(mockRealisateurRepository.Object);

        // Act
        service.CreateRealisateur(realisateur);

        // Assert
        mockRealisateurRepository.Verify(repo => repo.CreateRealisateur(realisateur), Times.Once);
    }
    
    [Fact]
    public void CreateRealisateur_EmptyName_ThrowsEmptyNameException()
    {
        // Arrange
        var mockRealisateurRepository = new Mock<IRealisateurRepository>();
        var realisateur = new Realisateur { Nom = "" };
        var service = new RealisateurService(mockRealisateurRepository.Object);

        // Act & Assert
        var exception = Assert.Throws<EmptyNameException>(() => service.CreateRealisateur(realisateur));
        Assert.Equal("Le nom du réalisateur ne peut pas être vide", exception.Message);
    }
    
    [Fact]
    public void CreateRealisateur_NameTooShort_ThrowsInvalidNameLengthException()
    {
        // Arrange
        var mockRealisateurRepository = new Mock<IRealisateurRepository>();
        var realisateur = new Realisateur { Nom = "Al" };

        var service = new RealisateurService(mockRealisateurRepository.Object);

        // Act & Assert
        var exception = Assert.Throws<InvalidNameLengthException>(() => service.CreateRealisateur(realisateur));
        Assert.Equal("Le nom du réalisateur doit contenir entre 3 et 50 charactères", exception.Message);
    }
    
    [Fact]
    public void CreateRealisateur_NameTooLong_ThrowsInvalidNameLengthException()
    {
        // Arrange
        var mockRealisateurRepository = new Mock<IRealisateurRepository>();
        var realisateur = new Realisateur { Nom = "Christopher Nolan Christopher Nolan Christopher Nolan" };

        var service = new RealisateurService(mockRealisateurRepository.Object);

        // Act & Assert
        var exception = Assert.Throws<InvalidNameLengthException>(() => service.CreateRealisateur(realisateur));
        Assert.Equal("Le nom du réalisateur doit contenir entre 3 et 50 charactères", exception.Message);
    }

}
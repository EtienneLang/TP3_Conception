using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using Moq;

namespace CineQuebec.Tests.Tests;

public class TestsRealisateur
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

}
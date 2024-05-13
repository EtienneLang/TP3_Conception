using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using Moq;

namespace CineQuebec.Tests.Tests;

public class AbonneTests
{
    [Fact]
    public void ReadAbonnes_RetourneUneListeAbonnes_WhenSuccessful()
    {
        // Arrange
        Mock<IAbonneRepository> abonneRepoMock = new Mock<IAbonneRepository>();
        Mock<IPreferenceRepository> preferenceRepoMock = new Mock<IPreferenceRepository>();
        abonneRepoMock.Setup(x => x.ReadAbonnes()).Returns(new List<Abonne>() { new Abonne(), new Abonne() });
        AbonneService abonneService = new AbonneService(abonneRepoMock.Object, preferenceRepoMock.Object);
        
        // Act
        List<Abonne> abonnes = abonneService.ReadAbonnes();
        
        // Assert
        Assert.Equal(2, abonnes.Count);
    }
    
    [Fact]
    public void GetAbonneByUsername_RetourneUnAbonne_WhenSuccessful()
    {
        // Arrange
        Mock<IAbonneRepository> abonneRepoMock = new Mock<IAbonneRepository>();
        Mock<IPreferenceRepository> preferenceRepoMock = new Mock<IPreferenceRepository>();
        Abonne abonne = new Abonne();
        abonneRepoMock.Setup(x => x.GetAbonneByUsername("test")).Returns(abonne);
        AbonneService abonneService = new AbonneService(abonneRepoMock.Object, preferenceRepoMock.Object);
        
        // Act
        Abonne abonneResult = abonneService.GetAbonneByUsername("test");
        
        // Assert
        Assert.Equal(abonne, abonneResult);
    }
    
    [Fact]
    public void CreateAbonne_CreerUnAbonne_WhenSuccessful()
    {
        // Arrange
        Mock<IAbonneRepository> abonneRepoMock = new Mock<IAbonneRepository>();
        Mock<IPreferenceRepository> preferenceRepoMock = new Mock<IPreferenceRepository>();
        abonneRepoMock.Setup(x => x.CreateAbonne(It.IsAny<Abonne>()));
        Abonne abonne = new Abonne();
        AbonneService abonneService = new AbonneService(abonneRepoMock.Object, preferenceRepoMock.Object);
        
        // Act
        abonneService.CreateAbonne(abonne);
        
        // Assert
        abonneRepoMock.Verify(x => x.CreateAbonne(abonne), Times.Once);
    }
    
    [Fact]
    public void OffrirBillet_OffrirUnBillet_WhenSuccessful()
    {
        // Arrange
        Mock<IAbonneRepository> abonneRepoMock = new Mock<IAbonneRepository>();
        Mock<IPreferenceRepository> preferenceRepoMock = new Mock<IPreferenceRepository>();
        abonneRepoMock.Setup(x => x.OffrirBillet(It.IsAny<MongoDB.Bson.ObjectId>(), It.IsAny<MongoDB.Bson.ObjectId>()));
        AbonneService abonneService = new AbonneService(abonneRepoMock.Object, preferenceRepoMock.Object);
        
        // Act
        abonneService.OffrirBillet(new MongoDB.Bson.ObjectId(), new MongoDB.Bson.ObjectId());
        
        // Assert
        abonneRepoMock.Verify(x => x.OffrirBillet(It.IsAny<MongoDB.Bson.ObjectId>(), It.IsAny<MongoDB.Bson.ObjectId>()), Times.Once);
    }
}
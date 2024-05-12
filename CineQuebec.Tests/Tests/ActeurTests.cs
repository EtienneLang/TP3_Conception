using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;
using Moq;

namespace CineQuebec.Tests.Tests;

public class ActeurTests
{
    [Fact]
    public void ReadActeurs_RetourneUneListeActeurs_WhenSuccessful()
    {
        // Arrange
        Mock<IActeurRepository> acteurRepoMock = new Mock<IActeurRepository>();
        acteurRepoMock.Setup(x => x.ReadActeurs()).Returns(new List<Acteur>() { new Acteur(), new Acteur() });
        ActeurService acteurService = new ActeurService(acteurRepoMock.Object);
        
        // Act
        List<Acteur> acteurs = acteurService.ReadActeurs();
        
        // Assert
        Assert.Equal(2, acteurs.Count);
    }

    [Fact]
    public void ReadActeurFromId_RetourneUnActeur_WhenSuccessful()
    {
        // Arrange
        Mock<IActeurRepository> acteurRepoMock = new Mock<IActeurRepository>();
        Acteur acteur = new Acteur();
        acteurRepoMock.Setup(x => x.ReadActeurFromId(It.IsAny<ObjectId>())).Returns(acteur);
        ActeurService acteurService = new ActeurService(acteurRepoMock.Object);
        
        // Act
        Acteur acteurResult = acteurService.ReadActeurFromId(ObjectId.GenerateNewId());
        
        // Assert
        Assert.Equal(acteur, acteurResult);
    }
    
    [Fact]
    public void ReadActeurFromId_ThrowInexistingEntityException_WhenIdDoesNotExist()
    {
        // Arrange
        Mock<IActeurRepository> acteurRepoMock = new Mock<IActeurRepository>();
        ObjectId inexistingActeurId = ObjectId.GenerateNewId();
        acteurRepoMock.Setup(x => x.ReadActeurFromId(inexistingActeurId)).Returns((Acteur?)null);
        ActeurService acteurService = new ActeurService(acteurRepoMock.Object);
        
        // Act & Assert
        Assert.Throws<InexistingEntityException>(() => acteurService.ReadActeurFromId(inexistingActeurId));
    }
    
    [Fact]
    public void CreateActeur_CreerUnActeur_WhenSuccessful()
    {
        // Arrange
        Mock<IActeurRepository> acteurRepoMock = new Mock<IActeurRepository>();
        acteurRepoMock.Setup(x => x.CreateActeur(It.IsAny<Acteur>()));
        Acteur acteur = new Acteur() { Nom = "test" };
        ActeurService acteurService = new ActeurService(acteurRepoMock.Object);
        
        // Act
        acteurService.CreateActeur(acteur);
        
        // Assert
        acteurRepoMock.Verify(x => x.CreateActeur(acteur), Times.Once);
    }
    
    [Fact]
    public void CreateActeur_ThrowEmptyNameException_WhenNameIsEmpty()
    {
        // Arrange
        Mock<IActeurRepository> acteurRepoMock = new Mock<IActeurRepository>();
        Acteur acteur = new Acteur();
        ActeurService acteurService = new ActeurService(acteurRepoMock.Object);
        
        // Act & Assert
        Assert.Throws<EmptyNameException>(() => acteurService.CreateActeur(acteur));
    }
    
    [Fact]
    public void CreateActeur_ThrowInvalidNameLengthException_WhenNameIsTooShort()
    {
        // Arrange
        Mock<IActeurRepository> acteurRepoMock = new Mock<IActeurRepository>();
        Acteur acteur = new Acteur() { Nom = "a" };
        ActeurService acteurService = new ActeurService(acteurRepoMock.Object);
        
        // Act & Assert
        Assert.Throws<InvalidNameLengthException>(() => acteurService.CreateActeur(acteur));
    }
    
    [Fact]
    public void UpdateActeur_ModifieUnActeur_WhenSuccessful()
    {
        // Arrange
        Mock<IActeurRepository> acteurRepoMock = new Mock<IActeurRepository>();
        acteurRepoMock.Setup(x => x.UpdateActeur(It.IsAny<Acteur>()));
        Acteur acteur = new Acteur() { Nom = "test" };
        ActeurService acteurService = new ActeurService(acteurRepoMock.Object);
        
        // Act
        acteurService.UpdateActeur(acteur);
        
        // Assert
        acteurRepoMock.Verify(x => x.UpdateActeur(acteur), Times.Once);
    }
    
    [Fact]
    public void UpdateActeur_ThrowEmptyNameException_WhenNameIsEmpty()
    {
        // Arrange
        Mock<IActeurRepository> acteurRepoMock = new Mock<IActeurRepository>();
        Acteur acteur = new Acteur();
        ActeurService acteurService = new ActeurService(acteurRepoMock.Object);
        
        // Act & Assert
        Assert.Throws<EmptyNameException>(() => acteurService.UpdateActeur(acteur));
    }

    [Fact]
    public void UpdateActeur_ThrowInvalidNameLengthException_WhenNameIsToShort()
    {
        // Arrange
        Mock<IActeurRepository> acteurRepoMock = new Mock<IActeurRepository>();
        Acteur acteur = new Acteur() { Nom = "a" };
        ActeurService acteurService = new ActeurService(acteurRepoMock.Object);
        
        // Act & Assert
        Assert.Throws<InvalidNameLengthException>(() => acteurService.UpdateActeur(acteur));
    }
    
    
    [Fact]
    public void DeleteActeur_SupprimerUnActeur_WhenSuccessful()
    {
        // Arrange
        var mockActeurRepository = new Mock<IActeurRepository>();
        var acteurId = ObjectId.GenerateNewId();
        mockActeurRepository.Setup(repo => repo.ReadActeurFromId(acteurId)).Returns((Acteur?)null);
        var acteurService = new ActeurService(mockActeurRepository.Object);
    
        // Act & Assert
        var exception = Assert.Throws<InexistingEntityException>(() => acteurService.DeleteActeur(acteurId));
        Assert.Equal("L'acteur n'existe pas", exception.Message);
        mockActeurRepository.Verify(repo => repo.ReadActeurFromId(acteurId), Times.Once);
    }
    
    [Fact]
    public void DeleteActeur_ActeurDoesNotExist_ThrowsInexistingEntityException_WhenIdDoesNotExists()
    {
        // Arrange
        var mockActeurRepository = new Mock<IActeurRepository>();
        var acteurId = ObjectId.GenerateNewId();
    
        mockActeurRepository.Setup(repo => repo.ReadActeurFromId(acteurId)).Returns((Acteur?)null);
    
        var acteurService = new ActeurService(mockActeurRepository.Object);
    
        // Act & Assert
        var exception = Assert.Throws<InexistingEntityException>(() => acteurService.DeleteActeur(acteurId));
        Assert.Equal("L'acteur n'existe pas", exception.Message);
        mockActeurRepository.Verify(repo => repo.ReadActeurFromId(acteurId), Times.Once);
    }
}
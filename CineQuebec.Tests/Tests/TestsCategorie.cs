using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;
using Moq;

namespace CineQuebec.Tests.Tests;

public class TestsCategorie
{
    [Fact]
    public void ReadCategories_RetourneUneListeCategories()
    {
        // Arrange
        Mock<ICategorieRepository> categorieRepoMock = new Mock<ICategorieRepository>();
        categorieRepoMock.Setup(x => x.ReadCategories())
            .Returns(new List<Categorie>() { new Categorie(), new Categorie() });
        CategorieService categorieService = new CategorieService(categorieRepoMock.Object);

        // Act
        List<Categorie> categories = categorieService.ReadCategories();

        // Assert
        Assert.Equal(2, categories.Count);
    }

    [Fact]
    public void ReadCategorieFromId_RetourneUneCategorie()
    {
        // Arrange
        Mock<ICategorieRepository> categorieRepoMock = new Mock<ICategorieRepository>();
        Categorie categorie = new Categorie();
        categorieRepoMock.Setup(x => x.ReadCategorieFromId(It.IsAny<ObjectId>())).Returns(categorie);
        CategorieService categorieService = new CategorieService(categorieRepoMock.Object);

        // Act
        Categorie categorieResult = categorieService.ReadCategorieFromId(ObjectId.GenerateNewId());

        // Assert
        Assert.Equal(categorie, categorieResult);
    }
    
    [Fact]
    public void ReadCategorieFromId_CategorieInexistante()
    {
        Mock<ICategorieRepository> mockCategorieRepository = new Mock<ICategorieRepository>();
        ObjectId inexistingCategorieId = ObjectId.GenerateNewId();

        mockCategorieRepository.Setup(repo => repo.ReadCategorieFromId(inexistingCategorieId))
            .Returns((Categorie)null);

        var categorieService = new CategorieService(mockCategorieRepository.Object);

        // Act & Assert
        Assert.Throws<InexistingEntityException>(() =>
            categorieService.ReadCategorieFromId(inexistingCategorieId));
    }

    [Fact]
    public void CreateCategorie_CreerUneCategorie()
    {
        // Arrange
        Mock<ICategorieRepository> categorieRepoMock = new Mock<ICategorieRepository>();
        categorieRepoMock.Setup(x => x.CreateCategorie(It.IsAny<Categorie>()));
        Categorie categorie = new Categorie() { NomCategorie = "Test" };
        CategorieService categorieService = new CategorieService(categorieRepoMock.Object);

        // Act
        categorieService.CreateCategorie(categorie);

        // Assert
        categorieRepoMock.Verify(x => x.CreateCategorie(categorie), Times.Once);
    }
    
    [Fact]
    public void CreateCategorie_NomCategorieVide()
    {
        Mock<ICategorieRepository> mockCategorieRepository = new Mock<ICategorieRepository>();
        CategorieService categorieService = new CategorieService(mockCategorieRepository.Object);

        // Act & Assert
        Assert.Throws<EmptyCategorieNameException>(() =>
            categorieService.CreateCategorie(new Categorie { NomCategorie = "" }));
    }

    [Fact]
    public void UpdateCategorie_ModifierUneCategorie()
    {
        // Arrange
        Mock<ICategorieRepository> categorieRepoMock = new Mock<ICategorieRepository>();
        categorieRepoMock.Setup(x => x.UpdateCategorie(It.IsAny<Categorie>()));
        Categorie categorie = new Categorie() { NomCategorie = "Test" };

        CategorieService categorieService = new CategorieService(categorieRepoMock.Object);

        // Act
        categorieService.UpdateCategorie(categorie);

        // Assert
        categorieRepoMock.Verify(x => x.UpdateCategorie(categorie), Times.Once);
    }

    [Fact]
    public void UpdateCategorie_NomCategorieVide()
    {
        Mock<ICategorieRepository> mockCategorieRepository = new Mock<ICategorieRepository>();
        ObjectId inexistingCategorieId = ObjectId.GenerateNewId();

        mockCategorieRepository.Setup(repo => repo.ReadCategorieFromId(inexistingCategorieId))
            .Returns((Categorie)null);

        var categorieService = new CategorieService(mockCategorieRepository.Object);

        // Act & Assert
        Assert.Throws<EmptyCategorieNameException>(() =>
            categorieService.UpdateCategorie(new Categorie { Id = inexistingCategorieId }));
    }
    
    [Fact]
    public void UpdateCategorie_CategorieAlreadyExists()
    {
        Mock<ICategorieRepository> mockCategorieRepository = new Mock<ICategorieRepository>();
        ObjectId existingCategorieId = ObjectId.GenerateNewId();

        mockCategorieRepository.Setup(repo => repo.ReadCategorieFromId(existingCategorieId))
            .Returns(new Categorie() { NomCategorie = "Test" });
        mockCategorieRepository.Setup(repo => repo.ReadCategorieFromName(It.IsAny<string>()))
            .Returns(new Categorie() { NomCategorie = "Test" });

        var categorieService = new CategorieService(mockCategorieRepository.Object);

        // Act & Assert
        Assert.Throws<CategorieAlreadyExistsException>(() =>
            categorieService.UpdateCategorie(new Categorie { NomCategorie = "Test" }));
    }

    [Fact]
    public void DeleteCategorie_SupprimerUneCategorie()
    {
        Mock<ICategorieRepository> mockCategorieRepository = new Mock<ICategorieRepository>();
        ObjectId existingCategorieId = ObjectId.GenerateNewId();

        mockCategorieRepository.Setup(repo => repo.ReadCategorieFromId(existingCategorieId))
            .Returns(new Categorie());
        mockCategorieRepository.Setup(repo => repo.DeleteCategorie(It.IsAny<ObjectId>()))
            .Verifiable();

        var categorieService = new CategorieService(mockCategorieRepository.Object);

        // Act
        categorieService.DeleteCategorie(existingCategorieId);

        // Assert
        mockCategorieRepository.Verify(repo => repo.DeleteCategorie(existingCategorieId), Times.Once);
    }

    [Fact]
    public void DeleteCategorie_CategorieInexistante()
    {
        Mock<ICategorieRepository> mockCategorieRepository = new Mock<ICategorieRepository>();
        ObjectId inexistingCategorieId = ObjectId.GenerateNewId();

        mockCategorieRepository.Setup(repo => repo.ReadCategorieFromId(inexistingCategorieId))
            .Returns((Categorie)null);

        var categorieService = new CategorieService(mockCategorieRepository.Object);

        // Act & Assert
        Assert.Throws<InexistingEntityException>(() => categorieService.DeleteCategorie(inexistingCategorieId));
    }
}
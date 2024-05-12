using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;
using Moq;

namespace CineQuebec.Tests.Tests;

public class PreferenceTests
{
    [Fact]
    public void ReadPreferences_RetourneUneListePreferences_WhenSuccessfulReading()
    {
        // Arrange
        Mock<IPreferenceRepository> preferenceRepoMock = new Mock<IPreferenceRepository>();
        Mock<IAbonneRepository> abonneRepositoryMock = new Mock<IAbonneRepository>();
        preferenceRepoMock.Setup(x => x.ReadPreferences())
            .Returns(new List<Preference>() { new Preference(), new Preference() });
        abonneRepositoryMock.Setup(x => x.ReadAbonnes()).Returns(new List<Abonne>() { new Abonne(), new Abonne() });
        PreferenceService preferenceService =
            new PreferenceService(preferenceRepoMock.Object, abonneRepositoryMock.Object);

        // Act
        List<Preference> preferences = preferenceService.ReadPreference();

        // Assert
        Assert.Equal(2, preferences.Count);
    }

    [Fact]
    public void ReadPreferenceFromId_RetourneUnePreference_WhenSuccessfulReading()
    {
        // Arrange
        Mock<IPreferenceRepository> preferenceRepoMock = new Mock<IPreferenceRepository>();
        Mock<IAbonneRepository> abonneRepositoryMock = new Mock<IAbonneRepository>();
        Preference preference = new Preference();
        preferenceRepoMock.Setup(x => x.ReadPreferenceFromId(It.IsAny<ObjectId>())).Returns(preference);
        abonneRepositoryMock.Setup(x => x.ReadAbonnes()).Returns(new List<Abonne>() { new Abonne(), new Abonne() });
        PreferenceService preferenceService =
            new PreferenceService(preferenceRepoMock.Object, abonneRepositoryMock.Object);

        // Act
        Preference preferenceResult = preferenceService.ReadPreferenceFromId(ObjectId.GenerateNewId());

        // Assert
        Assert.Equal(preference, preferenceResult);
    }

    [Fact]
    public void ReadPreferenceFromId_ThrowInexistingEntityException_WhenIdDoesNotExist()
    {
        // Arrange
        Mock<IPreferenceRepository> preferenceRepoMock = new Mock<IPreferenceRepository>();
        Mock<IAbonneRepository> abonneRepositoryMock = new Mock<IAbonneRepository>();
        ObjectId inexistingPreferenceId = ObjectId.GenerateNewId();
        preferenceRepoMock.Setup(x => x.ReadPreferenceFromId(inexistingPreferenceId)).Returns((Preference?)null);
        abonneRepositoryMock.Setup(x => x.ReadAbonnes()).Returns(new List<Abonne>() { new Abonne(), new Abonne() });
        PreferenceService preferenceService =
            new PreferenceService(preferenceRepoMock.Object, abonneRepositoryMock.Object);

        // Act & Assert
        Assert.Throws<InexistingEntityException>(() => preferenceService.ReadPreferenceFromId(inexistingPreferenceId));
    }

    [Fact]
    public void ReadPreferenceFromUserId_RetourneUnePreference_WhenSuccessfulReading()
    {
        // Arrange
        var mockAbonneRepository = new Mock<IAbonneRepository>();
        var mockPreferenceRepository = new Mock<IPreferenceRepository>();
        var userId = ObjectId.GenerateNewId();
        var preference = new Preference();

        mockAbonneRepository.Setup(repo => repo.ReadAbonneById(userId)).Returns(new Abonne());
        mockPreferenceRepository.Setup(repo => repo.ReadPreferenceFromUserId(userId)).Returns(preference);

        var preferenceService = new PreferenceService(mockPreferenceRepository.Object, mockAbonneRepository.Object);

        // Act
        var result = preferenceService.ReadPreferenceFromUserId(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(preference, result);
    }

    [Fact]
    public void ReadPreferenceFromUserId_ThrowsInexistingUserException_WhenUserDoesNotExist()
    {
        // Arrange
        var mockAbonneRepository = new Mock<IAbonneRepository>();
        var userId = ObjectId.GenerateNewId();

        mockAbonneRepository.Setup(repo => repo.ReadAbonneById(userId)).Returns((Abonne)null);

        var preferenceService =
            new PreferenceService(new Mock<IPreferenceRepository>().Object, mockAbonneRepository.Object);

        // Act & Assert
        var exception =
            Assert.Throws<InexistingUserException>(() => preferenceService.ReadPreferenceFromUserId(userId));
        Assert.Equal("L'utilisateur n'existe pas", exception.Message);
    }

    [Fact]
    public void ReadPreferenceFromUserId_ThrowsInexistingEntityException_WhenPreferencesDoNotExist()
    {
        // Arrange
        var mockAbonneRepository = new Mock<IAbonneRepository>();
        var mockPreferenceRepository = new Mock<IPreferenceRepository>();
        var userId = ObjectId.GenerateNewId();
        var abonne = new Abonne();

        mockAbonneRepository.Setup(repo => repo.ReadAbonneById(userId)).Returns(abonne);
        mockPreferenceRepository.Setup(repo => repo.ReadPreferenceFromUserId(userId)).Returns((Preference)null);

        var preferenceService = new PreferenceService(mockPreferenceRepository.Object, mockAbonneRepository.Object);

        // Act & Assert
        var exception =
            Assert.Throws<InexistingEntityException>(() => preferenceService.ReadPreferenceFromUserId(userId));
        Assert.Equal("Les préférences de l'abonné n'existe pas", exception.Message);
    }

    [Fact]
    public void CreatePreference_CreerUnePreference_WhenSuccessful()
    {
        // Arrange
        var mockAbonneRepository = new Mock<IAbonneRepository>();
        var mockPreferenceRepository = new Mock<IPreferenceRepository>();
        var userId = ObjectId.GenerateNewId();
        var preference = new Preference { UserId = userId };
        var abonne = new Abonne { Id = userId };
        preference.Categories = new List<ObjectId>();
        preference.Realisateurs = new List<ObjectId>();
        preference.Acteurs = new List<ObjectId>();

        mockAbonneRepository.Setup(repo => repo.ReadAbonneById(userId)).Returns(abonne);
        mockPreferenceRepository.Setup(repo => repo.ReadPreferenceFromUserId(userId)).Returns((Preference)null);

        var preferenceService = new PreferenceService(mockPreferenceRepository.Object, mockAbonneRepository.Object);

        // Act
        preferenceService.CreatePreference(preference);

        // Assert
        mockPreferenceRepository.Verify(repo => repo.CreatePreference(preference), Times.Once);
    }

    [Fact]
    public void CreatePreference_ThrowsUserAlreadyHasPreferenceException_WhenUserAlreadyHasPreference()
    {
        // Arrange
        var mockAbonneRepository = new Mock<IAbonneRepository>();
        var mockPreferenceRepository = new Mock<IPreferenceRepository>();
        var userId = ObjectId.GenerateNewId();
        var existingPreference = new Preference { UserId = userId };
        var abonne = new Abonne { Id = userId };

        mockAbonneRepository.Setup(repo => repo.ReadAbonneById(userId)).Returns(abonne);
        mockPreferenceRepository.Setup(repo => repo.ReadPreferenceFromUserId(userId)).Returns(existingPreference);

        var preferenceService = new PreferenceService(mockPreferenceRepository.Object, mockAbonneRepository.Object);

        // Act & Assert
        var exception = Assert.Throws<UserAlreadyHasPreferenceException>(() =>
            preferenceService.CreatePreference(new Preference { UserId = userId }));
        Assert.Equal("L'utilisateur a déjà des préférences prédéfinies", exception.Message);
    }

    [Fact]
    public void CreatePreference_ThrowsTooManyCategorieException_WhenExceedingCategoryLimit()
    {
        // Arrange
        var mockAbonneRepository = new Mock<IAbonneRepository>();
        var mockPreferenceRepository = new Mock<IPreferenceRepository>();
        var userId = ObjectId.GenerateNewId();
        var preference = new Preference
            { UserId = userId, Categories = Enumerable.Range(0, 4).Select(_ => new ObjectId()).ToList() };
        var abonne = new Abonne { Id = userId };

        mockAbonneRepository.Setup(repo => repo.ReadAbonneById(userId)).Returns(abonne);
        mockPreferenceRepository.Setup(repo => repo.ReadPreferenceFromUserId(userId)).Returns((Preference)null);

        var preferenceService = new PreferenceService(mockPreferenceRepository.Object, mockAbonneRepository.Object);

        // Act & Assert
        var exception = Assert.Throws<TooManyCategorieException>(() => preferenceService.CreatePreference(preference));
        Assert.Equal($"Les préférences ne peuvent pas contenir plus de 3 catégories", exception.Message);
    }

    [Fact]
    public void CreatePreference_ThrowsTooManyRealisateurException_WhenExceedingRealisateurLimit()
    {
        // Arrange
        var mockAbonneRepository = new Mock<IAbonneRepository>();
        var mockPreferenceRepository = new Mock<IPreferenceRepository>();
        var userId = ObjectId.GenerateNewId();
        var preference = new Preference
            { UserId = userId, Realisateurs = Enumerable.Range(0, 6).Select(_ => new ObjectId()).ToList() };
        preference.Categories = new List<ObjectId>();
        var abonne = new Abonne { Id = userId };

        mockAbonneRepository.Setup(repo => repo.ReadAbonneById(userId)).Returns(abonne);
        mockPreferenceRepository.Setup(repo => repo.ReadPreferenceFromUserId(userId)).Returns((Preference)null);

        var preferenceService = new PreferenceService(mockPreferenceRepository.Object, mockAbonneRepository.Object);

        // Act & Assert
        var exception =
            Assert.Throws<TooManyRealisateurException>(() => preferenceService.CreatePreference(preference));
        Assert.Equal($"Les préférences ne peuvent pas contenir plus de 5 réalisateurs", exception.Message);
    }

    [Fact]
    public void CreatePreference_ThrowsTooManyActeurException_WhenExceedingActeurLimit()
    {
        // Arrange
        var mockAbonneRepository = new Mock<IAbonneRepository>();
        var mockPreferenceRepository = new Mock<IPreferenceRepository>();
        var userId = ObjectId.GenerateNewId();
        var preference = new Preference
            { UserId = userId, Acteurs = Enumerable.Range(0, 6).Select(_ => new ObjectId()).ToList() };
        preference.Categories = new List<ObjectId>();
        preference.Realisateurs = new List<ObjectId>();
        var abonne = new Abonne { Id = userId };

        mockAbonneRepository.Setup(repo => repo.ReadAbonneById(userId)).Returns(abonne);
        mockPreferenceRepository.Setup(repo => repo.ReadPreferenceFromUserId(userId)).Returns((Preference)null);

        var preferenceService = new PreferenceService(mockPreferenceRepository.Object, mockAbonneRepository.Object);

        // Act & Assert
        var exception = Assert.Throws<TooManyActeurException>(() => preferenceService.CreatePreference(preference));
        Assert.Equal($"Les préférences ne peuvent pas contenir plus de 5 acteurs", exception.Message);
    }

    [Fact]
    public void UpdatePreference_MetAJourUnePreference_WhenSuccessfulUpdate()
    {
        // Arrange
        var mockAbonneRepository = new Mock<IAbonneRepository>();
        var mockPreferenceRepository = new Mock<IPreferenceRepository>();
        var userId = ObjectId.GenerateNewId();
        var preference = new Preference { UserId = userId };
        var abonne = new Abonne { Id = userId };
        preference.Categories = new List<ObjectId>();
        preference.Realisateurs = new List<ObjectId>();
        preference.Acteurs = new List<ObjectId>();

        mockAbonneRepository.Setup(repo => repo.ReadAbonneById(userId)).Returns(abonne);
        mockPreferenceRepository.Setup(repo => repo.UpdatePreference(It.IsAny<Preference>()));

        var preferenceService = new PreferenceService(mockPreferenceRepository.Object, mockAbonneRepository.Object);

        // Act
        preferenceService.UpdatePreference(preference);

        // Assert
        mockPreferenceRepository.Verify(repo => repo.UpdatePreference(preference), Times.Once);
    }

    [Fact]
    public void UpdatePreference_ThrowsTooManyCategorieException_WhenExceedingCategoryLimit()
    {
        // Arrange
        var mockAbonneRepository = new Mock<IAbonneRepository>();
        var mockPreferenceRepository = new Mock<IPreferenceRepository>();
        var userId = ObjectId.GenerateNewId();
        var preference = new Preference { Categories = Enumerable.Range(0, 4).Select(_ => new ObjectId()).ToList() };
        var abonne = new Abonne { Id = userId };

        mockAbonneRepository.Setup(repo => repo.ReadAbonneById(userId)).Returns(abonne);
        mockPreferenceRepository.Setup(repo => repo.UpdatePreference(It.IsAny<Preference>()));

        var preferenceService = new PreferenceService(mockPreferenceRepository.Object, mockAbonneRepository.Object);

        // Act & Assert
        var exception =
            Assert.Throws<TooManyCategorieException>(() => preferenceService.UpdatePreference(preference));
        Assert.Equal($"Les préférences ne peuvent pas contenir plus de 3 catégories",
            exception.Message);
    }

    [Fact]
    public void UpdatePreference_ThrowsTooManyRealisateurException_WhenExceedingRealisateurLimit()
    {
        // Arrange
        var mockAbonneRepository = new Mock<IAbonneRepository>();
        var mockPreferenceRepository = new Mock<IPreferenceRepository>();
        var userId = ObjectId.GenerateNewId();
        var preference = new Preference { Realisateurs = Enumerable.Range(0, 6).Select(_ => new ObjectId()).ToList() };
        var abonne = new Abonne { Id = userId };
        preference.Categories = new List<ObjectId>();

        mockAbonneRepository.Setup(repo => repo.ReadAbonneById(userId)).Returns(abonne);
        mockPreferenceRepository.Setup(repo => repo.UpdatePreference(It.IsAny<Preference>()));

        var preferenceService = new PreferenceService(mockPreferenceRepository.Object, mockAbonneRepository.Object);

        // Act & Assert
        var exception =
            Assert.Throws<TooManyRealisateurException>(() => preferenceService.UpdatePreference(preference));
        Assert.Equal($"Les préférences ne peuvent pas contenir plus de 5 réalisateurs",
            exception.Message);
    }

    [Fact]
    public void UpdatePreference_ThrowsTooManyActeurException_WhenExceedingActeurLimit()
    {
        // Arrange
        var mockAbonneRepository = new Mock<IAbonneRepository>();
        var mockPreferenceRepository = new Mock<IPreferenceRepository>();
        var userId = ObjectId.GenerateNewId();
        var preference = new Preference { Acteurs = Enumerable.Range(0, 6).Select(_ => new ObjectId()).ToList() };
        var abonne = new Abonne { Id = userId };
        preference.Categories = new List<ObjectId>();
        preference.Realisateurs = new List<ObjectId>();

        mockAbonneRepository.Setup(repo => repo.ReadAbonneById(userId)).Returns(abonne);
        mockPreferenceRepository.Setup(repo => repo.UpdatePreference(It.IsAny<Preference>()));

        var preferenceService = new PreferenceService(mockPreferenceRepository.Object, mockAbonneRepository.Object);

        // Act & Assert
        var exception =
            Assert.Throws<TooManyActeurException>(() => preferenceService.UpdatePreference(preference));
        Assert.Equal($"Les préférences ne peuvent pas contenir plus de 5 acteurs",
            exception.Message);
    }

    [Fact]
    public void DeletePreference_DeletesPreference_WhenExistingPreference()
    {
        // Arrange
        var mockAbonneRepository = new Mock<IAbonneRepository>();
        var mockPreferenceRepository = new Mock<IPreferenceRepository>();
        var preferenceId = ObjectId.GenerateNewId();
        var preference = new Preference { Id = preferenceId }; 


        mockPreferenceRepository.Setup(repo => repo.ReadPreferenceFromId(preferenceId)).Returns(preference);
        mockPreferenceRepository.Setup(repo => repo.DeletePreference(preferenceId));

        var preferenceService = new PreferenceService(mockPreferenceRepository.Object, mockAbonneRepository.Object);

        // Act
        preferenceService.DeletePreference(preferenceId);

        // Assert
        mockPreferenceRepository.Verify(repo => repo.DeletePreference(preferenceId), Times.Once);
    }

    [Fact]
    public void DeletePreference_ThrowsInexistingEntityException_WhenNonExistingPreference()
    {
        // Arrange
        var mockAbonneRepository = new Mock<IAbonneRepository>();
        var mockPreferenceRepository = new Mock<IPreferenceRepository>();
        var preferenceId = ObjectId.GenerateNewId();
        
        mockPreferenceRepository.Setup(repo => repo.ReadPreferenceFromId(preferenceId)).Returns((Preference)null);

        var preferenceService = new PreferenceService(mockPreferenceRepository.Object, mockAbonneRepository.Object);

        // Act & Assert
        var exception =
            Assert.Throws<InexistingEntityException>(() => preferenceService.DeletePreference(preferenceId));
        Assert.Equal("L'utilisateur n'as pas de préférences prédéfinies", exception.Message);
    }
}
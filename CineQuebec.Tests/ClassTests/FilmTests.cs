using MongoDB.Bson;
using System.Collections.Generic;
using CineQuebec.Windows.DAL.Data;

public class FilmTests
{
    [Fact]
    public void ToString_RetourneLeTitreDuFilm()
    {
        // Arrange
        var film = new Film
        {
            Id = ObjectId.GenerateNewId(),
            Titre = "Le Matrix",
        };

        // Act
        var resultat = film.ToString();

        // Assert
        Assert.Equal("Le Matrix", resultat);
    }

    [Fact]
    public void Film_ThrowsArgumentNullException_WhenTitreIsNull()
    {
        // Arrange
        var film = new Film();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => film.Titre = "");
    }
}
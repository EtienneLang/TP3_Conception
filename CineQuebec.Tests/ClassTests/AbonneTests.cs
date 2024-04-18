using MongoDB.Bson;
using System.Collections.Generic;
using CineQuebec.Windows.DAL.Data;

public class AbonneTests
{
    [Fact]
    public void ToString_RetourneLeNomDeLAbonne()
    {
        // Arrange
        var abonne = new Abonne
        {
            Id = ObjectId.GenerateNewId(),
            Username = "JohnDoe",
            DateJoined = new DateTime(2021, 1, 1)
        };

        // Act
        var resultat = abonne.ToString();

        // Assert
        Assert.Equal("JohnDoe - Membre depuis 2021/1/1", resultat);
    }
    
    [Fact]
    public void Abonne_ThrowsArgumentNullException_WhenUsernameIsNull()
    {
        // Arrange
        var abonne = new Abonne();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => abonne.Username = "");
    }
}
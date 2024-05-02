using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Tests.ClassTests;

public class ProjectionTest
{
    [Fact]
    public void ToString_RetourneLeTitreDuFilm()
    {
        // Arrange
        var projection = new Projection
        {
            Id = ObjectId.GenerateNewId(),
            Date = new DateTime(2025, 1, 1),
            IdFilm = ObjectId.GenerateNewId(),
        };
        
        DateTime date = new DateTime(2025, 1, 1);
        // Act
        var resultat = projection.ToString();
        
        // Assert
        Assert.Equal($"{date.Day}/{date.Month}/{date.Year} à {date.Hour}h{date.Minute:d2}", resultat);
    }
    
    [Fact]
    public void Projection_ThrowsException_WhenDateIsBeforeNow()
    {
        // Arrange
        var projection = new Projection();
        
        // Act & Assert
        Assert.Throws<Exception>(() => projection.Date = new DateTime(2020, 1, 1));
    }
    
    [Fact]
    public void Projection_ThrowsException_WhenDateIsEmpty()
    {
        // Arrange
        var projection = new Projection();
        
        // Act & Assert
        Assert.Throws<Exception>(() => projection.Date = DateTime.MinValue);
    }
    
    [Fact]
    public void Projection_ThrowsException_WhenIdFilmIsEmpty()
    {
        // Arrange
        var projection = new Projection();
        
        // Act & Assert
        Assert.Throws<Exception>(() => projection.IdFilm = ObjectId.Empty);
    }
    
    [Fact]
    public void Projection_ThrowsException_WhenIdIsEmpty()
    {
        // Arrange
        var projection = new Projection();
        
        // Act & Assert
        Assert.Throws<Exception>(() => projection.Id = ObjectId.Empty);
    }
}
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;

namespace CineQuebec.Tests.Tests;
using CineQuebec.Windows.BLL.Services;
using MongoDB.Bson;
using Moq;
public class TestsAuth
{
    [Fact]
    public void AbonneExiste_RetourneVrai_SiAbonnerExiste()
    {
        Mock<IAuthRepository> authRepoMock = new Mock<IAuthRepository>();
        authRepoMock.Setup(x => x.AbonneExiste(It.IsAny<string>())).Returns(true);
        AuthService authService = new AuthService(authRepoMock.Object);
        
        bool abonneExiste = authService.AbonneExiste("test");
        
        Assert.True(abonneExiste);
    }
    
    [Fact]
    public void AbonneExiste_RetourneFalse_SiAbonnerExistePas()
    {
        Mock<IAuthRepository> authRepoMock = new Mock<IAuthRepository>();
        authRepoMock.Setup(x => x.AbonneExiste(It.IsAny<string>())).Returns(false);
        AuthService authService = new AuthService(authRepoMock.Object);
        
        bool abonneExiste = authService.AbonneExiste("test");
        
        Assert.False(abonneExiste);
    }
    
    [Fact]
    public void Login_RetourneUnAbonne()
    {
        Mock<IAuthRepository> authRepoMock = new Mock<IAuthRepository>();
        Abonne abonne = new Abonne();
        authRepoMock.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(abonne);
        AuthService authService = new AuthService(authRepoMock.Object);
        
        Abonne abonneResult = authService.Login("test", "test");
        
        Assert.Equal(abonne, abonneResult);
    }
}
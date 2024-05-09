using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.BLL.Interfaces;

public interface IAuthService
{
    bool AbonneExiste(string username);
    Abonne? Login(string username, string password);
}
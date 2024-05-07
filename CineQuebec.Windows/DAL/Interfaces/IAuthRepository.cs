using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.DAL.Interfaces;

public interface IAuthRepository
{
    Boolean AbonneExiste(string username);
    Abonne Login(string username, string password);
}
using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;
using System.Text;
using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;

namespace CineQuebec.Windows.BLL.Services;

public class AuthService : IAuthService
{
    private static readonly byte[] Salt = Encoding.UTF8.GetBytes("SecretSalt");
    private IAuthRepository _authRepository;
    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    public bool AbonneExiste(string username)
    {
        try
        {
            return _authRepository.AbonneExiste(username);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public Abonne? Login(string username, string password)
    {
        try
        {
            string hashedPassword = HashPassword(password);
            return _authRepository.Login(username, hashedPassword);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    //Code emprunter
    public static string HashPassword(string password)
    {
        byte[] hash = PBKDF2(password, Salt, 10000, 32);
        return Convert.ToBase64String(Salt) + ":" + Convert.ToBase64String(hash);
    }

    private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
        {
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
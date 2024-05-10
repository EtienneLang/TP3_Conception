using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Services;

public class AbonneService : IAbonneService
{
    private readonly IAbonneRepository _abonneRepo;
    
    public AbonneService(IAbonneRepository abonneRepo)
    {
        _abonneRepo = abonneRepo;
    }
    public Abonne GetAbonneByUsername(string username)
    {
        return _abonneRepo.GetAbonneByUsername(username);
    }

    public void CreateAbonne(Abonne abonne)
    {
        try
        {
            _abonneRepo.CreateAbonne(abonne);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<Abonne> ReadAbonnes()
    {
       return _abonneRepo.ReadAbonnes();
    }

    public void OffrirBillet(ObjectId idAbonne, ObjectId idFilm)
    {
        _abonneRepo.OffrirBillet(idAbonne, idFilm);
    }
}
using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Services;

public class AbonneService : IAbonneService
{
    private readonly IAbonneRepository _abonneRepo;
    private readonly IPreferenceRepository _preferenceRepository;
    
    public AbonneService(IAbonneRepository abonneRepo, IPreferenceRepository preferenceRepository)
    {
        _abonneRepo = abonneRepo;
        _preferenceRepository = preferenceRepository;
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

    public List<Abonne> ReadAbonnesInterestedInCategorie(ObjectId categorieId)
    {
        List<Preference> preferences = _preferenceRepository.ReadPreferenceFromCategorieId(categorieId);
        List<Abonne> abonnes = new List<Abonne>();
        foreach (Preference preference in preferences)
        {
            abonnes.Add(_abonneRepo.ReadAbonneById(preference.UserId));
        }
        return abonnes.OrderByDescending(kv => kv.Reservations.Count).ToList();
    }

    public List<Abonne> ReadAbonnesInterestedInActeurAndCategorie(List<ObjectId> acteursIds, List<ObjectId> realisateursIds)
    {
        List<Preference> preferences =
            _preferenceRepository.ReadPreferencesFromActeursAndRealisateurs(acteursIds, realisateursIds);
        List<Abonne> abonnes = new List<Abonne>();
        foreach (Preference preference in preferences)
        {
            abonnes.Add(_abonneRepo.ReadAbonneById(preference.UserId));
        }
        return abonnes.OrderByDescending(kv => kv.Reservations.Count).ToList();
    }
}
using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Services;

public class PreferenceService : IPreferenceService
{
    private int NB_MAX_CATEGORIES = 3;
    private int NB_MAX_REALISATEUR = 5;
    private int NB_MAX_ACTEURS = 5;
    private IPreferenceRepository _preferenceRepository;
    private IAbonneRepository _abonneRepository;

    public PreferenceService(IPreferenceRepository preferenceRepository, IAbonneRepository abonneRepository)
    {
        _preferenceRepository = preferenceRepository;
        _abonneRepository = abonneRepository;
    }
    public List<Preference> ReadPreference()
    {
        try
        {
            return _preferenceRepository.ReadPreferences();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Preference ReadPreferenceFromId(ObjectId preferenceId)
    {
        try
        {
            Preference preference = _preferenceRepository.ReadPreferenceFromId(preferenceId);
            if (preference == null)
            {
                throw new InexistingEntityException("La préférence n'existe pas");
            }

            return preference;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Preference ReadPreferenceFromUserId(ObjectId userId)
    {
        try
        {
            Abonne abonne = _abonneRepository.ReadAbonneById(userId);
            if (abonne == null)
            {
                throw new InexistingUserException("L'utilisateur n'existe pas");
            }

            Preference preference = _preferenceRepository.ReadPreferenceFromUserId(userId);
            if (preference == null)
            {
                throw new InexistingEntityException("Les préférences de l'abonné n'existe pas");
            }

            return preference;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void CreatePreference(Preference preference)
    {
        try
        {
            Preference existingPreference = _preferenceRepository.ReadPreferenceFromUserId(preference.UserId);
            if (existingPreference != null)
            {
                throw new UserAlreadyHasPreferenceException("L'utilisateur a déjà des préférences prédéfinies");
            }

            if (preference.Categories.Count > NB_MAX_CATEGORIES)
            {
                throw new TooManyCategorieException(
                    $"Les préférences ne peuvent pas contenir plus de {NB_MAX_CATEGORIES} catégories");
            }
            if (preference.Realisateurs.Count > NB_MAX_REALISATEUR)
            {
                throw new TooManyRealisateurException(
                    $"Les préférences ne peuvent pas contenir plus de {NB_MAX_REALISATEUR} réalisateurs");
            }
            if (preference.Acteurs.Count > NB_MAX_ACTEURS)
            {
                throw new TooManyActeurException(
                    $"Les préférences ne peuvent pas contenir plus de {NB_MAX_ACTEURS} acteurs");
            }
            _preferenceRepository.CreatePreference(preference);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdatePreference(Preference newPreference)
    {
        if (newPreference.Categories.Count > NB_MAX_CATEGORIES)
        {
            throw new TooManyCategorieException(
                $"Les préférences ne peuvent pas contenir plus de {NB_MAX_CATEGORIES} catégories");
        }
        if (newPreference.Realisateurs.Count > NB_MAX_REALISATEUR)
        {
            throw new TooManyRealisateurException(
                $"Les préférences ne peuvent pas contenir plus de {NB_MAX_REALISATEUR} réalisateurs");
        }
        if (newPreference.Acteurs.Count > NB_MAX_ACTEURS)
        {
            throw new TooManyActeurException(
                $"Les préférences ne peuvent pas contenir plus de {NB_MAX_ACTEURS} acteurs");
        }
        try
        {
            _preferenceRepository.UpdatePreference(newPreference);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeletePreference(ObjectId preferenceId)
    {
        try
        {
            Preference preference = _preferenceRepository.ReadPreferenceFromId(preferenceId);
            if (preference == null)
            {
                throw new InexistingEntityException("L'utilisateur n'as pas de préférences prédéfinies");
            }
            _preferenceRepository.DeletePreference(preferenceId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
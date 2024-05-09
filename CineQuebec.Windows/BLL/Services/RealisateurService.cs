using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Services;

public class RealisateurService : IRealisateurService
{
    private IRealisateurRepository _realisateurRepository;
    public RealisateurService(IRealisateurRepository realisateurRepository)
    {
        _realisateurRepository = realisateurRepository;
    }
    public List<Realisateur> ReadRealisateurs()
    {
        try
        {
            return _realisateurRepository.ReadRealisateurs();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Realisateur ReadRealisateurFromId(ObjectId realisateurId)
    {
        try
        {
            Realisateur realisateur = _realisateurRepository.ReadRealisateurFromId(realisateurId);
            if (realisateur == null)
            {
                throw new InexistingEntityException("Le réalisateur n'existe pas");
            }
            return realisateur;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void CreateRealisateur(Realisateur realisateur)
    {
        if (String.IsNullOrWhiteSpace(realisateur.Nom))
        {
            throw new EmptyNameException("Le nom du réalisateur ne peut pas être vide");
        }
        realisateur.Nom = realisateur.Nom.Trim();
        if (realisateur.Nom.Length < 3 || realisateur.Nom.Length > 50)
        {
            throw new InvalidNameLengthException("Le nom du réalisateur doit contenir entre 3 et 50 charactères");
        }
        try
        {
            _realisateurRepository.CreateRealisateur(realisateur);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateRealisateur(Realisateur realisateur)
    {
        if (String.IsNullOrWhiteSpace(realisateur.Nom))
        {
            throw new EmptyNameException("Le nom du réalisateur ne peut pas être vide");
        }
        realisateur.Nom = realisateur.Nom.Trim();
        if (realisateur.Nom.Length < 3 || realisateur.Nom.Length > 50)
        {
            throw new InvalidNameLengthException("Le nom du réalisateur doit contenir entre 3 et 50 charactères");
        }
        try
        {
            _realisateurRepository.UpdateRealisateur(realisateur);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteRealisateur(ObjectId realisateurId)
    {
        Realisateur? realisateur = null;
        try
        {
            realisateur = _realisateurRepository.ReadRealisateurFromId(realisateurId);
            if (realisateur == null)
            {
                throw new InexistingEntityException("Le réalisateur n'existe pas");
            }
            _realisateurRepository.DeleteRealisateur(realisateurId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
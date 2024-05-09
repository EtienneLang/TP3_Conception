using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Services;

public class ActeurService : IActeurService
{
    private IActeurRepository _acteurRepository;

    public ActeurService(IActeurRepository acteurRepository)
    {
        _acteurRepository = acteurRepository;
    }
    public List<Acteur> ReadActeurs()
    {
        try
        {
            return _acteurRepository.ReadActeurs();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Acteur ReadActeurFromId(ObjectId acteurId)
    {
        try
        {
            Acteur acteur = _acteurRepository.ReadActeurFromId(acteurId);
            if (acteur == null)
            {
                throw new InexistingEntityException("L'acteur n'existe pas");
            }
            return acteur;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void CreateActeur(Acteur acteur)
    {
        if (String.IsNullOrWhiteSpace(acteur.Nom))
        {
            throw new EmptyNameException("Le nom de l'acteur ne peut pas être vide");
        }
        acteur.Nom = acteur.Nom.Trim();
        if (acteur.Nom.Length < 3 || acteur.Nom.Length > 50)
        {
            throw new InvalidNameLengthException("Le nom de l'acteur doit contenir entre 3 et 50 charactères");
        }
        try
        {
            _acteurRepository.CreateActeur(acteur);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateActeur(Acteur newActeur)
    {
        if (String.IsNullOrWhiteSpace(newActeur.Nom))
        {
            throw new EmptyNameException("Le nom de l'acteur ne peut pas être vide");
        }
        newActeur.Nom = newActeur.Nom.Trim();
        if (newActeur.Nom.Length < 3 || newActeur.Nom.Length > 50)
        {
            throw new InvalidNameLengthException("Le nom de l'acteur doit contenir entre 3 et 50 charactères");
        }
        try
        {
            _acteurRepository.UpdateActeur(newActeur);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteActeur(ObjectId acteurId)
    {
        Acteur? acteur = null;
        try
        {
            acteur = _acteurRepository.ReadActeurFromId(acteurId);
            if (acteur == null)
            {
                throw new InexistingEntityException("L'acteur n'existe pas");
            }
            _acteurRepository.DeleteActeur(acteurId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}
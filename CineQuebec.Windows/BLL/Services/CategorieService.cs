using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.Repositories;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Services;

public class CategorieService : ICategorieService
{
    private ICategorieRepository _categorieRepository;

    public CategorieService(ICategorieRepository categorieRepository)
    {
        _categorieRepository = categorieRepository;
    }
    public List<Categorie> ReadCategories()
    {
        try
        {
            return _categorieRepository.ReadCategories();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Categorie ReadCategorieFromId(ObjectId categorieId)
    {
        try
        {
            Categorie categorie = null;
            categorie = _categorieRepository.ReadCategorieFromId(categorieId);
            if (categorie == null)
            {
                throw new InexistingEntityException("La catégorie n'existe pas");
            }

            return categorie;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void CreateCategorie(Categorie categorie)
    {
        try
        {
            if (String.IsNullOrWhiteSpace(categorie.NomCategorie))
            {
                throw new EmptyCategorieNameException("Le nom de la catégorie ne peut pas être vide");
            }
            Categorie? categorieTrouvee = _categorieRepository.ReadCategorieFromName(categorie.NomCategorie);
            if (categorieTrouvee != null)
            {
                throw new CategorieAlreadyExistsException("La catégorie existe déjà");
            }
            _categorieRepository.CreateCategorie(categorie);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateCategorie(Categorie newCategorie)
    {
        try
        {
            if (String.IsNullOrWhiteSpace(newCategorie.NomCategorie))
            {
                throw new EmptyCategorieNameException("Le nom de la catégorie ne peut pas être vide");
            }
            Categorie? categorieTrouvee = _categorieRepository.ReadCategorieFromName(newCategorie.NomCategorie);
            if (categorieTrouvee != null)
            {
                throw new CategorieAlreadyExistsException("La catégorie existe déjà");
            }
            _categorieRepository.UpdateCategorie(newCategorie);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteCategorie(ObjectId categorieId)
    {
        try
        {
            Categorie categorie = _categorieRepository.ReadCategorieFromId(categorieId);
            if (categorie == null)
            {
                throw new InexistingEntityException("La catégorie n'existe pas");
            }
            _categorieRepository.DeleteCategorie(categorieId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
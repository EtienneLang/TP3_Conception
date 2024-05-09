using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Interfaces;

public interface ICategorieRepository
{
    List<Categorie> ReadCategories();
    Categorie ReadCategorieFromId(ObjectId categorieId);
    void CreateCategorie(Categorie categorie);
    void UpdateCategorie(Categorie newCategorie);
    void DeleteCategorie(ObjectId categorieId);
}
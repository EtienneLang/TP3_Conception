using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Interfaces;

public interface ICategorieService
{
    List<Categorie> ReadCategories();
    Categorie ReadCategorieFromId(ObjectId categorieId);
    void CreateCategorie(Categorie categorie);
    void UpdateCategorie(Categorie newCategorie);
    void DeleteCategorie(ObjectId categorieId);
}
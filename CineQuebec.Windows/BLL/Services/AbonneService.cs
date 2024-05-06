using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using CineQuebec.Windows.DAL.Repositories;
using MongoDB.Bson;


namespace CineQuebec.Windows.DAL;

public class AbonneService : IAbonneService
{
    private readonly IMongoClient _mongoDBClient;
    private readonly IMongoDatabase _database;
    private readonly IAbonneRepository _abonneRepo;
    
    public AbonneService(IAbonneRepository abonneRepo)
    {
        _abonneRepo = abonneRepo;
    }
    public Abonne GetAbonneByUsername(string username)
    {
        return _abonneRepo.GetAbonneByUsername(username);
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
using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL
{
    public class ProjectionService : IProjectionService
    {
        private readonly IProjectionRepository _projectionRepo;
        private readonly IAbonneRepository _abonneRepo;

        public ProjectionService(IProjectionRepository projectionRepository)
        {
            _projectionRepo = projectionRepository;
        }
        
        public void ReserverPlace(Projection projection, ObjectId idAbonne)
        {
            try
            {
                _projectionRepo.ReserverPlace(projection, idAbonne);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Projection GetProjectionById(ObjectId id)
        {
            try
            {
                return _projectionRepo.GetProjectionById(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public virtual List<Projection> ReadAvantPremieres()
        {
            try
            {
                return _projectionRepo.ReadAvantPremieres();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
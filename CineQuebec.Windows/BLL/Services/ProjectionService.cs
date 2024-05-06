using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using CineQuebec.Windows.BLL.Interfaces;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Services
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
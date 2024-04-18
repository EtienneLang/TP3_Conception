using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.DAL.Interfaces;

public interface IDatabaseAbonnes
{
    List<Abonne> ReadAbonnes();
}
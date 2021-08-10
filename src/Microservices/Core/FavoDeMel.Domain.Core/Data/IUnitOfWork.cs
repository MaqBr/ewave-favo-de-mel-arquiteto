using System.Threading.Tasks;

namespace FavoDeMel.Domain.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
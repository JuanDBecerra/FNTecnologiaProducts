using Prueba.Domain.Entities.Model;

namespace Prueba.Domain.Interfaces
{
    public interface IMediatorService
    {
        IQueryable<Category> GetCategory();
        IQueryable<Status> GetStatus();
    }
}

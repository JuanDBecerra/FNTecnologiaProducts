using Prueba.Domain.Entities.Model;
using Prueba.Domain.Interfaces;

namespace Prueba.Application.Services
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        public CategoryService(IRepository<Category> repository, IMediatorService mediator) : base(repository, mediator) { }

    }
}

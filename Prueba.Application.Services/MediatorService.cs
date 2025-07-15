using Microsoft.Extensions.DependencyInjection;
using Prueba.Domain.Entities.Model;
using Prueba.Domain.Interfaces;

namespace Prueba.Application.Services
{
    public class MediatorService : IMediatorService
    {
        private readonly IServiceProvider _serviceProvider;

        public MediatorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private T GetService<T>()
        {
            return _serviceProvider.GetService<T>()
                   ?? throw new KeyNotFoundException("No se encontró servicio disponible");
        }

        #region Category

        public IQueryable<Category> GetCategory()
            => GetService<ICategoryService>().Get();

        #endregion

        #region Status

        public IQueryable<Status> GetStatus()
            => GetService<IStatusService>().Get();

        #endregion

    }
}

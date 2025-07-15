using Prueba.Domain.Entities.Model;
using Prueba.Domain.Interfaces;

namespace Prueba.Application.Services
{
    public class StatusService : GenericService<Status>, IStatusService
    {
        public StatusService(IRepository<Status> repository, IMediatorService mediator) : base(repository, mediator) { }
    }
}

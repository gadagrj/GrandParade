using Convey.CQRS.Queries;
using GrandParade.Registration.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;

namespace GrandParade.Registration.Interface
{

    /// <summary>
    /// Registration services
    /// </summary>
    public interface IRegistration
    {
        Task<PagedResult<RegistrationBaseDTO>> GetAll(SearchPaginationQuery query);
        Task<RegistrationBaseDTO> GetAsync(Guid orderId);
        Task Register(RegistrationBaseDTO registration);
    }
}

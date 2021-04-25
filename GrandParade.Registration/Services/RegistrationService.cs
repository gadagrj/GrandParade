using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using GrandParade.Registration.DTO;
using GrandParade.Registration.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandParade.Registration.Services
{
    public class RegistrationService : IRegistration
    {
        private readonly IMongoRepository<RegistrationBaseDTO, Guid> _repository;
        private readonly ILogger<RegistrationService> _logger;
        public RegistrationService(IMongoRepository<RegistrationBaseDTO, Guid> repository, ILogger<RegistrationService> logger)
        {
            _repository = repository;
            _logger = logger;

        }

        public async Task<PagedResult<RegistrationBaseDTO>> GetAll(SearchPaginationQuery query)
        {
            var results = await _repository.BrowseAsync(x => x.Id !=Guid.Empty, query);
            return results;
        }

        public async Task<RegistrationBaseDTO> GetAsync(Guid registrationID)
        {
            var exists = await _repository.ExistsAsync(r => r.Id == registrationID);
            if (!exists)
            {
                throw new InvalidOperationException($"Registration with given id: {registrationID} is not exists!");
            }

            var registration = await _repository.GetAsync(registrationID);

            return registration;

        }

        public async Task Register(RegistrationBaseDTO registration)
        {
            try
            {
                _logger.LogInformation($"creating registration for {registration.Id}");

                await _repository.AddAsync(registration);
                _logger.LogInformation($"registration is success");
            }
            catch (Exception e)
            {
              _logger.LogError(e.StackTrace);
                throw;
            }
        }
    }
}

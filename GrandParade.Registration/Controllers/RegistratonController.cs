using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.WebApi;
using GrandParade.Registration.DTO;
using GrandParade.Registration.Enum;
using GrandParade.Registration.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrandParade.Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistratonController : ControllerBase
    {
        private readonly IRegistration _registrationService;

        public RegistratonController(IRegistration registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<RegistrationBaseDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PagedResult<RegistrationBaseDTO>>> Get([FromQuery] SearchPaginationQuery query)
        {
            var registration = await _registrationService.GetAll(query);
            if (registration is null || !registration.Items.Any())
            {
                return NotFound();
            }

            return registration;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RegistrationBaseDTO),201)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RegistrationBaseDTO>> Get(Guid id)
        {
            var registration = await _registrationService.GetAsync(id);
            if (registration is null)
            {
                return NotFound();
            }

            return registration;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Post([FromBody] RegistrationBaseDTO registration)
        {
            if (registration.Brand == BrandType.None)
            {
                return BadRequest("Please provide the associated Brand for Registration");
            }
            await _registrationService.Register(registration);
            return CreatedAtAction(nameof(Get), new { id = registration.Id }, new object());
        }
    }

    
}

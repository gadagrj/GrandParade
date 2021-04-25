using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandParade.Registration.Enum;

namespace GrandParade.Registration.DTO
{
    public class RedBetRegistrationDto:RegistrationBaseDTO
    {
        public string TeamName { get; set; }

        public RedBetRegistrationDto(BrandType brand) : base(brand)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandParade.Registration.Enum;

namespace GrandParade.Registration.DTO
{
    public class GreenRegistrationDTO :RegistrationBaseDTO
    {
        public int PersonalNumber { get; set; }

        public GreenRegistrationDTO(BrandType brand) : base(brand)
        {
        }
    }
}

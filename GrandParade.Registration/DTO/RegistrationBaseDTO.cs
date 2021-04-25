using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks    ;
using Convey.Types;
using GrandParade.Registration.Enum;
using GrandParade.Registration.Formatters;
using Newtonsoft.Json;

namespace GrandParade.Registration.DTO
{
    [JsonConverter(typeof(RegistrationJSONConverter))]
    public class RegistrationBaseDTO : IIdentifiable<Guid>
    {
        public RegistrationBaseDTO(BrandType brand)
        {
            Brand = brand;
        }
        public BrandType Brand { get; set; }
        
        public Guid Id { get;  set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressDTO Address { get; set; }
    }


    

}

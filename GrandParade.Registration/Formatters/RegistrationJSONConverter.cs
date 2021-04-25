using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandParade.Registration.DTO;
using GrandParade.Registration.Enum;
using Newtonsoft.Json.Linq;

namespace GrandParade.Registration.Formatters
{
    public class RegistrationJSONConverter: JSONResponseConvertor<RegistrationBaseDTO>
    {
        protected override RegistrationBaseDTO Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");

            if (jObject["TeamName"] != null)
            {
                return new RedBetRegistrationDto(BrandType.RedBet);
            }
            else if (jObject["PersonalNumber"] != null)
            {
                return new GreenRegistrationDTO(BrandType.MrGreen);
            }
            else
            {
                return new RegistrationBaseDTO(BrandType.None);
            }
        }
    }
}

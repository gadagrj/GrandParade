using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Convey.CQRS.Queries;
using GrandParade.Registration.DTO;
using GrandParade.Registration.Enum;

namespace GrandParade.API.Tests.MoqData
{
    public static class TestData
    {

        public static PagedResult<RegistrationBaseDTO> GetList()
        {
            var list = PagedResult<RegistrationBaseDTO>.Create(GetData(),1, 10, 1, 10);
            return list;
        }

        private static List<RegistrationBaseDTO> GetData()
        {
            var list = new List<RegistrationBaseDTO>();
            var data = new RegistrationBaseDTO(BrandType.MrGreen)
            {
              
                FirstName = "TestGreen",
                LastName = "User",
                Address = new AddressDTO() {StreetName = "TestStreet", ZipCode = "31-503"}
            };
            list = Enumerable.Repeat(data, 5).ToList();
            data = new RegistrationBaseDTO(BrandType.RedBet)
            {
                FirstName = "TestBet",
                LastName = "User",
                Address = new AddressDTO() { StreetName = "TestStreet", ZipCode = "31-503" }
            };
            list.AddRange(Enumerable.Repeat(data, 5).ToList());
            list[0].Id = Guid.Parse("4ba2df9c-21ec-934d-a50e-335f4c55bbef");
            return list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GrandParade.Registration.Enum
{
    public enum BrandType
    {
        [EnumMember(Value = "None")]
        None,

        [EnumMember(Value = "MrGreen")]
        MrGreen,

        [EnumMember(Value = "RedBet")]
        RedBet
    }
}

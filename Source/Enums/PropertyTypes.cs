using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source.Enums
{
    [Flags]
    public enum PropertyTypes
    {
        Default = 0,
        House = 1,
        Apartment,
        Unit,
        Townhouse,
        Villa,
        Duplex,
        Land,
        Acereage
    }
}

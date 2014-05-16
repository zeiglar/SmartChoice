using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source.Estates
{
    public class Address
    {
        #region Properties
        public string NoUnit { get; set; }
        public string NoStreet { get; set; }
        public string Street { get; set; }
        public Suburb Suburb { get; set; }
        #endregion

        public bool Equals(Address address)
        {
            if (address == null)
                return false;

            return NoUnit == address.NoUnit && NoStreet == address.NoStreet
                && Street == address.Street && Suburb == address.Suburb;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Address address = obj as Address;
            if (address == null)
                return false;

            return Equals(address);
        }

        public override int GetHashCode()
        {
            int hash = 17;

            hash *= 23 + NoUnit.GetHashCode();
            hash *= 23 + NoStreet.GetHashCode();
            hash *= 23 + Street.GetHashCode();
            hash *= 23 + Suburb.GetHashCode();

            return hash;
        }
    }
}

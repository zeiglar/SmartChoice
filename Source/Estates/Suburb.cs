using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source.Estates
{
    [DelimitedRecord(","), IgnoreFirst(1)]
    public class Suburb
    {
        #region Properties
        public string Name { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        #endregion

        public bool Equals(Suburb suburb)
        {
            if (suburb == null)
                return false;

            return Name == suburb.Name && Postcode == suburb.Postcode
                && State == suburb.State;
        }

        #region Overrides
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Suburb suburb = obj as Suburb;
            if (suburb == null)
                return false;

            return Equals(suburb);
        }

        public override int GetHashCode()
        {
            int hash = 17;

            hash *= 23 + Name.GetHashCode();
            hash *= 23 + Postcode.GetHashCode();
            hash *= 23 + State.GetHashCode();

            return hash;
        }

        public static bool operator ==(Suburb subA, Suburb subB)
        {
            if (ReferenceEquals(subA, subB))
                return true;

            if ((object)subA == null || (object)subB == null)
                return false;

            return subA.Name == subB.Name && subA.Postcode == subB.Postcode
                && subA.State == subB.State;
        }

        public static bool operator !=(Suburb subA, Suburb subB)
        {
            return !(subA == subB);
        }
        #endregion
    }
}

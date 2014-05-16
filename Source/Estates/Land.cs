using Source.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source.Estates
{
    public class Land
    {
        #region Properties
        public int LandSize { get; set; }
        public LandTypes LandType { get; set; }
        #endregion

        #region Functions
        public bool Equals(Land land)
        {
            if (land == null) return false;

            return LandSize == land.LandSize && LandType == land.LandType;
        }
        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Land land = obj as Land;
            if (land == null)
                return false;

            return Equals(land);
        }

        public override int GetHashCode()
        {
            int hash = 17;

            hash *= 23 + LandSize;
            hash *= 23 + LandType.GetHashCode();

            return hash;
        }

        #endregion

    }
}

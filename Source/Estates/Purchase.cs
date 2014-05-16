using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source.Estates
{
    public class Purchase
    {
        #region Purchase Cost
        public long Price { get; set; }
        public long Duty { get; set; }
        public long Levy { get; set; }

        public DateTimeOffset SaleDate { get; set; }
        #endregion

        public long GetTotalCost()
        {
            return this.Price + this.Duty + this.Levy;
        }
    }
}

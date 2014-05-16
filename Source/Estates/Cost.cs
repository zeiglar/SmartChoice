using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source.Estates
{
    public class Cost
    {
        #region Properties
        // Quarter Cost
        public decimal Levy { get; set; }
        public decimal Strata { get; set; }
        public decimal Council{ get; set; }

        public Purchase Purchase { get; set; }
        #endregion

        public decimal GetTotalQuarterCost()
        {
            return this.Levy + this.Strata + this.Council;
        }

        public long GetPurchasePrice()
        {
            return this.Purchase.Price;
        }
    }
}

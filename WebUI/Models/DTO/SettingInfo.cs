using Source.Estates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.DTO
{
    public class SettingInfo
    {
        public decimal Desposit { get; set; }
        public decimal MortgageRate { get; set; }
        public decimal Strate { get; set; }
        public decimal Water { get; set; }
        public decimal CouncilRate { get; set; }
        public decimal Rental { get; set; }
        public decimal AgentCommission { get; set; }
        public decimal LettingFee { get; set; }

        public List<Suburb> Suburbs { get; set; }
    }
}
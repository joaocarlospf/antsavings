using MyFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Web.Models
{
    public class PercentageRuleViewModel
    {
        public List<DistributionPercentage> DistributionPercentageList { get; set; }
        public DistributionPercentage DistributionPercentage { get; set; }
        public List<SelectListItem> FundsList { get; set; }
        public List<SelectListItem> ReservesList { get; set; }
    }
}
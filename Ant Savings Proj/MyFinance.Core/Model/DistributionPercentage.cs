using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Core.Model
{
    public class DistributionPercentage
    {
        public int ID { get; set; }
        
        [ForeignKey("Reserve")]
        public int ReserveID { get; set; }
        public virtual Reserve Reserve { get; set; }

        [ForeignKey("Fund")]
        public int? FundID { get; set; }
        public virtual Fund Fund { get; set; }
        
        public decimal Percentage { get; set; }

        [ForeignKey("DistributionRule")]
        public int DistributionRuleId { get; set; }
        public virtual DistributionRule DistributionRule { get; set; }

    }
}

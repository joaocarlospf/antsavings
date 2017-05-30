using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Core.Model
{
    public class DistributionRule
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<DistributionPercentage> DistributionPercentages { get; set; }
    }
}

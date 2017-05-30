using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Core.Model
{
    public class Fund
    {
        public int ID { get; set; }

        [Display(Name = "Descrição")]
        public string Name { get; set; }

        [StringLength(5)]
        [Display(Name = "Abreviação")]
        public string NameAbbreviation { get; set; }
        [Display(Name = "Tipo")]
        public EFundType FundType { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<DistributionPercentage> DistributionPercentages { get; set; }
        [InverseProperty("Fund")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}

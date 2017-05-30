using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Core.Model
{
    public class Reserve
    {
        public int ID { get; set; }

        [Display(Name = "Objetivo")]
        public string Name { get; set; }

        [StringLength(5)]
        [Display(Name = "Abreviação")]
        public string NameAbbreviation { get; set; }
        [Display(Name = "Data p/ Resgate")]
        public DateTime DateToWithdraw { get; set; }
        [Display(Name = "Período p/ Resgate")]
        public int PeriodToWithdraw { get; set;}
        [Display(Name = "Valor do Objetivo")]
        public decimal FinalExpectedValue { get; set; }
        public ETimeUnit TimeUnit { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<DistributionPercentage> DistributionPercentages { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}

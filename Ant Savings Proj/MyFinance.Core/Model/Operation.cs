using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Core.Model
{
    public class Operation
    {
        public int ID { get; set; }

        public decimal TotalValue { get; set; }

        public string Description { get; set; }

        public System.DateTime Date { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public EOperationType Type { get; set; }

        public string UserId { get; set; }

    }
}

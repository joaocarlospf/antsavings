using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Core.Model
{
    public class Transaction
    {
        public int ID { get; set; }

        public decimal Value { get; set; }

        public int ReserveID { get; set; }
        public virtual Reserve Reserve { get; set; }

        public int FundID { get; set; }
        public virtual Fund Fund { get; set; }

        public int? OperationId { get; set; }

        public virtual Operation Operation { get; set; }

    }
}

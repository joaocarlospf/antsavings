using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFinance.Web.Models
{
    public class UpdateFundToDepositViewModel
    {
        public int FundId { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Value { get; set; }
        public string CurrentBalance { get; set; }
        public string FundName { get; set; }
    }
}
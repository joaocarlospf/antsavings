using MyFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Web.Models
{
    public class TransactionViewModel
    {
        public List<SelectListItem> FundList { get; set; }
        public List<SelectListItem> ReserveList { get; set; }
        public List<Transaction> TransactionList { get; set; }
        public List<OperationVM> OperationList { get; set; }
        public decimal Balance { get; set; }
        public DepositViewModel DepositViewModel { get; set; }
        public WithdrawViewModel WithdrawViewModel { get; set; }
        public UpdateBalanceViewModel UpdateBalanceViewModel { get; set; }
    }

    public class OperationVM
    {
        public DateTime Date { get; set; }
        public string Reserves { get; set; }
        public string Funds { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
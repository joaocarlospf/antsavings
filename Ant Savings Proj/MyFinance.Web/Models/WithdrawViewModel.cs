using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Web.Models
{
    public class WithdrawViewModel
    {
        [Display(Name = "Descrição")]
        public string Origin { get; set; }
        
        [Display(Name = "Fundo")]
        public int SelectedFundId { get; set; }
        public List<SelectListItem> FundList { get; set; }
        [Display(Name = "Objetivo")]
        public int SelectedReserveId { get; set; }
        public List<SelectListItem> ReserveList { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [DataType(DataType.Currency)]
        public decimal Value { get; set; }
    }
}
using MyFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Web.Models
{
    public class FundsViewModel
    {
        public Fund Fund { get; set; }
        public List<SelectListItem> FundTypeList { get; set; }
        public List<Fund> FundList { get; set; }
    }
}
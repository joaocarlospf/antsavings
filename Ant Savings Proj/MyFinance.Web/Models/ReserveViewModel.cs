using MyFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Web.Models
{
    public class ReserveViewModel
    {
        public Reserve Reserve { get; set; }
        public List<SelectListItem> TimeUnitList { get; set; }
        public List<Reserve> ReserveList { get; set; }
    }
}
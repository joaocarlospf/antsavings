using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyFinance.Core.Model;
using System.Web.Mvc;

namespace MyFinance.Web.Models
{
    public class RulesViewModel
    {
        public DistributionRuleViewModel RuleViewModel { get; set; }
        public List<DistributionRule> RuleList { get; set; }
    }

    public class DistributionRuleViewModel
    {
        public DistributionRule Rule { get; set; }
        public List<SelectListItem> Funds { get; set; }
        public List<SelectListItem> Reserves { get; set; }
    }

    public class ModuleDepositRules
    {
        public int? id { get; set; }
        public string nome { get; set; }
        public regra[] regras { get; set; }
    }

    public class regra
    {
        public regra()
        {
            fixa = false;
        }
        public int id { get; set; }
        public fundo fundo { get; set; }
        public objetivo objetivo { get; set; }
        public decimal porcentagem { get; set; }
        public bool fixa { get; set; }
    }

    public class fundo
    {
        public int id { get; set; }
        public string text { get; set; }
    }

    public class objetivo
    {
        public int id { get; set; }
        public string text { get; set; }
    }
}
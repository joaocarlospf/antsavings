using MyFinance.Core;
using MyFinance.Core.Model;
using MyFinance.Web.Helpers;
using MyFinance.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Web.Controllers
{
    public class RulesController : Controller
    {
        public PartialViewResult RulesPartial()
        {
            List<DistributionRule> ruleList = null;
            List<SelectListItem> fundList = null;
            List<SelectListItem> reserveList = null;
            using (var queries = new FinancialQueries())
            {
                ruleList = queries.GetDistributionRules(User.Identity.Name);
                var funds = queries.GetFunds(User.Identity.Name);
                var reserves = queries.GetReserves(User.Identity.Name);
                fundList = Helper.GetListItem(funds);
                reserveList = Helper.GetListItem(reserves);
            }

            RulesViewModel rvm = new RulesViewModel()
            {
                RuleList = ruleList,
                RuleViewModel = new DistributionRuleViewModel()
            };

            rvm.RuleViewModel.Rule = new DistributionRule() { DistributionPercentages = new List<DistributionPercentage>() };
            rvm.RuleViewModel.Funds = fundList;
            rvm.RuleViewModel.Reserves = reserveList;


            return PartialView("RulesPartial", rvm);
        }

        public ActionResult GetRule(int ruleId)
        {
            using(var q = new FinancialQueries())
	        {
                var rule = q.GetRule(ruleId);
                var mdr = new ModuleDepositRules()
                {
                    id = rule.Id,
                    nome = rule.Name,
                    regras = new regra[rule.DistributionPercentages.Count]
                };

                for (int i = 0; i < rule.DistributionPercentages.Count; i++)
                {
                    var p = rule.DistributionPercentages.ElementAt(i);
                    regra r = new regra()
                    {
                        id = p.ID,
                        fundo = new fundo() { id = p.FundID.Value, text = p.Fund.Name },
                        objetivo = new objetivo() { id = p.ReserveID, text = p.Reserve.Name },
                        porcentagem = p.Percentage * 100
                    };
                    mdr.regras[i] = r;
                }

                return Json(mdr);
	        }
        }

        [HttpPost]
        public ActionResult SubmitRule(ModuleDepositRules mdr)
        {
            DistributionRule dr = null;
            var percsToRemove = new List<DistributionPercentage>();
            if (mdr.id == 0)
            {
                dr = new DistributionRule() { Name = mdr.nome, UserId = User.Identity.Name, DistributionPercentages = new List<DistributionPercentage>() };
                
                foreach (var perc in mdr.regras)
                {
                    var dp = new DistributionPercentage()
                    {
                        FundID = perc.fundo.id,
                        ReserveID = perc.objetivo.id,
                        Percentage = perc.porcentagem / 100,
                    };
                    dr.DistributionPercentages.Add(dp);
                }

                using (var o = new FinancialOperations())
                    o.AddRule(dr);
            }
            else using (var q = new FinancialQueries())
            {
                dr = q.GetDistributionRule(mdr.id.Value);
                for (int i = 0; i < dr.DistributionPercentages.Count; i++)
                {
                    var currPerc = dr.DistributionPercentages.ElementAt(i);
                    if (mdr.regras.Any(r => r.fundo.id == currPerc.FundID && r.objetivo.id == currPerc.ReserveID) == false)
                        percsToRemove.Add(currPerc);
                }

                dr.Name = mdr.nome;
                foreach (var perc in mdr.regras)
                {
                    var dp = dr.DistributionPercentages.SingleOrDefault(p => p.FundID == perc.fundo.id && p.ReserveID == perc.objetivo.id);
                    if (dp == null)
                    {
                        dp = new DistributionPercentage()
                        {
                            FundID = perc.fundo.id,
                            ReserveID = perc.objetivo.id,
                            Percentage = perc.porcentagem / 100
                        };
                        dr.DistributionPercentages.Add(dp);
                    }
                    else
                    {
                        dp.FundID = perc.fundo.id;
                        dp.ReserveID = perc.objetivo.id;
                        dp.Percentage = perc.porcentagem;
                    }
                }

                using (var o = new FinancialOperations(q.Context))
                    o.UpdateRule(dr, percsToRemove);
            }

            return RulesPartial();
        }

        public ActionResult AddRule(RulesViewModel rvm)
        {
            var rule = rvm.RuleViewModel.Rule;

            using (FinancialOperations fo = new FinancialOperations())
            {
                rule.UserId = User.Identity.Name;
                fo.AddRule(rule);
            }

            return RulesPartial();
        }

        public ActionResult RemoveRule(int ruleId)
        {
            using (FinancialOperations fo = new FinancialOperations())
            {
                fo.RemoveRule(ruleId);
            }

            return RulesPartial();
        }

        public PartialViewResult RulesPercentagePartial(int ruleId)
        {
            List<DistributionPercentage> percentageList = null;
            List<Fund> fundList = null;
            List<Reserve> reserveList = null;
            using (var queries = new FinancialQueries())
            {
                fundList = queries.GetFunds(User.Identity.Name);
                reserveList = queries.GetReserves(User.Identity.Name);
                percentageList = queries.GetPercentageList(ruleId);
            }

            PercentageRuleViewModel prvm = new PercentageRuleViewModel()
            {
                DistributionPercentageList = percentageList,
                DistributionPercentage = new DistributionPercentage(),
                FundsList = Helper.GetListItem(fundList, "NONE"),
                ReservesList = Helper.GetListItem(reserveList)
            };

            prvm.DistributionPercentage.DistributionRuleId = ruleId;

            return PartialView("RulesPercentagePartial", prvm);
        }

        public ActionResult AddPercentageRule(PercentageRuleViewModel prvm)
        {
            var percentageRule = prvm.DistributionPercentage;

            if (percentageRule.FundID == -1)
                percentageRule.FundID = null;

            using (FinancialOperations fo = new FinancialOperations())
            {
                fo.AddPercentageRule(percentageRule);
            }

            return RulesPercentagePartial(prvm.DistributionPercentage.DistributionRuleId);
        }

        public ActionResult RemovePercentageRule(int percentageRuleId)
        {
            int ruleId;
            using (FinancialOperations fo = new FinancialOperations())
            {
                ruleId = fo.RemovePercentageRule(percentageRuleId);
            }

            return RulesPercentagePartial(ruleId);
        }

	}
}
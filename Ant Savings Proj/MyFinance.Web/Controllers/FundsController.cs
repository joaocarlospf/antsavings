using MyFinance.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFinance.Core.Model;
using MyFinance.Core;
using MyFinance.Web.Helpers;

namespace MyFinance.Web.Controllers
{
    public class FundsController : Controller
    {
        //
        // GET: /Funds/
        public PartialViewResult FundsPartial()
        {
            FundsViewModel fvm = new FundsViewModel();
            using (FinancialQueries fq = new FinancialQueries())
            {
                fvm.FundList = fq.GetFunds(User.Identity.Name);
                fvm.Fund = new Fund();
                fvm.FundTypeList = Helper.GetDescriptions(typeof(EFundType));
            }
            
            return PartialView("FundsPartial", fvm);
        }

        [HttpPost]
        public ActionResult AddFund(FundsViewModel fvm)
        {
            // comentario para forcar atualizacao
            Fund fund = fvm.Fund;
            using (FinancialOperations fo = new FinancialOperations())
            {
                fund.UserId = User.Identity.Name;
                fo.AddFund(fund);
            }

            return FundsPartial();
        }

        public ActionResult RemoveFund(int fundId)
        {
            using (FinancialOperations fo = new FinancialOperations())
            {
                fo.RemoveFund(fundId);
            }

            return FundsPartial();
        }
	}
}
using MyFinance.Core;
using MyFinance.Core.Model;
using MyFinance.Web.Helpers;
using MyFinance.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Web.Controllers
{
    public class ManagerController : Controller
    {
        //
        // GET: /Manager/
        public PartialViewResult SavingsAccountPartial()
        {
            var tvm = new TransactionViewModel();
            using (FinancialQueries queries = new FinancialQueries())
            {
                List<Fund> fundList = queries.GetFunds(User.Identity.Name);
                List<Reserve> reserveList = queries.GetReserves(User.Identity.Name);
                List<SelectListItem> distributionRuleItems = Helper.GetListItem(queries.GetDistributionRules(User.Identity.Name));

                tvm.TransactionList = queries.GetTransactions(null, null, DateTime.Today, User.Identity.Name);

                var ops = tvm.TransactionList.GroupBy(t => t.Operation);
                var opsList = new List<OperationVM>();

                foreach (var op in ops)
                {
                    var opVW = new OperationVM()
                    {
                        Date = op.Key.Date,
                        Description = op.Key.Type != EOperationType.BalanceUpdate ? op.Key.Description : "Rendimento",
                        Value = op.Key.TotalValue,
                        Reserves = op.Select(o => o.ReserveID).Distinct().Count() == 1
                            ? op.FirstOrDefault().Reserve.Name
                            : String.Concat(op.Select(o => o.Reserve.NameAbbreviation + "/").Distinct()).Trim('/'),
                        Funds = op.Select(o => o.FundID).Distinct().Count() == 1
                            ? op.FirstOrDefault().Fund.Name
                            : String.Concat(op.Select(o => o.Fund.NameAbbreviation + "/").Distinct()).Trim('/')
                    };
                    opsList.Add(opVW);
                }
                tvm.OperationList = opsList;
                tvm.FundList = Helper.GetListItem(fundList, "TODOS");
                tvm.ReserveList = Helper.GetListItem(reserveList, "TODOS");
                tvm.Balance = tvm.TransactionList.Sum(t => t.Value);

                List<UpdateFundToDepositViewModel> fundsBalanceList = new List<UpdateFundToDepositViewModel>();
                fundsBalanceList.AddRange(
                    fundList.Select(
                        f => new UpdateFundToDepositViewModel()
                        { FundId = f.ID, FundName = f.Name, CurrentBalance = queries.GetBalance(DateTime.Today, User.Identity.Name,f.ID).ToString("0.00") }
                    )
                );

                DepositViewModel dvm = new DepositViewModel()
                {
                    FundList = Helper.GetListItem(fundList, "NENHUM"),
                    ReserveList = Helper.GetListItem(reserveList, "NENHUM"),
                    DistributionRuleList = distributionRuleItems,
                    Date = DateTime.Today,
                    fundsBalanceList = fundsBalanceList
                };
                tvm.DepositViewModel = dvm;

                WithdrawViewModel wvm = new WithdrawViewModel()
                {
                    FundList = Helper.GetListItem(fundList),
                    ReserveList = Helper.GetListItem(reserveList),
                    Date = DateTime.Today
                };
                tvm.WithdrawViewModel = wvm;

                UpdateBalanceViewModel ubvm = new UpdateBalanceViewModel()
                {
                    FundList = Helper.GetListItem(fundList),
                    Date = DateTime.Today
                };
                tvm.UpdateBalanceViewModel = ubvm;
            }
            ModelState.Clear();
            return PartialView("SavingsAccountPartial", tvm);
        }

        public PartialViewResult Undo()
        {
            using (var fo = new FinancialOperations())
            {
                fo.UndoLastOperation(User.Identity.Name);
            }

            return SavingsAccountPartial();
        }

        [HttpPost]
        public ActionResult GetTransationList(int? fundId, int? reserveId = null)
        {
            if (fundId == -1) fundId = null;
            if (reserveId == -1) reserveId = null;
            using (FinancialQueries queries = new FinancialQueries())
            {
                List<Transaction> transactionList = queries.GetTransactions(
                    fundId, reserveId, DateTime.Today, User.Identity.Name);

                var ops = transactionList.GroupBy(t => t.Operation);
                var opsList = new List<OperationVM>();

                foreach (var op in ops)
                {
                    var opVW = new OperationVM()
                    {
                        Date = op.Key.Date,
                        Description = op.Key.Type != EOperationType.BalanceUpdate ? op.Key.Description : "Rendimento",
                        Value = op.Where(o =>
                            (reserveId == null || o.ReserveID == reserveId) &&
                            (fundId == null || o.FundID == fundId))
                            .Sum(o => o.Value),
                        Reserves = op.Select(o => o.ReserveID).Distinct().Count() == 1
                            ? op.FirstOrDefault().Reserve.Name
                            : String.Concat(op.Select(o => o.Reserve.NameAbbreviation + "/").Distinct()).Trim('/'),
                        Funds = op.Select(o => o.FundID).Distinct().Count() == 1
                            ? op.FirstOrDefault().Fund.Name
                            : String.Concat(op.Select(o => o.Fund.NameAbbreviation + "/").Distinct()).Trim('/')
                    };
                    opsList.Add(opVW);
                }

                return Json(new
                {
                    balance = transactionList.Sum(t => t.Value).ToString("0.00"),
                    transactionList = Helper.RenderPartialViewToString("TransactionListPartial", opsList, ControllerContext, ViewData, TempData)
                });
            }
        }

        public ActionResult GetFundsFromDepositRule(int depositRuleId)
        {
            using (FinancialQueries fq = new FinancialQueries())
            {
                if (depositRuleId != -1)
                {
                    int[] funds = fq.GetDistributionRule(depositRuleId).DistributionPercentages.Where(dp => dp.FundID != null).Select(dp => dp.FundID.Value).Distinct().ToArray();
                    return Json(funds);
                }
            }

            return Json(new { });
        }

        [HttpPost]
        public ActionResult UpdateBalance(UpdateBalanceViewModel ubvm)
        {
            using (var fo = new FinancialOperations())
            using (var fq = new FinancialQueries())
            {
                var fundBal = fq.GetBalance(ubvm.Date, User.Identity.Name, ubvm.SelectedFundId);
                fo.UpdateBalance(ubvm.Value - fundBal, ubvm.SelectedFundId, ubvm.Date, User.Identity.Name);
            }

            return SavingsAccountPartial();
        }

        [HttpPost]
        public ActionResult Deposit(DepositViewModel dvm)
        {
            using (FinancialOperations fo = new FinancialOperations())
            {
                Dictionary<int, decimal> balancesBeforeDeposit = new Dictionary<int,decimal>();
                foreach (var fb in dvm.fundsBalanceList)
                    if (fb.Value.HasValue)
                        balancesBeforeDeposit.Add(fb.FundId, fb.Value.Value);

                fo.Deposit(dvm.Value, balancesBeforeDeposit, dvm.Date, dvm.Origin, dvm.SelectedFundId,
                    dvm.SelectedReserveId, dvm.SelectedDistriutionRuleId, User.Identity.Name);
            }

            return SavingsAccountPartial();
        }

        //[HttpPost]
        public ActionResult Withdraw(WithdrawViewModel wvm)
        {
            using (FinancialOperations fo = new FinancialOperations())
            {
                fo.WithDraw(wvm.Value, wvm.Date, wvm.Origin, wvm.SelectedFundId, wvm.SelectedReserveId, User.Identity.Name);
            }

            return SavingsAccountPartial();
        }
        
    }
}
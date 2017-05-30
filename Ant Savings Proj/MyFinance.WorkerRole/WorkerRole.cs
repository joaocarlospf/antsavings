using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using MyFinance.Core.Model;
using MyFinance.Core;
using MyFinance.Core.DAL;
using MyFinance.Core.WSSGS;
using System.Globalization;

namespace MyFinance.WorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.TraceInformation("MyFinance.WorkerRole entry point called");

            var values = GetVariationIndexFromBCSavings();
            CheckForAutomaticBalanceUpdate(values);
            Trace.WriteLine("Terminou!");
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }

        public void CheckForAutomaticBalanceUpdate(Dictionary<DateTime, decimal> profitIndices)
        {
            using (FinancialQueries queries = new FinancialQueries())
            {
                List<Fund> funds = queries.GetFundsWithAutomaticUpdateBalance(EFundType.Poupanca);

                foreach (var f in funds)
                {
                    var balBaseDays = new Dictionary<int, decimal>();
                    List<Transaction> transactions = queries.GetTransactions(f.ID, null, DateTime.Today, f.UserId);
                    if (transactions.Count == 0) continue;

                    DateTime currDate = new DateTime(2014, 1, 1);
                    while(currDate < DateTime.Today)
                    {
                        bool updatedBalance = false;

                        var dayDeposits = transactions.Where(
                            t => t.Operation.Date == currDate
                            && (t.Operation.Type == EOperationType.Deposit
                            || t.Operation.Type == EOperationType.BalanceUpdate));

                        if (dayDeposits.Any(t => t.Operation.Type == EOperationType.BalanceUpdate))
                            updatedBalance = true;

                        if (!updatedBalance && balBaseDays.ContainsKey(currDate.Day))
                            balBaseDays[currDate.Day] = AdjustFundBalance(balBaseDays[currDate.Day], currDate, profitIndices[currDate], f);

                        // deposits
                        foreach (var t in dayDeposits)
                        {
                            int day = Math.Min(28, t.Operation.Date.Day);
                            if (!balBaseDays.ContainsKey(day))
                                balBaseDays.Add(day, 0);
                            else if (t.Operation.Type == EOperationType.BalanceUpdate)
                                updatedBalance = true;

                            balBaseDays[day] += t.Value;
                        }

                        // withdraws
                        var dayWithdraws = transactions.Where(
                            t => t.Operation.Date == currDate && t.Operation.Type == EOperationType.Withdraw);

                        foreach (var t in dayWithdraws)
                        {
                            decimal value = -t.Value;
                            int day = currDate.Day;
                            while (value != 0)
                            {
                                int? nextDay = null;
                                int? diffNextDay = null;
                                foreach (var d in balBaseDays.Keys)
                                {
                                    if (nextDay == null || (d < day && (day - d) < diffNextDay)
                                        || (d > day && day + (28 - d) < diffNextDay))
                                    {
                                        if (d < day)
                                            diffNextDay = day - d;
                                        else if (d > day)
                                            diffNextDay = day + (28 - d);

                                        nextDay = d;
                                    }
                                }
                                decimal wdValue = Math.Min(value, balBaseDays[nextDay.Value]);
                                balBaseDays[nextDay.Value] -= wdValue;
                                value -= wdValue;
                            }
                        }

                        currDate = currDate.AddDays(1);
                    }
                }
            }
        }

        private decimal AdjustFundBalance(decimal currBalance, DateTime date, decimal profitIndex, Fund fund)
        {
            using (var finOp = new FinancialOperations())
            {
                decimal profit = currBalance * (profitIndex / 100);
                finOp.UpdateBalance(profit, fund.ID, date, fund.UserId);
                return currBalance + profit;
            }
        }

        private Dictionary<DateTime, decimal> GetVariationIndexFromBCSavings()
        {
            var wssgs = new FachadaWSSGSClient();
            var codes = new long[1];
            codes[0] = 195;
            var res = wssgs.getValoresSeriesVO(codes, "01/01/2014", DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year);
            var values = new Dictionary<DateTime, decimal>();
            if (res.Length > 0)
            {
                foreach (var item in res[0].valores)
                {
                    var d = new DateTime(item.ano, item.mes, item.dia);
                    var value = Convert.ToDecimal(item.svalor, CultureInfo.InvariantCulture);
                    values.Add(d, value);
                }
            }
            return values;
        }
    }
}

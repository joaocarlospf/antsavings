using MyFinance.Core.DAL;
using MyFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Core
{
    public class FinancialOperations : IDisposable
    {
        private MyFinanceDataContext Context;

        public FinancialOperations()
        {
            Context = new MyFinanceDataContext();
        }

        public FinancialOperations(MyFinanceDataContext context)
        {
            Context = context;
        }

        public void UndoLastOperation(string userId)
        {
            var operation = Context.Operations.Where(op => op.UserId == userId).ToList().LastOrDefault();
            Context.Operations.Remove(operation);
            Context.SaveChanges();
        }

        public void UpdateBalance(decimal profitAmount, int fundId, DateTime updateDate, string userId)
        {
            Dictionary<int, decimal> profitPerReserve = new Dictionary<int, decimal>();

            using (FinancialQueries queries = new FinancialQueries(Context))
            {
                List<Reserve> existingReserves = queries.GetReserves(userId);
                foreach (var reserve in existingReserves)
                    profitPerReserve.Add(reserve.ID, 0);

                decimal currentBalance = queries.GetBalance(updateDate, userId, fundId: fundId);

                if (profitAmount == 0)
                    return;

                DateTime? lastBalanceUpdate = queries.GetLastBalanceUpdate(fundId);

                if (lastBalanceUpdate == null)
                    throw new Exception("It's not possible to update the balance without any transaction.");

                if (lastBalanceUpdate >= updateDate)
                    throw new Exception("It's not possible to update the balance before the last balance update date.");

                int profitActionInterval = (int)updateDate.Date.Subtract(lastBalanceUpdate.Value.Date).TotalDays;

                List<DateTime> thresholds =
                    Context.Transactions.Where(t => t.Operation.Date > lastBalanceUpdate && t.Operation.Date < updateDate
                        && t.FundID == fundId).Select(t => t.Operation.Date).Distinct().ToList();

                for (int i = 0; i <= thresholds.Count; i++)
                {
                    DateTime ini = i == 0 ? lastBalanceUpdate.Value : thresholds[i - 1];
                    DateTime fim = i == thresholds.Count ? updateDate : thresholds[i];
                    int interv = (int)fim.Subtract(ini).TotalDays;

                    decimal fundBalance = queries.GetBalance(ini, userId, fundId);
                    foreach (var reserve in existingReserves)
                    {
                        decimal reserveBalance = queries.GetBalance(ini, userId, fundId, reserve.ID);
                        decimal reserveProportion = fundBalance > 0 ? (reserveBalance / fundBalance) : ((decimal)1 / existingReserves.Count);

                        profitPerReserve[reserve.ID] += profitAmount * reserveProportion * ((decimal)interv / profitActionInterval);
                    }
                }
            }

            Operation operation = new Operation()
            {
                TotalValue = profitAmount,
                Date = updateDate,
                Type = EOperationType.BalanceUpdate,
                UserId = userId,
                Transactions = new List<Transaction>()
            };

            foreach (var reserveProfit in profitPerReserve)
            {
                if (reserveProfit.Value == 0)
                    continue;

                Transaction trans = new Transaction()
                {
                    ReserveID = reserveProfit.Key,
                    FundID = fundId,
                    Value = reserveProfit.Value
                };
                operation.Transactions.Add(trans);
            }

            Context.Operations.Add(operation);
            Context.SaveChanges();
        }

        public void WithDraw(decimal value, DateTime date, string description, int fundID, int reserveID, string userId)
        {
            // verify balance
            using (FinancialQueries queries = new FinancialQueries(Context))
            {
                decimal balance = queries.GetBalance(date, userId, fundID, reserveID);
                if (balance - value < 0)
                    throw new Exception("There is no balance for this transaction.");
            }

            Operation operation = new Operation()
            {
                TotalValue = -value,
                Description = description,
                Date = date,
                Type = EOperationType.Withdraw,
                UserId = userId,
                Transactions = new List<Transaction>()
            };

            Transaction trans = new Transaction()
            {
                ReserveID = reserveID,
                FundID = fundID,
                Value = -value
            };
            operation.Transactions.Add(trans);

            Context.Operations.Add(operation);
            Context.SaveChanges();
        }

        public void Deposit(decimal value, Dictionary<int, decimal> balancesBeforeDeposit, DateTime date,
            string description, int? fundId, int? reserveId, int? distributionRuleId, string userId)
        {
            if (balancesBeforeDeposit != null)
                foreach (var balancefund in balancesBeforeDeposit)
                {
                    var q = new FinancialQueries(Context);
                    var fundBal = q.GetBalance(date, userId, balancefund.Key);
                    UpdateBalance(balancefund.Value - fundBal, balancefund.Key, date, userId);
                }

            IEnumerable<DistributionPercentage> dp = (distributionRuleId.HasValue && distributionRuleId != -1)
                ? Context.DistributionPercentages.Where(d => d.DistributionRuleId == distributionRuleId) : null;

            Operation operation = new Operation()
            {
                TotalValue = value,
                Description = description,
                Date = date,
                Type = EOperationType.Deposit,
                UserId = userId,
                Transactions = new List<Transaction>()
            };

            if (dp == null)
            {
                Transaction trans = new Transaction()
                {
                    ReserveID = reserveId.Value,
                    FundID = fundId.Value,
                    Value = value
                };
                operation.Transactions.Add(trans);
            }
            else
            {
                List<Transaction> transactionList = new List<Transaction>();
                foreach (var percentageRule in dp)
                {
                    Transaction trans = new Transaction()
                    {
                        ReserveID = percentageRule.ReserveID,
                        FundID = percentageRule.FundID.HasValue ? percentageRule.FundID.Value : fundId.Value,
                        Value = value * percentageRule.Percentage

                    };
                    operation.Transactions.Add(trans);
                }
            }
            Context.Operations.Add(operation);
            Context.SaveChanges();
        }

        public void Dispose()
        {

        }

        public void AddFund(Fund fund)
        {
            Context.Funds.Add(fund);
            Context.SaveChanges();
        }

        public void RemoveFund(int fundId)
        {
            Context.Funds.Remove(Context.Funds.SingleOrDefault(f => f.ID == fundId));
            Context.SaveChanges();
        }

        public void RemoveReserve(int reserveId)
        {
            Context.Reserves.Remove(Context.Reserves.SingleOrDefault(r => r.ID == reserveId));
            Context.SaveChanges();
        }

        public void AddReserve(Reserve reserve)
        {
            Context.Reserves.Add(reserve);
            Context.SaveChanges();
        }

        public void AddRule(DistributionRule rule)
        {
            Context.DistributionRules.Add(rule);
            Context.SaveChanges();
        }

        public void RemoveRule(int ruleId)
        {
            Context.DistributionRules.Remove(Context.DistributionRules.SingleOrDefault(r => r.Id == ruleId));
            Context.SaveChanges();
        }

        public void AddPercentageRule(DistributionPercentage percentageRule)
        {
            Context.DistributionPercentages.Add(percentageRule);
            Context.SaveChanges();
        }

        public int RemovePercentageRule(int percentageRuleId)
        {
            DistributionPercentage dp = Context.DistributionPercentages.SingleOrDefault(r => r.ID == percentageRuleId);
            int ruleId = dp.DistributionRuleId;
            Context.DistributionPercentages.Remove(dp);
            Context.SaveChanges();

            return ruleId;
        }

        public void UpdateRule(DistributionRule dr, List<DistributionPercentage> percsToRemove)
        {
            Context.DistributionPercentages.RemoveRange(percsToRemove);

            Context.SaveChanges();
        }
    }

    
}

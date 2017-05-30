using MyFinance.Core.DAL;
using MyFinance.Core.Model;
using MyFinance.Core.WSSGS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Core
{
    public class FinancialQueries : IDisposable
    {
        public MyFinanceDataContext Context;

        public FinancialQueries()
        {
            Context = new MyFinanceDataContext();
        }

        public FinancialQueries(MyFinanceDataContext context)
        {
            Context = context;
        }

        public DateTime? GetLastBalanceUpdate(int fundId)
        {
            var operations = Context.Transactions.Where(p => p.FundID == fundId && p.Operation.Type == EOperationType.BalanceUpdate);
            if (operations.Count() == 0)
            {
                operations = Context.Transactions.Where(p => p.FundID == fundId);
                
                if (operations.Count() == 0) return null;
                return operations.Min(p => p.Operation.Date);
            }
            return operations.Max(p => p.Operation.Date);
        }

        /// <summary>
        /// The balance resulted from every transaction in the fund/reserve until the date (including itself) passed as parameter.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="fundId"></param>
        /// <param name="reserveId"></param>
        /// <returns></returns>
        public decimal GetBalance(DateTime date, string userId, int? fundId = null, int? reserveId = null)
        {
            List<Transaction> transactions = GetTransactions(fundId, reserveId, date, userId);

            return transactions.Sum(t => t.Value);
        }
        
        public List<Transaction> GetTransactions(int? fundID, int? reserveID, DateTime? date, string userId)
        {
            return Context.Transactions.Where(t => t.Operation.UserId == userId
                && (date == null || t.Operation.Date <= date)
                && (fundID == null || t.FundID == fundID)
                && (reserveID == null || t.ReserveID == reserveID))
                .OrderBy(t => t.Operation.Date).ToList();
        }

        public void Dispose()
        {
        }

        public List<Fund> GetFunds(string userId)
        {
            return Context.Funds.Where(f => f.UserId == userId).ToList();
        }

        public List<Fund> GetFundsWithAutomaticUpdateBalance(EFundType fundType)
        {
            return Context.Funds.Where(f => f.FundType == fundType).ToList();
        }

        public List<Reserve> GetReserves(string userId)
        {
            return Context.Reserves.Where(r => r.UserId == userId).ToList();
        }

        public List<DistributionRule> GetDistributionRules(string userId)
        {
            return Context.DistributionRules.Where(d => d.UserId == userId).ToList();
        }

        public List<DistributionPercentage> GetPercentageList(int ruleId)
        {
            return Context.DistributionPercentages.Where(dp => dp.DistributionRuleId == ruleId).ToList();
        }

        public DistributionRule GetDistributionRule(int depositRuleId)
        {
            return Context.DistributionRules.SingleOrDefault(dr => dr.Id == depositRuleId);
        }

        public Fund GetFund(int fundId)
        {
            return Context.Funds.SingleOrDefault(f => f.ID == fundId);
        }

        public DistributionRule GetRule(int id)
        {
            return Context.DistributionRules.SingleOrDefault(r => r.Id == id);
        }
    }
}

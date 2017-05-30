using MyFinance.Core;
using MyFinance.Core.Model;
using MyFinance.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Web.Controllers
{
    public class SimulatorController : Controller
    {
        public PartialViewResult SimulatorPartial()
        {
            using (var q = new FinancialQueries())
            {
                var reserves = q.GetReserves(User.Identity.Name).OrderByDescending(r => r.DateToWithdraw).ToList();
                return PartialView("SimulatorPartial", reserves);
            }
        }


        public ActionResult GetProjection(string[] checkedRes)
        {
            using (var q = new FinancialQueries())
            {
                var user = User.Identity.Name;
                var funds = q.GetFunds(user);
                var reserves = q.GetReserves(user).Where(r => checkedRes.Any(c => c == r.ID.ToString())).OrderByDescending(r => r.DateToWithdraw);
                var today = DateTime.Today;

                var labels = new List<string>();
                DateTime end = reserves.Max(r => r.DateToWithdraw);
                DateTime currDate1 = today;

                int incr = (int)Math.Ceiling(end.Subtract(today).TotalDays / 1095);

                while (currDate1.Year < end.Year || (currDate1.Year == end.Year && currDate1.Month <= end.Month))
                {
                    labels.Add(GetMonth(currDate1.Month) + "-" + GetYear(currDate1.Year));
                    currDate1 = currDate1.AddMonths(incr);
                }

                var datasets = new List<dataset>();
                int x = 0;
                decimal totalMonthlyDep = 0;
                foreach (var res in reserves)
                {
                    x++;
                    int monthsToAchieve = 0;
                    var currDate = today;
                    while (currDate <= res.DateToWithdraw)
                    {
                        monthsToAchieve++;
                        currDate = currDate.AddMonths(1);
                    }

                    // escolhendo fundo
                    decimal? percIncome = null;
                    if (monthsToAchieve < 24)
                        percIncome = 1.0015m;
                    else if (monthsToAchieve < 84)
                        percIncome = 1.002875m;
                    else percIncome = 1.0063125m;

                    var resBal = q.GetBalance(today, user, null, res.ID);

                    decimal monthlyDep = Math.Max(0, res.FinalExpectedValue - resBal);
                    decimal div = 0;
                    for (int i = 0; i < monthsToAchieve; i++)
                        div += (decimal)Math.Pow((double)percIncome, i);
                    monthlyDep /= Math.Max(1, div);

                    var data = new List<decimal>();
                    decimal currBal = q.GetBalance(today, user, reserveId: res.ID);
                    int currIt = 0;
                    for (int i = 0; i < monthsToAchieve; i++)
                    {
                        currIt++;
                        if (currIt == 1)
                            data.Add(currBal);

                        if (currIt == incr)
                            currIt = 0;

                        currBal *= percIncome.Value;
                        currBal += monthlyDep;
                    }
                    currIt++;
                    while (currIt != 1)
                    {
                        if (currIt == incr)
                            currIt = 0;

                        currBal *= percIncome.Value;
                        currBal += monthlyDep;

                        currIt++;
                    }
                    data.Add(currBal);

                    totalMonthlyDep += monthlyDep;

                    var dataset = new dataset()
                    {
                        reserveId = res.ID,
                        monthlyDep = monthlyDep.ToString("0.00"),
                        data = data,
                        fillColor = "rgba(151,187,205,0)",
                        strokeColor = GetColor(x),
                        pointColor = "rgba(151,187,205,1)",
                        pointStrokeColor = "#fff"
                    };
                    datasets.Add(dataset);
                }

                var simularRes = new simulatorResult()
                {
                    totalMonthlyDep = totalMonthlyDep.ToString("0.00"),
                    labels = labels,
                    datasets = datasets
                };
                return Json(simularRes);
            }
        }

        private string GetColor(int pos)
        {
            if (pos == 1) return "#00C12B";
            else if (pos == 2) return "#4013AF";
            else if (pos == 3) return "#FFD900";
            else if (pos == 4) return "#FF1300";
            return null;
        }

        private string GetYear(int year)
        {
            return year.ToString().Substring(2, 2);
        }

        private string GetMonth(int month)
        {
            if (month == 1) return "Jan";
            else if (month == 2) return "Fev";
            else if (month == 3) return "Mar";
            else if (month == 4) return "Abr";
            else if (month == 5) return "Mai";
            else if (month == 6) return "Jun";
            else if (month == 7) return "Jul";
            else if (month == 8) return "Ago";
            else if (month == 9) return "Set";
            else if (month == 10) return "Out";
            else if (month == 11) return "Nov";
            else if (month == 12) return "Dez";
            return null;
        }
	}
}
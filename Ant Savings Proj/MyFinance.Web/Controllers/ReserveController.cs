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
    public class ReserveController : Controller
    {
        //
        // GET: /Reserve/
        public PartialViewResult ReservesPartial()
        {
            ReserveViewModel rvm = new ReserveViewModel();
            using (FinancialQueries fq = new FinancialQueries())
            {
                rvm.ReserveList = fq.GetReserves(User.Identity.Name);
            }
            rvm.Reserve = new Reserve();
            rvm.Reserve.DateToWithdraw = DateTime.Today;
            rvm.TimeUnitList = Helper.GetDescriptions(typeof(ETimeUnit));

            return PartialView("ReservesPartial", rvm);
        }

        public ActionResult AddReserve(ReserveViewModel rvm)
        {
            var reserve = rvm.Reserve;

            using (FinancialOperations fo = new FinancialOperations())
            {
                reserve.UserId = User.Identity.Name;
                fo.AddReserve(reserve);
            }

            return ReservesPartial();
        }

        public ActionResult RemoveReserve(int reserveId)
        {
            using (FinancialOperations fo = new FinancialOperations())
            {
                fo.RemoveReserve(reserveId);
            }

            return ReservesPartial();
        }

	}
}
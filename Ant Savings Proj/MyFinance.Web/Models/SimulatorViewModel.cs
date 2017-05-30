using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFinance.Web.Models
{
    public class SimulatorViewModel
    {

    }

    public class simulatorResult
    {
        public List<string> labels;
        public List<dataset> datasets;
        public string totalMonthlyDep;
    }

    public class dataset
    {
        public int reserveId;
        public string monthlyDep;
        public string fillColor;
        public string strokeColor;
        public string pointColor;
        public string pointStrokeColor;
        public List<decimal> data;
    }
}
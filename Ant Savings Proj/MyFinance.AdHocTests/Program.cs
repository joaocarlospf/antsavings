using MyFinance.Core.WSSGS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.AdHocTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var wssgs = new FachadaWSSGSClient();
            var codes = new long[1];
            codes[0] = 195;
            var res = wssgs.getValoresSeriesVO(codes, "01/01/2014", "14/04/2014");
            var values = new Dictionary<DateTime, decimal>();
            if (res.Length > 0)
            {
                foreach (var item in res[0].valores)
                {
                    var date = new DateTime(item.ano, item.mes, item.dia);
                    var value = Convert.ToDecimal(item.svalor, CultureInfo.InvariantCulture);
                    values.Add(date, value);
                }
            }
            Console.WriteLine();
        }
    }
}
using MyFinance.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilitarios;

namespace MyFinance.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            /*InitializeComponent();

            using (FinancialQueries fq = new FinancialQueries())
            {
                List<Fund> funds = fq.GetFunds();
                List<Reserve> reserves = fq.GetReserves();

                cboFundo.DataSource = funds;
                cboFundo.DisplayMember = "Name";
                cboFundo.ValueMember = "ID";

                cboFilterByFund.DataSource = funds;
                cboFilterByFund.DisplayMember = "Name";
                cboFilterByFund.ValueMember = "ID";

                cboFilterByReserve.DataSource = reserves;
                cboFilterByReserve.DisplayMember = "Name";
                cboFilterByReserve.ValueMember = "ID";
            }*/
        }

        protected override void OnLoad(EventArgs e)
        {
            LoadGrid();
        }

        public void LoadGrid()
        {
            //using (FinancialQueries fq = new FinancialQueries())
            //{
            //    int? fundID = chkFilterFund.Checked ? (int?)cboFilterByFund.SelectedValue : null;
            //    int? reserveID = chkFiltrarReserva.Checked ? (int?)cboFilterByReserve.SelectedValue : null;

            //    List<Transaction> transactions = fq.GetTransactions(fundID, reserveID);

            //    decimal saldo = 0;
            //    List<object[]> conteudo = new List<object[]>();
            //    foreach (var item in transactions)
            //    {
            //        decimal currValor = item.Type == 0 ? item.Value : -item.Value;
            //        saldo += currValor;
            //        object[] linha = new object[]
            //        {
            //            item,
            //            item.Origins,
            //            item.Date.ToShortDateString(),
            //            currValor,
            //            item.Reserve.Name,
            //            item.Fund.Name
            //        };
            //        conteudo.Add(linha);
            //    }
            //    txtSaldo.Text = saldo.ToString("0.00");
            //    Util.AdicionarConteudoAGrid(gridTransactions, conteudo, true);
            //}
            
        }

        private void txtDistribPerReserve_Click(object sender, EventArgs e)
        {
            /*FinancialOperations fo = new FinancialOperations();
            fo.DistributeDepositByReserve(numValue.Value, dtpData.Value.Date, (int)cboFundo.SelectedValue, txtOrigins.Text);

            LoadGrid();*/
        }

        private void cboFilterByFund_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void cboFilterByReserve_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void chkFilterFund_CheckedChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void chkFiltrarReserva_CheckedChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }
    }
}

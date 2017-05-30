using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;

namespace Utilitarios
{
    public class Pair<TFirst, TSecond>
    {
        public Pair()
        {
        }

        public Pair(TFirst first, TSecond second)
        {
            this.first = first;
            this.second = second;
        }

        #region Attributes
        private TFirst first;
        private TSecond second;
        #endregion

        #region Properties
        public TFirst First
        {
            get { return first; }
            set { first = value; }
        }

        public TSecond Second
        {
            get { return second; }
            set { second = value; }
        }

        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            Pair<TFirst, TSecond> pair = obj as Pair<TFirst, TSecond>;
            if (pair == null) return false;
            return Equals(first, pair.first) && Equals(second, pair.second);
        }

        public override int GetHashCode()
        {
            return (first != null ? first.GetHashCode() : 0) + 29 * (second != null ? second.GetHashCode() : 0);
        }

        public static List<Pair<TFirst, TSecond>> Dictionary2List(IDictionary<TFirst, TSecond> dictionary)
        {
            List<Pair<TFirst, TSecond>> result = new List<Pair<TFirst, TSecond>>();
            if (dictionary != null)
            {
                foreach (KeyValuePair<TFirst, TSecond> pair in dictionary)
                {
                    result.Add(new Pair<TFirst, TSecond>(pair.Key, pair.Value));
                }
            }
            return result;
        }

        public static Dictionary<TFirst, TSecond> List2Dictionary(IList<Pair<TFirst, TSecond>> list)
        {
            Dictionary<TFirst, TSecond> result = new Dictionary<TFirst, TSecond>();
            if (list != null)
            {
                foreach (Pair<TFirst, TSecond> pair in list)
                {
                    result.Add(pair.First, pair.Second);
                }
            }
            return result;
        }

        public static Dictionary<TFirst, List<TSecond>> List2TwoDimentionalDictionary(IList<Pair<TFirst, TSecond>> list)
        {
            Dictionary<TFirst, List<TSecond>> result = new Dictionary<TFirst, List<TSecond>>();
            if (list != null)
            {
                foreach (Pair<TFirst, TSecond> pair in list)
                {
                    List<TSecond> dimentionList;
                    if (!result.TryGetValue(pair.First, out dimentionList))
                    {
                        dimentionList = new List<TSecond>();
                        result[pair.First] = dimentionList;
                    }

                    dimentionList.Add(pair.Second);
                }
            }
            return result;
        }
        #endregion
    }

    public class Util
    {
        #region Enumerador Base
        private const char ENUM_SEPERATOR_CHARACTER = ',';
        public static string GetDescription(Enum value)
        {
            // Check for Enum that is marked with FlagAttribute         
            var entries = value.ToString().Split(ENUM_SEPERATOR_CHARACTER);
            var description = new string[entries.Length];
            for (var i = 0; i < entries.Length; i++)
            {
                var fieldInfo = value.GetType().GetField(entries[i].Trim());
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                description[i] = (attributes.Length > 0) ? attributes[0].Description : entries[i].Trim();
            }

            return String.Join(", ", description);
        }

        public static T GetEnumValueWithDescription<T>(string description)
        {
            foreach (T item in Enum.GetValues(typeof(T)))
            {
                if (item is Enum && description.Equals(Util.GetDescription(item as Enum)))
                    return item;
            }

            return (T)(new object());
        } 
        #endregion

        #region Grid
        // Se "inclueObjeto" = true: o objeto deve ser o primeiro item do array [0].
        public static void AdicionarConteudoAGrid(DataGridView grid, List<object[]> conteudo, bool inclueObjeto)
        {
            DataTable dtConteudo = new DataTable();

            if (inclueObjeto)
                dtConteudo.Columns.Add(new DataColumn("Objeto") { DataType = Type.GetType("System.Object") });

            foreach (DataGridViewColumn column in grid.Columns)
            {
                dtConteudo.Columns.Add(new DataColumn(column.Name));
                column.DataPropertyName = column.Name;
            }

            grid.AutoGenerateColumns = false;
            grid.DataSource = dtConteudo;

            foreach (object[] linha in conteudo)
                Util.AdicionarLinhaAGrid(grid, linha, inclueObjeto);
        }

       
        public static void ConcatenarConteudoAGrid(DataGridView grid, List<object[]> conteudo, bool inclueObjeto)
        {
            if (grid.DataSource != null)
            {
                DataTable dt = (DataTable)grid.DataSource;

                foreach (object[] linha in conteudo)
                {
                    DataRow dr = dt.NewRow();

                    for (int i = 0; i < linha.Length; i++)
                        dr[i] = linha[i];

                    dt.Rows.Add(dr);
                }
            }
            else
                AdicionarConteudoAGrid(grid, conteudo, inclueObjeto);
        }

        public static void AdicionarLinhaAGrid(DataGridView grid, object[] p, bool inclueObjeto)
        {
            if (grid.DataSource is DataTable)
            {
                DataTable dt = (DataTable)grid.DataSource;
                DataRow dr = dt.NewRow();

                for (int i = 0; i < p.Length; i++)
                    dr[i] = p[i];

                dt.Rows.Add(dr);
            }
        }
        
        public static DateTime GetApenasDiaMesAno(DateTime data)
        {
            return new DateTime(data.Year, data.Month, data.Day, 0, 0, 0);
        }

        public static DataRow GetLinha(DataGridViewRow linha)
        {
            if (linha != null)
                return (DataRow)((DataRowView)linha.DataBoundItem).Row;
            else
                return null;
        }

        public static DataRow GetLinhaSelecionada(DataGridView grid)
        {
            DataGridViewRow linha = (grid.SelectedRows != null && grid.SelectedRows.Count > 0) ? grid.SelectedRows[0] : null;

            if (linha != null)
                return (DataRow)((DataRowView)linha.DataBoundItem).Row;
            else
                return null;
        }

        public static List<DataGridViewRow> GetLinhasSelecionadasNew1(DataGridView grid)
        {
            List<DataGridViewRow> linhasSelecionadas = new List<DataGridViewRow>();

            foreach (DataGridViewRow linha in grid.Rows)
            {
                DataGridViewCheckBoxCell cell = ((DataGridViewCheckBoxCell)linha.Cells[linha.Cells.Count - 1]);
                bool check = !DBNull.Value.Equals(cell.Value) && (string)cell.Value == "True";
                if (check)
                    linhasSelecionadas.Add(linha);
            }

            return linhasSelecionadas;
        }

        public static List<DataRow> GetDadosLinhasSelecionadas(DataGridView grid)
        {
            List<DataRow> linhasSelecionadas = new List<DataRow>();

            foreach (DataGridViewRow linha in grid.Rows)
            {
                DataGridViewCheckBoxCell cell = ((DataGridViewCheckBoxCell)linha.Cells[linha.Cells.Count - 1]);
                bool check = !DBNull.Value.Equals(cell.Value) && (string)cell.Value == "True";
                if (check)
                    linhasSelecionadas.Add((DataRow)((DataRowView)linha.DataBoundItem).Row);
            }                    

            return linhasSelecionadas;
        }

        public static List<DataRow> GetLinhasSelecionadas(DataGridView grid)
        {
            List<DataRow> linhasSelecionadas = new List<DataRow>();

            if (grid.SelectedRows != null)
            {
                foreach (DataGridViewRow linha in grid.SelectedRows)
                    linhasSelecionadas.Add((DataRow)((DataRowView)linha.DataBoundItem).Row);
            }

            return linhasSelecionadas;
        }
        
        public static List<DataRow> GetLinhasVisiveis(DataGridView grid)
        {
            List<DataRow> linhasVisiveis = new List<DataRow>();

            foreach (DataGridViewRow linha in grid.Rows)
            {
                if (linha.Visible)
                {
                    DataTable dt = (DataTable)grid.DataSource;
                    int indice = grid.Rows.IndexOf(linha);
                    linhasVisiveis.Add(dt.Rows[indice]);
                }
            }

            return linhasVisiveis;
        }

        public static void FormatarDataGridViewWithBorder(DataGridView d)
        {
            FormatarDataGridView(d);
            d.BorderStyle = BorderStyle.FixedSingle;
        }

        public static void FormatarDataGridView(DataGridView d)
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();

            d.AllowUserToAddRows = false;
            d.AllowUserToDeleteRows = false;
            d.AllowUserToResizeRows = false;
            d.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            d.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            d.BorderStyle = System.Windows.Forms.BorderStyle.None;
            d.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            d.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            d.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            d.ColumnHeadersHeight = 24;

            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            d.DefaultCellStyle = dataGridViewCellStyle2;
            d.Dock = System.Windows.Forms.DockStyle.Fill;
            d.MultiSelect = false;
            d.Name = "grid";
            d.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            d.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            d.RowHeadersVisible = false;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.DimGray;
            d.RowsDefaultCellStyle = dataGridViewCellStyle4;
            d.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            d.ShowCellErrors = false;
            d.ShowCellToolTips = false;
            d.ShowEditingIcon = false;
            d.ShowRowErrors = false;
        }
        #endregion

        #region Diálogos

        public static void EntradaInvalidaMessage(string texto)
        {
            MessageBox.Show(texto, "Entrada Inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult PerguntaMessage(string texto, IWin32Window owner)
        {
            return  MessageBox.Show(owner, texto, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult PerguntaMessage(string texto)
        {
            return MessageBox.Show(texto, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static void ErrorMessage(string texto)
        {
            MessageBox.Show(texto,
                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ErrorMessage(IWin32Window telaPai, string texto)
        {
            MessageBox.Show(telaPai, texto,
                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void SuccessoMessage(string texto)
        {
            MessageBox.Show(texto,
                "Operação realizada com sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void AlertaMessage(string texto)
        {
            MessageBox.Show(texto, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void ErrorMessage(Exception ex)
        {
            MessageBox.Show(ex.Message + "\n\n" + ex.Source + "\n\n" + ex.StackTrace,
                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void LogMessageToFile(string filename, string msg)
        {
            System.IO.StreamWriter sw = System.IO.File.AppendText(filename + ".log");
            try
            {
                string logLine = System.String.Format(
                    "{0:G}: {1}.", System.DateTime.Now, msg);
                sw.WriteLine(logLine);
            }
            finally
            {
                sw.Close();
            }
        }

        /*public static void ErrorMessageBox(ValidationException excecao)
        {
            MessageBox.Show(excecao.Message, excecao.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }*/

        public static void PrecisaRemoverFilhos(string texto)
        {
            MessageBox.Show(texto, "Remova os Filhos", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool DesejaExcluir(string texto)
        {
            return MessageBox.Show(texto, "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public static bool DesejaCancelarAlteracoes()
        {
            return MessageBox.Show("Você realmente deseja cancelar as alterações realizadas?", "Cancelar Edição", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public static bool DesejaImprimir()
        {
            return MessageBox.Show("Você deseja imprimir o agendamento finalizado?", "Imprimir Agendamento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
        #endregion

    }

}

namespace MyFinance.Forms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtDistribPerReserve = new System.Windows.Forms.Button();
            this.txtOrigins = new System.Windows.Forms.TextBox();
            this.dtpData = new System.Windows.Forms.DateTimePicker();
            this.cboFundo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSaldo = new System.Windows.Forms.TextBox();
            this.cboFilterByReserve = new System.Windows.Forms.ComboBox();
            this.cboFilterByFund = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numValue = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridTransactions = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkFilterFund = new System.Windows.Forms.CheckBox();
            this.chkFiltrarReserva = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cboFundOrigin = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numValue)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTransactions)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDistribPerReserve
            // 
            this.txtDistribPerReserve.Location = new System.Drawing.Point(120, 125);
            this.txtDistribPerReserve.Name = "txtDistribPerReserve";
            this.txtDistribPerReserve.Size = new System.Drawing.Size(221, 23);
            this.txtDistribPerReserve.TabIndex = 1;
            this.txtDistribPerReserve.Text = "Depósito com distribuição por reserva";
            this.txtDistribPerReserve.UseVisualStyleBackColor = true;
            this.txtDistribPerReserve.Click += new System.EventHandler(this.txtDistribPerReserve_Click);
            // 
            // txtOrigins
            // 
            this.txtOrigins.Location = new System.Drawing.Point(120, 19);
            this.txtOrigins.Name = "txtOrigins";
            this.txtOrigins.Size = new System.Drawing.Size(221, 20);
            this.txtOrigins.TabIndex = 2;
            // 
            // dtpData
            // 
            this.dtpData.Location = new System.Drawing.Point(120, 45);
            this.dtpData.Name = "dtpData";
            this.dtpData.Size = new System.Drawing.Size(221, 20);
            this.dtpData.TabIndex = 3;
            // 
            // cboFundo
            // 
            this.cboFundo.FormattingEnabled = true;
            this.cboFundo.Location = new System.Drawing.Point(120, 72);
            this.cboFundo.Name = "cboFundo";
            this.cboFundo.Size = new System.Drawing.Size(221, 21);
            this.cboFundo.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Origens do depósito";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Data";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fundo de destino";
            // 
            // txtSaldo
            // 
            this.txtSaldo.Location = new System.Drawing.Point(621, 335);
            this.txtSaldo.Name = "txtSaldo";
            this.txtSaldo.Size = new System.Drawing.Size(133, 20);
            this.txtSaldo.TabIndex = 8;
            // 
            // cboFilterByReserve
            // 
            this.cboFilterByReserve.FormattingEnabled = true;
            this.cboFilterByReserve.Location = new System.Drawing.Point(530, 12);
            this.cboFilterByReserve.Name = "cboFilterByReserve";
            this.cboFilterByReserve.Size = new System.Drawing.Size(221, 21);
            this.cboFilterByReserve.TabIndex = 9;
            this.cboFilterByReserve.SelectedIndexChanged += new System.EventHandler(this.cboFilterByReserve_SelectedIndexChanged);
            // 
            // cboFilterByFund
            // 
            this.cboFilterByFund.FormattingEnabled = true;
            this.cboFilterByFund.Location = new System.Drawing.Point(117, 10);
            this.cboFilterByFund.Name = "cboFilterByFund";
            this.cboFilterByFund.Size = new System.Drawing.Size(221, 21);
            this.cboFilterByFund.TabIndex = 10;
            this.cboFilterByFund.SelectedIndexChanged += new System.EventHandler(this.cboFilterByFund_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(568, 338);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Saldo";
            // 
            // numValue
            // 
            this.numValue.DecimalPlaces = 2;
            this.numValue.Location = new System.Drawing.Point(120, 100);
            this.numValue.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numValue.Name = "numValue";
            this.numValue.Size = new System.Drawing.Size(120, 20);
            this.numValue.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Valor";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.gridTransactions);
            this.panel1.Location = new System.Drawing.Point(12, 36);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(1);
            this.panel1.Size = new System.Drawing.Size(742, 296);
            this.panel1.TabIndex = 71;
            // 
            // gridTransactions
            // 
            this.gridTransactions.AllowDrop = true;
            this.gridTransactions.AllowUserToAddRows = false;
            this.gridTransactions.AllowUserToDeleteRows = false;
            this.gridTransactions.AllowUserToResizeRows = false;
            this.gridTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridTransactions.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gridTransactions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridTransactions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridTransactions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.gridTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column5,
            this.Column2,
            this.Column3,
            this.Column4});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Calibri", 8F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTransactions.DefaultCellStyle = dataGridViewCellStyle10;
            this.gridTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTransactions.Location = new System.Drawing.Point(1, 1);
            this.gridTransactions.MultiSelect = false;
            this.gridTransactions.Name = "gridTransactions";
            this.gridTransactions.ReadOnly = true;
            this.gridTransactions.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTransactions.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.gridTransactions.RowHeadersVisible = false;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.gridTransactions.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.gridTransactions.RowTemplate.Height = 20;
            this.gridTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTransactions.ShowCellErrors = false;
            this.gridTransactions.ShowCellToolTips = false;
            this.gridTransactions.ShowEditingIcon = false;
            this.gridTransactions.ShowRowErrors = false;
            this.gridTransactions.Size = new System.Drawing.Size(740, 294);
            this.gridTransactions.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Origens";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Data";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Valor";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Reserva";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Fundo";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // chkFilterFund
            // 
            this.chkFilterFund.AutoSize = true;
            this.chkFilterFund.Location = new System.Drawing.Point(12, 12);
            this.chkFilterFund.Name = "chkFilterFund";
            this.chkFilterFund.Size = new System.Drawing.Size(99, 17);
            this.chkFilterFund.TabIndex = 72;
            this.chkFilterFund.Text = "Filtrar por fundo";
            this.chkFilterFund.UseVisualStyleBackColor = true;
            this.chkFilterFund.CheckedChanged += new System.EventHandler(this.chkFilterFund_CheckedChanged);
            // 
            // chkFiltrarReserva
            // 
            this.chkFiltrarReserva.AutoSize = true;
            this.chkFiltrarReserva.Location = new System.Drawing.Point(417, 14);
            this.chkFiltrarReserva.Name = "chkFiltrarReserva";
            this.chkFiltrarReserva.Size = new System.Drawing.Size(107, 17);
            this.chkFiltrarReserva.TabIndex = 73;
            this.chkFiltrarReserva.Text = "Filtrar por reserva";
            this.chkFiltrarReserva.UseVisualStyleBackColor = true;
            this.chkFiltrarReserva.CheckedChanged += new System.EventHandler(this.chkFiltrarReserva_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtOrigins);
            this.groupBox1.Controls.Add(this.txtDistribPerReserve);
            this.groupBox1.Controls.Add(this.dtpData);
            this.groupBox1.Controls.Add(this.cboFundo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numValue);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 366);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 171);
            this.groupBox1.TabIndex = 74;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Depósito";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboFundOrigin);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(380, 366);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(362, 171);
            this.groupBox2.TabIndex = 75;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Transferência";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(120, 125);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(221, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Transferência";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(120, 45);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(221, 20);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(120, 72);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(221, 21);
            this.comboBox1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Valor";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Fundo de Origem";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Location = new System.Drawing.Point(120, 100);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Data";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Fundo de destino";
            // 
            // cboFundOrigin
            // 
            this.cboFundOrigin.FormattingEnabled = true;
            this.cboFundOrigin.Location = new System.Drawing.Point(120, 18);
            this.cboFundOrigin.Name = "cboFundOrigin";
            this.cboFundOrigin.Size = new System.Drawing.Size(221, 21);
            this.cboFundOrigin.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 549);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkFiltrarReserva);
            this.Controls.Add(this.chkFilterFund);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboFilterByFund);
            this.Controls.Add(this.cboFilterByReserve);
            this.Controls.Add(this.txtSaldo);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numValue)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTransactions)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button txtDistribPerReserve;
        private System.Windows.Forms.TextBox txtOrigins;
        private System.Windows.Forms.DateTimePicker dtpData;
        private System.Windows.Forms.ComboBox cboFundo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSaldo;
        private System.Windows.Forms.ComboBox cboFilterByReserve;
        private System.Windows.Forms.ComboBox cboFilterByFund;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numValue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gridTransactions;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.CheckBox chkFilterFund;
        private System.Windows.Forms.CheckBox chkFiltrarReserva;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboFundOrigin;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}


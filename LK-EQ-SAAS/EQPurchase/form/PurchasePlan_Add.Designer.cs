namespace EQPurchase.form
{
    partial class PurchasePlan_Add
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchasePlan_Add));
            YtWinContrl.com.datagrid.TvList tvList1 = new YtWinContrl.com.datagrid.TvList();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.add_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.Get_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.DeleButton = new System.Windows.Forms.ToolStripButton();
            this.save_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.cancel_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Pass = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_No = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.memo_yTextBox = new YtWinContrl.com.contrl.YTextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.InWare_selTextInpt = new YtWinContrl.com.contrl.SelTextInpt();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.totalmoney_yTextBox = new YtWinContrl.com.contrl.YTextBox();
            this.ytComboBox_Plan = new YtWinContrl.com.contrl.YtComboBox();
            this.dataGView1 = new YtWinContrl.com.datagrid.DataGView();
            this.eqname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eqid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.country = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitcode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.nownum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rowno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.applyid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.choscode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockflowno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.add_toolStripButton,
            this.Get_toolStripButton,
            this.DeleButton,
            this.save_toolStripButton,
            this.cancel_toolStripButton,
            this.toolStripButton_Pass,
            this.toolStripButton_No});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(818, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // add_toolStripButton
            // 
            this.add_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("add_toolStripButton.Image")));
            this.add_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.add_toolStripButton.Name = "add_toolStripButton";
            this.add_toolStripButton.Size = new System.Drawing.Size(121, 22);
            this.add_toolStripButton.Text = "手工添加采购设备";
            this.add_toolStripButton.Click += new System.EventHandler(this.add_toolStripButton_Click);
            // 
            // Get_toolStripButton
            // 
            this.Get_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("Get_toolStripButton.Image")));
            this.Get_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Get_toolStripButton.Name = "Get_toolStripButton";
            this.Get_toolStripButton.Size = new System.Drawing.Size(121, 22);
            this.Get_toolStripButton.Text = "从设备请购单添加";
            this.Get_toolStripButton.Click += new System.EventHandler(this.Get_toolStripButton_Click);
            // 
            // DeleButton
            // 
            this.DeleButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleButton.Image")));
            this.DeleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleButton.Name = "DeleButton";
            this.DeleButton.Size = new System.Drawing.Size(97, 22);
            this.DeleButton.Text = "删除采购设备";
            this.DeleButton.Click += new System.EventHandler(this.DeleButton_Click);
            // 
            // save_toolStripButton
            // 
            this.save_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("save_toolStripButton.Image")));
            this.save_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.save_toolStripButton.Name = "save_toolStripButton";
            this.save_toolStripButton.Size = new System.Drawing.Size(85, 22);
            this.save_toolStripButton.Text = "保存采购单";
            this.save_toolStripButton.Click += new System.EventHandler(this.save_toolStripButton_Click);
            // 
            // cancel_toolStripButton
            // 
            this.cancel_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cancel_toolStripButton.Image")));
            this.cancel_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cancel_toolStripButton.Name = "cancel_toolStripButton";
            this.cancel_toolStripButton.Size = new System.Drawing.Size(49, 22);
            this.cancel_toolStripButton.Text = "取消";
            this.cancel_toolStripButton.Click += new System.EventHandler(this.cancel_toolStripButton_Click);
            // 
            // toolStripButton_Pass
            // 
            this.toolStripButton_Pass.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Pass.Image")));
            this.toolStripButton_Pass.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Pass.Name = "toolStripButton_Pass";
            this.toolStripButton_Pass.Size = new System.Drawing.Size(73, 22);
            this.toolStripButton_Pass.Text = "审核通过";
            this.toolStripButton_Pass.Click += new System.EventHandler(this.toolStripButton_Pass_Click);
            // 
            // toolStripButton_No
            // 
            this.toolStripButton_No.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_No.Image")));
            this.toolStripButton_No.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_No.Name = "toolStripButton_No";
            this.toolStripButton_No.Size = new System.Drawing.Size(85, 22);
            this.toolStripButton_No.Text = "审核不通过";
            this.toolStripButton_No.Click += new System.EventHandler(this.toolStripButton_No_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.memo_yTextBox);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.InWare_selTextInpt);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.totalmoney_yTextBox);
            this.groupBox1.Controls.Add(this.ytComboBox_Plan);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(818, 136);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // memo_yTextBox
            // 
            // 
            // 
            // 
            this.memo_yTextBox.Border.Class = "TextBoxBorder";
            this.memo_yTextBox.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.memo_yTextBox.Location = new System.Drawing.Point(180, 96);
            this.memo_yTextBox.Name = "memo_yTextBox";
            this.memo_yTextBox.Size = new System.Drawing.Size(450, 21);
            this.memo_yTextBox.TabIndex = 0;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(493, 26);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(135, 21);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // InWare_selTextInpt
            // 
            this.InWare_selTextInpt.ColDefText = null;
            this.InWare_selTextInpt.ColStyle = null;
            this.InWare_selTextInpt.DataType = null;
            this.InWare_selTextInpt.DbConn = null;
            this.InWare_selTextInpt.Enabled = false;
            this.InWare_selTextInpt.Location = new System.Drawing.Point(182, 25);
            this.InWare_selTextInpt.Name = "InWare_selTextInpt";
            this.InWare_selTextInpt.NextFocusControl = null;
            this.InWare_selTextInpt.ReadOnly = false;
            this.InWare_selTextInpt.SelParam = null;
            this.InWare_selTextInpt.ShowColNum = 0;
            this.InWare_selTextInpt.ShowWidth = 0;
            this.InWare_selTextInpt.Size = new System.Drawing.Size(135, 22);
            this.InWare_selTextInpt.Sql = null;
            this.InWare_selTextInpt.SqlStr = null;
            this.InWare_selTextInpt.TabIndex = 1;
            this.InWare_selTextInpt.TvColName = null;
            this.InWare_selTextInpt.Value = null;
            this.InWare_selTextInpt.WatermarkText = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(413, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "制定日期";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(110, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "计划类型";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(429, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "总金额";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(130, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "备注";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(114, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "采购库房";
            // 
            // totalmoney_yTextBox
            // 
            // 
            // 
            // 
            this.totalmoney_yTextBox.Border.Class = "TextBoxBorder";
            this.totalmoney_yTextBox.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.totalmoney_yTextBox.Location = new System.Drawing.Point(493, 61);
            this.totalmoney_yTextBox.Name = "totalmoney_yTextBox";
            this.totalmoney_yTextBox.ReadOnly = true;
            this.totalmoney_yTextBox.Size = new System.Drawing.Size(135, 21);
            this.totalmoney_yTextBox.TabIndex = 0;
            // 
            // ytComboBox_Plan
            // 
            this.ytComboBox_Plan.CacheKey = null;
            this.ytComboBox_Plan.DbConn = null;
            this.ytComboBox_Plan.DefText = null;
            this.ytComboBox_Plan.DefValue = null;
            this.ytComboBox_Plan.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ytComboBox_Plan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ytComboBox_Plan.EnableEmpty = true;
            this.ytComboBox_Plan.FirstText = null;
            this.ytComboBox_Plan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ytComboBox_Plan.Fomart = null;
            this.ytComboBox_Plan.ItemStr = "";
            this.ytComboBox_Plan.Location = new System.Drawing.Point(181, 61);
            this.ytComboBox_Plan.Name = "ytComboBox_ifUse";
            this.ytComboBox_Plan.Param = null;
            this.ytComboBox_Plan.Size = new System.Drawing.Size(135, 22);
            this.ytComboBox_Plan.Sql = null;
            this.ytComboBox_Plan.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ytComboBox_Plan.TabIndex = 0;
            this.ytComboBox_Plan.Tag = tvList1;
            this.ytComboBox_Plan.Value = null;
            // 
            // dataGView1
            // 
            this.dataGView1.AllowUserToAddRows = false;
            this.dataGView1.AllowUserToDeleteRows = false;
            this.dataGView1.AllowUserToResizeRows = false;
            this.dataGView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGView1.ChangeDataColumName = null;
            this.dataGView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.eqname,
            this.eqid,
            this.gg,
            this.xh,
            this.country,
            this.unitcode,
            this.nownum,
            this.num,
            this.price,
            this.money,
            this.scs,
            this.gys,
            this.memo,
            this.rowno,
            this.planid,
            this.applyid,
            this.txm,
            this.choscode,
            this.stockflowno});
            this.dataGView1.DbConn = null;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGView1.DwColIndex = 0;
            this.dataGView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGView1.IsEditOnEnter = true;
            this.dataGView1.IsFillForm = true;
            this.dataGView1.IsPage = false;
            this.dataGView1.Key = null;
            this.dataGView1.Location = new System.Drawing.Point(0, 161);
            this.dataGView1.Name = "dataGView1";
            this.dataGView1.RowHeadersWidth = 20;
            this.dataGView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGView1.RowTemplate.Height = 23;
            this.dataGView1.Size = new System.Drawing.Size(818, 285);
            this.dataGView1.TabIndex = 41;
            this.dataGView1.TjFmtStr = null;
            this.dataGView1.TjFormat = null;
            this.dataGView1.Url = null;
            this.dataGView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGView1_RowPostPaint);
            // 
            // eqname
            // 
            this.eqname.DataPropertyName = "EQNAME";
            this.eqname.HeaderText = "设备名称";
            this.eqname.Name = "eqname";
            this.eqname.Width = 78;
            // 
            // eqid
            // 
            this.eqid.DataPropertyName = "EQID";
            this.eqid.HeaderText = "设备ID";
            this.eqid.Name = "eqid";
            this.eqid.ReadOnly = true;
            this.eqid.Width = 66;
            // 
            // gg
            // 
            this.gg.DataPropertyName = "GG";
            this.gg.HeaderText = "规格";
            this.gg.Name = "gg";
            this.gg.ReadOnly = true;
            this.gg.Width = 54;
            // 
            // xh
            // 
            this.xh.DataPropertyName = "XH";
            this.xh.HeaderText = "型号";
            this.xh.Name = "xh";
            this.xh.ReadOnly = true;
            this.xh.Width = 54;
            // 
            // country
            // 
            this.country.DataPropertyName = "COUNTRY";
            this.country.HeaderText = "国别";
            this.country.Name = "country";
            this.country.Width = 54;
            // 
            // unitcode
            // 
            this.unitcode.DataPropertyName = "UNITCODE";
            this.unitcode.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.unitcode.HeaderText = "单位编码";
            this.unitcode.Name = "unitcode";
            this.unitcode.ReadOnly = true;
            this.unitcode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.unitcode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.unitcode.Width = 78;
            // 
            // nownum
            // 
            this.nownum.DataPropertyName = "NOWNUM";
            this.nownum.HeaderText = "当前库存数量";
            this.nownum.Name = "nownum";
            this.nownum.ReadOnly = true;
            this.nownum.Width = 102;
            // 
            // num
            // 
            this.num.DataPropertyName = "NUM";
            this.num.HeaderText = "采购数量";
            this.num.Name = "num";
            this.num.Width = 78;
            // 
            // price
            // 
            this.price.DataPropertyName = "PRICE";
            this.price.HeaderText = "采购单价";
            this.price.Name = "price";
            this.price.Width = 78;
            // 
            // money
            // 
            this.money.DataPropertyName = "MONEY";
            this.money.HeaderText = "采购金额";
            this.money.Name = "money";
            this.money.ReadOnly = true;
            this.money.Width = 78;
            // 
            // scs
            // 
            this.scs.DataPropertyName = "SCS";
            this.scs.HeaderText = "生产商";
            this.scs.Name = "scs";
            this.scs.Width = 66;
            // 
            // gys
            // 
            this.gys.DataPropertyName = "GYS";
            this.gys.HeaderText = "供应商";
            this.gys.Name = "gys";
            this.gys.Width = 66;
            // 
            // memo
            // 
            this.memo.DataPropertyName = "MEMO";
            this.memo.HeaderText = "备注";
            this.memo.Name = "memo";
            this.memo.Width = 54;
            // 
            // rowno
            // 
            this.rowno.DataPropertyName = "ROWNO";
            this.rowno.HeaderText = "行号";
            this.rowno.Name = "rowno";
            this.rowno.ReadOnly = true;
            this.rowno.Width = 54;
            // 
            // planid
            // 
            this.planid.DataPropertyName = "PLANID";
            this.planid.HeaderText = "采购计划ID";
            this.planid.Name = "planid";
            this.planid.Width = 90;
            // 
            // applyid
            // 
            this.applyid.DataPropertyName = "APPLYID";
            this.applyid.HeaderText = "请购ID";
            this.applyid.Name = "applyid";
            this.applyid.Width = 66;
            // 
            // txm
            // 
            this.txm.DataPropertyName = "TXM";
            this.txm.HeaderText = "条形码";
            this.txm.Name = "txm";
            this.txm.ReadOnly = true;
            this.txm.Width = 66;
            // 
            // choscode
            // 
            this.choscode.DataPropertyName = "CHOSCODE";
            this.choscode.HeaderText = "医疗机构编码";
            this.choscode.Name = "choscode";
            this.choscode.ReadOnly = true;
            this.choscode.Visible = false;
            this.choscode.Width = 102;
            // 
            // stockflowno
            // 
            this.stockflowno.DataPropertyName = "STOCKFLOWNO";
            this.stockflowno.HeaderText = "对应的库存流水表的流水号";
            this.stockflowno.Name = "stockflowno";
            this.stockflowno.ReadOnly = true;
            this.stockflowno.Width = 174;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(647, 64);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 12);
            this.label12.TabIndex = 75;
            this.label12.Text = "共：0条";
            // 
            // PurchasePlan_Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 446);
            this.Controls.Add(this.dataGView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "PurchasePlan_Add";
            this.Text = "采购计划单";
            this.Load += new System.EventHandler(this.PurchasePlan_Add_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton add_toolStripButton;
        private System.Windows.Forms.ToolStripButton DeleButton;
        private System.Windows.Forms.ToolStripButton save_toolStripButton;
        private System.Windows.Forms.ToolStripButton cancel_toolStripButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private YtWinContrl.com.contrl.YTextBox memo_yTextBox;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private YtWinContrl.com.contrl.SelTextInpt InWare_selTextInpt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private YtWinContrl.com.contrl.YTextBox totalmoney_yTextBox;
        private YtWinContrl.com.contrl.YtComboBox ytComboBox_Plan;
        private System.Windows.Forms.ToolStripButton Get_toolStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButton_Pass;
        private System.Windows.Forms.ToolStripButton toolStripButton_No;
        private YtWinContrl.com.datagrid.DataGView dataGView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn eqname;
        private System.Windows.Forms.DataGridViewTextBoxColumn eqid;
        private System.Windows.Forms.DataGridViewTextBoxColumn gg;
        private System.Windows.Forms.DataGridViewTextBoxColumn xh;
        private System.Windows.Forms.DataGridViewTextBoxColumn country;
        private System.Windows.Forms.DataGridViewComboBoxColumn unitcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn nownum;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn money;
        private System.Windows.Forms.DataGridViewTextBoxColumn scs;
        private System.Windows.Forms.DataGridViewTextBoxColumn gys;
        private System.Windows.Forms.DataGridViewTextBoxColumn memo;
        private System.Windows.Forms.DataGridViewTextBoxColumn rowno;
        private System.Windows.Forms.DataGridViewTextBoxColumn planid;
        private System.Windows.Forms.DataGridViewTextBoxColumn applyid;
        private System.Windows.Forms.DataGridViewTextBoxColumn txm;
        private System.Windows.Forms.DataGridViewTextBoxColumn choscode;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockflowno;
        private System.Windows.Forms.Label label12;
    }
}
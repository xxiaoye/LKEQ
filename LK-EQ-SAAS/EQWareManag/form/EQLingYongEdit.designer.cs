namespace EQWareManag.form
{
    partial class EQLingYongEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EQLingYongEdit));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Add_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.del_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.Save_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.Cancel_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.outfalg_selTextInpt = new YtWinContrl.com.contrl.SelTextInpt();
            this.targetDeptid_selTextInpt = new YtWinContrl.com.contrl.SelTextInpt();
            this.ware_selTextInpt = new YtWinContrl.com.contrl.SelTextInpt();
            this.memo_textBox = new System.Windows.Forms.TextBox();
            this.TotalMoney_textBox1 = new System.Windows.Forms.TextBox();
            this.RecipeCode_textBox = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGView1 = new YtWinContrl.com.datagrid.DataGView();
            this.SheBeiMingChen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KuCunLiuShui_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shuliang_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memo_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KCYLColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unitcode_Column = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Liushuihao_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chuku_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.danjia_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jine_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yunzafei_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chengbendanjia_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chengbenjine_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guige_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xinghao_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.changdi_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiaoxingma_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.changjiaid_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.changjiamingcheng_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shengchanriqi_Column = new YtWinContrl.com.datagrid.CalendarColumn();
            this.youxiaoqi_Column = new YtWinContrl.com.datagrid.CalendarColumn();
            this.Choscode_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EqIdName_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label14 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add_toolStrip,
            this.del_toolStrip,
            this.Save_toolStrip,
            this.Cancel_toolStrip});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(948, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Add_toolStrip
            // 
            this.Add_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("Add_toolStrip.Image")));
            this.Add_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Add_toolStrip.Name = "Add_toolStrip";
            this.Add_toolStrip.Size = new System.Drawing.Size(73, 22);
            this.Add_toolStrip.Text = "新增设备";
            this.Add_toolStrip.Click += new System.EventHandler(this.Add_toolStrip_Click);
            // 
            // del_toolStrip
            // 
            this.del_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("del_toolStrip.Image")));
            this.del_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.del_toolStrip.Name = "del_toolStrip";
            this.del_toolStrip.Size = new System.Drawing.Size(73, 22);
            this.del_toolStrip.Text = "删除设备";
            this.del_toolStrip.Click += new System.EventHandler(this.del_toolStrip_Click);
            // 
            // Save_toolStrip
            // 
            this.Save_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("Save_toolStrip.Image")));
            this.Save_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Save_toolStrip.Name = "Save_toolStrip";
            this.Save_toolStrip.Size = new System.Drawing.Size(49, 22);
            this.Save_toolStrip.Text = "保存";
            this.Save_toolStrip.Click += new System.EventHandler(this.Save_toolStrip_Click);
            // 
            // Cancel_toolStrip
            // 
            this.Cancel_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("Cancel_toolStrip.Image")));
            this.Cancel_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Cancel_toolStrip.Name = "Cancel_toolStrip";
            this.Cancel_toolStrip.Size = new System.Drawing.Size(49, 22);
            this.Cancel_toolStrip.Text = "关闭";
            this.Cancel_toolStrip.Click += new System.EventHandler(this.Cancel_toolStrip_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.outfalg_selTextInpt);
            this.groupBox1.Controls.Add(this.targetDeptid_selTextInpt);
            this.groupBox1.Controls.Add(this.ware_selTextInpt);
            this.groupBox1.Controls.Add(this.memo_textBox);
            this.groupBox1.Controls.Add(this.TotalMoney_textBox1);
            this.groupBox1.Controls.Add(this.RecipeCode_textBox);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(948, 178);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "领用表信息(蓝色为必填)";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(790, 75);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(120, 21);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // outfalg_selTextInpt
            // 
            this.outfalg_selTextInpt.ColDefText = null;
            this.outfalg_selTextInpt.ColStyle = null;
            this.outfalg_selTextInpt.DataType = null;
            this.outfalg_selTextInpt.DbConn = null;
            this.outfalg_selTextInpt.Location = new System.Drawing.Point(790, 29);
            this.outfalg_selTextInpt.Name = "outfalg_selTextInpt";
            this.outfalg_selTextInpt.NextFocusControl = null;
            this.outfalg_selTextInpt.ReadOnly = false;
            this.outfalg_selTextInpt.SelParam = null;
            this.outfalg_selTextInpt.ShowColNum = 0;
            this.outfalg_selTextInpt.ShowWidth = 0;
            this.outfalg_selTextInpt.Size = new System.Drawing.Size(120, 22);
            this.outfalg_selTextInpt.Sql = null;
            this.outfalg_selTextInpt.SqlStr = null;
            this.outfalg_selTextInpt.TabIndex = 2;
            this.outfalg_selTextInpt.TvColName = null;
            this.outfalg_selTextInpt.Value = null;
            this.outfalg_selTextInpt.WatermarkText = "";
            // 
            // targetDeptid_selTextInpt
            // 
            this.targetDeptid_selTextInpt.ColDefText = null;
            this.targetDeptid_selTextInpt.ColStyle = null;
            this.targetDeptid_selTextInpt.DataType = null;
            this.targetDeptid_selTextInpt.DbConn = null;
            this.targetDeptid_selTextInpt.Location = new System.Drawing.Point(409, 29);
            this.targetDeptid_selTextInpt.Name = "targetDeptid_selTextInpt";
            this.targetDeptid_selTextInpt.NextFocusControl = null;
            this.targetDeptid_selTextInpt.ReadOnly = false;
            this.targetDeptid_selTextInpt.SelParam = null;
            this.targetDeptid_selTextInpt.ShowColNum = 0;
            this.targetDeptid_selTextInpt.ShowWidth = 0;
            this.targetDeptid_selTextInpt.Size = new System.Drawing.Size(120, 22);
            this.targetDeptid_selTextInpt.Sql = null;
            this.targetDeptid_selTextInpt.SqlStr = null;
            this.targetDeptid_selTextInpt.TabIndex = 1;
            this.targetDeptid_selTextInpt.TvColName = null;
            this.targetDeptid_selTextInpt.Value = null;
            this.targetDeptid_selTextInpt.WatermarkText = "";
            // 
            // ware_selTextInpt
            // 
            this.ware_selTextInpt.ColDefText = null;
            this.ware_selTextInpt.ColStyle = null;
            this.ware_selTextInpt.DataType = null;
            this.ware_selTextInpt.DbConn = null;
            this.ware_selTextInpt.Location = new System.Drawing.Point(97, 29);
            this.ware_selTextInpt.Name = "ware_selTextInpt";
            this.ware_selTextInpt.NextFocusControl = null;
            this.ware_selTextInpt.ReadOnly = false;
            this.ware_selTextInpt.SelParam = null;
            this.ware_selTextInpt.ShowColNum = 0;
            this.ware_selTextInpt.ShowWidth = 0;
            this.ware_selTextInpt.Size = new System.Drawing.Size(120, 22);
            this.ware_selTextInpt.Sql = null;
            this.ware_selTextInpt.SqlStr = null;
            this.ware_selTextInpt.TabIndex = 0;
            this.ware_selTextInpt.TvColName = null;
            this.ware_selTextInpt.Value = null;
            this.ware_selTextInpt.WatermarkText = "";
            // 
            // memo_textBox
            // 
            this.memo_textBox.Location = new System.Drawing.Point(97, 130);
            this.memo_textBox.Name = "memo_textBox";
            this.memo_textBox.Size = new System.Drawing.Size(813, 21);
            this.memo_textBox.TabIndex = 4;
            // 
            // TotalMoney_textBox1
            // 
            this.TotalMoney_textBox1.Location = new System.Drawing.Point(409, 81);
            this.TotalMoney_textBox1.Name = "TotalMoney_textBox1";
            this.TotalMoney_textBox1.ReadOnly = true;
            this.TotalMoney_textBox1.Size = new System.Drawing.Size(120, 21);
            this.TotalMoney_textBox1.TabIndex = 1;
            this.TotalMoney_textBox1.TabStop = false;
            // 
            // RecipeCode_textBox
            // 
            this.RecipeCode_textBox.Location = new System.Drawing.Point(97, 82);
            this.RecipeCode_textBox.Name = "RecipeCode_textBox";
            this.RecipeCode_textBox.ReadOnly = true;
            this.RecipeCode_textBox.Size = new System.Drawing.Size(120, 21);
            this.RecipeCode_textBox.TabIndex = 1;
            this.RecipeCode_textBox.TabStop = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(37, 137);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(29, 12);
            this.label22.TabIndex = 0;
            this.label22.Text = "备注";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(704, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "制单日期";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(310, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "总金额";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "单据号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(704, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "出库方式";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(310, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "申领到科室-->";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(37, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "从库房";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGView1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 203);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(948, 314);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "领用表信息";
            // 
            // dataGView1
            // 
            this.dataGView1.AllowUserToAddRows = false;
            this.dataGView1.AllowUserToDeleteRows = false;
            this.dataGView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGView1.ChangeDataColumName = null;
            this.dataGView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SheBeiMingChen,
            this.KuCunLiuShui_Column,
            this.shuliang_Column,
            this.memo_Column,
            this.KCYLColumn,
            this.Unitcode_Column,
            this.Liushuihao_Column,
            this.chuku_Column,
            this.danjia_Column,
            this.jine_Column,
            this.yunzafei_Column,
            this.chengbendanjia_Column,
            this.chengbenjine_Column,
            this.guige_Column,
            this.xinghao_Column,
            this.changdi_Column,
            this.tiaoxingma_Column,
            this.changjiaid_Column,
            this.changjiamingcheng_Column,
            this.shengchanriqi_Column,
            this.youxiaoqi_Column,
            this.Choscode_Column,
            this.EqIdName_Column});
            this.dataGView1.DbConn = null;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGView1.DwColIndex = 0;
            this.dataGView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGView1.IsEditOnEnter = true;
            this.dataGView1.IsFillForm = true;
            this.dataGView1.IsPage = false;
            this.dataGView1.Key = null;
            this.dataGView1.Location = new System.Drawing.Point(3, 17);
            this.dataGView1.MultiSelect = false;
            this.dataGView1.Name = "dataGView1";
            this.dataGView1.RowHeadersWidth = 15;
            this.dataGView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGView1.RowTemplate.Height = 23;
            this.dataGView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGView1.Size = new System.Drawing.Size(942, 294);
            this.dataGView1.TabIndex = 6;
            this.dataGView1.TjFmtStr = null;
            this.dataGView1.TjFormat = null;
            this.dataGView1.Url = null;
            this.dataGView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGView1_CellValueChanged);
            this.dataGView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGView1_RowPostPaint);
            this.dataGView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGView1_CellEnter);
            // 
            // SheBeiMingChen
            // 
            this.SheBeiMingChen.DataPropertyName = "EQNAME";
            this.SheBeiMingChen.HeaderText = "设备名称";
            this.SheBeiMingChen.Name = "SheBeiMingChen";
            this.SheBeiMingChen.Width = 78;
            // 
            // KuCunLiuShui_Column
            // 
            this.KuCunLiuShui_Column.DataPropertyName = "STOCKFLOWNO";
            this.KuCunLiuShui_Column.HeaderText = "库存流水号";
            this.KuCunLiuShui_Column.Name = "KuCunLiuShui_Column";
            this.KuCunLiuShui_Column.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.KuCunLiuShui_Column.Width = 90;
            // 
            // shuliang_Column
            // 
            this.shuliang_Column.DataPropertyName = "NUM";
            this.shuliang_Column.HeaderText = "数量";
            this.shuliang_Column.Name = "shuliang_Column";
            this.shuliang_Column.Width = 54;
            // 
            // memo_Column
            // 
            this.memo_Column.DataPropertyName = "MEMO";
            this.memo_Column.HeaderText = "备注";
            this.memo_Column.Name = "memo_Column";
            this.memo_Column.Width = 54;
            // 
            // KCYLColumn
            // 
            this.KCYLColumn.DataPropertyName = "库存余量";
            this.KCYLColumn.HeaderText = "库存余量";
            this.KCYLColumn.Name = "KCYLColumn";
            this.KCYLColumn.ReadOnly = true;
            this.KCYLColumn.Width = 78;
            // 
            // Unitcode_Column
            // 
            this.Unitcode_Column.DataPropertyName = "UNITCODE";
            this.Unitcode_Column.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.Unitcode_Column.HeaderText = "单位";
            this.Unitcode_Column.Name = "Unitcode_Column";
            this.Unitcode_Column.ReadOnly = true;
            this.Unitcode_Column.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Unitcode_Column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Unitcode_Column.Width = 54;
            // 
            // Liushuihao_Column
            // 
            this.Liushuihao_Column.DataPropertyName = "DETAILNO";
            this.Liushuihao_Column.HeaderText = "流水号";
            this.Liushuihao_Column.Name = "Liushuihao_Column";
            this.Liushuihao_Column.ReadOnly = true;
            this.Liushuihao_Column.Width = 66;
            // 
            // chuku_Column
            // 
            this.chuku_Column.DataPropertyName = "OUTID";
            this.chuku_Column.HeaderText = "出库ID";
            this.chuku_Column.Name = "chuku_Column";
            this.chuku_Column.ReadOnly = true;
            this.chuku_Column.Width = 66;
            // 
            // danjia_Column
            // 
            this.danjia_Column.DataPropertyName = "PRICE";
            this.danjia_Column.HeaderText = "单价";
            this.danjia_Column.Name = "danjia_Column";
            this.danjia_Column.ReadOnly = true;
            this.danjia_Column.Width = 54;
            // 
            // jine_Column
            // 
            this.jine_Column.DataPropertyName = "MONEY";
            this.jine_Column.HeaderText = "金额";
            this.jine_Column.Name = "jine_Column";
            this.jine_Column.ReadOnly = true;
            this.jine_Column.Width = 54;
            // 
            // yunzafei_Column
            // 
            this.yunzafei_Column.DataPropertyName = "OTHERMONEY";
            this.yunzafei_Column.HeaderText = "运杂费";
            this.yunzafei_Column.Name = "yunzafei_Column";
            this.yunzafei_Column.ReadOnly = true;
            this.yunzafei_Column.Width = 66;
            // 
            // chengbendanjia_Column
            // 
            this.chengbendanjia_Column.DataPropertyName = "TOTALPRICE";
            this.chengbendanjia_Column.HeaderText = "成本单价";
            this.chengbendanjia_Column.Name = "chengbendanjia_Column";
            this.chengbendanjia_Column.ReadOnly = true;
            this.chengbendanjia_Column.Width = 78;
            // 
            // chengbenjine_Column
            // 
            this.chengbenjine_Column.DataPropertyName = "TOTALMONEY";
            this.chengbenjine_Column.HeaderText = "成本金额";
            this.chengbenjine_Column.Name = "chengbenjine_Column";
            this.chengbenjine_Column.ReadOnly = true;
            this.chengbenjine_Column.Width = 78;
            // 
            // guige_Column
            // 
            this.guige_Column.DataPropertyName = "GG";
            this.guige_Column.HeaderText = "规格";
            this.guige_Column.Name = "guige_Column";
            this.guige_Column.ReadOnly = true;
            this.guige_Column.Width = 54;
            // 
            // xinghao_Column
            // 
            this.xinghao_Column.DataPropertyName = "XH";
            this.xinghao_Column.HeaderText = "型号";
            this.xinghao_Column.Name = "xinghao_Column";
            this.xinghao_Column.ReadOnly = true;
            this.xinghao_Column.Width = 54;
            // 
            // changdi_Column
            // 
            this.changdi_Column.DataPropertyName = "CD";
            this.changdi_Column.HeaderText = "产地";
            this.changdi_Column.Name = "changdi_Column";
            this.changdi_Column.ReadOnly = true;
            this.changdi_Column.Width = 54;
            // 
            // tiaoxingma_Column
            // 
            this.tiaoxingma_Column.DataPropertyName = "TXM";
            this.tiaoxingma_Column.HeaderText = "条形码";
            this.tiaoxingma_Column.Name = "tiaoxingma_Column";
            this.tiaoxingma_Column.ReadOnly = true;
            this.tiaoxingma_Column.Width = 66;
            // 
            // changjiaid_Column
            // 
            this.changjiaid_Column.DataPropertyName = "SUPPLYID";
            this.changjiaid_Column.HeaderText = "生产厂家ID";
            this.changjiaid_Column.Name = "changjiaid_Column";
            this.changjiaid_Column.ReadOnly = true;
            this.changjiaid_Column.Width = 90;
            // 
            // changjiamingcheng_Column
            // 
            this.changjiamingcheng_Column.DataPropertyName = "SUPPLYNAME";
            this.changjiamingcheng_Column.HeaderText = "生产厂家名称";
            this.changjiamingcheng_Column.Name = "changjiamingcheng_Column";
            this.changjiamingcheng_Column.ReadOnly = true;
            this.changjiamingcheng_Column.Width = 102;
            // 
            // shengchanriqi_Column
            // 
            this.shengchanriqi_Column.DataPropertyName = "PRODUCTDATE";
            this.shengchanriqi_Column.Formt = null;
            this.shengchanriqi_Column.HeaderText = "生产日期";
            this.shengchanriqi_Column.Name = "shengchanriqi_Column";
            this.shengchanriqi_Column.ReadOnly = true;
            this.shengchanriqi_Column.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.shengchanriqi_Column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.shengchanriqi_Column.Width = 78;
            // 
            // youxiaoqi_Column
            // 
            this.youxiaoqi_Column.DataPropertyName = "VALIDDATE";
            this.youxiaoqi_Column.Formt = null;
            this.youxiaoqi_Column.HeaderText = "有效期";
            this.youxiaoqi_Column.Name = "youxiaoqi_Column";
            this.youxiaoqi_Column.ReadOnly = true;
            this.youxiaoqi_Column.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.youxiaoqi_Column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.youxiaoqi_Column.Width = 66;
            // 
            // Choscode_Column
            // 
            this.Choscode_Column.DataPropertyName = "CHOSCODE";
            this.Choscode_Column.HeaderText = "医疗机构编码";
            this.Choscode_Column.Name = "Choscode_Column";
            this.Choscode_Column.ReadOnly = true;
            this.Choscode_Column.Width = 102;
            // 
            // EqIdName_Column
            // 
            this.EqIdName_Column.DataPropertyName = "EQID";
            this.EqIdName_Column.HeaderText = "设备ID";
            this.EqIdName_Column.Name = "EqIdName_Column";
            this.EqIdName_Column.ReadOnly = true;
            this.EqIdName_Column.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.EqIdName_Column.Width = 66;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(551, 85);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 12);
            this.label14.TabIndex = 76;
            this.label14.Text = "共：0条";
            // 
            // EQLingYongEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 517);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "EQLingYongEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备领用单";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.EQLingYongEdit_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Add_toolStrip;
        private System.Windows.Forms.ToolStripButton del_toolStrip;
        private System.Windows.Forms.ToolStripButton Save_toolStrip;
        private System.Windows.Forms.ToolStripButton Cancel_toolStrip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private YtWinContrl.com.contrl.SelTextInpt outfalg_selTextInpt;
        private YtWinContrl.com.contrl.SelTextInpt targetDeptid_selTextInpt;
        private YtWinContrl.com.contrl.SelTextInpt ware_selTextInpt;
        private System.Windows.Forms.TextBox memo_textBox;
        private System.Windows.Forms.TextBox TotalMoney_textBox1;
        private System.Windows.Forms.TextBox RecipeCode_textBox;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private YtWinContrl.com.datagrid.DataGView dataGView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SheBeiMingChen;
        private System.Windows.Forms.DataGridViewTextBoxColumn KuCunLiuShui_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn shuliang_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn memo_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn KCYLColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn Unitcode_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn Liushuihao_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn chuku_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn danjia_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn jine_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn yunzafei_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn chengbendanjia_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn chengbenjine_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn guige_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn xinghao_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn changdi_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiaoxingma_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn changjiaid_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn changjiamingcheng_Column;
        private YtWinContrl.com.datagrid.CalendarColumn shengchanriqi_Column;
        private YtWinContrl.com.datagrid.CalendarColumn youxiaoqi_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn Choscode_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn EqIdName_Column;
        private System.Windows.Forms.Label label14;
    }
}
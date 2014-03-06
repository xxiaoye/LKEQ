namespace EQWareManag
{
    partial class EQOut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EQOut));
            YtWinContrl.com.datagrid.TvList tvList2 = new YtWinContrl.com.datagrid.TvList();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.add_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ModifyButton = new System.Windows.Forms.ToolStripButton();
            this.DeleButton = new System.Windows.Forms.ToolStripButton();
            this.check_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SubmitCheck_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.InWare_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.scan_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Search_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimeDuan1 = new YtWinContrl.com.contrl.DateTimeDuan();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.InStatus_ytComboBox = new YtWinContrl.com.contrl.YtComboBox();
            this.InWare_selTextInpt = new YtWinContrl.com.contrl.SelTextInpt();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.JinEHeJi = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.TiaoSu = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dataGView1 = new YtWinContrl.com.datagrid.DataGView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column25 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.add_toolStripButton,
            this.ModifyButton,
            this.DeleButton,
            this.check_toolStripButton,
            this.SubmitCheck_toolStripButton,
            this.InWare_toolStripButton,
            this.scan_toolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(879, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // add_toolStripButton
            // 
            this.add_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("add_toolStripButton.Image")));
            this.add_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.add_toolStripButton.Name = "add_toolStripButton";
            this.add_toolStripButton.Size = new System.Drawing.Size(97, 22);
            this.add_toolStripButton.Text = "新增出库信息";
            this.add_toolStripButton.Click += new System.EventHandler(this.add_toolStripButton_Click);
            // 
            // ModifyButton
            // 
            this.ModifyButton.Image = ((System.Drawing.Image)(resources.GetObject("ModifyButton.Image")));
            this.ModifyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ModifyButton.Name = "ModifyButton";
            this.ModifyButton.Size = new System.Drawing.Size(97, 22);
            this.ModifyButton.Text = "编辑出库信息";
            this.ModifyButton.Click += new System.EventHandler(this.ModifyButton_Click);
            // 
            // DeleButton
            // 
            this.DeleButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleButton.Image")));
            this.DeleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleButton.Name = "DeleButton";
            this.DeleButton.Size = new System.Drawing.Size(97, 22);
            this.DeleButton.Text = "删除出库信息";
            this.DeleButton.Click += new System.EventHandler(this.DeleButton_Click);
            // 
            // check_toolStripButton
            // 
            this.check_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("check_toolStripButton.Image")));
            this.check_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.check_toolStripButton.Name = "check_toolStripButton";
            this.check_toolStripButton.Size = new System.Drawing.Size(49, 22);
            this.check_toolStripButton.Text = "审核";
            this.check_toolStripButton.Click += new System.EventHandler(this.check_toolStripButton_Click);
            // 
            // SubmitCheck_toolStripButton
            // 
            this.SubmitCheck_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SubmitCheck_toolStripButton.Image")));
            this.SubmitCheck_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SubmitCheck_toolStripButton.Name = "SubmitCheck_toolStripButton";
            this.SubmitCheck_toolStripButton.Size = new System.Drawing.Size(73, 22);
            this.SubmitCheck_toolStripButton.Text = "提交审核";
            this.SubmitCheck_toolStripButton.Click += new System.EventHandler(this.UnCheck_toolStripButton_Click);
            // 
            // InWare_toolStripButton
            // 
            this.InWare_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("InWare_toolStripButton.Image")));
            this.InWare_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InWare_toolStripButton.Name = "InWare_toolStripButton";
            this.InWare_toolStripButton.Size = new System.Drawing.Size(49, 22);
            this.InWare_toolStripButton.Text = "冲销";
            this.InWare_toolStripButton.Click += new System.EventHandler(this.InWare_toolStripButton_Click);
            // 
            // scan_toolStripButton
            // 
            this.scan_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("scan_toolStripButton.Image")));
            this.scan_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.scan_toolStripButton.Name = "scan_toolStripButton";
            this.scan_toolStripButton.Size = new System.Drawing.Size(49, 22);
            this.scan_toolStripButton.Text = "查看";
            this.scan_toolStripButton.Click += new System.EventHandler(this.scan_toolStripButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Search_button);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.dateTimeDuan1);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.InStatus_ytComboBox);
            this.groupBox1.Controls.Add(this.InWare_selTextInpt);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(879, 99);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(149, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "出库库房";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(354, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 33;
            this.label1.Text = "出库状态";
            // 
            // Search_button
            // 
            this.Search_button.Image = ((System.Drawing.Image)(resources.GetObject("Search_button.Image")));
            this.Search_button.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.Search_button.Location = new System.Drawing.Point(614, 62);
            this.Search_button.Name = "Search_button";
            this.Search_button.Size = new System.Drawing.Size(82, 26);
            this.Search_button.TabIndex = 32;
            this.Search_button.Text = "   查询";
            this.Search_button.UseVisualStyleBackColor = true;
            this.Search_button.Click += new System.EventHandler(this.Search_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(563, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 31;
            this.label3.Text = "到";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(372, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 30;
            this.label4.Text = "从";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(139, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 29;
            this.label5.Text = "常用时间段";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Checked = false;
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(591, 21);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(127, 21);
            this.dateTimePicker2.TabIndex = 28;
            // 
            // dateTimeDuan1
            // 
            this.dateTimeDuan1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dateTimeDuan1.End = this.dateTimePicker2;
            this.dateTimeDuan1.FormattingEnabled = true;
            this.dateTimeDuan1.Location = new System.Drawing.Point(220, 20);
            this.dateTimeDuan1.Name = "dateTimeDuan1";
            this.dateTimeDuan1.Size = new System.Drawing.Size(108, 20);
            this.dateTimeDuan1.Start = this.dateTimePicker1;
            this.dateTimeDuan1.TabIndex = 27;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Checked = false;
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(417, 21);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(127, 21);
            this.dateTimePicker1.TabIndex = 26;
            // 
            // InStatus_ytComboBox
            // 
            this.InStatus_ytComboBox.CacheKey = null;
            this.InStatus_ytComboBox.DbConn = null;
            this.InStatus_ytComboBox.DefText = null;
            this.InStatus_ytComboBox.DefValue = null;
            this.InStatus_ytComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.InStatus_ytComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InStatus_ytComboBox.EnableEmpty = true;
            this.InStatus_ytComboBox.FirstText = null;
            this.InStatus_ytComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InStatus_ytComboBox.Fomart = null;
            this.InStatus_ytComboBox.ItemStr = "";
            this.InStatus_ytComboBox.Location = new System.Drawing.Point(419, 64);
            this.InStatus_ytComboBox.Name = "InStatus_ytComboBox";
            this.InStatus_ytComboBox.Param = null;
            this.InStatus_ytComboBox.Size = new System.Drawing.Size(127, 22);
            this.InStatus_ytComboBox.Sql = null;
            this.InStatus_ytComboBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.InStatus_ytComboBox.TabIndex = 0;
            this.InStatus_ytComboBox.Tag = tvList2;
            this.InStatus_ytComboBox.Value = null;
            // 
            // InWare_selTextInpt
            // 
            this.InWare_selTextInpt.ColDefText = null;
            this.InWare_selTextInpt.ColStyle = null;
            this.InWare_selTextInpt.DataType = null;
            this.InWare_selTextInpt.DbConn = null;
            this.InWare_selTextInpt.Location = new System.Drawing.Point(219, 63);
            this.InWare_selTextInpt.Name = "InWare_selTextInpt";
            this.InWare_selTextInpt.NextFocusControl = null;
            this.InWare_selTextInpt.ReadOnly = false;
            this.InWare_selTextInpt.SelParam = null;
            this.InWare_selTextInpt.ShowColNum = 0;
            this.InWare_selTextInpt.ShowWidth = 0;
            this.InWare_selTextInpt.Size = new System.Drawing.Size(111, 22);
            this.InWare_selTextInpt.Sql = null;
            this.InWare_selTextInpt.SqlStr = null;
            this.InWare_selTextInpt.TabIndex = 34;
            this.InWare_selTextInpt.TvColName = null;
            this.InWare_selTextInpt.Value = null;
            this.InWare_selTextInpt.WatermarkText = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.JinEHeJi);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.TiaoSu);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 449);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(879, 34);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            // 
            // JinEHeJi
            // 
            this.JinEHeJi.AutoSize = true;
            this.JinEHeJi.Location = new System.Drawing.Point(259, 16);
            this.JinEHeJi.Name = "JinEHeJi";
            this.JinEHeJi.Size = new System.Drawing.Size(23, 12);
            this.JinEHeJi.TabIndex = 33;
            this.JinEHeJi.Text = "0元";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(185, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 12);
            this.label15.TabIndex = 32;
            this.label15.Text = "总金额合计：";
            // 
            // TiaoSu
            // 
            this.TiaoSu.AutoSize = true;
            this.TiaoSu.Location = new System.Drawing.Point(46, 16);
            this.TiaoSu.Name = "TiaoSu";
            this.TiaoSu.Size = new System.Drawing.Size(23, 12);
            this.TiaoSu.TabIndex = 31;
            this.TiaoSu.Text = "0笔";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(21, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 30;
            this.label13.Text = "共：";
            // 
            // dataGView1
            // 
            this.dataGView1.AllowUserToAddRows = false;
            this.dataGView1.AllowUserToDeleteRows = false;
            this.dataGView1.AllowUserToResizeRows = false;
            this.dataGView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGView1.ChangeDataColumName = null;
            this.dataGView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGView1.ColumnHeadersHeight = 25;
            this.dataGView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column5,
            this.Column7,
            this.Column3,
            this.Column20,
            this.Column38,
            this.Column9,
            this.Column25,
            this.Column10,
            this.Column11,
            this.Column39,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column17,
            this.Column18,
            this.Column19,
            this.Column23,
            this.Column16});
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
            this.dataGView1.Location = new System.Drawing.Point(0, 124);
            this.dataGView1.Name = "dataGView1";
            this.dataGView1.ReadOnly = true;
            this.dataGView1.RowHeadersWidth = 30;
            this.dataGView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGView1.RowTemplate.Height = 23;
            this.dataGView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGView1.Size = new System.Drawing.Size(879, 325);
            this.dataGView1.TabIndex = 41;
            this.dataGView1.TjFmtStr = null;
            this.dataGView1.TjFormat = null;
            this.dataGView1.Url = null;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "OUTID";
            this.Column1.HeaderText = "出库ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 64;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "IOID";
            this.Column2.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.Column2.HeaderText = "出库方式ID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column2.Width = 88;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "WARECODE";
            this.Column4.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.Column4.HeaderText = "出库库房编码";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "TOTALMONEY";
            this.Column5.HeaderText = "总金额";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 64;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "STATUS";
            this.Column7.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.Column7.HeaderText = "状态";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column7.Width = 52;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "OPFLAG";
            this.Column3.HeaderText = "操作标志";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 76;
            // 
            // Column20
            // 
            this.Column20.DataPropertyName = "RECIPECODE";
            this.Column20.HeaderText = "单据号";
            this.Column20.Name = "Column20";
            this.Column20.ReadOnly = true;
            this.Column20.Width = 64;
            // 
            // Column38
            // 
            this.Column38.DataPropertyName = "OUTDATE";
            this.Column38.HeaderText = "制单日期";
            this.Column38.Name = "Column38";
            this.Column38.ReadOnly = true;
            this.Column38.Width = 76;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "MEMO";
            this.Column9.HeaderText = "备注";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 52;
            // 
            // Column25
            // 
            this.Column25.DataPropertyName = "TARGETWARECODE";
            this.Column25.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.Column25.HeaderText = "出库目的库房编码";
            this.Column25.Name = "Column25";
            this.Column25.ReadOnly = true;
            this.Column25.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column25.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column25.Width = 124;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "USERID";
            this.Column10.HeaderText = "操作员ID";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 76;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "USERNAME";
            this.Column11.HeaderText = "操作员名称";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 88;
            // 
            // Column39
            // 
            this.Column39.DataPropertyName = "INID";
            this.Column39.HeaderText = "对应入库ID";
            this.Column39.Name = "Column39";
            this.Column39.ReadOnly = true;
            this.Column39.Width = 88;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "RECDATE";
            this.Column12.HeaderText = "修改时间";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 76;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "SHDATE";
            this.Column13.HeaderText = "审核日期";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 76;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "SHUSERID";
            this.Column14.HeaderText = "审核操作员ID";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "SHUSERNAME";
            this.Column15.HeaderText = "审核操作员姓名";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Width = 112;
            // 
            // Column17
            // 
            this.Column17.DataPropertyName = "CXDATE";
            this.Column17.HeaderText = "冲销日期";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Width = 76;
            // 
            // Column18
            // 
            this.Column18.DataPropertyName = "CXUSERID";
            this.Column18.HeaderText = "冲销操作员ID";
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            // 
            // Column19
            // 
            this.Column19.DataPropertyName = "CXUSERNAME";
            this.Column19.HeaderText = "冲销操作员姓名";
            this.Column19.Name = "Column19";
            this.Column19.ReadOnly = true;
            this.Column19.Width = 112;
            // 
            // Column23
            // 
            this.Column23.DataPropertyName = "TARGETDEPTID";
            this.Column23.HeaderText = "出库目的科室ID";
            this.Column23.Name = "Column23";
            this.Column23.ReadOnly = true;
            this.Column23.Width = 112;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "CHOSCODE";
            this.Column16.HeaderText = "医疗机构编码";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            // 
            // EQOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 483);
            this.Controls.Add(this.dataGView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "EQOut";
            this.Text = "设备出库";
            this.Load += new System.EventHandler(this.EQIn_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton add_toolStripButton;
        private System.Windows.Forms.ToolStripButton ModifyButton;
        private System.Windows.Forms.ToolStripButton DeleButton;
        private System.Windows.Forms.ToolStripButton check_toolStripButton;
        private System.Windows.Forms.ToolStripButton SubmitCheck_toolStripButton;
        private System.Windows.Forms.ToolStripButton InWare_toolStripButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private YtWinContrl.com.contrl.SelTextInpt InWare_selTextInpt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Search_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private YtWinContrl.com.contrl.DateTimeDuan dateTimeDuan1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private YtWinContrl.com.contrl.YtComboBox InStatus_ytComboBox;
        private System.Windows.Forms.ToolStripButton scan_toolStripButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label JinEHeJi;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label TiaoSu;
        private System.Windows.Forms.Label label13;
        private YtWinContrl.com.datagrid.DataGView dataGView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column2;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column38;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column25;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column39;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column23;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
    }
}
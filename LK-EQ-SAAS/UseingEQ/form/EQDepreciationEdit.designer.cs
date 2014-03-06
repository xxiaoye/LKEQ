namespace UseingEQ.form
{
    partial class EQDepreciationEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EQDepreciationEdit));
            YtWinContrl.com.datagrid.TvList tvList2 = new YtWinContrl.com.datagrid.TvList();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.AddtoolStrip = new System.Windows.Forms.ToolStripButton();
            this.del_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.SleDeptid_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.Save_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.Cancel_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ZJType_ComboBox = new YtWinContrl.com.contrl.YtComboBox();
            this.Memo_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DateId_selTextInp = new YtWinContrl.com.contrl.SelTextInpt();
            this.Deptid_selTextInpt = new YtWinContrl.com.contrl.SelTextInpt();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGView1 = new YtWinContrl.com.datagrid.DataGView();
            this.CardEQColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardId_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NowMonthZJColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NowMonthWorkColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddtoolStrip,
            this.del_toolStrip,
            this.SleDeptid_toolStrip,
            this.Save_toolStrip,
            this.Cancel_toolStrip});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(865, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // AddtoolStrip
            // 
            this.AddtoolStrip.Image = ((System.Drawing.Image)(resources.GetObject("AddtoolStrip.Image")));
            this.AddtoolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddtoolStrip.Name = "AddtoolStrip";
            this.AddtoolStrip.Size = new System.Drawing.Size(97, 22);
            this.AddtoolStrip.Text = "添加折旧信息";
            this.AddtoolStrip.Click += new System.EventHandler(this.AddtoolStrip_Click);
            // 
            // del_toolStrip
            // 
            this.del_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("del_toolStrip.Image")));
            this.del_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.del_toolStrip.Name = "del_toolStrip";
            this.del_toolStrip.Size = new System.Drawing.Size(97, 22);
            this.del_toolStrip.Text = "删除折旧信息";
            this.del_toolStrip.Click += new System.EventHandler(this.del_toolStrip_Click);
            // 
            // SleDeptid_toolStrip
            // 
            this.SleDeptid_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("SleDeptid_toolStrip.Image")));
            this.SleDeptid_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SleDeptid_toolStrip.Name = "SleDeptid_toolStrip";
            this.SleDeptid_toolStrip.Size = new System.Drawing.Size(121, 22);
            this.SleDeptid_toolStrip.Text = "从科室生成折旧单";
            this.SleDeptid_toolStrip.Click += new System.EventHandler(this.SleDeptid_toolStrip_Click);
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
            this.groupBox1.Controls.Add(this.ZJType_ComboBox);
            this.groupBox1.Controls.Add(this.Memo_textBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.DateId_selTextInp);
            this.groupBox1.Controls.Add(this.Deptid_selTextInpt);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(865, 110);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "折旧主表信息";
            // 
            // ZJType_ComboBox
            // 
            this.ZJType_ComboBox.CacheKey = null;
            this.ZJType_ComboBox.DbConn = null;
            this.ZJType_ComboBox.DefText = null;
            this.ZJType_ComboBox.DefValue = null;
            this.ZJType_ComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ZJType_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ZJType_ComboBox.EnableEmpty = true;
            this.ZJType_ComboBox.FirstText = null;
            this.ZJType_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ZJType_ComboBox.Fomart = null;
            this.ZJType_ComboBox.ItemStr = "";
            this.ZJType_ComboBox.Location = new System.Drawing.Point(676, 30);
            this.ZJType_ComboBox.Name = "ytComboBox1";
            this.ZJType_ComboBox.Param = null;
            this.ZJType_ComboBox.Size = new System.Drawing.Size(121, 22);
            this.ZJType_ComboBox.Sql = null;
            this.ZJType_ComboBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ZJType_ComboBox.TabIndex = 2;
            this.ZJType_ComboBox.Tag = tvList2;
            this.ZJType_ComboBox.Value = null;
            // 
            // Memo_textBox
            // 
            this.Memo_textBox.Location = new System.Drawing.Point(124, 73);
            this.Memo_textBox.Multiline = true;
            this.Memo_textBox.Name = "Memo_textBox";
            this.Memo_textBox.Size = new System.Drawing.Size(675, 21);
            this.Memo_textBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "备注";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(59, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "科室";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(601, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "折旧类型";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(336, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "期间划分";
            // 
            // DateId_selTextInp
            // 
            this.DateId_selTextInp.ColDefText = null;
            this.DateId_selTextInp.ColStyle = null;
            this.DateId_selTextInp.DataType = null;
            this.DateId_selTextInp.DbConn = null;
            this.DateId_selTextInp.Location = new System.Drawing.Point(418, 30);
            this.DateId_selTextInp.Name = "DateId_selTextInp";
            this.DateId_selTextInp.NextFocusControl = null;
            this.DateId_selTextInp.ReadOnly = false;
            this.DateId_selTextInp.SelParam = null;
            this.DateId_selTextInp.ShowColNum = 0;
            this.DateId_selTextInp.ShowWidth = 0;
            this.DateId_selTextInp.Size = new System.Drawing.Size(112, 22);
            this.DateId_selTextInp.Sql = null;
            this.DateId_selTextInp.SqlStr = null;
            this.DateId_selTextInp.TabIndex = 1;
            this.DateId_selTextInp.TvColName = null;
            this.DateId_selTextInp.Value = null;
            this.DateId_selTextInp.WatermarkText = "";
            // 
            // Deptid_selTextInpt
            // 
            this.Deptid_selTextInpt.ColDefText = null;
            this.Deptid_selTextInpt.ColStyle = null;
            this.Deptid_selTextInpt.DataType = null;
            this.Deptid_selTextInpt.DbConn = null;
            this.Deptid_selTextInpt.Location = new System.Drawing.Point(124, 28);
            this.Deptid_selTextInpt.Name = "Deptid_selTextInpt";
            this.Deptid_selTextInpt.NextFocusControl = null;
            this.Deptid_selTextInpt.ReadOnly = false;
            this.Deptid_selTextInpt.SelParam = null;
            this.Deptid_selTextInpt.ShowColNum = 0;
            this.Deptid_selTextInpt.ShowWidth = 0;
            this.Deptid_selTextInpt.Size = new System.Drawing.Size(115, 22);
            this.Deptid_selTextInpt.Sql = null;
            this.Deptid_selTextInpt.SqlStr = null;
            this.Deptid_selTextInpt.TabIndex = 0;
            this.Deptid_selTextInpt.TvColName = null;
            this.Deptid_selTextInpt.Value = null;
            this.Deptid_selTextInpt.WatermarkText = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGView1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(865, 315);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "折旧细表信息";
            // 
            // dataGView1
            // 
            this.dataGView1.AllowUserToAddRows = false;
            this.dataGView1.AllowUserToDeleteRows = false;
            this.dataGView1.AllowUserToResizeRows = false;
            this.dataGView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGView1.ChangeDataColumName = null;
            this.dataGView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CardEQColumn,
            this.CardId_Column,
            this.Column1,
            this.Column3,
            this.NowMonthZJColumn,
            this.Column5,
            this.Column6,
            this.NowMonthWorkColumn,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12});
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
            this.dataGView1.RowHeadersWidth = 20;
            this.dataGView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGView1.RowTemplate.Height = 23;
            this.dataGView1.Size = new System.Drawing.Size(859, 295);
            this.dataGView1.TabIndex = 4;
            this.dataGView1.TjFmtStr = null;
            this.dataGView1.TjFormat = null;
            this.dataGView1.Url = null;
            this.dataGView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGView1_CellEnter);
            // 
            // CardEQColumn
            // 
            this.CardEQColumn.DataPropertyName = "EQNAME";
            this.CardEQColumn.HeaderText = "卡片设备";
            this.CardEQColumn.Name = "CardEQColumn";
            this.CardEQColumn.Width = 78;
            // 
            // CardId_Column
            // 
            this.CardId_Column.DataPropertyName = "CARDID";
            this.CardId_Column.HeaderText = "卡片ID";
            this.CardId_Column.Name = "CardId_Column";
            this.CardId_Column.ReadOnly = true;
            this.CardId_Column.Width = 66;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "DEPREID";
            this.Column1.HeaderText = "折旧ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 66;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "TOTALZJ";
            this.Column3.HeaderText = "累计折旧";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 78;
            // 
            // NowMonthZJColumn
            // 
            this.NowMonthZJColumn.DataPropertyName = "MONTHZJ";
            this.NowMonthZJColumn.HeaderText = "本月折旧";
            this.NowMonthZJColumn.Name = "NowMonthZJColumn";
            this.NowMonthZJColumn.Width = 78;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "TOTALWORK";
            this.Column5.HeaderText = "总工作量";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 78;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "TOTALEDWORK";
            this.Column6.HeaderText = "累计工作量";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 90;
            // 
            // NowMonthWorkColumn
            // 
            this.NowMonthWorkColumn.DataPropertyName = "MONTHWORK";
            this.NowMonthWorkColumn.HeaderText = "本月工作量";
            this.NowMonthWorkColumn.Name = "NowMonthWorkColumn";
            this.NowMonthWorkColumn.Width = 90;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "MEMO";
            this.Column8.HeaderText = "备注";
            this.Column8.Name = "Column8";
            this.Column8.Width = 54;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "CHOSCODE";
            this.Column9.HeaderText = "医疗机构编码";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 102;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "USERID";
            this.Column10.HeaderText = "操作员ID";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 78;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "USERNAME";
            this.Column11.HeaderText = "操作员姓名";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 90;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "RECDATE";
            this.Column12.HeaderText = "修改时间";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 78;
            // 
            // EQDepreciationEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "EQDepreciationEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统设备折旧单";
            this.Load += new System.EventHandler(this.EQDepreciationEdit_Load);
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
        private System.Windows.Forms.ToolStripButton del_toolStrip;
        private System.Windows.Forms.ToolStripButton Save_toolStrip;
        private System.Windows.Forms.ToolStripButton Cancel_toolStrip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private YtWinContrl.com.contrl.SelTextInpt DateId_selTextInp;
        private YtWinContrl.com.contrl.SelTextInpt Deptid_selTextInpt;
        private System.Windows.Forms.GroupBox groupBox2;
        private YtWinContrl.com.datagrid.DataGView dataGView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Memo_textBox;
        private System.Windows.Forms.Label label4;
        private YtWinContrl.com.contrl.YtComboBox ZJType_ComboBox;
        private System.Windows.Forms.ToolStripButton AddtoolStrip;
        private System.Windows.Forms.ToolStripButton SleDeptid_toolStrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardEQColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardId_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn NowMonthZJColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn NowMonthWorkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
    }
}
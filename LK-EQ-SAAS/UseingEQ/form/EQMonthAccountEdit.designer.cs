namespace UseingEQ.form
{
    partial class EQMonthAccountEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EQMonthAccountEdit));
            YtWinContrl.com.datagrid.TvList tvList1 = new YtWinContrl.com.datagrid.TvList();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.QueRen_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.HuiFu_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.Close_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ZJType_ComboBox = new YtWinContrl.com.contrl.YtComboBox();
            this.Memo_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DateId_selTextInp = new YtWinContrl.com.contrl.SelTextInpt();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGView1 = new YtWinContrl.com.datagrid.DataGView();
            this.CardEQColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QiYongDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BFdateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YJCZColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YJSYNXColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NZJLColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YZColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZJTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardId_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NowMonthZJColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZGZLColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LJGZLColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.QueRen_toolStrip,
            this.HuiFu_toolStrip,
            this.Close_toolStrip});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(895, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // QueRen_toolStrip
            // 
            this.QueRen_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("QueRen_toolStrip.Image")));
            this.QueRen_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.QueRen_toolStrip.Name = "QueRen_toolStrip";
            this.QueRen_toolStrip.Size = new System.Drawing.Size(73, 22);
            this.QueRen_toolStrip.Text = "确认月结";
            this.QueRen_toolStrip.Click += new System.EventHandler(this.QueRen_toolStrip_Click);
            // 
            // HuiFu_toolStrip
            // 
            this.HuiFu_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("HuiFu_toolStrip.Image")));
            this.HuiFu_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.HuiFu_toolStrip.Name = "HuiFu_toolStrip";
            this.HuiFu_toolStrip.Size = new System.Drawing.Size(73, 22);
            this.HuiFu_toolStrip.Text = "确认恢复";
            this.HuiFu_toolStrip.Click += new System.EventHandler(this.HuiFu_toolStrip_Click);
            // 
            // Close_toolStrip
            // 
            this.Close_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("Close_toolStrip.Image")));
            this.Close_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Close_toolStrip.Name = "Close_toolStrip";
            this.Close_toolStrip.Size = new System.Drawing.Size(49, 22);
            this.Close_toolStrip.Text = "关闭";
            this.Close_toolStrip.Click += new System.EventHandler(this.Close_toolStrip_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ZJType_ComboBox);
            this.groupBox1.Controls.Add(this.Memo_textBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.DateId_selTextInp);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(895, 110);
            this.groupBox1.TabIndex = 5;
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
            this.ZJType_ComboBox.Enabled = false;
            this.ZJType_ComboBox.EnableEmpty = true;
            this.ZJType_ComboBox.FirstText = null;
            this.ZJType_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ZJType_ComboBox.Fomart = null;
            this.ZJType_ComboBox.ItemStr = "";
            this.ZJType_ComboBox.Location = new System.Drawing.Point(602, 30);
            this.ZJType_ComboBox.Name = "ytComboBox1";
            this.ZJType_ComboBox.Param = null;
            this.ZJType_ComboBox.Size = new System.Drawing.Size(160, 22);
            this.ZJType_ComboBox.Sql = null;
            this.ZJType_ComboBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ZJType_ComboBox.TabIndex = 2;
            this.ZJType_ComboBox.Tag = tvList1;
            this.ZJType_ComboBox.Value = null;
            // 
            // Memo_textBox
            // 
            this.Memo_textBox.Location = new System.Drawing.Point(124, 73);
            this.Memo_textBox.Multiline = true;
            this.Memo_textBox.Name = "Memo_textBox";
            this.Memo_textBox.Size = new System.Drawing.Size(638, 21);
            this.Memo_textBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "备注";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(509, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "折旧类型";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(42, 30);
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
            this.DateId_selTextInp.Enabled = false;
            this.DateId_selTextInp.Location = new System.Drawing.Point(124, 26);
            this.DateId_selTextInp.Name = "DateId_selTextInp";
            this.DateId_selTextInp.NextFocusControl = null;
            this.DateId_selTextInp.ReadOnly = false;
            this.DateId_selTextInp.SelParam = null;
            this.DateId_selTextInp.ShowColNum = 0;
            this.DateId_selTextInp.ShowWidth = 0;
            this.DateId_selTextInp.Size = new System.Drawing.Size(165, 22);
            this.DateId_selTextInp.Sql = null;
            this.DateId_selTextInp.SqlStr = null;
            this.DateId_selTextInp.TabIndex = 1;
            this.DateId_selTextInp.TvColName = null;
            this.DateId_selTextInp.Value = null;
            this.DateId_selTextInp.WatermarkText = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGView1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(895, 302);
            this.groupBox2.TabIndex = 6;
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
            this.QiYongDateColumn,
            this.BFdateColumn,
            this.YJCZColumn,
            this.YJSYNXColumn,
            this.NZJLColumn,
            this.YZColumn,
            this.ZJTypeColumn,
            this.CardId_Column,
            this.Column1,
            this.Column3,
            this.NowMonthZJColumn,
            this.ZGZLColumn,
            this.LJGZLColumn,
            this.NowMonthWorkColumn,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12});
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
            this.dataGView1.Location = new System.Drawing.Point(3, 17);
            this.dataGView1.MultiSelect = false;
            this.dataGView1.Name = "dataGView1";
            this.dataGView1.ReadOnly = true;
            this.dataGView1.RowHeadersWidth = 20;
            this.dataGView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGView1.RowTemplate.Height = 23;
            this.dataGView1.Size = new System.Drawing.Size(889, 282);
            this.dataGView1.TabIndex = 4;
            this.dataGView1.TjFmtStr = null;
            this.dataGView1.TjFormat = null;
            this.dataGView1.Url = null;
            // 
            // CardEQColumn
            // 
            this.CardEQColumn.DataPropertyName = "EQNAME";
            this.CardEQColumn.HeaderText = "卡片设备";
            this.CardEQColumn.Name = "CardEQColumn";
            this.CardEQColumn.ReadOnly = true;
            this.CardEQColumn.Width = 78;
            // 
            // QiYongDateColumn
            // 
            this.QiYongDateColumn.DataPropertyName = "STARTDATE";
            this.QiYongDateColumn.HeaderText = "启用日期";
            this.QiYongDateColumn.Name = "QiYongDateColumn";
            this.QiYongDateColumn.ReadOnly = true;
            this.QiYongDateColumn.Width = 78;
            // 
            // BFdateColumn
            // 
            this.BFdateColumn.DataPropertyName = "BFDATE";
            this.BFdateColumn.HeaderText = "报废日期";
            this.BFdateColumn.Name = "BFdateColumn";
            this.BFdateColumn.ReadOnly = true;
            this.BFdateColumn.Width = 78;
            // 
            // YJCZColumn
            // 
            this.YJCZColumn.DataPropertyName = "CZRATE";
            this.YJCZColumn.HeaderText = "预计残值率";
            this.YJCZColumn.Name = "YJCZColumn";
            this.YJCZColumn.ReadOnly = true;
            this.YJCZColumn.Visible = false;
            this.YJCZColumn.Width = 90;
            // 
            // YJSYNXColumn
            // 
            this.YJSYNXColumn.DataPropertyName = "USEYEAR";
            this.YJSYNXColumn.HeaderText = "预计使用年限";
            this.YJSYNXColumn.Name = "YJSYNXColumn";
            this.YJSYNXColumn.ReadOnly = true;
            this.YJSYNXColumn.Visible = false;
            this.YJSYNXColumn.Width = 102;
            // 
            // NZJLColumn
            // 
            this.NZJLColumn.DataPropertyName = "ZJRATE";
            this.NZJLColumn.HeaderText = "年折旧率";
            this.NZJLColumn.Name = "NZJLColumn";
            this.NZJLColumn.ReadOnly = true;
            this.NZJLColumn.Visible = false;
            this.NZJLColumn.Width = 78;
            // 
            // YZColumn
            // 
            this.YZColumn.DataPropertyName = "YPRICE";
            this.YZColumn.HeaderText = "原值";
            this.YZColumn.Name = "YZColumn";
            this.YZColumn.ReadOnly = true;
            this.YZColumn.Visible = false;
            this.YZColumn.Width = 54;
            // 
            // ZJTypeColumn
            // 
            this.ZJTypeColumn.DataPropertyName = "ZJTYPE";
            this.ZJTypeColumn.HeaderText = "折旧类型";
            this.ZJTypeColumn.Name = "ZJTypeColumn";
            this.ZJTypeColumn.ReadOnly = true;
            this.ZJTypeColumn.Visible = false;
            this.ZJTypeColumn.Width = 78;
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
            this.NowMonthZJColumn.ReadOnly = true;
            this.NowMonthZJColumn.Width = 78;
            // 
            // ZGZLColumn
            // 
            this.ZGZLColumn.DataPropertyName = "TOTALWORK";
            this.ZGZLColumn.HeaderText = "总工作量";
            this.ZGZLColumn.Name = "ZGZLColumn";
            this.ZGZLColumn.ReadOnly = true;
            this.ZGZLColumn.Width = 78;
            // 
            // LJGZLColumn
            // 
            this.LJGZLColumn.DataPropertyName = "TOTALEDWORK";
            this.LJGZLColumn.HeaderText = "累计工作量";
            this.LJGZLColumn.Name = "LJGZLColumn";
            this.LJGZLColumn.ReadOnly = true;
            this.LJGZLColumn.Width = 90;
            // 
            // NowMonthWorkColumn
            // 
            this.NowMonthWorkColumn.DataPropertyName = "MONTHWORK";
            this.NowMonthWorkColumn.HeaderText = "本月工作量";
            this.NowMonthWorkColumn.Name = "NowMonthWorkColumn";
            this.NowMonthWorkColumn.ReadOnly = true;
            this.NowMonthWorkColumn.Width = 90;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "MEMO";
            this.Column8.HeaderText = "备注";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
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
            // EQMonthAccountEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 437);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "EQMonthAccountEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备月结单";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.EQMonthAccountEdit_Load);
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
        private System.Windows.Forms.ToolStripButton QueRen_toolStrip;
        private System.Windows.Forms.ToolStripButton Close_toolStrip;
        private System.Windows.Forms.GroupBox groupBox1;
        private YtWinContrl.com.contrl.YtComboBox ZJType_ComboBox;
        private System.Windows.Forms.TextBox Memo_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private YtWinContrl.com.contrl.SelTextInpt DateId_selTextInp;
        private System.Windows.Forms.GroupBox groupBox2;
        private YtWinContrl.com.datagrid.DataGView dataGView1;
        private System.Windows.Forms.ToolStripButton HuiFu_toolStrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardEQColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn QiYongDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BFdateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn YJCZColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn YJSYNXColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NZJLColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn YZColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZJTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardId_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn NowMonthZJColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZGZLColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LJGZLColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NowMonthWorkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;

    }
}
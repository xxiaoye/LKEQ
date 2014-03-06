namespace EQWareManag.form
{
    partial class EQPanDian_DetailScan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EQPanDian_DetailScan));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Del = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Cancel = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.selTextInpt_Ware = new YtWinContrl.com.contrl.SelTextInpt();
            this.label1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.yTextBox_Rec = new YtWinContrl.com.contrl.YTextBox();
            this.yTextBox_Name = new YtWinContrl.com.contrl.YTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.TiaoSu = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGView1 = new YtWinContrl.com.datagrid.DataGView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column24 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Save,
            this.toolStripButton_Del,
            this.toolStripButton_Cancel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(812, 25);
            this.toolStrip1.TabIndex = 26;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_Save
            // 
            this.toolStripButton_Save.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Save.Image")));
            this.toolStripButton_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Save.Name = "toolStripButton_Save";
            this.toolStripButton_Save.Size = new System.Drawing.Size(97, 22);
            this.toolStripButton_Save.Text = "保存盘点数据";
            this.toolStripButton_Save.Click += new System.EventHandler(this.toolStripButton_Save_Click);
            // 
            // toolStripButton_Del
            // 
            this.toolStripButton_Del.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Del.Image")));
            this.toolStripButton_Del.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Del.Name = "toolStripButton_Del";
            this.toolStripButton_Del.Size = new System.Drawing.Size(121, 22);
            this.toolStripButton_Del.Text = "删除盘点细表数据";
            this.toolStripButton_Del.Click += new System.EventHandler(this.toolStripButton_Del_Click);
            // 
            // toolStripButton_Cancel
            // 
            this.toolStripButton_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Cancel.Image")));
            this.toolStripButton_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Cancel.Name = "toolStripButton_Cancel";
            this.toolStripButton_Cancel.Size = new System.Drawing.Size(49, 22);
            this.toolStripButton_Cancel.Text = "取消";
            this.toolStripButton_Cancel.Click += new System.EventHandler(this.toolStripButton_Cancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.selTextInpt_Ware);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.yTextBox_Rec);
            this.groupBox2.Controls.Add(this.yTextBox_Name);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(812, 104);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "盘点主表信息";
            // 
            // selTextInpt_Ware
            // 
            this.selTextInpt_Ware.ColDefText = null;
            this.selTextInpt_Ware.ColStyle = null;
            this.selTextInpt_Ware.DataType = null;
            this.selTextInpt_Ware.DbConn = null;
            this.selTextInpt_Ware.Location = new System.Drawing.Point(137, 22);
            this.selTextInpt_Ware.Name = "selTextInpt_Ware";
            this.selTextInpt_Ware.NextFocusControl = null;
            this.selTextInpt_Ware.ReadOnly = false;
            this.selTextInpt_Ware.SelParam = null;
            this.selTextInpt_Ware.ShowColNum = 0;
            this.selTextInpt_Ware.ShowWidth = 0;
            this.selTextInpt_Ware.Size = new System.Drawing.Size(179, 22);
            this.selTextInpt_Ware.Sql = null;
            this.selTextInpt_Ware.SqlStr = null;
            this.selTextInpt_Ware.TabIndex = 0;
            this.selTextInpt_Ware.TvColName = null;
            this.selTextInpt_Ware.Value = null;
            this.selTextInpt_Ware.WatermarkText = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(76, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "备注";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Blue;
            this.label13.Location = new System.Drawing.Point(63, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 24;
            this.label13.Text = "盘点库房";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(352, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "盘点名称";
            // 
            // yTextBox_Rec
            // 
            // 
            // 
            // 
            this.yTextBox_Rec.Border.Class = "TextBoxBorder";
            this.yTextBox_Rec.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.yTextBox_Rec.Location = new System.Drawing.Point(137, 54);
            this.yTextBox_Rec.Name = "yTextBox_User";
            this.yTextBox_Rec.Size = new System.Drawing.Size(500, 21);
            this.yTextBox_Rec.TabIndex = 2;
            // 
            // yTextBox_Name
            // 
            // 
            // 
            // 
            this.yTextBox_Name.Border.Class = "TextBoxBorder";
            this.yTextBox_Name.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.yTextBox_Name.Location = new System.Drawing.Point(437, 23);
            this.yTextBox_Name.Name = "yTextBox_User";
            this.yTextBox_Name.Size = new System.Drawing.Size(200, 21);
            this.yTextBox_Name.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.TiaoSu);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Location = new System.Drawing.Point(0, 744);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(812, 34);
            this.groupBox4.TabIndex = 31;
            this.groupBox4.TabStop = false;
            // 
            // TiaoSu
            // 
            this.TiaoSu.AutoSize = true;
            this.TiaoSu.Location = new System.Drawing.Point(46, 16);
            this.TiaoSu.Name = "TiaoSu";
            this.TiaoSu.Size = new System.Drawing.Size(23, 12);
            this.TiaoSu.TabIndex = 31;
            this.TiaoSu.Text = "0条";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "共：";
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
            this.dataGView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column16,
            this.Column31,
            this.Column2,
            this.Column12,
            this.Column9,
            this.Column24,
            this.Column27,
            this.Column28,
            this.Column4,
            this.Column29,
            this.Column32,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column5,
            this.Column3,
            this.Column18,
            this.Column26,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column10,
            this.Column11,
            this.Column17});
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
            this.dataGView1.Location = new System.Drawing.Point(0, 129);
            this.dataGView1.Name = "dataGView1";
            this.dataGView1.RowHeadersWidth = 30;
            this.dataGView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGView1.RowTemplate.Height = 23;
            this.dataGView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGView1.Size = new System.Drawing.Size(812, 615);
            this.dataGView1.TabIndex = 32;
            this.dataGView1.TjFmtStr = null;
            this.dataGView1.TjFormat = null;
            this.dataGView1.Url = null;
            this.dataGView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGView1_RowPostPaint);
            this.dataGView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGView1_CellEndEdit);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "EQNAME";
            this.Column1.HeaderText = "设备名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 78;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "EQID";
            this.Column16.HeaderText = "设备ID";
            this.Column16.Name = "Column16";
            this.Column16.Visible = false;
            this.Column16.Width = 66;
            // 
            // Column31
            // 
            this.Column31.DataPropertyName = "SUPPLYNAME";
            this.Column31.HeaderText = "生产厂家名称";
            this.Column31.Name = "Column31";
            this.Column31.ReadOnly = true;
            this.Column31.Width = 102;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "FACTNUM";
            this.Column2.HeaderText = "实存数量";
            this.Column2.Name = "Column2";
            this.Column2.Width = 78;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "STOCKNUM";
            this.Column12.HeaderText = "库存数量";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 78;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "YKNUM";
            this.Column9.HeaderText = "盈亏数量";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 78;
            // 
            // Column24
            // 
            this.Column24.DataPropertyName = "UNITCODE";
            this.Column24.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.Column24.HeaderText = "单位编码";
            this.Column24.Name = "Column24";
            this.Column24.ReadOnly = true;
            this.Column24.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column24.Width = 78;
            // 
            // Column27
            // 
            this.Column27.DataPropertyName = "PRICE";
            this.Column27.HeaderText = "单价";
            this.Column27.Name = "Column27";
            this.Column27.ReadOnly = true;
            this.Column27.Width = 54;
            // 
            // Column28
            // 
            this.Column28.DataPropertyName = "GG";
            this.Column28.HeaderText = "规格";
            this.Column28.Name = "Column28";
            this.Column28.ReadOnly = true;
            this.Column28.Width = 54;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "XH";
            this.Column4.HeaderText = "型号";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 54;
            // 
            // Column29
            // 
            this.Column29.DataPropertyName = "CD";
            this.Column29.HeaderText = "产地";
            this.Column29.Name = "Column29";
            this.Column29.ReadOnly = true;
            this.Column29.Width = 54;
            // 
            // Column32
            // 
            this.Column32.DataPropertyName = "PRODUCTDATE";
            this.Column32.HeaderText = "生产日期";
            this.Column32.Name = "Column32";
            this.Column32.ReadOnly = true;
            this.Column32.Width = 78;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "VALIDDATE";
            this.Column13.HeaderText = "有效期";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 66;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "MEMO";
            this.Column14.HeaderText = "备注";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Width = 54;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "FLOWNO";
            this.Column15.HeaderText = "流水号";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Visible = false;
            this.Column15.Width = 66;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "ROWNO";
            this.Column5.HeaderText = "行号";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 54;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "BEFORENUM";
            this.Column3.HeaderText = "入库前数量";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 90;
            // 
            // Column18
            // 
            this.Column18.DataPropertyName = "NUM";
            this.Column18.HeaderText = "入库数";
            this.Column18.Name = "Column18";
            this.Column18.Width = 66;
            // 
            // Column26
            // 
            this.Column26.DataPropertyName = "OUTNUM";
            this.Column26.HeaderText = "出库数";
            this.Column26.Name = "Column26";
            this.Column26.ReadOnly = true;
            this.Column26.Width = 66;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "TXM";
            this.Column6.HeaderText = "条形码";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 66;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "RECIPECODE";
            this.Column7.HeaderText = "入库单据号";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 90;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "SHDH";
            this.Column8.HeaderText = "随货单号";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 78;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "INDATE";
            this.Column10.HeaderText = "入库时间";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 78;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "CHOSCODE";
            this.Column11.HeaderText = "医疗机构编码";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 102;
            // 
            // Column17
            // 
            this.Column17.DataPropertyName = "STOCKID";
            this.Column17.HeaderText = "库存ID";
            this.Column17.Name = "Column17";
            this.Column17.Width = 66;
            // 
            // EQPanDian_DetailScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 778);
            this.Controls.Add(this.dataGView1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "EQPanDian_DetailScan";
            this.Text = "盘点流水查看";
            this.Load += new System.EventHandler(this.EQPanDian_DetailScan_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_Save;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label4;
        private YtWinContrl.com.contrl.YTextBox yTextBox_Rec;
        private System.Windows.Forms.ToolStripButton toolStripButton_Del;
        private System.Windows.Forms.ToolStripButton toolStripButton_Cancel;
        private System.Windows.Forms.Label label1;
        private YtWinContrl.com.contrl.YTextBox yTextBox_Name;
        private YtWinContrl.com.contrl.SelTextInpt selTextInpt_Ware;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label TiaoSu;
        private System.Windows.Forms.Label label2;
        private YtWinContrl.com.datagrid.DataGView dataGView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column31;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column24;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column27;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column28;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column29;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column32;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column26;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
    }
}
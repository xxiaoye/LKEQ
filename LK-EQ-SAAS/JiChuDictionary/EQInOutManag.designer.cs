namespace JiChuDictionary
{
    partial class EQInOutManag
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EQInOutManag));
            YtWinContrl.com.datagrid.TvList tvList3 = new YtWinContrl.com.datagrid.TvList();
            YtWinContrl.com.datagrid.TvList tvList4 = new YtWinContrl.com.datagrid.TvList();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.add_toolstrip = new System.Windows.Forms.ToolStripButton();
            this.modify_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.copy_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.enable_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.disable_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.del_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.refresh_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.serachKind_ytComboBox = new YtWinContrl.com.contrl.YtComboBox();
            this.Filter_ytComboBox = new YtWinContrl.com.contrl.YtComboBox();
            this.search_yTextBox = new YtWinContrl.com.contrl.YTextBox();
            this.dataGView1 = new YtWinContrl.com.datagrid.DataGView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IOFlagColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.OPFlagColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.add_toolstrip,
            this.modify_toolStrip,
            this.copy_toolStrip,
            this.enable_toolStrip,
            this.disable_toolStrip,
            this.del_toolStrip,
            this.refresh_toolStrip});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(772, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // add_toolstrip
            // 
            this.add_toolstrip.Image = ((System.Drawing.Image)(resources.GetObject("add_toolstrip.Image")));
            this.add_toolstrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.add_toolstrip.Name = "add_toolstrip";
            this.add_toolstrip.Size = new System.Drawing.Size(49, 22);
            this.add_toolstrip.Text = "新增";
            this.add_toolstrip.Click += new System.EventHandler(this.add_toolstrip_Click);
            // 
            // modify_toolStrip
            // 
            this.modify_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("modify_toolStrip.Image")));
            this.modify_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.modify_toolStrip.Name = "modify_toolStrip";
            this.modify_toolStrip.Size = new System.Drawing.Size(49, 22);
            this.modify_toolStrip.Text = "修改";
            this.modify_toolStrip.Click += new System.EventHandler(this.modify_toolStrip_Click);
            // 
            // copy_toolStrip
            // 
            this.copy_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("copy_toolStrip.Image")));
            this.copy_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copy_toolStrip.Name = "copy_toolStrip";
            this.copy_toolStrip.Size = new System.Drawing.Size(49, 22);
            this.copy_toolStrip.Text = "复制";
            this.copy_toolStrip.Click += new System.EventHandler(this.copy_toolStrip_Click);
            // 
            // enable_toolStrip
            // 
            this.enable_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("enable_toolStrip.Image")));
            this.enable_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.enable_toolStrip.Name = "enable_toolStrip";
            this.enable_toolStrip.Size = new System.Drawing.Size(49, 22);
            this.enable_toolStrip.Text = "启用";
            this.enable_toolStrip.Click += new System.EventHandler(this.enable_toolStrip_Click);
            // 
            // disable_toolStrip
            // 
            this.disable_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("disable_toolStrip.Image")));
            this.disable_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.disable_toolStrip.Name = "disable_toolStrip";
            this.disable_toolStrip.Size = new System.Drawing.Size(49, 22);
            this.disable_toolStrip.Text = "停用";
            this.disable_toolStrip.Click += new System.EventHandler(this.disable_toolStrip_Click);
            // 
            // del_toolStrip
            // 
            this.del_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("del_toolStrip.Image")));
            this.del_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.del_toolStrip.Name = "del_toolStrip";
            this.del_toolStrip.Size = new System.Drawing.Size(49, 22);
            this.del_toolStrip.Text = "删除";
            this.del_toolStrip.Click += new System.EventHandler(this.del_toolStrip_Click);
            // 
            // refresh_toolStrip
            // 
            this.refresh_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("refresh_toolStrip.Image")));
            this.refresh_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refresh_toolStrip.Name = "refresh_toolStrip";
            this.refresh_toolStrip.Size = new System.Drawing.Size(49, 22);
            this.refresh_toolStrip.Text = "刷新";
            this.refresh_toolStrip.Click += new System.EventHandler(this.refresh_toolStrip_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.serachKind_ytComboBox);
            this.groupBox1.Controls.Add(this.Filter_ytComboBox);
            this.groupBox1.Controls.Add(this.search_yTextBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(772, 66);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(640, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(407, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "过滤条件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "关键字";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "查询类型";
            // 
            // serachKind_ytComboBox
            // 
            this.serachKind_ytComboBox.CacheKey = null;
            this.serachKind_ytComboBox.DbConn = null;
            this.serachKind_ytComboBox.DefText = null;
            this.serachKind_ytComboBox.DefValue = null;
            this.serachKind_ytComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.serachKind_ytComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serachKind_ytComboBox.EnableEmpty = true;
            this.serachKind_ytComboBox.FirstText = null;
            this.serachKind_ytComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.serachKind_ytComboBox.Fomart = null;
            this.serachKind_ytComboBox.ItemStr = "";
            this.serachKind_ytComboBox.Location = new System.Drawing.Point(77, 28);
            this.serachKind_ytComboBox.Name = "serachKind_ytComboBox";
            this.serachKind_ytComboBox.Param = null;
            this.serachKind_ytComboBox.Size = new System.Drawing.Size(100, 22);
            this.serachKind_ytComboBox.Sql = null;
            this.serachKind_ytComboBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.serachKind_ytComboBox.TabIndex = 0;
            this.serachKind_ytComboBox.Tag = tvList3;
            this.serachKind_ytComboBox.Value = null;
            // 
            // Filter_ytComboBox
            // 
            this.Filter_ytComboBox.CacheKey = null;
            this.Filter_ytComboBox.DbConn = null;
            this.Filter_ytComboBox.DefText = null;
            this.Filter_ytComboBox.DefValue = null;
            this.Filter_ytComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Filter_ytComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Filter_ytComboBox.EnableEmpty = true;
            this.Filter_ytComboBox.FirstText = null;
            this.Filter_ytComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Filter_ytComboBox.Fomart = null;
            this.Filter_ytComboBox.ItemStr = "";
            this.Filter_ytComboBox.Location = new System.Drawing.Point(466, 28);
            this.Filter_ytComboBox.Name = "Filter_ytComboBox";
            this.Filter_ytComboBox.Param = null;
            this.Filter_ytComboBox.Size = new System.Drawing.Size(100, 22);
            this.Filter_ytComboBox.Sql = null;
            this.Filter_ytComboBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Filter_ytComboBox.TabIndex = 2;
            this.Filter_ytComboBox.Tag = tvList4;
            this.Filter_ytComboBox.Value = null;
            // 
            // search_yTextBox
            // 
            // 
            // 
            // 
            this.search_yTextBox.Border.Class = "TextBoxBorder";
            this.search_yTextBox.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.search_yTextBox.Location = new System.Drawing.Point(273, 29);
            this.search_yTextBox.Name = "search_yTextBox";
            this.search_yTextBox.Size = new System.Drawing.Size(100, 21);
            this.search_yTextBox.TabIndex = 1;
            // 
            // dataGView1
            // 
            this.dataGView1.AllowUserToAddRows = false;
            this.dataGView1.AllowUserToDeleteRows = false;
            this.dataGView1.AllowUserToResizeRows = false;
            this.dataGView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGView1.ChangeDataColumName = null;
            this.dataGView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column6,
            this.IOFlagColumn,
            this.OPFlagColumn,
            this.Column5,
            this.Column12,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column3,
            this.Column4,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16,
            this.Column17});
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
            this.dataGView1.Location = new System.Drawing.Point(0, 91);
            this.dataGView1.Name = "dataGView1";
            this.dataGView1.ReadOnly = true;
            this.dataGView1.RowHeadersWidth = 20;
            this.dataGView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGView1.RowTemplate.Height = 23;
            this.dataGView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGView1.Size = new System.Drawing.Size(772, 335);
            this.dataGView1.TabIndex = 2;
            this.dataGView1.TjFmtStr = null;
            this.dataGView1.TjFormat = null;
            this.dataGView1.Url = null;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "IOID";
            this.Column1.HeaderText = "入出ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "IONAME";
            this.Column2.HeaderText = "入出名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "RECIPECODE";
            this.Column6.HeaderText = "单据前缀";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // IOFlagColumn
            // 
            this.IOFlagColumn.DataPropertyName = "IOFLAG";
            this.IOFlagColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.IOFlagColumn.HeaderText = "入出标志";
            this.IOFlagColumn.Name = "IOFlagColumn";
            this.IOFlagColumn.ReadOnly = true;
            this.IOFlagColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IOFlagColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // OPFlagColumn
            // 
            this.OPFlagColumn.DataPropertyName = "OPFLAG";
            this.OPFlagColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.OPFlagColumn.HeaderText = "操作标志";
            this.OPFlagColumn.Items.AddRange(new object[] {
            "普通",
            "调拨",
            "申领",
            "盘点"});
            this.OPFlagColumn.Name = "OPFlagColumn";
            this.OPFlagColumn.ReadOnly = true;
            this.OPFlagColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OPFlagColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "IFUSE";
            this.Column5.FalseValue = "0";
            this.Column5.HeaderText = "是否使用";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column5.TrueValue = "1";
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "IFDEFAULT";
            this.Column12.FalseValue = "0";
            this.Column12.HeaderText = "是否默认值";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column12.TrueValue = "1";
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "RECIPELENGTH";
            this.Column7.HeaderText = "单据编码长度";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "RECIPEYEAR";
            this.Column8.HeaderText = "单据年份";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "RECIPEMONTH";
            this.Column9.HeaderText = "单据月份";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "PYCODE";
            this.Column3.HeaderText = "拼音码";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "WBCODE";
            this.Column4.HeaderText = "五笔码";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "MEMO";
            this.Column13.HeaderText = "备注";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "USERID";
            this.Column14.HeaderText = "操作员ID";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "USERNAME";
            this.Column15.HeaderText = "操作员姓名";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "RECDATE";
            this.Column16.HeaderText = "修改时间";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            // 
            // Column17
            // 
            this.Column17.DataPropertyName = "CHOSCODE";
            this.Column17.HeaderText = "医疗机构编码";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            // 
            // EQInOutManag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 426);
            this.Controls.Add(this.dataGView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "EQInOutManag";
            this.Text = "设备出入库类型管理";
            this.Load += new System.EventHandler(this.EQInOutManag_Load);
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
        private System.Windows.Forms.ToolStripButton add_toolstrip;
        private System.Windows.Forms.ToolStripButton modify_toolStrip;
        private System.Windows.Forms.ToolStripButton copy_toolStrip;
        private System.Windows.Forms.ToolStripButton enable_toolStrip;
        private System.Windows.Forms.ToolStripButton disable_toolStrip;
        private System.Windows.Forms.ToolStripButton refresh_toolStrip;
        private System.Windows.Forms.ToolStripButton del_toolStrip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private YtWinContrl.com.contrl.YtComboBox Filter_ytComboBox;
        private YtWinContrl.com.contrl.YtComboBox serachKind_ytComboBox;
        private YtWinContrl.com.contrl.YTextBox search_yTextBox;
        private YtWinContrl.com.datagrid.DataGView dataGView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewComboBoxColumn IOFlagColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn OPFlagColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
    }
}
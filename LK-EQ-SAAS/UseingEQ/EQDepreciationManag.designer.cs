namespace UseingEQ
{
    partial class EQDepreciationManag
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EQDepreciationManag));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Add_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.Edit_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.Del_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.View_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.refresh_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimeDuan1 = new YtWinContrl.com.contrl.DateTimeDuan();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.selTextInpt1 = new YtWinContrl.com.contrl.SelTextInpt();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGView1 = new YtWinContrl.com.datagrid.DataGView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepreTypeColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add_toolStrip,
            this.Edit_toolStrip,
            this.Del_toolStrip,
            this.View_toolStrip,
            this.refresh_toolStrip});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(915, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Add_toolStrip
            // 
            this.Add_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("Add_toolStrip.Image")));
            this.Add_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Add_toolStrip.Name = "Add_toolStrip";
            this.Add_toolStrip.Size = new System.Drawing.Size(49, 22);
            this.Add_toolStrip.Text = "新增";
            this.Add_toolStrip.Click += new System.EventHandler(this.Add_toolStrip_Click);
            // 
            // Edit_toolStrip
            // 
            this.Edit_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("Edit_toolStrip.Image")));
            this.Edit_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Edit_toolStrip.Name = "Edit_toolStrip";
            this.Edit_toolStrip.Size = new System.Drawing.Size(49, 22);
            this.Edit_toolStrip.Text = "编辑";
            this.Edit_toolStrip.Click += new System.EventHandler(this.Edit_toolStrip_Click);
            // 
            // Del_toolStrip
            // 
            this.Del_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("Del_toolStrip.Image")));
            this.Del_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Del_toolStrip.Name = "Del_toolStrip";
            this.Del_toolStrip.Size = new System.Drawing.Size(49, 22);
            this.Del_toolStrip.Text = "删除";
            this.Del_toolStrip.Click += new System.EventHandler(this.Del_toolStrip_Click);
            // 
            // View_toolStrip
            // 
            this.View_toolStrip.Image = ((System.Drawing.Image)(resources.GetObject("View_toolStrip.Image")));
            this.View_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.View_toolStrip.Name = "View_toolStrip";
            this.View_toolStrip.Size = new System.Drawing.Size(49, 22);
            this.View_toolStrip.Text = "浏览";
            this.View_toolStrip.Click += new System.EventHandler(this.View_toolStrip_Click);
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
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.dateTimeDuan1);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.selTextInpt1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(915, 59);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(363, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 36;
            this.label2.Text = "到";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 35;
            this.label4.Text = "从";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Checked = false;
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(394, 22);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(127, 21);
            this.dateTimePicker2.TabIndex = 2;
            // 
            // dateTimeDuan1
            // 
            this.dateTimeDuan1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dateTimeDuan1.End = this.dateTimePicker2;
            this.dateTimeDuan1.FormattingEnabled = true;
            this.dateTimeDuan1.Location = new System.Drawing.Point(75, 22);
            this.dateTimeDuan1.Name = "dateTimeDuan1";
            this.dateTimeDuan1.Size = new System.Drawing.Size(108, 20);
            this.dateTimeDuan1.Start = this.dateTimePicker1;
            this.dateTimeDuan1.TabIndex = 0;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Checked = false;
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(224, 22);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(127, 21);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(547, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "科室";
            // 
            // selTextInpt1
            // 
            this.selTextInpt1.ColDefText = null;
            this.selTextInpt1.ColStyle = null;
            this.selTextInpt1.DataType = null;
            this.selTextInpt1.DbConn = null;
            this.selTextInpt1.Location = new System.Drawing.Point(613, 21);
            this.selTextInpt1.Name = "selTextInpt1";
            this.selTextInpt1.NextFocusControl = null;
            this.selTextInpt1.ReadOnly = false;
            this.selTextInpt1.SelParam = null;
            this.selTextInpt1.ShowColNum = 0;
            this.selTextInpt1.ShowWidth = 0;
            this.selTextInpt1.Size = new System.Drawing.Size(137, 22);
            this.selTextInpt1.Sql = null;
            this.selTextInpt1.SqlStr = null;
            this.selTextInpt1.TabIndex = 3;
            this.selTextInpt1.TvColName = null;
            this.selTextInpt1.Value = null;
            this.selTextInpt1.WatermarkText = "";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(804, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "时间段";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGView1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 84);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(915, 350);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "设备使用信息";
            // 
            // dataGView1
            // 
            this.dataGView1.AllowUserToAddRows = false;
            this.dataGView1.AllowUserToDeleteRows = false;
            this.dataGView1.AllowUserToResizeRows = false;
            this.dataGView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGView1.ChangeDataColumName = null;
            this.dataGView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column10,
            this.DepreTypeColumn,
            this.Column4,
            this.Column3,
            this.StatusColumn,
            this.Column6,
            this.Column9,
            this.Column5,
            this.Column7,
            this.Column8});
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
            this.dataGView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGView1.Size = new System.Drawing.Size(909, 330);
            this.dataGView1.TabIndex = 5;
            this.dataGView1.TjFmtStr = null;
            this.dataGView1.TjFormat = null;
            this.dataGView1.Url = null;
            this.dataGView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGView1_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "DEPREID";
            this.Column1.HeaderText = "折旧ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "DEPTID";
            this.Column2.HeaderText = "科室ID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "名称";
            this.Column10.HeaderText = "科室名称";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // DepreTypeColumn
            // 
            this.DepreTypeColumn.DataPropertyName = "DEPRETYPE";
            this.DepreTypeColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.DepreTypeColumn.HeaderText = "折旧类型";
            this.DepreTypeColumn.Name = "DepreTypeColumn";
            this.DepreTypeColumn.ReadOnly = true;
            this.DepreTypeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DepreTypeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "DATEID";
            this.Column4.HeaderText = "期间划分ID";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Visible = false;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "DATENAME";
            this.Column3.HeaderText = "期间划分名称";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // StatusColumn
            // 
            this.StatusColumn.DataPropertyName = "STATUS";
            this.StatusColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.StatusColumn.HeaderText = "状态";
            this.StatusColumn.Name = "StatusColumn";
            this.StatusColumn.ReadOnly = true;
            this.StatusColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.StatusColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "MEMO";
            this.Column6.HeaderText = "备注";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "CHOSCODE";
            this.Column9.HeaderText = "医疗机构编码";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "USERID";
            this.Column5.HeaderText = "操作员ID";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "USERNAME";
            this.Column7.HeaderText = "操作员姓名";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "RECDATE";
            this.Column8.HeaderText = "修改时间";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // EQDepreciationManag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 434);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "EQDepreciationManag";
            this.Text = "设备折旧管理";
            this.Load += new System.EventHandler(this.EQDepreciationManag_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Add_toolStrip;
        private System.Windows.Forms.ToolStripButton Edit_toolStrip;
        private System.Windows.Forms.ToolStripButton Del_toolStrip;
        private System.Windows.Forms.ToolStripButton View_toolStrip;
        private System.Windows.Forms.ToolStripButton refresh_toolStrip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private YtWinContrl.com.contrl.SelTextInpt selTextInpt1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private YtWinContrl.com.datagrid.DataGView dataGView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private YtWinContrl.com.contrl.DateTimeDuan dateTimeDuan1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewComboBoxColumn DepreTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewComboBoxColumn StatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    }
}
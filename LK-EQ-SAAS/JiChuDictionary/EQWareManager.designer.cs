namespace JiChuDictionary
{
    partial class EQWareManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EQWareManager));
            YtWinContrl.com.datagrid.TvList tvList1 = new YtWinContrl.com.datagrid.TvList();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.AddtoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ModifytoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.DeltoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ReftoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.StoptoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.UsetoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ViewtoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SearchgroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Search_ytComboBox1 = new YtWinContrl.com.contrl.YtComboBox();
            this.Search_yTextBox1 = new YtWinContrl.com.contrl.YTextBox();
            this.dataGView1 = new YtWinContrl.com.datagrid.DataGView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.SearchgroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddtoolStripButton,
            this.ModifytoolStripButton,
            this.DeltoolStripButton,
            this.ReftoolStripButton,
            this.StoptoolStripButton,
            this.UsetoolStripButton,
            this.ViewtoolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(851, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // AddtoolStripButton
            // 
            this.AddtoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("AddtoolStripButton.Image")));
            this.AddtoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddtoolStripButton.Name = "AddtoolStripButton";
            this.AddtoolStripButton.Size = new System.Drawing.Size(67, 22);
            this.AddtoolStripButton.Text = "新增(&A)";
            this.AddtoolStripButton.Click += new System.EventHandler(this.AddtoolStripButton_Click);
            // 
            // ModifytoolStripButton
            // 
            this.ModifytoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ModifytoolStripButton.Image")));
            this.ModifytoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ModifytoolStripButton.Name = "ModifytoolStripButton";
            this.ModifytoolStripButton.Size = new System.Drawing.Size(67, 22);
            this.ModifytoolStripButton.Text = "修改(&M)";
            this.ModifytoolStripButton.Click += new System.EventHandler(this.ModifytoolStripButton_Click);
            // 
            // DeltoolStripButton
            // 
            this.DeltoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("DeltoolStripButton.Image")));
            this.DeltoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeltoolStripButton.Name = "DeltoolStripButton";
            this.DeltoolStripButton.Size = new System.Drawing.Size(67, 22);
            this.DeltoolStripButton.Text = "删除(&D)";
            this.DeltoolStripButton.Click += new System.EventHandler(this.DeltoolStripButton_Click);
            // 
            // ReftoolStripButton
            // 
            this.ReftoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ReftoolStripButton.Image")));
            this.ReftoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReftoolStripButton.Name = "ReftoolStripButton";
            this.ReftoolStripButton.Size = new System.Drawing.Size(49, 22);
            this.ReftoolStripButton.Text = "刷新";
            this.ReftoolStripButton.Click += new System.EventHandler(this.ReftoolStripButton_Click);
            // 
            // StoptoolStripButton
            // 
            this.StoptoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("StoptoolStripButton.Image")));
            this.StoptoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StoptoolStripButton.Name = "StoptoolStripButton";
            this.StoptoolStripButton.Size = new System.Drawing.Size(49, 22);
            this.StoptoolStripButton.Text = "停用";
            this.StoptoolStripButton.Click += new System.EventHandler(this.StoptoolStripButton_Click);
            // 
            // UsetoolStripButton
            // 
            this.UsetoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("UsetoolStripButton.Image")));
            this.UsetoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UsetoolStripButton.Name = "UsetoolStripButton";
            this.UsetoolStripButton.Size = new System.Drawing.Size(49, 22);
            this.UsetoolStripButton.Text = "启用";
            this.UsetoolStripButton.Click += new System.EventHandler(this.UsetoolStripButton_Click);
            // 
            // ViewtoolStripButton
            // 
            this.ViewtoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ViewtoolStripButton.Image")));
            this.ViewtoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ViewtoolStripButton.Name = "ViewtoolStripButton";
            this.ViewtoolStripButton.Size = new System.Drawing.Size(49, 22);
            this.ViewtoolStripButton.Text = "浏览";
            this.ViewtoolStripButton.Click += new System.EventHandler(this.ViewtoolStripButton_Click);
            // 
            // SearchgroupBox
            // 
            this.SearchgroupBox.Controls.Add(this.label2);
            this.SearchgroupBox.Controls.Add(this.label1);
            this.SearchgroupBox.Controls.Add(this.button1);
            this.SearchgroupBox.Controls.Add(this.Search_ytComboBox1);
            this.SearchgroupBox.Controls.Add(this.Search_yTextBox1);
            this.SearchgroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.SearchgroupBox.Location = new System.Drawing.Point(0, 25);
            this.SearchgroupBox.Name = "SearchgroupBox";
            this.SearchgroupBox.Size = new System.Drawing.Size(851, 64);
            this.SearchgroupBox.TabIndex = 1;
            this.SearchgroupBox.TabStop = false;
            this.SearchgroupBox.Text = "查询条件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "关键字";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "查询方式";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(466, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "查询(Q)";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Search_ytComboBox1
            // 
            this.Search_ytComboBox1.CacheKey = null;
            this.Search_ytComboBox1.DbConn = null;
            this.Search_ytComboBox1.DefText = null;
            this.Search_ytComboBox1.DefValue = null;
            this.Search_ytComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Search_ytComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Search_ytComboBox1.EnableEmpty = true;
            this.Search_ytComboBox1.FirstText = null;
            this.Search_ytComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Search_ytComboBox1.Fomart = null;
            this.Search_ytComboBox1.ItemStr = "";
            this.Search_ytComboBox1.Location = new System.Drawing.Point(87, 22);
            this.Search_ytComboBox1.Name = "Search_ytComboBox1";
            this.Search_ytComboBox1.Param = null;
            this.Search_ytComboBox1.Size = new System.Drawing.Size(121, 22);
            this.Search_ytComboBox1.Sql = null;
            this.Search_ytComboBox1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Search_ytComboBox1.TabIndex = 0;
            this.Search_ytComboBox1.Tag = tvList1;
            this.Search_ytComboBox1.Value = null;
            this.Search_ytComboBox1.SelectionChangeCommitted += new System.EventHandler(this.Search_ytComboBox1_TextChanged);
            // 
            // Search_yTextBox1
            // 
            // 
            // 
            // 
            this.Search_yTextBox1.Border.Class = "TextBoxBorder";
            this.Search_yTextBox1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Search_yTextBox1.Location = new System.Drawing.Point(316, 22);
            this.Search_yTextBox1.Name = "yTextBox1";
            this.Search_yTextBox1.Size = new System.Drawing.Size(100, 21);
            this.Search_yTextBox1.TabIndex = 1;
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
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12});
            this.dataGView1.DbConn = "";
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
            this.dataGView1.Location = new System.Drawing.Point(0, 89);
            this.dataGView1.MultiSelect = false;
            this.dataGView1.Name = "dataGView1";
            this.dataGView1.ReadOnly = true;
            this.dataGView1.RowHeadersWidth = 20;
            this.dataGView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGView1.RowTemplate.Height = 23;
            this.dataGView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGView1.Size = new System.Drawing.Size(851, 420);
            this.dataGView1.TabIndex = 2;
            this.dataGView1.TjFmtStr = null;
            this.dataGView1.TjFormat = null;
            this.dataGView1.Url = null;
            this.dataGView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGView1_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "warecode";
            this.Column1.HeaderText = "库房编码";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "choscode";
            this.Column2.HeaderText = "医疗机构编码";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "warename";
            this.Column3.HeaderText = "库房名称";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "pycode";
            this.Column4.HeaderText = "拼音码";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "wbcode";
            this.Column5.HeaderText = "五笔码";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "DEPTName";
            this.Column6.HeaderText = "科室";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "ifuse";
            this.Column7.FalseValue = "0";
            this.Column7.HeaderText = "是否使用";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column7.TrueValue = "1";
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "ifall";
            this.Column8.FalseValue = "0";
            this.Column8.HeaderText = "IFALL";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column8.TrueValue = "1";
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "memo";
            this.Column9.HeaderText = "备注";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "userid";
            this.Column10.HeaderText = "操作员ID";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "username";
            this.Column11.HeaderText = "操作员名称";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "recdate";
            this.Column12.HeaderText = "修改时间";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            // 
            // EQWareManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 509);
            this.Controls.Add(this.dataGView1);
            this.Controls.Add(this.SearchgroupBox);
            this.Controls.Add(this.toolStrip1);
            this.Name = "EQWareManager";
            this.Text = "设备库房管理";
            this.Load += new System.EventHandler(this.EQWareManager_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.SearchgroupBox.ResumeLayout(false);
            this.SearchgroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton AddtoolStripButton;
        private System.Windows.Forms.ToolStripButton ModifytoolStripButton;
        private System.Windows.Forms.ToolStripButton DeltoolStripButton;
        private System.Windows.Forms.ToolStripButton ReftoolStripButton;
        private System.Windows.Forms.ToolStripButton StoptoolStripButton;
        private System.Windows.Forms.ToolStripButton UsetoolStripButton;
        private System.Windows.Forms.ToolStripButton ViewtoolStripButton;
        private System.Windows.Forms.GroupBox SearchgroupBox;
        private YtWinContrl.com.contrl.YtComboBox Search_ytComboBox1;
        private YtWinContrl.com.contrl.YTextBox Search_yTextBox1;
        private System.Windows.Forms.Button button1;
        private YtWinContrl.com.datagrid.DataGView dataGView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column7;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
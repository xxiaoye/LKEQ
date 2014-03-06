namespace JiChuDictionary
{
    partial class EQStatusManag
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EQStatusManag));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("统计类别");
            YtWinContrl.com.datagrid.TvList tvList1 = new YtWinContrl.com.datagrid.TvList();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.Enable_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.Stop_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Add_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.Edit_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.Del_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.Refresh_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ytTreeView1 = new YtWinContrl.com.YtTreeView();
            this.SearchgroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Search_ytComboBox1 = new YtWinContrl.com.contrl.YtComboBox();
            this.Search_yTextBox1 = new YtWinContrl.com.contrl.YTextBox();
            this.dataGView1 = new YtWinContrl.com.datagrid.DataGView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.SearchgroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "CheckGreen.bmp");
            this.imageList1.Images.SetKeyName(1, "CheckRed.bmp");
            // 
            // Enable_toolStripButton
            // 
            this.Enable_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("Enable_toolStripButton.Image")));
            this.Enable_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Enable_toolStripButton.Name = "Enable_toolStripButton";
            this.Enable_toolStripButton.Size = new System.Drawing.Size(49, 22);
            this.Enable_toolStripButton.Text = "启用";
            this.Enable_toolStripButton.Click += new System.EventHandler(this.Enable_toolStripButton_Click);
            // 
            // Stop_toolStripButton
            // 
            this.Stop_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("Stop_toolStripButton.Image")));
            this.Stop_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Stop_toolStripButton.Name = "Stop_toolStripButton";
            this.Stop_toolStripButton.Size = new System.Drawing.Size(49, 22);
            this.Stop_toolStripButton.Text = "停用";
            this.Stop_toolStripButton.Click += new System.EventHandler(this.Stop_toolStripButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add_toolStripButton,
            this.Edit_toolStripButton,
            this.Del_toolStripButton,
            this.Stop_toolStripButton,
            this.Enable_toolStripButton,
            this.Refresh_toolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(782, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Add_toolStripButton
            // 
            this.Add_toolStripButton.BackColor = System.Drawing.SystemColors.Control;
            this.Add_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("Add_toolStripButton.Image")));
            this.Add_toolStripButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Add_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Add_toolStripButton.Name = "Add_toolStripButton";
            this.Add_toolStripButton.Size = new System.Drawing.Size(49, 22);
            this.Add_toolStripButton.Text = "新增";
            this.Add_toolStripButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Add_toolStripButton.Click += new System.EventHandler(this.Add_toolStripButton_Click);
            // 
            // Edit_toolStripButton
            // 
            this.Edit_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("Edit_toolStripButton.Image")));
            this.Edit_toolStripButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Edit_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Edit_toolStripButton.Name = "Edit_toolStripButton";
            this.Edit_toolStripButton.Size = new System.Drawing.Size(49, 22);
            this.Edit_toolStripButton.Text = "编辑";
            this.Edit_toolStripButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Edit_toolStripButton.Click += new System.EventHandler(this.Edit_toolStripButton_Click);
            // 
            // Del_toolStripButton
            // 
            this.Del_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("Del_toolStripButton.Image")));
            this.Del_toolStripButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.Del_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Del_toolStripButton.Name = "Del_toolStripButton";
            this.Del_toolStripButton.Size = new System.Drawing.Size(49, 22);
            this.Del_toolStripButton.Text = "删除";
            this.Del_toolStripButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Del_toolStripButton.Click += new System.EventHandler(this.Del_toolStripButton_Click);
            // 
            // Refresh_toolStripButton
            // 
            this.Refresh_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("Refresh_toolStripButton.Image")));
            this.Refresh_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Refresh_toolStripButton.Name = "Refresh_toolStripButton";
            this.Refresh_toolStripButton.Size = new System.Drawing.Size(49, 22);
            this.Refresh_toolStripButton.Text = "刷新";
            this.Refresh_toolStripButton.Click += new System.EventHandler(this.Refresh_toolStripButton_Click);
            // 
            // ytTreeView1
            // 
            this.ytTreeView1.DbConn = null;
            this.ytTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ytTreeView1.HideSelection = false;
            this.ytTreeView1.ImageIndex = 0;
            this.ytTreeView1.ImageList = this.imageList1;
            this.ytTreeView1.LoadHaveData = false;
            this.ytTreeView1.Location = new System.Drawing.Point(0, 0);
            this.ytTreeView1.Name = "ytTreeView1";
            treeNode1.Name = "节点0";
            treeNode1.Text = "统计类别";
            this.ytTreeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.ytTreeView1.NoNodeTag = null;
            this.ytTreeView1.SelectedImageIndex = 1;
            this.ytTreeView1.Size = new System.Drawing.Size(782, 401);
            this.ytTreeView1.TabIndex = 3;
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
            this.SearchgroupBox.Size = new System.Drawing.Size(782, 54);
            this.SearchgroupBox.TabIndex = 6;
            this.SearchgroupBox.TabStop = false;
            this.SearchgroupBox.Text = "查询条件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "关键字";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "查询方式";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(568, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
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
            this.Search_ytComboBox1.Location = new System.Drawing.Point(103, 19);
            this.Search_ytComboBox1.Name = "Search_ytComboBox1";
            this.Search_ytComboBox1.Param = null;
            this.Search_ytComboBox1.Size = new System.Drawing.Size(121, 22);
            this.Search_ytComboBox1.Sql = null;
            this.Search_ytComboBox1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Search_ytComboBox1.TabIndex = 1;
            this.Search_ytComboBox1.Tag = tvList1;
            this.Search_ytComboBox1.Value = null;
            // 
            // Search_yTextBox1
            // 
            // 
            // 
            // 
            this.Search_yTextBox1.Border.Class = "TextBoxBorder";
            this.Search_yTextBox1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Search_yTextBox1.Location = new System.Drawing.Point(378, 19);
            this.Search_yTextBox1.Name = "yTextBox1";
            this.Search_yTextBox1.Size = new System.Drawing.Size(100, 21);
            this.Search_yTextBox1.TabIndex = 2;
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
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column3,
            this.Column4,
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
            this.dataGView1.Location = new System.Drawing.Point(0, 79);
            this.dataGView1.MultiSelect = false;
            this.dataGView1.Name = "dataGView1";
            this.dataGView1.ReadOnly = true;
            this.dataGView1.RowHeadersWidth = 20;
            this.dataGView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGView1.RowTemplate.Height = 23;
            this.dataGView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGView1.Size = new System.Drawing.Size(782, 322);
            this.dataGView1.TabIndex = 7;
            this.dataGView1.TjFmtStr = null;
            this.dataGView1.TjFormat = null;
            this.dataGView1.Url = null;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "STATUSCODE";
            this.Column1.HeaderText = "状态编码";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "STATUSNAME";
            this.Column2.HeaderText = "状态名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
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
            // Column6
            // 
            this.Column6.DataPropertyName = "IFDEPRECIATION";
            this.Column6.FalseValue = "0";
            this.Column6.HeaderText = "是否计提折旧";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column6.TrueValue = "1";
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "IFDEFAULT";
            this.Column7.FalseValue = "0";
            this.Column7.HeaderText = "是否默认值";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column7.TrueValue = "1";
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
            // Column8
            // 
            this.Column8.DataPropertyName = "MEMO";
            this.Column8.HeaderText = "备注";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "USERID";
            this.Column9.HeaderText = "操作员ID";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "USERNAME";
            this.Column10.HeaderText = "操作员姓名";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "RECDATE";
            this.Column11.HeaderText = "修改时间";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "CHOSCODE";
            this.Column12.HeaderText = "医疗机构编码";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            // 
            // EQStatusManag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 401);
            this.Controls.Add(this.dataGView1);
            this.Controls.Add(this.SearchgroupBox);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.ytTreeView1);
            this.Name = "EQStatusManag";
            this.Text = "设备状态管理";
            this.Load += new System.EventHandler(this.EQStatusManag_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.SearchgroupBox.ResumeLayout(false);
            this.SearchgroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton Enable_toolStripButton;
        private System.Windows.Forms.ToolStripButton Stop_toolStripButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Add_toolStripButton;
        private System.Windows.Forms.ToolStripButton Edit_toolStripButton;
        private System.Windows.Forms.ToolStripButton Del_toolStripButton;
        private YtWinContrl.com.YtTreeView ytTreeView1;
        private System.Windows.Forms.ToolStripButton Refresh_toolStripButton;
        private System.Windows.Forms.GroupBox SearchgroupBox;
        private System.Windows.Forms.Button button1;
        private YtWinContrl.com.contrl.YtComboBox Search_ytComboBox1;
        private YtWinContrl.com.contrl.YTextBox Search_yTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private YtWinContrl.com.datagrid.DataGView dataGView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
    }
}
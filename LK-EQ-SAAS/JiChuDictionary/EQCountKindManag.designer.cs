namespace JiChuDictionary
{
    partial class EQCountKindManag
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EQCountKindManag));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("统计类别");
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Add_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.Edit_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.Del_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.Stop_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.Enable_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.Search_toolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.Search_toolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.SearchtoolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ytTreeView1 = new YtWinContrl.com.YtTreeView();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "CheckGreen.bmp");
            this.imageList1.Images.SetKeyName(1, "CheckRed.bmp");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add_toolStripButton,
            this.Edit_toolStripButton,
            this.Del_toolStripButton,
            this.Stop_toolStripButton,
            this.Enable_toolStripButton,
            this.Search_toolStripLabel,
            this.Search_toolStripComboBox,
            this.toolStripTextBox1,
            this.SearchtoolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(608, 25);
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
            // Stop_toolStripButton
            // 
            this.Stop_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("Stop_toolStripButton.Image")));
            this.Stop_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Stop_toolStripButton.Name = "Stop_toolStripButton";
            this.Stop_toolStripButton.Size = new System.Drawing.Size(49, 22);
            this.Stop_toolStripButton.Text = "停用";
            this.Stop_toolStripButton.Click += new System.EventHandler(this.Stop_toolStripButton_Click);
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
            // Search_toolStripLabel
            // 
            this.Search_toolStripLabel.Margin = new System.Windows.Forms.Padding(50, 1, 0, 2);
            this.Search_toolStripLabel.Name = "Search_toolStripLabel";
            this.Search_toolStripLabel.Size = new System.Drawing.Size(53, 22);
            this.Search_toolStripLabel.Text = "查找类型";
            // 
            // Search_toolStripComboBox
            // 
            this.Search_toolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Search_toolStripComboBox.Items.AddRange(new object[] {
            "五笔码",
            "编码",
            "名称",
            "拼音码"});
            this.Search_toolStripComboBox.MergeIndex = 0;
            this.Search_toolStripComboBox.Name = "Search_toolStripComboBox";
            this.Search_toolStripComboBox.Size = new System.Drawing.Size(80, 25);
            this.Search_toolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.Search_toolStripComboBox_SelectedIndexChanged);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.AcceptsTab = true;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBox1.Text = "查找关键字";
            this.toolStripTextBox1.Leave += new System.EventHandler(this.toolStripTextBox1_Leave);
            this.toolStripTextBox1.Enter += new System.EventHandler(this.toolStripTextBox1_Enter);
            this.toolStripTextBox1.TextChanged += new System.EventHandler(this.toolStripTextBox1_TextChanged);
            // 
            // SearchtoolStripButton1
            // 
            this.SearchtoolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("SearchtoolStripButton1.Image")));
            this.SearchtoolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SearchtoolStripButton1.Name = "SearchtoolStripButton1";
            this.SearchtoolStripButton1.Size = new System.Drawing.Size(49, 22);
            this.SearchtoolStripButton1.Text = "查找";
            this.SearchtoolStripButton1.Click += new System.EventHandler(this.SearchtoolStripButton1_Click);
            // 
            // ytTreeView1
            // 
            this.ytTreeView1.DbConn = null;
            this.ytTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ytTreeView1.HideSelection = false;
            this.ytTreeView1.ImageIndex = 0;
            this.ytTreeView1.ImageList = this.imageList1;
            this.ytTreeView1.LoadHaveData = false;
            this.ytTreeView1.Location = new System.Drawing.Point(0, 25);
            this.ytTreeView1.Name = "ytTreeView1";
            treeNode1.Name = "节点0";
            treeNode1.Text = "统计类别";
            this.ytTreeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.ytTreeView1.NoNodeTag = null;
            this.ytTreeView1.SelectedImageIndex = 1;
            this.ytTreeView1.Size = new System.Drawing.Size(608, 341);
            this.ytTreeView1.TabIndex = 1;
            // 
            // EQCountKindManag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 366);
            this.Controls.Add(this.ytTreeView1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "EQCountKindManag";
            this.Text = "设备统计类别管理";
            this.Load += new System.EventHandler(this.EQCountKindManag_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Add_toolStripButton;
        private System.Windows.Forms.ToolStripButton Edit_toolStripButton;
        private System.Windows.Forms.ToolStripButton Del_toolStripButton;
        private System.Windows.Forms.ToolStripButton Stop_toolStripButton;
        private System.Windows.Forms.ToolStripButton Enable_toolStripButton;
        private System.Windows.Forms.ToolStripLabel Search_toolStripLabel;
        private System.Windows.Forms.ToolStripComboBox Search_toolStripComboBox;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripButton SearchtoolStripButton1;
        private YtWinContrl.com.YtTreeView ytTreeView1;
    }
}
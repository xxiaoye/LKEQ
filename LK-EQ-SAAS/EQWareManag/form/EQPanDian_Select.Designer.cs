namespace EQWareManag.form
{
    partial class EQPanDian_Select
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("所有设备分类");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EQPanDian_Select));
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox_IsZero = new System.Windows.Forms.CheckBox();
            this.ytTreeView1 = new YtWinContrl.com.YtTreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.yTextBox_Ware = new YtWinContrl.com.contrl.YTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(307, 332);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(74, 26);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(99, 332);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(74, 26);
            this.btn_Save.TabIndex = 3;
            this.btn_Save.Text = "确定";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Checked = false;
            this.dateTimePicker3.CustomFormat = "yyyy年MM月dd日 HH:mm";
            this.dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker3.Location = new System.Drawing.Point(156, 282);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(201, 21);
            this.dateTimePicker3.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(78, 288);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "盘点时间";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox_IsZero);
            this.groupBox2.Controls.Add(this.ytTreeView1);
            this.groupBox2.Controls.Add(this.yTextBox_Ware);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.dateTimePicker3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(583, 316);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "内容";
            // 
            // checkBox_IsZero
            // 
            this.checkBox_IsZero.AutoSize = true;
            this.checkBox_IsZero.Location = new System.Drawing.Point(401, 255);
            this.checkBox_IsZero.Name = "checkBox_IsZero";
            this.checkBox_IsZero.Size = new System.Drawing.Size(144, 16);
            this.checkBox_IsZero.TabIndex = 1;
            this.checkBox_IsZero.Text = "只查询库存大于零设备";
            this.checkBox_IsZero.UseVisualStyleBackColor = true;
            // 
            // ytTreeView1
            // 
            this.ytTreeView1.CheckBoxes = true;
            this.ytTreeView1.DbConn = null;
            this.ytTreeView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ytTreeView1.HideSelection = false;
            this.ytTreeView1.ImageIndex = 1;
            this.ytTreeView1.ImageList = this.imageList1;
            this.ytTreeView1.LoadHaveData = false;
            this.ytTreeView1.Location = new System.Drawing.Point(3, 17);
            this.ytTreeView1.Name = "ytTreeView1";
            treeNode1.Name = "节点0";
            treeNode1.Text = "所有设备分类";
            this.ytTreeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.ytTreeView1.NoNodeTag = null;
            this.ytTreeView1.SelectedImageIndex = 1;
            this.ytTreeView1.Size = new System.Drawing.Size(577, 219);
            this.ytTreeView1.TabIndex = 25;
            this.ytTreeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ytTreeView1_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "CheckGreen.bmp");
            this.imageList1.Images.SetKeyName(1, "12N3N22910-2U2N.ico");
            // 
            // yTextBox_Ware
            // 
            // 
            // 
            // 
            this.yTextBox_Ware.Border.Class = "TextBoxBorder";
            this.yTextBox_Ware.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.yTextBox_Ware.Location = new System.Drawing.Point(155, 250);
            this.yTextBox_Ware.Name = "yTextBox_Ware";
            this.yTextBox_Ware.Size = new System.Drawing.Size(200, 21);
            this.yTextBox_Ware.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Blue;
            this.label13.Location = new System.Drawing.Point(80, 255);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 24;
            this.label13.Text = "盘点库房";
            // 
            // EQPanDian_Select
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 382);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Save);
            this.Name = "EQPanDian_Select";
            this.Text = "设备盘点条件设置界面";
            this.Load += new System.EventHandler(this.EQPanDian_Select_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label13;
        private YtWinContrl.com.YtTreeView ytTreeView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox checkBox_IsZero;
        private YtWinContrl.com.contrl.YTextBox yTextBox_Ware;
    }
}
namespace ScreenCover
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainPicBox = new System.Windows.Forms.PictureBox();
            this.MainCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TopPanel = new System.Windows.Forms.Panel();
            this.TabCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MainPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPicBox
            // 
            this.MainPicBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.MainPicBox.ContextMenuStrip = this.MainCMS;
            this.MainPicBox.Location = new System.Drawing.Point(237, 35);
            this.MainPicBox.Name = "MainPicBox";
            this.MainPicBox.Size = new System.Drawing.Size(175, 231);
            this.MainPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MainPicBox.TabIndex = 0;
            this.MainPicBox.TabStop = false;
            // 
            // MainCMS
            // 
            this.MainCMS.Name = "MainCMS";
            this.MainCMS.Size = new System.Drawing.Size(61, 4);
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(133)))), ((int)(((byte)(181)))));
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(424, 29);
            this.TopPanel.TabIndex = 1;
            // 
            // TabCMS
            // 
            this.TabCMS.Name = "TabCMS";
            this.TabCMS.Size = new System.Drawing.Size(153, 26);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 561);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.MainPicBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Opacity = 0.5D;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.MainPicBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox MainPicBox;
        private System.Windows.Forms.ContextMenuStrip MainCMS;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.ContextMenuStrip TabCMS;
    }
}


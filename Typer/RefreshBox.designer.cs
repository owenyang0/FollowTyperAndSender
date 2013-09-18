namespace FollowTyper
{
    partial class RefreshBox
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
            this.labVer = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSure = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rbxDescrip = new System.Windows.Forms.RichTextBox();
            this.btnOpenUrl = new System.Windows.Forms.Button();
            this.cbxpromot = new System.Windows.Forms.CheckBox();
            this.labUrl = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // labVer
            // 
            this.labVer.AutoSize = true;
            this.labVer.Location = new System.Drawing.Point(78, 9);
            this.labVer.Name = "labVer";
            this.labVer.Size = new System.Drawing.Size(0, 12);
            this.labVer.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "更新说明：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "下载地址：跟打器目录下亦可查看";
            // 
            // btnSure
            // 
            this.btnSure.Location = new System.Drawing.Point(128, 193);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(75, 23);
            this.btnSure.TabIndex = 7;
            this.btnSure.Text = "关闭";
            this.btnSure.UseVisualStyleBackColor = true;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "新版本号：";
            // 
            // rbxDescrip
            // 
            this.rbxDescrip.BackColor = System.Drawing.SystemColors.Control;
            this.rbxDescrip.Location = new System.Drawing.Point(9, 96);
            this.rbxDescrip.Name = "rbxDescrip";
            this.rbxDescrip.Size = new System.Drawing.Size(194, 65);
            this.rbxDescrip.TabIndex = 10;
            this.rbxDescrip.Text = "";
            // 
            // btnOpenUrl
            // 
            this.btnOpenUrl.Location = new System.Drawing.Point(128, 167);
            this.btnOpenUrl.Name = "btnOpenUrl";
            this.btnOpenUrl.Size = new System.Drawing.Size(75, 23);
            this.btnOpenUrl.TabIndex = 11;
            this.btnOpenUrl.Text = "下载";
            this.btnOpenUrl.UseVisualStyleBackColor = true;
            this.btnOpenUrl.Click += new System.EventHandler(this.btnOpenUrl_Click);
            // 
            // cbxpromot
            // 
            this.cbxpromot.AutoSize = true;
            this.cbxpromot.Location = new System.Drawing.Point(9, 185);
            this.cbxpromot.Name = "cbxpromot";
            this.cbxpromot.Size = new System.Drawing.Size(96, 16);
            this.cbxpromot.TabIndex = 12;
            this.cbxpromot.Text = "今日不再提示";
            this.cbxpromot.UseVisualStyleBackColor = true;
            // 
            // labUrl
            // 
            this.labUrl.AutoSize = true;
            this.labUrl.Location = new System.Drawing.Point(7, 58);
            this.labUrl.Name = "labUrl";
            this.labUrl.Size = new System.Drawing.Size(65, 12);
            this.labUrl.TabIndex = 13;
            this.labUrl.TabStop = true;
            this.labUrl.Text = "linkLabel1";
           // this.labUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labUrl_LinkClicked);
            this.labUrl.Click += new System.EventHandler(this.labUrl_Click);
            // 
            // RefreshBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 217);
            this.Controls.Add(this.labUrl);
            this.Controls.Add(this.cbxpromot);
            this.Controls.Add(this.btnOpenUrl);
            this.Controls.Add(this.rbxDescrip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labVer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RefreshBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "跟打器检测到更新";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RefreshBox_FormClosing);
            this.Load += new System.EventHandler(this.RefreshBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labVer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rbxDescrip;
        private System.Windows.Forms.Button btnOpenUrl;
        private System.Windows.Forms.CheckBox cbxpromot;
        private System.Windows.Forms.LinkLabel labUrl;
    }
}
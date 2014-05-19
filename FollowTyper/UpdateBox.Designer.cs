namespace FollowTyper
{
    partial class UpdateBox
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
            this.labCheckInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labCheckInfo
            // 
            this.labCheckInfo.AutoSize = true;
            this.labCheckInfo.Location = new System.Drawing.Point(63, 26);
            this.labCheckInfo.Name = "labCheckInfo";
            this.labCheckInfo.Size = new System.Drawing.Size(167, 12);
            this.labCheckInfo.TabIndex = 1;
            this.labCheckInfo.Text = "正在检测新的版本，请稍后...";
            // 
            // checkUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 64);
            this.Controls.Add(this.labCheckInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "checkUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "跟打器升级";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.UpdateBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labCheckInfo;
    }
}
namespace FollowTyper
{
    partial class SendText
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendText));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.labAllPara = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxSendNO = new System.Windows.Forms.TextBox();
            this.tbxParaNO = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richbxText = new System.Windows.Forms.RichTextBox();
            this.openFileDialogNewFile = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnLastSen = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.lableTimelast = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAuto = new System.Windows.Forms.Button();
            this.timerLastTime = new System.Windows.Forms.Timer(this.components);
            this.timerSend = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGetForm = new System.Windows.Forms.Button();
            this.cbxFrom = new System.Windows.Forms.ComboBox();
            this.rdoParaph = new System.Windows.Forms.RadioButton();
            this.rdoSingle = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxAdvise = new System.Windows.Forms.TextBox();
            this.tbxTitle = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.labAllPara);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.tbxSendNO);
            this.groupBox4.Controls.Add(this.tbxParaNO);
            this.groupBox4.ForeColor = System.Drawing.Color.Navy;
            this.groupBox4.Location = new System.Drawing.Point(265, 28);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(104, 124);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "设定框";
            // 
            // labAllPara
            // 
            this.labAllPara.AutoSize = true;
            this.labAllPara.Location = new System.Drawing.Point(68, 95);
            this.labAllPara.Name = "labAllPara";
            this.labAllPara.Size = new System.Drawing.Size(11, 12);
            this.labAllPara.TabIndex = 10;
            this.labAllPara.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Purple;
            this.label5.Location = new System.Drawing.Point(12, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "总段数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Purple;
            this.label4.Location = new System.Drawing.Point(7, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "发送段数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Purple;
            this.label3.Location = new System.Drawing.Point(7, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "发文字数";
            // 
            // tbxSendNO
            // 
            this.tbxSendNO.ForeColor = System.Drawing.Color.Maroon;
            this.tbxSendNO.Location = new System.Drawing.Point(62, 38);
            this.tbxSendNO.Name = "tbxSendNO";
            this.tbxSendNO.Size = new System.Drawing.Size(34, 21);
            this.tbxSendNO.TabIndex = 4;
            this.tbxSendNO.Text = "100";
            this.tbxSendNO.TextChanged += new System.EventHandler(this.tbxSendNO_TextChanged);
            this.tbxSendNO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxSendNO_KeyPress);
            // 
            // tbxParaNO
            // 
            this.tbxParaNO.Location = new System.Drawing.Point(62, 66);
            this.tbxParaNO.Name = "tbxParaNO";
            this.tbxParaNO.Size = new System.Drawing.Size(34, 21);
            this.tbxParaNO.TabIndex = 6;
            this.tbxParaNO.Text = "1";
            this.tbxParaNO.Leave += new System.EventHandler(this.tbxParaNO_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richbxText);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBox1.Location = new System.Drawing.Point(7, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 124);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文本内容";
            // 
            // richbxText
            // 
            this.richbxText.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.richbxText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richbxText.Location = new System.Drawing.Point(6, 20);
            this.richbxText.Name = "richbxText";
            this.richbxText.ReadOnly = true;
            this.richbxText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richbxText.Size = new System.Drawing.Size(240, 98);
            this.richbxText.TabIndex = 0;
            this.richbxText.Text = "";
            // 
            // openFileDialogNewFile
            // 
            this.openFileDialogNewFile.FileName = "openFileDialog1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSend);
            this.groupBox2.Controls.Add(this.btnLastSen);
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.lableTimelast);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnAuto);
            this.groupBox2.Location = new System.Drawing.Point(7, 158);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(362, 88);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "半自动发送";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(243, 25);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(54, 23);
            this.btnSend.TabIndex = 10;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnLastSen
            // 
            this.btnLastSen.Location = new System.Drawing.Point(303, 25);
            this.btnLastSen.Name = "btnLastSen";
            this.btnLastSen.Size = new System.Drawing.Size(54, 23);
            this.btnLastSen.TabIndex = 9;
            this.btnLastSen.Text = "上一段";
            this.btnLastSen.UseVisualStyleBackColor = true;
            this.btnLastSen.Click += new System.EventHandler(this.btnLastSen_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(303, 57);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(54, 23);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(109, 27);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown1.TabIndex = 7;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // lableTimelast
            // 
            this.lableTimelast.AutoSize = true;
            this.lableTimelast.ForeColor = System.Drawing.Color.Maroon;
            this.lableTimelast.Location = new System.Drawing.Point(142, 62);
            this.lableTimelast.Name = "lableTimelast";
            this.lableTimelast.Size = new System.Drawing.Size(89, 12);
            this.lableTimelast.TabIndex = 5;
            this.lableTimelast.Text = "剩余时间（秒）";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "距下次发文时间（秒）：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "发文周期（秒）";
            // 
            // btnAuto
            // 
            this.btnAuto.Location = new System.Drawing.Point(243, 57);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(54, 23);
            this.btnAuto.TabIndex = 1;
            this.btnAuto.Text = "半自动";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // timerLastTime
            // 
            this.timerLastTime.Interval = 1000;
            this.timerLastTime.Tick += new System.EventHandler(this.timerLastTime_Tick);
            // 
            // timerSend
            // 
            this.timerSend.Tick += new System.EventHandler(this.timerSend_Tick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnGetForm);
            this.groupBox3.Controls.Add(this.cbxFrom);
            this.groupBox3.Controls.Add(this.rdoParaph);
            this.groupBox3.Controls.Add(this.rdoSingle);
            this.groupBox3.Location = new System.Drawing.Point(7, 252);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(362, 43);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "发送选项";
            // 
            // btnGetForm
            // 
            this.btnGetForm.Location = new System.Drawing.Point(279, 17);
            this.btnGetForm.Name = "btnGetForm";
            this.btnGetForm.Size = new System.Drawing.Size(78, 23);
            this.btnGetForm.TabIndex = 11;
            this.btnGetForm.Text = "获取窗口";
            this.btnGetForm.UseVisualStyleBackColor = true;
            this.btnGetForm.Click += new System.EventHandler(this.btnGetForm_Click);
            // 
            // cbxFrom
            // 
            this.cbxFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFrom.FormattingEnabled = true;
            this.cbxFrom.Location = new System.Drawing.Point(144, 19);
            this.cbxFrom.Name = "cbxFrom";
            this.cbxFrom.Size = new System.Drawing.Size(129, 20);
            this.cbxFrom.TabIndex = 2;
            // 
            // rdoParaph
            // 
            this.rdoParaph.AutoSize = true;
            this.rdoParaph.Checked = true;
            this.rdoParaph.Location = new System.Drawing.Point(77, 20);
            this.rdoParaph.Name = "rdoParaph";
            this.rdoParaph.Size = new System.Drawing.Size(47, 16);
            this.rdoParaph.TabIndex = 1;
            this.rdoParaph.TabStop = true;
            this.rdoParaph.Text = "文段";
            this.rdoParaph.UseVisualStyleBackColor = true;
            // 
            // rdoSingle
            // 
            this.rdoSingle.AutoSize = true;
            this.rdoSingle.Location = new System.Drawing.Point(13, 20);
            this.rdoSingle.Name = "rdoSingle";
            this.rdoSingle.Size = new System.Drawing.Size(47, 16);
            this.rdoSingle.TabIndex = 0;
            this.rdoSingle.Text = "单字";
            this.rdoSingle.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Purple;
            this.label6.Location = new System.Drawing.Point(5, 309);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "发文说明";
            // 
            // tbxAdvise
            // 
            this.tbxAdvise.Location = new System.Drawing.Point(64, 303);
            this.tbxAdvise.Name = "tbxAdvise";
            this.tbxAdvise.Size = new System.Drawing.Size(305, 21);
            this.tbxAdvise.TabIndex = 2;
            // 
            // tbxTitle
            // 
            this.tbxTitle.BackColor = System.Drawing.Color.LightGray;
            this.tbxTitle.Location = new System.Drawing.Point(7, 3);
            this.tbxTitle.Name = "tbxTitle";
            this.tbxTitle.Size = new System.Drawing.Size(252, 21);
            this.tbxTitle.TabIndex = 26;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(265, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(104, 23);
            this.btnOpen.TabIndex = 27;
            this.btnOpen.Text = "打开文件 ";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click_1);
            // 
            // SendText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(376, 333);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.tbxTitle);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbxAdvise);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SendText";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "发文器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SendText_FormClosing);
            this.Load += new System.EventHandler(this.SendText_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label labAllPara;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxSendNO;
        private System.Windows.Forms.TextBox tbxParaNO;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richbxText;
        private System.Windows.Forms.OpenFileDialog openFileDialogNewFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.Label lableTimelast;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Timer timerLastTime;
        private System.Windows.Forms.Timer timerSend;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoParaph;
        private System.Windows.Forms.RadioButton rdoSingle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxAdvise;
        private System.Windows.Forms.Button btnLastSen;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnGetForm;
        private System.Windows.Forms.ComboBox cbxFrom;
        private System.Windows.Forms.TextBox tbxTitle;
        private System.Windows.Forms.Button btnOpen;
    }
}
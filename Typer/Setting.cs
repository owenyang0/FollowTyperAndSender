using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Win32;
namespace FollowTyper
{
    public delegate void MyDelegateSign(string Sign, string InputMethod, bool flagVersion); //定义该委托的事件    
    public delegate void delegateHotKey(Keys keyLoad, Keys GetNM, Keys keySend); //定义该委托的事件 
    public delegate void delegateSendStyle(bool typesend,bool sigle,bool sendtorich); //定义该委托的事件 
    public delegate void deleBool(bool[] bola); //定义该委托的事件 


    public partial class Setting : Form
    {
        //bool flagVersion;
        string filePath = Application.StartupPath + @"\TyperConfig.ini";
        //public event MyDelegateSign MyEventSign;
        public event delegateHotKey EventKeys;
        public event delegateSendStyle eventSend;
        public event deleBool eventBool;
        private static bool[] bolA = new bool[8];

        public Setting()//(string Sign ,string InputMethod,bool flagVersion)
        {
            InitializeComponent();
        }
        //    this.tbxSign.Text = Sign;
        //    this.cmbInput.Text = InputMethod;
        //    rdb2011.Checked = flagVersion;
        //}
        //public Setting(Keys keyLoad, Keys GetNM, Keys keySend)
        //{
        //    //InitializeComponent();
        //    this.tbxHotKeyGetText.Text = keyLoad.ToString();
        //    this.tbxHotKeyGetGN.Text = GetNM.ToString();
        //    this.tbxHotKeySend.Text = keySend.ToString();
        //}
        private void Interpretation_Paint(object sender, PaintEventArgs e)
        {
            Pen blackpen = new System.Drawing.Pen(Color.LightGray, 1);
            Graphics graphics = this.CreateGraphics();
            graphics.DrawLine(blackpen, 110, 0, 110, this.Height );
            graphics.DrawLine(blackpen, 110, 190, this .Width  , 190);
            btnBasic.BackColor  = Color.LightGray; 
        }
        private void btnBasic_Click(object sender, EventArgs e)
        {
            panelBasic.BringToFront();
            foreach (Control btn in panelBtn.Controls)
            {
                if (btn.TabIndex == 0)
                {
                    btn.BackColor = Color.LightGray;
                }
                else
                {
                    btn.BackColor = Color.Transparent;
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            panelSend.BringToFront();
            foreach (Control btn in panelBtn.Controls)
            {
                if (btn.TabIndex == 1)
                {
                    btn.BackColor = Color.LightGray;
                }
                else
                {
                    btn.BackColor = Color.Transparent;
                }
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            panelSign.BringToFront();
            foreach (Control btn in panelBtn.Controls)
            {
                if (btn.TabIndex == 2)
                {
                    btn.BackColor = Color.LightGray;
                }
                else
                {
                    btn.BackColor = Color.Transparent;
                }
            }
        }

        private void btnSendText_Click(object sender, EventArgs e)
        {
            panelSendText.BringToFront();
            foreach (Control btn in panelBtn.Controls)
            {
                if (btn.TabIndex == 3)
                {
                    btn.BackColor = Color.LightGray;
                }
                else
                {
                    btn.BackColor = Color.Transparent;
                }
            }
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            foreach (InputLanguage iL in InputLanguage.InstalledInputLanguages)
            {
                cmbInput.Items.Add(iL.LayoutName);
            }
            cmbInput.Text = cmbInput.Items[0].ToString();
            StringBuilder retVal = new StringBuilder(0xff);
           
            win32Normal.GetPrivateProfileString("功能", "个性签名", "无", retVal, 0xff, filePath);
            tbxSign.Text = retVal.ToString();
            win32Normal.GetPrivateProfileString("功能", "输入法", "无", retVal, 0xff, filePath);
            cmbInput.Text = retVal.ToString();
            win32Normal.GetPrivateProfileString("功能", "热键载入", "F4", retVal, 0xff, filePath);
            tbxHotKeyGetText.Text = retVal.ToString();
            win32Normal.GetPrivateProfileString("功能", "热键发送", "F2", retVal, 0xff, filePath);
            tbxHotKeySend.Text = retVal.ToString();

            win32Normal.GetPrivateProfileString("发送设置", "错字", "true", retVal, 0xff, filePath);
            cbxWrongW.Checked = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("发送设置", "正误", "true", retVal, 0xff, filePath);
            cbxWroRigh.Checked = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("发送设置", "字数", "true", retVal, 0xff, filePath);
            cbxCountWords.Checked = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("发送设置", "签名", "true", retVal, 0xff, filePath);
            cbxSign.Checked = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("发送设置", "输入法", "true", retVal, 0xff, filePath);
            cbxInput.Checked = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("发送设置", "键数", "true", retVal, 0xff, filePath);
            cbxCountKey.Checked = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("发送设置", "用时", "true", retVal, 0xff, filePath);
            cbxTime.Checked = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("发送设置", "显示尾巴", "true", retVal, 0xff, filePath);
            cbxChineseEng.Checked = bool.Parse(retVal.ToString());
            win32Normal .GetPrivateProfileString ("发文","打完发送","false",retVal ,0xff,filePath );
            cbxTypeSend .Checked =bool .Parse (retVal.ToString ());
             win32Normal .GetPrivateProfileString ("发文","发送单字","false",retVal ,0xff,filePath );
            cbxSigleSend.Checked =bool .Parse (retVal.ToString ());
            win32Normal.GetPrivateProfileString("发文", "文本框获取", "false", retVal, 0xff, filePath);
            cbxSendtoRich.Checked = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("功能", "热键获取窗口", "F12", retVal, 0xff, filePath);
            tbxHotKeyGetGN.Text = retVal.ToString();
           
        }

        private void Setting_FormClosing(object sender, FormClosingEventArgs e)
        {
            writeIni();
            //MyEventSign(this.tbxSign.Text, this.cmbInput.Text, rdb2011.Checked);
            eventBool(bolA);
            eventSend(cbxTypeSend.Checked, cbxSigleSend.Checked,cbxSendtoRich .Checked);
            EventKeys((Keys)Enum.Parse(typeof(Keys), this.tbxHotKeyGetText.Text), (Keys)Enum.Parse(typeof(Keys), this.tbxHotKeyGetGN.Text), (Keys)Enum.Parse(typeof(Keys), this.tbxHotKeySend.Text));
        }
        private void writeIni()
        {
            win32Normal.WritePrivateProfileString("功能", "个性签名", tbxSign.Text, filePath);
            win32Normal.WritePrivateProfileString("功能", "输入法", cmbInput.Text, filePath);
           // win32Normal.WritePrivateProfileString("功能", "版本", rdb2011.Checked.ToString(), filePath);
            win32Normal.WritePrivateProfileString("功能", "热键载入", tbxHotKeyGetText.Text, filePath);
            win32Normal.WritePrivateProfileString("功能", "热键获取窗口", tbxHotKeyGetGN.Text, filePath);
            win32Normal.WritePrivateProfileString("功能", "热键发送", tbxHotKeySend.Text, filePath);
            win32Normal.WritePrivateProfileString("发送设置", "错字", cbxWrongW.Checked.ToString(), filePath);
            win32Normal.WritePrivateProfileString("发送设置", "正误", cbxWroRigh.Checked.ToString(), filePath);
            win32Normal.WritePrivateProfileString("发送设置", "字数", cbxCountWords.Checked.ToString(), filePath);
            win32Normal.WritePrivateProfileString("发送设置", "键数", cbxCountKey.Checked.ToString(), filePath);
            win32Normal.WritePrivateProfileString("发送设置", "输入法", cbxInput.Checked.ToString(), filePath);
            win32Normal.WritePrivateProfileString("发送设置", "签名", cbxSign.Checked.ToString(), filePath);
            win32Normal.WritePrivateProfileString("发送设置", "用时", cbxTime.Checked.ToString(), filePath);
            win32Normal.WritePrivateProfileString("发送设置", "显示尾巴", cbxChineseEng.Checked.ToString(), filePath);
            win32Normal.WritePrivateProfileString("发文", "打完发送", cbxTypeSend .Checked .ToString (), filePath);
            win32Normal.WritePrivateProfileString("发文", "发送单字", cbxSigleSend.Checked.ToString(), filePath);
            win32Normal.WritePrivateProfileString("发文", "文本框获取", cbxSigleSend.Checked.ToString(), filePath);
        }

        
        private void tbxHotKey_KeyUp(object sender, KeyEventArgs e)
        {
            tbxHotKeyGetText.Text = e.KeyCode.ToString();
        }
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            foreach (CheckBox cbx in groupBox6.Controls)
            {
                bolA[cbx.TabIndex] = cbx.Checked;

            }
        }
        private void tbxHotKeyGetGN_KeyUp(object sender, KeyEventArgs e)
        {
            tbxHotKeyGetGN.Text = e.KeyCode.ToString();
        }

        private void tbxHotKeySend_KeyUp(object sender, KeyEventArgs e)
        {
            tbxHotKeySend.Text = e.KeyCode.ToString();
        }
    }
}

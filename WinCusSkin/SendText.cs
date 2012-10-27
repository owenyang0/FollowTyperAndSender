using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

using System.Collections;
using Win32;
namespace FollowTyper
{

    public partial class SendText : Form
    {
        public SendText()
        {
            InitializeComponent();
        }
        static string FileName;
        static int count;
        static int p = 0;
        static string FilePath = "";
        static string AllPara = "";
        int b;
        int d = 0;
        static bool flagParaChang = false;
        public bool _SendSingle = true;
        IntPtr intp;
        IntPtr findPtr;
        int lasttime = 0;
        string LastSen = "";
        string MsgToQQ = "";
        #region API
        public string ConfigPath = Application.StartupPath + "\\TyperConfig.ini";//该变量保存INI文件所在的具体物理位置
        public string strOne = "SenderInfo";
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedString,
            int nSize,
            string lpFileName);

        public string ContentReader(string area, string key, string def)
        {
            StringBuilder stringBuilder = new StringBuilder(1024); 				//定义一个最大长度为1024的可变字符串
            GetPrivateProfileString(area, key, def, stringBuilder, 1024, ConfigPath); 			//读取INI文件
            return stringBuilder.ToString();								//返回INI文件的内容
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(
            string mpAppName,
            string mpKeyName,
            string mpDefault,
            string mpFileName);

        #endregion

        #region 按键是否可用
        public void btnInAvailable()
        {
            btnOpen.Enabled = false;
            tbxParaNO.Enabled = false;
            tbxSendNO.Enabled = false;
        }
        public void btnAvaiLable()
        {
            btnOpen.Enabled = true;
            tbxParaNO.Enabled = true;
            tbxSendNO.Enabled = true;
        }
        #endregion
        public void TextCut(ref int countTake, ref string FinalSend, ref string boxText)//,ref int p0)
        {
            int m = 0;
            if (!string.IsNullOrEmpty(richbxText.Text))
            {
                boxText = "s";
            }
            if (richbxText.Text != "" && tbxSendNO.Text != "")
            {
                int i;
                i = int.Parse(tbxSendNO.Text);
                if (i >= 1 && tbxParaNO.Text != "")
                {
                    string SendLast = "";
                    string s2 = "";
                    if (int.Parse(tbxParaNO.Text) > 0)
                    {
                        count = Convert.ToInt32(tbxParaNO.Text);
                        string TextSource = richbxText.Text;
                        TextSource = Regex.Replace(TextSource, @"[\s]", "");
                        richbxText.Text = TextSource;
                        char[] chars = TextSource.ToCharArray();
                        //if (!_SendSingle)
                        if(rdoParaph .Checked )
                        {
                            string[] sArray = TextSource.Split(new char[] { '，', '。', '？', '！', '.', ',' });

                            if (flagParaChang)
                            {
                                d = (count - 1) * i;
                                flagParaChang = false;
                                
                            }
                            else
                            {
                                d = b;
                            }
                            if (i * count < chars.Length)
                            {
                                for (b = 0; b < i * count; m++)
                                {
                                    b++;
                                    b = b + sArray[m].Length;
                                }
                                if (b > chars.Length)
                                    b = chars.Length;
                            }
                            else
                            {
                                b = chars.Length;
                            }
                            for (int x = d; x < b; x++)
                            {
                                s2 += chars[x].ToString();
                            }
                        }
                        else
                        {
                            if (flagParaChang)
                            {
                                d = (count - 1) * i;
                                flagParaChang = false;

                            }
                            else
                            {
                                d = b;
                            }
                            if (i * count < chars.Length)
                            {
                                for (b = 0; b < i * count; m++)
                                {
                                    b++;
                                }
                            }
                            else
                            {
                                b = chars.Length;
                            }
                            for (int x = d; x < b; x++)
                            {
                                s2 += chars[x].ToString();
                            }
                        }

                        int Yu = chars.Length % i;

                        if (p == 1)
                        {
                            FinalSend = "文已发空，谢谢跟打！易";
                            btnAvaiLable();
                        }

                        richbxText.Select(0, d);
                        richbxText.SelectionColor = Color.Black;
                        richbxText.Select(d, s2.Length);
                        richbxText.SelectionColor = Color.Maroon;
                        richbxText.ScrollToCaret();

                        int lastWords = 0;
                        lastWords = chars.Length - b;
                        if (lastWords <= 0 && p == 0)
                        {
                            SendLast = FileName + " " + tbxAdvise.Text +  "\r" + s2 + "\r" + "-----第" + count + "段-余0000" + "字◆";//-发文周期" + numericUpDown1.Value.ToString() + "秒◆易";
                            countTake = count;
                            FinalSend = SendLast;
                            tbxParaNO.Text = Convert.ToString(count);
                            p++;
                        }
                        else if (lastWords > 0)
                        {
                            SendLast = FileName + " " + tbxAdvise.Text +  "\r" + s2 + "\r" + "-----第" + count + "段-余" + lastWords + "字◆";//-发文周期" + numericUpDown1.Value.ToString() + "秒◆易";
                            countTake = count;
                            FinalSend = SendLast;
                            count++;
                            tbxParaNO.Text = Convert.ToString(count);
                        }

                    }
                }
            }
        }

        private void btnInstruct_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1.使用时，请在【WebQQ】界面登陆帐号；\n\n" +
                "2.在【WebQQ发文器】打开欲发送文件；\n\n" +
                "3.在【WebQQ】获取窗口，发送就可以了。", "使用帮助");
        }

        private void tbxSendNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 8)
                e.Handled = false;
            else
                e.Handled = true;
        }
        public void WriteIN()
        {
            WritePrivateProfileString(strOne, "TextPath", FilePath, ConfigPath);
            WritePrivateProfileString(strOne, "FileName", FileName, ConfigPath);
            WritePrivateProfileString(strOne, "WordsSetting", tbxSendNO.Text, ConfigPath);
            WritePrivateProfileString(strOne, "NowPara", tbxParaNO.Text, ConfigPath);
            WritePrivateProfileString(strOne, "AllPara", labAllPara.Text , ConfigPath);
            WritePrivateProfileString(strOne, "指针发送索引", b.ToString(), ConfigPath);
            WritePrivateProfileString(strOne, "发文周期", numericUpDown1.Value.ToString(), ConfigPath);

        }
        public void ReadOut()
        {
            StringBuilder retVal = new StringBuilder(0xff);
            FilePath = ContentReader(strOne, "TextPath", "");
            tbxSendNO.Text = ContentReader(strOne, "WordsSetting", "50");
            labAllPara.Text = ContentReader(strOne, "AllPara", "1");
            tbxParaNO.Text = ContentReader(strOne, "NowPara", "1");
            FileName = ContentReader(strOne, "FileName", "");
            numericUpDown1.Value = int.Parse(ContentReader(strOne, "发文周期", "30"));
            tbxTitle.Text=FileName;
            b = int.Parse(ContentReader(strOne, "指针发送索引", "1"));
            try
            {
                if (!string.IsNullOrEmpty(FilePath))
                {
                    StreamReader sr = new StreamReader(FilePath, Encoding.Default);
                    richbxText.Text = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch { }
        }

        private void SendText_Load(object sender, EventArgs e)
        {
            numericUpDown1.Minimum = 3;
            numericUpDown1.Maximum = 300;            
            ReadOut();

        }

        //#region 屏蔽关闭按钮
        //private const int CP_NOCLOSE_BUTTON = 0x200;
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams myCp = base.CreateParams;
        //        myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
        //        return myCp;
        //    }
        //}
        //#endregion
               
        private void tbxSendNO_TextChanged(object sender, EventArgs e)
        {
            int i;
            if (int.TryParse(tbxSendNO.Text, out i) && i > 0)
            {
                string TextSource = richbxText.Text;
                TextSource = Regex.Replace(TextSource, @"[\s]", "");
                labAllPara.Text = (TextSource.Length / i + 1).ToString();
                AllPara = labAllPara.Text;
            }
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value >= 3 && numericUpDown1.Value <= 300)
            {
                SendFinal();
                timerSend.Interval = (int)(numericUpDown1.Value) * 1000;
                timerSend.Start();
                lasttime = (int)numericUpDown1.Value;
                timerLastTime.Start();
            }
            else
            {
                MessageBox.Show("请将时间设置在3-300之间", "时间设置");
            }
        }
        private void SendFinal()
        {
            int Para = 0;            
            string boxText = "";
            LastSen = MsgToQQ;
            TextCut(ref Para, ref MsgToQQ, ref boxText);
            SendToQQ(MsgToQQ);
           
            timerSend.Interval = (int)(numericUpDown1.Value) * 1000;
            //win32Normal.SetForegroundWindow(FollowTyperc.inputbox);
        }
        private void SendToQQ(string s)
        {
            findPtr = win32Normal.FindWindow("TXGuiFoundation", cbxFrom.Text );// labelGroupName.Text);
            win32Normal.SetForegroundWindow(findPtr);
            //win32Normal.ShowWindow(intp, ConstFlag.SW_SHOWNORMAL);

            Clipboard.Clear();
            Clipboard.SetText(s);
            //SendKeys.SendWait("^a^v%s");
            int VK_RETURN = 13;
            int WM_KEYDOWN = 0x0100;
            //int VK_CONTROL = 0x11;
            //int WM_KEYUP = 0x101;
            const int WM_PASTE = 770;//粘贴
           // win32Normal.SetForegroundWindow(findPtr);//WM_LBUTTONDOWN   EN_KILLFOCUS   $10000000
            win32Normal.PostMessage(findPtr, WM_PASTE, 0, null);
            //win32Normal.PostMessage(findPtr, WM_KEYDOWN, VK_RETURN, null);
            SendKeys.SendWait("^a^v%s");
        }

        private void timerSend_Tick(object sender, EventArgs e)
        {
            SendFinal();
            lasttime = (int)numericUpDown1.Value;
            win32Normal.SetForegroundWindow(base .Handle);
        }

        private void timerLastTime_Tick(object sender, EventArgs e)
        {
            lasttime --;
            lableTimelast.Text = lasttime.ToString();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timerSend.Interval = (int)(numericUpDown1.Value) * 1000;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timerSend.Stop();
            timerLastTime.Stop();
        }

        private void SendText_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteIN();
            this.Hide();
            e.Cancel = true;
           
        }

        private void tbxParaNO_Leave(object sender, EventArgs e)
        {
            flagParaChang = true;
        }

        private void btnLastSen_Click(object sender, EventArgs e)
        {
            SendToQQ(LastSen );
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            SendFinal();

            timerLastTime.Stop();
            timerSend.Stop();
        }

        private void btnGetForm_Click(object sender, EventArgs e)
        {
            int i = 0;
            cbxFrom.Items.Clear();
            win32Normal api2 = new win32Normal();
            CallBack myCallBack = new CallBack(api2.Report);
            win32Normal.EnumWindows(myCallBack, 0);
            while (!string.IsNullOrEmpty(win32Normal.GroupName[i]))
            {
                //GName.Add(win32Normal.GroupName[i++]);
                cbxFrom.Items.Add(win32Normal.GroupName[i++]);
            }
            //labelGroupName.Text = GName[++GNum < GName.Count ? GNum : (GNum = 0)].ToString();
            cbxFrom .Text = cbxFrom.Items[0].ToString();
        }

        private void btnOpen_Click_1(object sender, EventArgs e)
        {
            openFileDialogNewFile.Filter = "文本文档(*.txt)|*.TXT";
            if (DialogResult.OK == openFileDialogNewFile.ShowDialog())
            {
                FileName = Path.GetFileNameWithoutExtension(openFileDialogNewFile.FileName);
                FilePath = openFileDialogNewFile.FileName;
                StreamReader sr = new StreamReader(FilePath, Encoding.Default);
                richbxText.Text = sr.ReadToEnd();
                sr.Close();
                tbxParaNO.Text = "1";
                int i;
                i = int.Parse(tbxSendNO.Text);
                string TextSource = richbxText.Text;
                TextSource = Regex.Replace(TextSource, @"[\s]", "");
                char[] chars = TextSource.ToCharArray();
                labAllPara.Text = (chars.Length / i + 1).ToString();
                AllPara = labAllPara.Text;
                b = 0;
                tbxTitle.Text = FileName;
            }
        }
    }
}
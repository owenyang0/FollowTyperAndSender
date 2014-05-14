using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using IWshRuntimeLibrary;
using System.Runtime.InteropServices;
using SqlGrades;
using UpGrade;
using Win32;

namespace FollowTyper
{
    public partial class FollowTyperc : Form
    {

        #region 变量常量申明
        String strImagesPath = Application.StartupPath;

        static string SubName = "purple";//主题
        static string Sign = string.Empty;//签名
        static string InputMethod = string.Empty;//输入法
        //static bool flagVersion = true;//选择QQ版本
        public static IntPtr findPtr = IntPtr.Zero;//当前窗口句柄
        static int GNum = 0;//当前窗口名索引

        private static bool pause_ = true;//是否暂停
        private static int pressCount = 0;//键数
        private static double startTime = 0.0;//开始跟打时间
        private static int back = 0;//回改数
        private static int timeCount = 0;//开打后，所用时间整数
        private static int preLen = 0;
        private static int actLen = 0;
        private static bool[] isRight = new bool[0x1388];
        private static int preLine = 0;
        private static int rightWord = 0;
        private static int wrongWord = 0;
        private static double countCost = 0.0;


        private static StringBuilder grade = new StringBuilder(255);//未发送成绩列表
        private static double totalSpeed = 0;//跟打速度
        private static double totalTimes = 0;//跟打计算次数
        private static double totalPressCount = 0;
        private static double averagePressCount = 0;
        private static double averageGrades = 0;

        public static int acm = 0;

        private static int avgBack = 0;
        private static int avgPress = 0;
        private static double avgTime = 0.0;
        private static int avgWords = 0;
        private static int avgWrongWords = 0;


        //private static int curEpleseTime = 0;
        //private static int curRowIndex = 0;

        private const int derWidth = 0;
        private static double endTime = 0.0;


        private const int MAX_INPUTLEN = 100;
        private static int num = 0;
        private static string page = "0";

        private static Keys keyGetGN = Keys.F10;
        private static Keys keyLoad = Keys.F4;
        private static Keys keySend = Keys.F2;
        private int binput = 0;

        //private static bool bwrongWord = true;
        //private static bool bcountWord = true;
        //private static bool bcoutnKeys = true;
        //private static bool bTime = true;
        //private static bool bwroRight = true;
        //private static bool binputLan = true;
        //private static bool bSigh = true;
        private static bool[] bolA = new bool[8];

        SendText sendtext = new SendText();
        private bool sendafterType = false;
        private bool sendSingle = false;
        private bool sendtoRich = false;
        public static IntPtr inputbox;
        private string _StartDate;//统计开始日期
        private static int _todayWordsCount;//今日打字字数
        //private bool _DateJot=true ;//指示是否
        private int _ToatlWordsCount;//总字数
        public bool english_Show = false;//是否英文显示，成绩
        private string _startCountTime = "";//开始统计时间

        Rectangle rect, rectOld; //QQ窗口范围，以及鼠标当前位置
        #endregion

        public FollowTyperc()
        {
            InitializeComponent();
        }


        #region 打字代码
        private bool IsFunctionPress(KeyEventArgs e)
        {

            if (e.KeyCode.ToString() == "F3")
            {
                this.Reset();
                return true;
            }

            if (e.KeyCode.ToString() == "F1")
            {
                if (!pause_)
                {
                    endTime = DateTime.Now.TimeOfDay.TotalSeconds;
                    countCost += endTime - startTime;
                    startTime = endTime;
                    this.timerCount.Stop();
                    this.labelTime.Text = "暂停";
                    pause_ = true;
                }
                return true;
            }
            if (e.KeyCode == keyGetGN)
            {
                GetGroupName();

                return true;
            }
            return false;
        }
        private void GetGroupName()
        {
            try
            {
                int i = 0;
                ArrayList GName = new ArrayList();
                win32Normal api2 = new win32Normal();
                CallBack myCallBack = new CallBack(api2.Report);
                win32Normal.EnumWindows(myCallBack, 0);
                while (!string.IsNullOrEmpty(win32Normal.GroupName[i]))
                {
                    GName.Add(win32Normal.GroupName[i++]);
                }
                labelGroupName.Text = GName[++GNum < GName.Count ? GNum : (GNum = 0)].ToString();

                findPtr = win32Normal.FindWindow("TXGuiFoundation", labelGroupName.Text);



            }
            catch
            {
                labelGroupName.Text = "未找到";
            }
        }
        private void SendToQQ(string s)
        {
            if (this.labelGroupName.Text != "未找到")
            {
                win32Normal.SetForegroundWindow(findPtr);
                win32Normal.ShowWindow(findPtr, ConstFlag.SW_SHOWNORMAL);


                Clipboard.Clear();
                Clipboard.SetText(s);

                //int VK_RETURN = 13;
                //int WM_KEYDOWN = 0x0100;
                //int VK_CONTROL = 0x11;
                //int WM_KEYUP = 0x101;
                const int WM_PASTE = 770;//粘贴
                // win32Normal.SetForegroundWindow(findPtr);//WM_LBUTTONDOWN   EN_KILLFOCUS   $10000000
                win32Normal.PostMessage(findPtr, WM_PASTE, 0, null);
                // win32Normal.PostMessage(findPtr, WM_KEYDOWN, VK_RETURN, null);
                SendKeys.SendWait("^a^v%s");

            }
        }

        private string IntToTime(int x)
        {
            int num = x / 60;
            return (num.ToString() + ":" + string.Format("{0:00}", x % 60));
        }

        private void Helper()
        {
            this.richTextBoxGot.AppendText("欢迎使用易跟打，使用方法:请点菜单栏的[其他]-[使用帮助]\n");
        }
        private void timerCount_Tick(object sender, EventArgs e)
        {
            timeCount++;
            this.labelTime.Text = this.IntToTime(timeCount);
        }
        private void Reset()
        {
            actLen = this.richTextBoxGot.TextLength - 1;
            this.progressBar1.Maximum = actLen;
            this.progressBar1.Minimum = 0;
            this.progressBar1.Step = 1;
            this.progressBar1.Value = 0;
            // this.lableRate.Text = "0.00%";

            this.labelLeave.Text = "余" + actLen.ToString() + "字";
            this.timerCount.Stop();
            timeCount = 0;
            pause_ = true;
            countCost = 0.0;
            preLine = 0;
            this.richTextBoxGot.SelectAll();
            this.richTextBoxGot.SelectionBackColor = Color.White;
            this.richTextBoxGot.Select(0, 1);
            this.richTextBoxGot.ScrollToCaret();
            num++;
            rightWord = 0;
            wrongWord = 0;
            back = 0;
            pressCount = 0;
            startTime = 0.0;
            endTime = 0.0;
            this.textBoxInput.Text = "";
            this.textBoxInput.ReadOnly = false;
            preLen = 0;
            this.textBoxInput.Focus();
            this.labelTime.Text = "0:00";
            this.lableSpeed.Text = "0";
        }

        private string GetMessageFromQQ()
        {
            string input = "";

            if (findPtr != IntPtr.Zero)
            {
                win32Normal.SetForegroundWindow(findPtr);
                win32Normal.ShowWindow(findPtr, ConstFlag.SW_SHOWNORMAL);
                win32Normal.GetWindowRect(findPtr, ref rect);
                //SendKeys.SendWait(" {BACKSPACE}{TAB}{TAB}{TAB}{TAB}");
                rectOld.X = MousePosition.X;
                rectOld.Y = MousePosition.Y;
                win32Normal.SetCursorPos(rect.X + 320, rect.Y + 200);
                win32Normal.mouse_event(MouseEventFlag.LeftDown, rect.X, rect.Y, 0, 0);
                win32Normal.mouse_event(MouseEventFlag.LeftUp, rect.X, rect.Y, 0, 0);
                SendKeys.SendWait("^a");
                SendKeys.SendWait("^c");
                SendKeys.SendWait("^a");
                SendKeys.SendWait("^c");
                win32Normal.SetCursorPos(rectOld.X, rectOld.Y);
                input = Clipboard.GetText();
            }
            return this.GetParagram(input);
        }
        private string GetParagram(string input)
        {
            string str = "";
            int length = input.Length;
            int num5 = 0;
            for (int i = length - 1; i >= 0; i--)
            {
                int num3;
                int num4;
                if (((input[i] == '-') && ((i + 1) < length)) && (input[i + 1] == '第'))
                {
                    num3 = i;
                    num5 = 0;
                    while ((i >= 0) && (input[i] == '-'))
                    {
                        num5++;
                        i--;
                    }
                    i++;
                    num4 = i - 1;
                    if ((num4 >= 0) && this.Empty(input[num4]))
                    {
                        goto Label_008F;
                    }
                }
                continue;
            Label_0089:
                num4--;
            Label_008F:
                if ((num4 > 0) && this.Empty(input[num4]))
                {
                    goto Label_0089;
                }
                switch (num5)
                {
                    case 5:
                    case 6:
                        num3 += 2;
                        page = "";
                        while ((num3 < length) && this.Dig(input[num3]))
                        {
                            page = page + input[num3].ToString();
                            num3++;
                        }
                        if (num5 == 5)
                        {
                            if ((num3 < length) && (input[num3] == '段'))
                            {
                                while (((num4 >= 0) && (input[num4] != '\n')) && (input[num4] != '\r'))
                                {
                                    num4--;
                                }
                                num4++;
                                while (((num4 < (i - 1)) && (input[num4] != '\n')) && (input[num4] != '\r'))
                                {
                                    str = str + input[num4].ToString();
                                    num4++;
                                }
                                this.lablePara.Text = "第" + page + "段";
                                if (str != "")
                                {
                                    return str;
                                }
                            }
                        }
                        break;
                }
            }
            return str;
        }
        private void Init()
        {
            Clipboard.Clear();

            this.dataGridViewResult.MultiSelect = false;
            this.dataGridViewResult.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.textBoxInput.ReadOnly = false;
            this.richTextBoxGot.ReadOnly = true;
            this.dataGridViewResult.RowHeadersVisible = false;
            this.dataGridViewResult.AllowUserToAddRows = false;
            this.dataGridViewResult.AllowUserToResizeColumns = true;
            this.dataGridViewResult.AllowUserToResizeRows = true;
            this.dataGridViewResult.Sort(this.dataGridViewResult.Columns[this.dataGridViewResult.Columns.Count - 1], ListSortDirection.Ascending);

            this.Helper();
        }
        private void SendText()
        {

            int Para = 0;
            string MsgToQQ = "";
            string boxText = "";
            //sendtext._SendSingle = sendSingle;
            sendtext.TextCut(ref Para, ref MsgToQQ, ref boxText);
            SendToQQ(MsgToQQ);
            if (sendtoRich)
            {
                richTextBoxGot.Text = GetParagram(MsgToQQ);
            }
            win32Normal.SetForegroundWindow(base.Handle); this.Reset();

            textBoxInput.Focus();
        }
        private void ProcessHotkey(Message m)
        {
            string str3;

            if (((str3 = m.WParam.ToString()) != null) && (str3 == "100"))
            {
                KeyGetMsg();
                //MessageBox.Show("test");
            }
            if (((str3 = m.WParam.ToString()) != null) && (str3 == "99"))
            {
                SendText();
                win32Normal.SetForegroundWindow(base.Handle);

            }
            if (binput < 4)
            {
                win32Normal input = new win32Normal();
                input.InputLan(InputMethod, this.textBoxInput.Handle);
                binput++;
            }
        }
        private void KeyGetMsg()
        {
            string messageFromQQ = this.GetMessageFromQQ();
            if (messageFromQQ != "")
            {
                this.richTextBoxGot.Text = messageFromQQ + "\r";

            }
            this.Reset();
            win32Normal.SetForegroundWindow(base.Handle);
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                this.ProcessHotkey(m);
            }
            base.WndProc(ref m);
        }

        private void richTextBoxGot_KeyDown(object sender, KeyEventArgs e)
        {
            IsFunctionPress(e);
        }

        private void WriteIni()
        {

            StringBuilder retVal = new StringBuilder(0xff);
            string filePath = Application.StartupPath + @"\TyperConfig.ini";
            win32Normal.WritePrivateProfileString("跟打器", "高度", this.Height.ToString(), filePath);
            win32Normal.WritePrivateProfileString("跟打器", "宽度", this.Width.ToString(), filePath);
            win32Normal.WritePrivateProfileString("跟打器", "X", this.Location.X.ToString(), filePath);
            win32Normal.WritePrivateProfileString("跟打器", "Y", this.Location.Y.ToString(), filePath);
            win32Normal.WritePrivateProfileString("对照框", "字体", this.richTextBoxGot.Font.FontFamily.ToString(), filePath);
            win32Normal.WritePrivateProfileString("对照框", "大小", this.richTextBoxGot.Font.Size.ToString(), filePath);
            win32Normal.WritePrivateProfileString("对照框", "风格", this.richTextBoxGot.Font.Style.ToString(), filePath);
            win32Normal.WritePrivateProfileString("输入框", "字体", this.textBoxInput.Font.ToString(), filePath);
            win32Normal.WritePrivateProfileString("输入框", "大小", this.textBoxInput.Font.Size.ToString(), filePath);
            win32Normal.WritePrivateProfileString("输入框", "风格", this.textBoxInput.Font.Style.ToString(), filePath);
            win32Normal.WritePrivateProfileString("输入框", "高度", this.splitContainer1.SplitterDistance.ToString(), filePath);
            win32Normal.WritePrivateProfileString("成绩列表", "高度", this.splitContainer2.SplitterDistance.ToString(), filePath);
            win32Normal.WritePrivateProfileString("功能", "主题", SubName, filePath);
            win32Normal.WritePrivateProfileString("更新", "日期", DateTime.Now.DayOfYear.ToString(), filePath);
            win32Normal.WritePrivateProfileString("语言选择", "英文显示", english_Show.ToString(), filePath);

            // win32Normal.GetPrivateProfileString("打字统计", "开始日期", null, retVal, 0xff, filePath);
            //     if (string.IsNullOrEmpty(retVal.ToString()))
            //     {
            //         win32Normal.WritePrivateProfileString("打字统计", "开始日期", DateTime.Now.ToShortDateString(), filePath);
            //     }                
            // win32Normal.WritePrivateProfileString("打字统计", "今日字数", _todayWordsCount.ToString(), filePath);
            //// win32Normal.WritePrivateProfileString("打字统计", "总字数", _ToatlWordsCount.ToString(), filePath);

        }
        private void ReadOutIni()
        {
            string filePath = Application.StartupPath + @"\TyperConfig.ini";
            StringBuilder retVal = new StringBuilder(0xff);
            string family = "";
            float size = 0f;
            string style = "";
            int x = 0, y = 0;
            win32Normal.GetPrivateProfileString("跟打器", "高度", "351", retVal, 0xff, filePath);
            this.Height = Convert.ToInt32(retVal.ToString());
            win32Normal.GetPrivateProfileString("跟打器", "宽度", "538", retVal, 0xff, filePath);
            this.Width = Convert.ToInt32(retVal.ToString());
            win32Normal.GetPrivateProfileString("跟打器", "X", "50", retVal, 0xff, filePath);
            x = Convert.ToInt32(retVal.ToString());
            win32Normal.GetPrivateProfileString("跟打器", "Y", "50", retVal, 0xff, filePath);
            y = Convert.ToInt32(retVal.ToString());
            //this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
            win32Normal.GetPrivateProfileString("功能", "个性签名", "无", retVal, 0xff, filePath);
            Sign = retVal.ToString();
            win32Normal.GetPrivateProfileString("功能", "输入法", "无", retVal, 0xff, filePath);
            InputMethod = retVal.ToString();
            win32Normal.GetPrivateProfileString("功能", "热键载入", "F4", retVal, 0xff, filePath);
            keyLoad = (Keys)Enum.Parse(typeof(Keys), retVal.ToString());
            win32Normal.GetPrivateProfileString("功能", "热键发送", "F2", retVal, 0xff, filePath);
            keySend = (Keys)Enum.Parse(typeof(Keys), retVal.ToString());

            win32Normal.GetPrivateProfileString("功能", "热键获取窗口", "F12", retVal, 0xff, filePath);

            keyGetGN = (Keys)Enum.Parse(typeof(Keys), retVal.ToString());//return false;


            //win32Normal.GetPrivateProfileString("功能", "版本", "true", retVal, 0xff, filePath);

            //Boolean.TryParse(retVal.ToString(), out flagVersion);
            win32Normal.GetPrivateProfileString("功能", "主题", "purple", retVal, 0xff, filePath);

            SubName = retVal.ToString();
            win32Normal.GetPrivateProfileString("对照框", "字体", "[Font: Name=SimSun, Size=16, Units=3, GdiCharSet=134, GdiVerticalFont=False]", retVal, 0xff, filePath);

            family = retVal.ToString();
            win32Normal.GetPrivateProfileString("对照框", "大小", "16", retVal, 0xff, filePath);

            size = (float)Convert.ToDouble(retVal.ToString());
            win32Normal.GetPrivateProfileString("对照框", "风格", "=Regular", retVal, 0xff, filePath);

            style = retVal.ToString();
            this.richTextBoxGot.Font.Dispose();
            this.richTextBoxGot.Font = ComMethod.GetFont(family, size, style);
            win32Normal.GetPrivateProfileString("输入框", "字体", "[Font: Name=SimSun, Size=12, Units=3, GdiCharSet=134, GdiVerticalFont=False]", retVal, 0xff, filePath);

            family = retVal.ToString();
            win32Normal.GetPrivateProfileString("输入框", "大小", "12", retVal, 0xff, filePath);

            size = (float)Convert.ToDouble(retVal.ToString());
            win32Normal.GetPrivateProfileString("输入框", "风格", "Regular", retVal, 0xff, filePath);

            style = retVal.ToString();
            this.textBoxInput.Font.Dispose();
            this.textBoxInput.Font = ComMethod.GetFont(family, size, style);
            win32Normal.GetPrivateProfileString("输入框", "高度", "null", retVal, 0xff, filePath);
            if (retVal.ToString() != "null")
            {
                this.splitContainer1.SplitterDistance = Convert.ToInt32(retVal.ToString());
            }
            win32Normal.GetPrivateProfileString("成绩列表", "高度", "null", retVal, 0xff, filePath);
            if (retVal.ToString() != "null")
            {
                if (this.splitContainer2.Panel2MinSize < this.splitContainer2.SplitterDistance && this.splitContainer2.SplitterDistance < Width - this.splitContainer2.Panel2MinSize)
                    this.splitContainer2.SplitterDistance = Convert.ToInt32(retVal.ToString());
                // MessageBox.Show(this.splitContainer2.Panel2MinSize.ToString(), this.splitContainer2.SplitterDistance.ToString());
            }
            win32Normal.GetPrivateProfileString("发送设置", "错字", "true", retVal, 0xff, filePath);
            bolA[0] = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("发送设置", "字数", "true", retVal, 0xff, filePath);
            bolA[1] = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("发送设置", "键数", "true", retVal, 0xff, filePath);
            bolA[2] = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("发送设置", "用时", "true", retVal, 0xff, filePath);
            bolA[3] = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("发送设置", "正误", "true", retVal, 0xff, filePath);
            bolA[4] = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("发送设置", "输入法", "true", retVal, 0xff, filePath);
            bolA[5] = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("发送设置", "签名", "true", retVal, 0xff, filePath);
            bolA[6] = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("发送设置", "显示尾巴", "true", retVal, 0xff, filePath);
            bolA[7] = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("发文", "打完发送", "false", retVal, 0xff, filePath);
            sendafterType = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("发文", "发送单字", "false", retVal, 0xff, filePath);
            sendSingle = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("发文", "文本框获取", "false", retVal, 0xff, filePath);
            sendtoRich = bool.Parse(retVal.ToString());
            win32Normal.GetPrivateProfileString("更新", "日期", "1", retVal, 0xff, filePath);
            int.TryParse(retVal.ToString(), out nowtime);
            win32Normal.GetPrivateProfileString("更新", "不再显示", "false", retVal, 0xff, filePath);
            notShow = bool.Parse(retVal.ToString());
            //win32Normal.GetPrivateProfileString("打字统计", "开始日期", DateTime.Now.ToShortDateString(), retVal, 0xff, filePath);
            //_StartDate = retVal.ToString();
            ////win32Normal.GetPrivateProfileString("打字统计", "总字数", "0", retVal, 0xff, filePath);
            ////_ToatlWordsCount = int.Parse(retVal.ToString());

            //win32Normal.GetPrivateProfileString("打字统计", "今日字数", "0", retVal, 0xff, filePath);
            //_todayWordsCount = int.Parse(retVal.ToString());
            //win32Normal.GetPrivateProfileString("打字统计", "今天日期", null, retVal, 0xff, filePath);
            //if (!DateTime.Now.ToShortDateString().Equals(retVal.ToString()))
            //{
            //    _todayWordsCount = 0;
            //    win32Normal.WritePrivateProfileString("打字统计", "今天日期", DateTime.Now.ToShortDateString(), filePath);
            //}
            win32Normal.GetPrivateProfileString("语言选择", "英文显示", "false", retVal, 0xff, filePath);
            //MessageBox.Show(retVal.ToString());
            if (english_Show = bool.Parse(retVal.ToString()))
            {
                labelInfo.Text = "成绩格式：英文";
            }
            else
            {
                labelInfo.Text = "成绩格式：中文";
            }

        }


        private void textBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.IsFunctionPress(e))
            {
                if (this.richTextBoxGot.Text == "")
                {
                    this.Helper();
                }
                if (pause_)
                {
                    startTime = DateTime.Now.TimeOfDay.TotalSeconds;
                    pause_ = false;
                    this.timerCount.Start();
                }
                pressCount++;
                if (e.KeyCode.ToString() == "Back")
                {
                    back++;
                }
            }
        }
        private bool Empty(char x)
        {
            if (((x != '\t') && (x != '\n')) && (x != '\r'))
            {
                return (x == ' ');
            }
            return true;
        }
        private bool Dig(char x)
        {
            return ((x >= '0') && (x <= '9'));
        }
        private int Vabs(int x)
        {
            if (x < 0)
            {
                return -x;
            }
            return x;
        }
        private string MyTrimEnd(string s)
        {
            string str = s;
            int length = s.Length;
            if ((length > 0) && (s[length - 1] == '\n'))
            {
                str = s.Substring(0, length - 2);
                this.textBoxInput.Text = str;
                this.textBoxInput.Select(this.textBoxInput.Text.Length, 1);
            }
            return str;
        }
        public bool _flagSpeed = false;
        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.Vabs(this.textBoxInput.Text.Length - preLen) > 100)
                {
                    this.Reset();
                }
                else
                {
                    if (!_flagSpeed)
                    {
                        timerSpeed.Start();
                    }
                    int num3;
                    this.MyTrimEnd(this.textBoxInput.Text);
                    if (this.textBoxInput.Text.Length >= actLen)
                    {
                        this.textBoxInput.Text = this.textBoxInput.Text.Substring(0, actLen);
                    }
                    this.labelLeave.Text = "余" + ((actLen - this.textBoxInput.TextLength)).ToString() + "字";
                    double num2 = ((double)(this.textBoxInput.Text.Length * 100)) / ((double)actLen);
                    //this.lableRate.Text = string.Format("{0:0.00}", num2) + "%";
                    this.progressBar1.Value = (this.textBoxInput.Text.Length * this.progressBar1.Maximum) / actLen;
                    for (num3 = preLen - 1; num3 >= this.textBoxInput.Text.Length; num3--)
                    {
                        this.richTextBoxGot.Select(num3, 1);
                        this.richTextBoxGot.SelectionBackColor = Color.White;
                    }
                    num3 = this.textBoxInput.TextLength - 30;
                    if (num3 < 0)
                    {
                        num3 = 0;
                    }
                    while (num3 < this.textBoxInput.Text.Length)
                    {
                        if (this.textBoxInput.Text[num3] == this.richTextBoxGot.Text[num3])
                        {
                            this.richTextBoxGot.Select(num3, 1);
                            this.richTextBoxGot.SelectionBackColor = Color.DarkGray;
                            isRight[num3] = true;
                        }
                        else
                        {
                            this.richTextBoxGot.Select(num3, 1);
                            this.richTextBoxGot.SelectionBackColor = Color.Red;
                            isRight[num3] = false;
                        }
                        num3++;
                    }
                    int selectionStart = this.richTextBoxGot.SelectionStart;
                    int lineFromCharIndex = this.richTextBoxGot.GetLineFromCharIndex(selectionStart);
                    if (lineFromCharIndex != preLine)
                    {
                        int num6 = this.richTextBoxGot.GetLineFromCharIndex(this.richTextBoxGot.TextLength - 1);
                        int start = this.richTextBoxGot.GetFirstCharIndexOfCurrentLine() - 1;
                        if (start < 0)
                        {
                            start = 0;
                        }
                        if (lineFromCharIndex < num6)
                        {
                            this.richTextBoxGot.Select(start, 1);
                            this.richTextBoxGot.ScrollToCaret();
                        }
                        preLine = lineFromCharIndex;
                    }
                    preLen = this.textBoxInput.Text.Length;
                    if (this.textBoxInput.Text.Length == actLen && textBoxInput.Text[actLen - 1] == richTextBoxGot.Text[actLen - 1])
                    {
                        // MessageBox.Show( richTextBoxGot.Text[actLen - 1].ToString (),textBoxInput.Text[actLen-1].ToString ());
                        double num8;
                        this.timerCount.Stop();
                        string str = "";
                        string str2 = "";
                        wrongWord = 0;
                        rightWord = 0;
                        for (num3 = 0; num3 < actLen; num3++)
                        {
                            if (this.richTextBoxGot.Text[num3] != this.textBoxInput.Text[num3])
                            {
                                wrongWord++;
                                str = str + this.richTextBoxGot.Text[num3];
                                str2 = str2 + this.textBoxInput.Text[num3];
                            }
                            else
                            {
                                rightWord++;
                            }
                        }
                        this.textBoxInput.ReadOnly = true;
                        this.textBoxInput.Select(this.textBoxInput.Text.Length, 0);
                        if (this.textBoxInput.Text.Length != 0)
                        {
                            num8 = ((double)pressCount) / ((double)this.textBoxInput.Text.Length);
                        }
                        else
                        {
                            num8 = 0.0;
                        }
                        endTime = DateTime.Now.TimeOfDay.TotalSeconds;
                        FollowTyperc.countCost += endTime - startTime;
                        avgTime += FollowTyperc.countCost;
                        avgBack += back;
                        avgPress += pressCount;
                        avgWords += actLen;
                        avgWrongWords += wrongWord;

                        double num9 = (((rightWord + wrongWord) - (5 * wrongWord)) * 60.0) / FollowTyperc.countCost;
                        double num10 = ((double)pressCount) / FollowTyperc.countCost;
                        this.dataGridViewResult.Rows.Add(new object[] { page, string.Format("{0:0.00}", num9), string.Format("{0:0.00}", num10), string.Format("{0:0.00}", num8), back.ToString(), string.Format("{0:0000}", num) });
                        this.dataGridViewResult.Sort(this.dataGridViewResult.Columns[this.dataGridViewResult.Columns.Count - 1], ListSortDirection.Descending);
                        string s = "";// this.lablePara.Text + " 速度";
                        double dsped = num9;
                        string spd = "";
                        int num12;
                        if (!english_Show)
                        {
                            s = this.lablePara.Text + " 速度";

                            //if (wrongWord > 0)
                            //{
                            //    spd = string.Format("{0:0.00}", num9) + "/";
                            //    s = s + string.Format("{0:0.00}", num9) + "/";
                            //    num9 = (rightWord * 60.0) / FollowTyperc.countCost;
                            //    s = s + string.Format("{0:0.00}", num9);
                            //    spd += string.Format("{0:0.00}", num9);
                            //}
                            //else
                            //{
                            s = s + string.Format("{0:0.00}", num9);
                            spd += string.Format("{0:0.00}", num9);
                            //}
                            timerSpeed.Stop();
                            _flagSpeed = false;
                            lableSpeed.Text = spd;
                            if (str == "")
                            {
                                str = " 错字:无";
                            }
                            else
                            {
                                str = " 正[" + str + "] 误[" + str2 + "]";
                            }
                            object obj2 = s;
                            s = string.Concat(new object[] { obj2, " 回改", back, " 击键", string.Format("{0:0.00}", num10), " 码长", string.Format("{0:0.00}", num8) });
                            double countCost = FollowTyperc.countCost;

                            num12 = (int)(countCost / 60.0);
                            countCost -= num12 * 60;


                            if (bolA[0])// (bwrongWord)
                            {
                                s += " 错字" + wrongWord;
                            }

                            if (bolA[1])//  (bcountWord)
                            {
                                s += " 字数" + actLen;
                            }

                            if (bolA[2])
                            {
                                s += " 键数" + pressCount;
                            }
                            string UseTime = " 用时" + num12 + ":" + string.Format("{0:0.00}", countCost);
                            if (bolA[3])// (bTime )
                            {
                                s += UseTime;
                            }

                            if (bolA[4])// (bwroRight  )
                            {
                                s += str;
                            }

                            _todayWordsCount += actLen;
                            _ToatlWordsCount += actLen;
                            s += SignSet();
                            SendToQQ(s);
                            // labelInfo.Text = "成绩格式：中文";
                            labWords.Text = "今日:" + _todayWordsCount + " 总数:" + _ToatlWordsCount;
                        }
                        else
                        {
                            s = "Paragraph:" + page + " Speed:";

                            if (wrongWord > 0)
                            {
                                spd = string.Format("{0:0.00}", num9) + "/";
                                s = s + string.Format("{0:0.00}", num9) + "/";
                                num9 = (rightWord * 60.0) / FollowTyperc.countCost;
                                s = s + string.Format("{0:0.00}", num9);
                                spd += string.Format("{0:0.00}", num9);
                            }
                            else
                            {
                                s = s + string.Format("{0:0.00}", num9);
                                spd += string.Format("{0:0.00}", num9);
                            }
                            timerSpeed.Stop();
                            _flagSpeed = false;
                            lableSpeed.Text = spd;
                            if (str == "")
                            {
                                str = " WrongWords:None";
                            }
                            else
                            {
                                str = " Ture[" + str + "] False[" + str2 + "]";
                            }
                            object obj2 = s;
                            s = string.Concat(new object[] { obj2, " BackSpace:", back, " Hits:", string.Format("{0:0.00}", num10), " WordLength:", string.Format("{0:0.00}", num8) });
                            double countCost = FollowTyperc.countCost;

                            num12 = (int)(countCost / 60.0);
                            countCost -= num12 * 60;


                            if (bolA[0])// (bwrongWord)
                            {
                                s += " WrongWordCounts:" + wrongWord;
                            }

                            if (bolA[1])//  (bcountWord)
                            {
                                s += " WordsCounts:" + actLen;
                            }

                            if (bolA[2])
                            {
                                s += " KeyCounts:" + pressCount;
                            }
                            string UseTime = " Time:" + num12 + ":" + string.Format("{0:0.00}", countCost);
                            if (bolA[3])// (bTime )
                            {
                                s += UseTime;
                            }

                            if (bolA[4])// (bwroRight  )
                            {
                                s += str;
                            }

                            _todayWordsCount += actLen;
                            _ToatlWordsCount += actLen;
                            s += SignSetEnglish();
                            SendToQQ(s);
                            // labelInfo.Text = "成绩格式：英文" ;
                            labWords.Text = "Today:" + _todayWordsCount + " Total:" + _ToatlWordsCount;
                        }
                        System.Threading.ThreadPool.QueueUserWorkItem(o =>
                        {
                            if (dsped > 4)
                            {
                                Invoke(new Action(() =>
                                {
                                    new GradesHis().Add(//向数据库中添加任务  
                                 DateTime.Now.ToString(),
                                 lablePara.Text,
                                double.Parse(spd),
                                back,
                                double.Parse(string.Format("{0:0.00}", num10)),
                                 double.Parse(string.Format("{0:0.00}", num8)),
                                 wrongWord,
                                 actLen,
                                 pressCount,
                                 num12 + ":" + string.Format("{0:0.00}", countCost)
                                 );
                                }));
                            }
                        });
                        System.Threading.ThreadPool.QueueUserWorkItem(o =>
                        {
                            if (sendafterType)
                            {
                                Invoke(new Action(() =>
                                {
                                    SendText();
                                }));
                            }
                        });


                        grade = new StringBuilder(s);
                        averageGrade(num9, num10);
                    }
                }
            }
            catch { }
        }
        private string SignSet()
        {
            string input = " 输入法:" + InputMethod.Replace(" ", "");
            string sigh = " 签名:" + Sign;
            string sr = "";
            if (bolA[5])// (binputLan)
            {
                sr += input;
            }
            if (bolA[6])//  (bSigh)
            {
                sr += sigh;
            }
            sr += " 统计[总字" + _ToatlWordsCount + " 今日" + _todayWordsCount + " 起" + _StartDate + "]";


            sr += "◆" + Application.ProductVersion.Substring(0, 3);

            return sr;
        }
        private string SignSetEnglish()
        {
            string input = " InputMethod:" + InputMethod.Replace(" ", "");
            string sigh = " Autograph:" + Sign;
            string sr = "";
            if (bolA[5])// (binputLan)
            {
                sr += input;
            }
            if (bolA[6])//  (bSigh)
            {
                sr += sigh;
            }
            sr += " Statistic[Total:" + _ToatlWordsCount + " Today:" + _todayWordsCount + " Start:" + _StartDate + "]";
            if (bolA[7])
            {
                sr += "◆" + Application.ProductVersion.Substring(0, 3);
            }
            return sr;
        }

        private void averageGrade(double speed, double presscount)
        {
            totalSpeed += speed;
            totalPressCount += presscount;
            totalTimes++;
            averageGrades = totalSpeed / totalTimes;
            averagePressCount = totalPressCount / totalTimes;


        }
        #endregion

        #region 菜单栏与工具栏
        private void 发送快捷方式到桌面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lnk CreateLnk = new Lnk();
            CreateLnk.CreateShortcutLnk(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                Application.ExecutablePath, "易跟打器", "欢迎使用", Application.ExecutablePath + ",0");
        }
        private void FollowTyper_Load(object sender, EventArgs e)
        {
            this.readAllInfo();
            System.Threading.ThreadPool.QueueUserWorkItem(o =>
            {
                if (isTodayRead(nowtime) && checkUpdate())
                {
                    if (isStop == true)
                    {
                        textBoxInput.Enabled = false;
                        richTextBoxGot.Enabled = false;
                    }
                    Invoke(new Action(() =>
                    {
                        refb.Show();
                    }));
                }
            });


        }
        private void readAllInfo()
        {
            ReadOutIni();
            sendtext.ReadOut();
            Init();
            inputbox = this.textBoxInput.Handle;
            win32Normal.RegisterHotKey(base.Handle, 100, 0, keyLoad);
            win32Normal.RegisterHotKey(base.Handle, 99, 0, keySend);//0x73);
            //labelInfo.Text = "跟打前按F12获得窗口，F4载入文本，亦可自由定制热键";
            this.Text = "易跟打" + Application.ProductVersion.Substring(0, 3);
            GetGroupName();
            GetTypeInfo_WordsCount();
            sendtext.Hide();
            labWords.Text = "今日:" + _todayWordsCount + " 总数:" + _ToatlWordsCount + " 开始时间：" + _startCountTime;
            timer1.Start();
            // labWords.Text = new GradesHis().command("select sum(wordscount) from grade where datediff('d',date,now())=0");

        }
        private void GetTypeInfo_WordsCount()
        {
            string tem = "";
            tem = new GradesHis().command("select sum(wordscount) from grade where datediff('d',date,now())=0");
            _todayWordsCount = string.IsNullOrEmpty(tem) ? 0 : int.Parse(tem);
            _ToatlWordsCount = int.Parse(new GradesHis().command("select sum(wordscount) from grade"));
            _StartDate = _startCountTime = new GradesHis().command("SELECT top 1 Format(date, 'yyyy-mm-dd') FROM grade ORDER BY  Format(date, 'yyyy-mm-dd') asc");

        }
        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            sendtext.Close();
            win32Normal.UnregisterHotKey(base.Handle, 100);
            win32Normal.UnregisterHotKey(base.Handle, 99);
            WriteIni();
        }
        private void 基本设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting set = new Setting();
            // SignAndSetInput set = new SignAndSetInput(Sign, InputMethod, flagVersion); //注册form2_MyEvent方法的MyEvent事件 
            //set.MyEventSign += new MyDelegateSign(set_MyEventSign);
            set.EventKeys += new delegateHotKey(set_EventKeys);
            set.eventSend += new delegateSendStyle(set_eventSend);
            set.eventBool += new deleBool(set_eventBool);
            win32Normal.UnregisterHotKey(base.Handle, 100);
            win32Normal.UnregisterHotKey(base.Handle, 99);
            set.Show();

        }

        void set_eventSend(bool typesend, bool sigle, bool sendtorich)
        {
            sendSingle = sigle;
            sendafterType = typesend;
            sendtoRich = sendtorich;
        }

        void set_eventBool(bool[] bola)
        {
            for (int i = 0; i < bola.Length; i++)
            {
                bolA[i] = bola[i];

            }

            english_Show = bolA[7];
            if (bolA[7])
            {
                labelInfo.Text = "成绩格式：英文";
            }
            else
            {
                labelInfo.Text = "成绩格式：中文";
            }

        }
        void set_EventKeys(Keys Load, Keys GetNM, Keys Send)
        {
            keyGetGN = GetNM;
            keyLoad = Load;
            keySend = Send;
            win32Normal.RegisterHotKey(base.Handle, 100, 0, keyLoad);
            win32Normal.RegisterHotKey(base.Handle, 99, 0, keySend);//0x73);

        }
        void set_MyEventSign(string Sign2, string Input2, bool flagVersion2)//将签名与输入法回调
        {
            Sign = Sign2;
            InputMethod = Input2;
            //flagVersion = flagVersion2;
        }
        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void 载入练习材料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opfile = new OpenFileDialog();
            opfile.Filter = "*.txt|*.TXT";
            if (DialogResult.OK == opfile.ShowDialog())
            {
                StreamReader sr = new StreamReader(opfile.FileName, Encoding.Default);
                richTextBoxGot.Text = sr.ReadToEnd();
                sr.Close();
            }
            deleteBlank();
        }
        private void deleteBlank()
        {
            string s0 = richTextBoxGot.Text;
            s0 = Regex.Replace(s0, @"[\s]", "");
            richTextBoxGot.Text = s0 + "\r";
        }

        private void 使用帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                  "1.获取窗口与载入文段可自定义热键，请在【基本设置】中设置相关项,\n\n" +
                  "2.默认:【F12】获得窗口，【F4】载入文段，【F3】重打文段,\n\n" +
                  "3.其他设置，请在【设置】按个人设置，\n\n" +
                  "4.如遇到无法载入文段，请尝试切换【基本设置】-【版本切换】", "使用帮助");
        }
        private void SelectResources_Click(object sender, EventArgs e)
        {
            switch (sender.ToString())
            {
                case "强暴333":
                    lablePara.Text = "单字333";
                    richTextBoxGot.Text = RandomWords(FollowTyper.Properties.Resources.强暴333);
                    break;
                case "凌辱444":
                    lablePara.Text = "单字444";
                    richTextBoxGot.Text = RandomWords(FollowTyper.Properties.Resources.凌辱444);
                    break;
                case "疯狂555":
                    lablePara.Text = "单字555";
                    richTextBoxGot.Text = RandomWords(FollowTyper.Properties.Resources.疯狂555);
                    break;
                case "十六大报告":
                    lablePara.Text = "十六大报告";
                    richTextBoxGot.Text = FollowTyper.Properties.Resources.十六大报告;
                    break;
                case "十七大报告":
                    lablePara.Text = "十七大报告";
                    richTextBoxGot.Text = FollowTyper.Properties.Resources.十七大报告;
                    break;
                case "儿童是祖国的未来":
                    lablePara.Text = "儿童是祖国的未来";
                    richTextBoxGot.Text = FollowTyper.Properties.Resources.儿童是祖国的未来;
                    break;
            }
            deleteBlank();
            Reset();
        }
        private string RandomWords(string s)
        {
            string tem = string.Empty;
            Random random = new Random();
            List<char> newList = new List<char>();
            char[] x = s.ToCharArray();
            foreach (char it in x)
            {
                // newList.Add(it);
                newList.Insert(random.Next(newList.Count), it);
            }
            foreach (char p in newList)
            {
                tem += p.ToString();
            }
            return tem;

        }

        private void 对照框ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontdialog2 = new FontDialog();
            fontdialog2.Font = richTextBoxGot.Font;
            if (fontdialog2.ShowDialog() == DialogResult.OK)
            {
                this.richTextBoxGot.Font = fontdialog2.Font;
            }
        }

        private void 输入框ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontdialog = new FontDialog();
            fontdialog.Font = textBoxInput.Font;
            if (fontdialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxInput.Font = fontdialog.Font;
            }
        }
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox About = new AboutBox();
            About.ShowDialog();
        }

        #endregion

        private void 发送上一段成绩ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendToQQ(grade.ToString());
        }

        private void 发送平均成绩ToolStripMenuItem_Click(object sender, EventArgs e) { }

        //{
        //    SendToQQ("本次跟打总段数" + totalTimes + " 平均速度" +
        //        string.Format("{0:0.00}", averageGrades) + " 平均击键" + averagePressCount+SignSet ());
        //}

        #region 检验更新
        public static string Version;
        public static string DownUrl;
        public static string Descrip1;
        public static string Descrip2;
        public static string Descrip3;
        public bool isStop = false;//跟打器是否可用
        private static int nowtime;
        private static bool notShow = false;
        RefreshBox refb = new RefreshBox();
        private bool isTodayRead(int itime)
        {
            if (itime != DateTime.Now.DayOfYear)
            {
                notShow = false;
            }
            if (itime <= DateTime.Now.DayOfYear && !notShow)
                return true;
            else
                return false;
        }
        delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.richTextBoxGot.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.richTextBoxGot.Text = text;
            }
        }

        private bool checkUpdate()
        {
            string filepath = Application.StartupPath + "\\有新版本.txt";
            SoftUpdate updateInstance = new SoftUpdate(Application.ExecutablePath, "跟打器");
            updateInstance.checkUpdate();
            if (!string.IsNullOrEmpty(updateInstance.updateItems.Message))
                this.SetText(updateInstance.updateItems.Message);
            StatusLabelInfo.Text = updateInstance.updateItems.Info;

            if (!string.IsNullOrEmpty(updateInstance.updateItems.IsStop) && updateInstance.updateItems.IsStop.ToLower() == "true")
            {
                isStop = true;
            }
            try
            {

                if (updateInstance.NeedUpdate)
                {
                    Version = updateInstance.updateItems.Version;
                    DownUrl = updateInstance.updateItems.DownloadAddress;
                    Descrip1 = updateInstance.updateItems.Descriptions[0];
                    Descrip2 = updateInstance.updateItems.Descriptions[1];
                    Descrip3 = updateInstance.updateItems.Descriptions[2];

                    win32Normal.WritePrivateProfileString("最新版本", "版本号", Version, filepath);
                    win32Normal.WritePrivateProfileString("最新版本", "下载地址", DownUrl, filepath);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        private void btnHideSender_Click(object sender, EventArgs e)
        {
            sendtext.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Setting fd = new Setting();
            fd.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _nowTime.Text = DateTime.Now.ToLongTimeString();

        }

        private void timerSpeed_Tick(object sender, EventArgs e)
        {
            if (startTime != 0)
            {
                _flagSpeed = true;
                double _endTime = DateTime.Now.TimeOfDay.TotalSeconds;
                double _totalTime = _endTime - startTime;
                double _speed = ((this.textBoxInput.TextLength) * 60.0) / _totalTime;//spd = string.Format("{0:0.00}", num9)
                string _strSpeed = string.Format("{0:0.00}", _speed);
                lableSpeed.Text = _strSpeed;
            }
        }
        private void MenuItemCheckUp_Click(object sender, EventArgs e)
        {
            UpdateBox chup = new FollowTyper.UpdateBox();
            chup.Show();
            RefreshBox refb = new RefreshBox();
            System.Threading.ThreadPool.QueueUserWorkItem(o =>
            {
                if (isTodayRead(nowtime) && checkUpdate())
                {
                    Invoke(new Action(() =>
                    {
                        refb.Show();
                        chup.Dispose();
                    }));
                }
            });
        }

        private void MenuItemTypeHis_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(Application.StartupPath + @"/TypeHistory.exe"))
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"/TypeHistory.exe");
            }
            else
            {
                MessageBox.Show("跟打历史，不存在。\n请重新下载。", "文件遗失");
            }
        }
        private void StatusLabelInfo_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(StatusLabelInfo.Text);
            }
            catch { }

        }

    }

}
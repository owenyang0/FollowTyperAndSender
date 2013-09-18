using System;
using System.Collections.Generic;

using System.Text;

using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace Win32
{
    public delegate bool CallBack(IntPtr hwnd, IntPtr lParam);

    public class ConstFlag
    {
        #region 窗口常量
        public const int x = 5;
        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;

        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int BACKSPACE = 32;
        public const int VK_ESCAPE = 0X1B;
        public const int VK_LEFT = 0x25;
        public const int VK_RIGHT = 0x27;
        //int VK_RETURN = 13;
        //int WM_KEYDOWN = 0x0100;
        //int VK_CONTROL = 0x11;
        //int WM_KEYUP = 0x101;

        #endregion
    }
    [Flags]
    public enum MouseEventFlag : uint
    {
        Move = 0x0001,
        LeftDown = 0x0002,
        LeftUp = 0x0004,
        RightDown = 0x0008,
        RightUp = 0x0010,
        MiddleDown = 0x0020,
        MiddleUp = 0x0040,
        XDown = 0x0080,
        XUp = 0x0100,
        Wheel = 0x0800,
        VirtualDesk = 0x4000,
        Absolute = 0x8000
    }

    public  class win32Normal
    {
        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
            byte bVk,
            byte bScan,
            int dwFlags,  //这里是整数类型  0 为按下，2为释放
            int dwExtraInfo  //这里是整数类型 一般情况下设成为 0
        );

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        public static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, int extraInfo);
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, ref Rectangle rect);

        public static string[] GroupName = new string[10];
        public int inNum = 0;
        [DllImport("user32")]
        public static extern int EnumWindows(CallBack x, int y);

        [DllImport("user32")]
        public static extern int GetClassNameA(IntPtr hwnd, StringBuilder lptrString, int nMaxCount);
        [DllImport("user32")]
        public static extern int GetParent(IntPtr hwnd);
        [DllImport("user32")]
        public static extern int IsWindowVisible(IntPtr hwnd);
        //查找窗口
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        //SwitchToThisWindow(FindWindow(null, labelGroupName.Text ), true); //激活窗口
        [DllImport("user32.dll", EntryPoint = "SwitchToThisWindow")]
        private static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
        
        //可将最小化窗口还原
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public  static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32", SetLastError = true)]
        public static extern int GetWindowText(
            IntPtr hWnd,//窗口句柄
            StringBuilder lpString,//标题
            int nMaxCount //最大值
            );
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, string lParam);


        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
       
        [DllImport("User32.dll")]
        public static extern bool RegisterHotKey(IntPtr hwnd, int id, uint fsModifiers, Keys vk);
        [DllImport("user32.dll", EntryPoint = "PostMessageA")]
        public static extern int PostMessage(IntPtr hwnd, int wMsg, int wParm, string lParam);
        public bool Report(IntPtr hwnd, IntPtr lParam)
        {
            
            StringBuilder classname = new StringBuilder(256);
            GetClassNameA(hwnd, classname, classname.Capacity);
            int pHwnd;
            pHwnd = GetParent(hwnd);
            if (pHwnd == 0 && IsWindowVisible(hwnd) == 1 && classname .ToString () == "TXGuiFoundation")
            {
                StringBuilder sb = new StringBuilder(512);
                GetWindowText(hwnd, sb, sb.Capacity);
                if (sb.Length > 0)
                {
                    string p = sb.ToString();
                    GroupName [inNum] = p;
                    inNum++;
                }
            } return true;
        }

        [DllImport("kernel32 ")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32 ")]
        public static extern int  GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #region 输入法
        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hWnd);

        [DllImport("imm32.dll")]
        public static extern bool ImmGetConversionStatus(IntPtr hIMC,
            ref int conversion, ref int sentence);

        [DllImport("imm32.dll")]
        public static extern bool ImmSetConversionStatus(IntPtr hIMC, int conversion, int sentence);
        
        public void  InputLan(string InputL, IntPtr intptr)
        {
            
            foreach (InputLanguage iL in InputLanguage.InstalledInputLanguages)
            {
                if (iL.LayoutName == InputL)
                {
                    InputLanguage.CurrentInputLanguage = iL;
                    break;
                }
            }
            IntPtr prt =ImmGetContext(intptr);
            int iMode = 1025;
            int iSentence = 0;
            win32Normal.ImmSetConversionStatus(prt, iMode, iSentence);
        }
        #endregion
    }
}
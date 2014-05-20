using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Win32
{
    public delegate bool CallBack(IntPtr hwnd, IntPtr lParam);

    public class Win32API
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

        [DllImport("user32")]
        public static extern int EnumWindows(CallBack x, int y);

        [DllImport("user32")]
        public static extern int GetClassNameA(IntPtr hwnd, StringBuilder lptrString, int nMaxCount);

        [DllImport("user32")]
        public static extern int GetParent(IntPtr hwnd);

        [DllImport("user32")]
        public static extern int IsWindowVisible(IntPtr hwnd);

        // 查找窗口
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // 可将最小化窗口还原
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32", SetLastError = true)]
        public static extern int GetWindowText(
            IntPtr hWnd,//窗口句柄
            StringBuilder lpString,//标题
            int nMaxCount //最大值
        );

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("User32.dll")]
        public static extern bool RegisterHotKey(IntPtr hwnd, int id, uint fsModifiers, Keys vk);

        [DllImport("user32.dll", EntryPoint = "PostMessageA")]
        public static extern int PostMessage(IntPtr hwnd, int wMsg, int wParm, string lParam);

        public static string[] GroupNames = new string[10];
        private int groupIndex = 0;
        public bool Report(IntPtr hwnd, IntPtr lParam)
        {
            StringBuilder className = new StringBuilder(256);
            GetClassNameA(hwnd, className, className.Capacity);
            if (GetParent(hwnd) != 0 || IsWindowVisible(hwnd) != 1 || className.ToString() != "TXGuiFoundation")
            {
                return true;
            }

            StringBuilder stringBuilder = new StringBuilder(512);
            GetWindowText(hwnd, stringBuilder, stringBuilder.Capacity);
            if (stringBuilder.Length <= 0)
            {
                return true;
            }

            GroupNames[groupIndex] = stringBuilder.ToString();
            groupIndex++;

            return true;
        }

        [DllImport("kernel32 ")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32 ")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #region InputMethod API
        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hWnd);

        [DllImport("imm32.dll")]
        public static extern bool ImmSetConversionStatus(IntPtr hIMC, int conversion, int sentence);

        public void InputLan(string InputL, IntPtr intptr)
        {
            foreach (InputLanguage iL in InputLanguage.InstalledInputLanguages.Cast<InputLanguage>().Where(iL => iL.LayoutName == InputL))
            {
                InputLanguage.CurrentInputLanguage = iL;
                break;
            }

            IntPtr prt = ImmGetContext(intptr);
            int iMode = 1025;
            int iSentence = 0;
            ImmSetConversionStatus(prt, iMode, iSentence);
        }
        #endregion
    }
}
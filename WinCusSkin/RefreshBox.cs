using System;

using System.Drawing;

using System.Windows.Forms;
using Win32;

namespace FollowTyper
{
    public partial class RefreshBox : Form
    {
        private System.Drawing.Rectangle Rect;//定义一个存储矩形框的数组
        public RefreshBox()
        {
            InitializeComponent();
            System.Drawing.Rectangle rect = System.Windows.Forms.Screen.GetWorkingArea(this);//实例化一个当前窗口的对象
            this.Rect = new System.Drawing.Rectangle(rect.Right - this.Width - 1, rect.Bottom - this.Height - 1, this.Width, this.Height);//为实例化的对象创建工作区域
        }

        private void RefreshBox_Load(object sender, EventArgs e)
        {
            labVer.Text  = FollowTyperc.Version;
            labUrl.Text = FollowTyperc.DownUrl;
            rbxDescrip.AppendText(FollowTyperc.Descrip1+"\n");
            rbxDescrip.AppendText(FollowTyperc.Descrip2 + "\n");
            rbxDescrip.AppendText(FollowTyperc.Descrip3 + "\n");
            this.SetBounds(Rect.X - 5, Rect.Y - 5, Rect.Width, Rect.Height);//设置当前窗体的边界
            this.TopMost = true;
        }
        private void labUrl_Click(object sender, EventArgs e)
        {
            openUrl();
        }
        private void btnSure_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnOpenUrl_Click(object sender, EventArgs e)
        {
            openUrl();
            this.Close();

        }
        private void openUrl()
        {
            try
            {
                System.Diagnostics.Process.Start(labUrl.Text);
            }
            catch { }
        }

        private void RefreshBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            string filePath = Application.StartupPath + @"\TyperConfig.ini";
            win32Normal.WritePrivateProfileString("更新", "不再显示", cbxpromot.Checked.ToString(), filePath);
        }


    }
}

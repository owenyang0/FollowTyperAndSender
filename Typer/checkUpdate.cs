using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FollowTyper
{
    public partial class checkUpdate : Form
    {
        public checkUpdate()
        {
            InitializeComponent();
        }

        private void checkUpdate_Load(object sender, EventArgs e)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(o =>
            {
                System.Threading.Thread.Sleep(6000);

                Invoke(new Action(() =>
                {
                    labCheckInfo.Text = "    您已是最新版本!";
                }));
            });
        }
    }
}

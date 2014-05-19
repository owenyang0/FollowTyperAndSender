using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SqlGrades;

namespace FollowTyper
{
    public partial class TypeHis : Form
    {
        public EventHandler up = null;
        public static string s = "";
        public string mdb = Application.StartupPath + "\\Grades.mdb";
        private List<fields> G_Task = new List<fields>();//任务集合

        public TypeHis()
        {
            InitializeComponent();
        }
        public delegate void myDelegate();

        private void TypeHis_Load(object sender, EventArgs e)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(o =>
            {
                dgvGrades.BeginInvoke(new myDelegate(GetMessage));
            });



        }

        private void GetMessage()
        {

            dgvGrades.Rows.Clear();//清空控件项 
            G_Task = new GradesHistory().Select();//得到任务集合
            dgvGrades.DataSource = G_Task;
            dgvGrades.Columns[0].HeaderText = "日期";
            dgvGrades.Columns[1].HeaderText = "段数";
            dgvGrades.Columns[2].HeaderText = "速度";
            dgvGrades.Columns[3].HeaderText = "回改";
            dgvGrades.Columns[4].HeaderText = "击键";
            dgvGrades.Columns[5].HeaderText = "码长";
            dgvGrades.Columns[6].HeaderText = "错字";
            dgvGrades.Columns[7].HeaderText = "字数";
            dgvGrades.Columns[8].HeaderText = "键数";
            dgvGrades.Columns[9].HeaderText = "用时";
            dgvGrades.Columns[0].MinimumWidth = 100;
            string tem = "";
            int totalWordtem = 0, totalDays = 1;
            string startTime = "", endTime = "";
            string temDayWords = "";

            tem = new GradesHistory().command("select count(ID) from T_Grade");
            labAllCount.Text = "总条数：" + tem;
            tem = new GradesHistory().command("select sum(wordscount) from T_Grade");
            totalWordtem = string.IsNullOrEmpty(tem) ? 0 : int.Parse(tem);
            labAllWords.Text = "总字数：" + tem;
            tem = new GradesHistory().command("select sum(keycount) from T_Grade");
            labkeyCount.Text = "总键数：" + tem;
            tem = new GradesHistory().command("select avg(speed) from T_Grade");
            labAverageSpeed.Text = "平均速度：" + tem.Substring(0, tem.IndexOf('.') + 3);
            tem = new GradesHistory().command("select avg(hitkey) from T_Grade");
            labAverageHitKeys.Text = "平均击键：" + tem.Substring(0, tem.IndexOf('.') + 3);
            tem = new GradesHistory().command("select avg(keylong) from T_Grade");
            labAverageWorkdsLength.Text = "平均码长：" + tem.Substring(0, tem.IndexOf('.') + 3);
            tem = new GradesHistory().command("select top 1 speed from T_Grade order by speed desc");//select top 5 * from T_Grade order by date desc
            labTopSpeed.Text = "最高速度：" + tem;
            tem = new GradesHistory().command("select top 1 hitkey from T_Grade order by hitkey desc");
            labBestHit.Text = "最佳击键：" + tem;
            tem = new GradesHistory().command("select top 1 keylong from T_Grade order by keylong asc");// order by keylong asc");
            labbestWordsLength.Text = "最佳码长：" + tem;

            tem = new GradesHistory().command("SELECT top 1 Format(CompletedDate, 'yyyy-mm-dd') FROM T_Grade ORDER BY  Format(CompletedDate, 'yyyy-mm-dd') asc");
            startTime = tem;
            labStaticTime.Text = "统计时间：" + tem;
            tem = new GradesHistory().command("SELECT top 1 Format(CompletedDate, 'yyyy-mm-dd') FROM T_Grade ORDER BY  Format(CompletedDate, 'yyyy-mm-dd') desc");
            endTime = tem;
            labStaticTime.Text += " to " + tem;
            tem = new GradesHistory().command("SELECT datediff('d',#" + startTime + "#,now())FROM T_Grade");
            totalDays = string.IsNullOrEmpty(tem) ? 0 : int.Parse(tem);
            temDayWords = ((double)totalWordtem / totalDays).ToString();
            dayWords.Text = "每日平均字数：" + temDayWords.Substring(0, temDayWords.IndexOf('.') + 3);
            tem = new GradesHistory().command("select sum(wordscount) from T_Grade where datediff('d',CompletedDate,now())=0");
            todayWords.Text = "今日字数：" + (string.IsNullOrEmpty(tem) ? 0 : int.Parse(tem)).ToString();
            tem = new GradesHistory().command("select avg(speed) from T_Grade where datediff('d',CompletedDate,now())=0");
            if (!string.IsNullOrEmpty(tem))
            {
                daySpeed.Text = "今日速度：" + tem.Substring(0, tem.IndexOf('.') + 3);
            }
            else
            {
                daySpeed.Text = "0";
            }
            for (int i = 0; i < dgvGrades.Rows.Count; i++)
            {
                if (i % 2 == 0)
                    dgvGrades.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
            }
        }

        private void dgvGrades_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < e.RowCount; i++)
            {
                this.dgvGrades.Rows[e.RowIndex + i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                this.dgvGrades.Rows[e.RowIndex + i].HeaderCell.Value = (e.RowIndex + i + 1).ToString();
            }
        }
    }
}

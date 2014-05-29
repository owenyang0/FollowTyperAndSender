using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DataOperation.DAL;
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

            labAllCount.Text = "总条数：" + GradeStatisticDAL.GetTotalCounts();
            labAllWords.Text = "总字数：" + GradeStatisticDAL.getTotalWords();
            labkeyCount.Text = "总键数：" + GradeStatisticDAL.getTotalKeys();
            labAverageSpeed.Text = "平均速度：" + GradeStatisticDAL.getAverageSpeed().ToString("F2");
            labAverageHitKeys.Text = "平均击键：" + GradeStatisticDAL.getAverageHits();
            labAverageWorkdsLength.Text = "平均码长：" + GradeStatisticDAL.getAverageKeyLong();
            labTopSpeed.Text = "最高速度：" + GradeStatisticDAL.getTopSpeed();
            labBestHit.Text = "最佳击键：" + GradeStatisticDAL.getBestHits();
            labbestWordsLength.Text = "最佳码长：" + GradeStatisticDAL.getBestKeyLong();
            labStaticTime.Text = "统计时间：" + GradeStatisticDAL.getTimeSpan();
            dayWords.Text = "每日平均字数：" + GradeStatisticDAL.getEverydayWords().ToString("F2");
            todayWords.Text = "今日字数：" + GradeStatisticDAL.getTodayWords().ToString("F2");
            daySpeed.Text = "今日速度：" + GradeStatisticDAL.getTodaySpeed().ToString("F2");

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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DAL;
using Models;

namespace StudentManager
{
    public partial class FrmScoreManage : Form
    {
        private ScoreListService objSocreService = new ScoreListService();
        public FrmScoreManage()
        {
            InitializeComponent();
            this.cboClass.DataSource = new StudentClassService().GetAllStuClass().Tables[0];
            this.cboClass.DisplayMember = "ClassName";
            this.cboClass.ValueMember = "ClassId";
            this.cboClass.SelectedIndex = -1;


            //添加事件
            this.cboClass.SelectedIndexChanged += new System.EventHandler(this.cboClass_SelectedIndexChanged);
        }
        //根据班级查询      
        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboClass.SelectedIndex == -1)
            {
                MessageBox.Show("请首先选择要查询的班级！", "查询提示");
                return;
            }
            this.gbStat.Text ="["+this.cboClass.Text.Trim()+"]考试成绩统计";
            //展示考试成绩列表
            this.dgvScoreList.AutoGenerateColumns = false;
            this.dgvScoreList.DataSource = objSocreService.GetScoreList(this.cboClass.Text.ToString());
            //查询成绩统计结果
            Dictionary<string, string> dic = objSocreService.GetScoreInfo(this.cboClass.SelectedValue.ToString());
            this.lblAttendCount.Text = dic["stuCount"];
            this.lblCSharpAvg.Text = dic["avgCsharp"];
            
            this.lblCount.Text = dic["absentCount"];
            //显示缺考学员
            List<string> list = objSocreService.getAbsentList(this.cboClass.SelectedValue.ToString());
            this.lblList.Items.Clear();
            this.lblList.Items.AddRange(list.ToArray());
            
        }
        //统计全校考试成绩
        private void btnStat_Click(object sender, EventArgs e)
        {
            this.gbStat.Text = "全校考试成绩";
            //展示考试成绩列表
            this.dgvScoreList.AutoGenerateColumns = false;
            this.dgvScoreList.DataSource = objSocreService.GetScoreList(null);
            //查询成绩统计结果
            Dictionary<string, string> dic = objSocreService.GetScoreInfo(null);
            this.lblAttendCount.Text = dic["stuCount"];
            this.lblCSharpAvg.Text = dic["avgCsharp"];
            
            this.lblCount.Text = dic["absentCount"];
            //显示缺考学员
            List<string> list = objSocreService.getAbsentList(null);
            this.lblList.Items.Clear();
            this.lblList.Items.AddRange(list.ToArray());
        }
        //关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmScoreManage_Load(object sender, EventArgs e)
        {

        }
    }
}
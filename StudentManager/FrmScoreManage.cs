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


            //����¼�
            this.cboClass.SelectedIndexChanged += new System.EventHandler(this.cboClass_SelectedIndexChanged);
        }
        //���ݰ༶��ѯ      
        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboClass.SelectedIndex == -1)
            {
                MessageBox.Show("������ѡ��Ҫ��ѯ�İ༶��", "��ѯ��ʾ");
                return;
            }
            this.gbStat.Text ="["+this.cboClass.Text.Trim()+"]���Գɼ�ͳ��";
            //չʾ���Գɼ��б�
            this.dgvScoreList.AutoGenerateColumns = false;
            this.dgvScoreList.DataSource = objSocreService.GetScoreList(this.cboClass.Text.ToString());
            //��ѯ�ɼ�ͳ�ƽ��
            Dictionary<string, string> dic = objSocreService.GetScoreInfo(this.cboClass.SelectedValue.ToString());
            this.lblAttendCount.Text = dic["stuCount"];
            this.lblCSharpAvg.Text = dic["avgCsharp"];
            
            this.lblCount.Text = dic["absentCount"];
            //��ʾȱ��ѧԱ
            List<string> list = objSocreService.getAbsentList(this.cboClass.SelectedValue.ToString());
            this.lblList.Items.Clear();
            this.lblList.Items.AddRange(list.ToArray());
            
        }
        //ͳ��ȫУ���Գɼ�
        private void btnStat_Click(object sender, EventArgs e)
        {
            this.gbStat.Text = "ȫУ���Գɼ�";
            //չʾ���Գɼ��б�
            this.dgvScoreList.AutoGenerateColumns = false;
            this.dgvScoreList.DataSource = objSocreService.GetScoreList(null);
            //��ѯ�ɼ�ͳ�ƽ��
            Dictionary<string, string> dic = objSocreService.GetScoreInfo(null);
            this.lblAttendCount.Text = dic["stuCount"];
            this.lblCSharpAvg.Text = dic["avgCsharp"];
            
            this.lblCount.Text = dic["absentCount"];
            //��ʾȱ��ѧԱ
            List<string> list = objSocreService.getAbsentList(null);
            this.lblList.Items.Clear();
            this.lblList.Items.AddRange(list.ToArray());
        }
        //�ر�
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmScoreManage_Load(object sender, EventArgs e)
        {

        }
    }
}
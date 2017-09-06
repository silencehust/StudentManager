using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Configuration;

namespace StudentManager
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.lblCurrentUser.Text = Program.CurrentAdmin.AdminName+"]";//��ʾ��¼�û���
            this.panelForm.BackgroundImage = Image.FromFile("main.jpg");//��ʾ�����屳��ͼƬ
            this.lblVersion.Text="�汾�ţ�V"+ ConfigurationManager.AppSettings["sysversion"].ToString();//��ʾ�汾��

        }    

        #region Ƕ�봰����ʾ
        //�򿪴���
        private void OpenForm(Form objForm)
        {
            objForm.TopLevel = false;//����ǰ��������Ϊ�Ƕ����ؼ�
            objForm.WindowState = FormWindowState.Maximized;//���ô������
            objForm.FormBorderStyle = FormBorderStyle.None;//ȥ������߿�
            objForm.Parent = this.panelForm;//ָ����ǰ�Ӵ�����ʾ������
            objForm.Show();
        }

        //�رմ���
        private void CloseForm()
        {
            foreach (Control item in this.panelForm.Controls)
            {
                if(item is Form)
                {
                    Form objControl = (Form)item;//ת��Ϊ���崰�����
                    objControl.Close();
                    this.panelForm.Controls.Remove(item);
                }
            }
        }
     
        //��ʾ�����ѧԱ����       
        private void tsmiAddStudent_Click(object sender, EventArgs e)
        {
            CloseForm();
            FrmAddStudent objForm = new FrmAddStudent();
            this.OpenForm(objForm);
          
        }
        //���ڴ�      
        private void tsmi_Card_Click(object sender, EventArgs e)
        {
            CloseForm();
            FrmAttendance objForm = new FrmAttendance();
            this.OpenForm(objForm);

        }
        //�ɼ����ٲ�ѯ��Ƕ����ʾ��
        private void tsmiQuery_Click(object sender, EventArgs e)
        {
            CloseForm();
            FrmScoreQuery objForm = new FrmScoreQuery();
            this.OpenForm(objForm);

        }
        //ѧԱ����Ƕ����ʾ��
        private void tsmiManageStudent_Click(object sender, EventArgs e)
        {
            CloseForm();
            FrmStudentManage objForm = new FrmStudentManage();
            this.OpenForm(objForm);

        }
        //��ʾ�ɼ���ѯ���������    
        private void tsmiQueryAndAnalysis_Click(object sender, EventArgs e)
        {
            CloseForm();
            FrmScoreManage objForm = new FrmScoreManage();
            this.OpenForm(objForm);
        }
        //���ڲ�ѯ
        private void tsmi_AQuery_Click(object sender, EventArgs e)
        {
            CloseForm();
            FrmAttendanceQuery objForm = new FrmAttendanceQuery();
            this.OpenForm(objForm);
        }
        #endregion

        #region �˳�ϵͳȷ��

        //�˳�ϵͳ
        private void tmiClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("ȷ���˳���", "�˳�ѯ��",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region ����

        //�����޸�
        private void tmiModifyPwd_Click(object sender, EventArgs e)
        {
            FrmModifyPwd objPwd = new FrmModifyPwd();
            objPwd.ShowDialog();
        }

        private void tsbAddStudent_Click(object sender, EventArgs e)
        {
            tsmiAddStudent_Click(null, null);
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tsmiManageStudent_Click(null, null);
        }
        private void tsbScoreAnalysis_Click(object sender, EventArgs e)
        {
            tsmiQueryAndAnalysis_Click(null, null);
        }
        private void tsbModifyPwd_Click(object sender, EventArgs e)
        {
            tmiModifyPwd_Click(null, null);
        }
        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tsbQuery_Click(object sender, EventArgs e)
        {
            tsmiQuery_Click(null, null);
        }   
     
        //���ʹ���
        private void tsmi_linkxkt_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "http://www.xiketang.com");
        }

        private void tsmi_about_Click(object sender, EventArgs e)
        {
           
        }


        #endregion

        private  void panelForm_Paint(object sender, PaintEventArgs e)
        {

        }
        
    }
}
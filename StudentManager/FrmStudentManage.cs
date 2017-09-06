using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using DAL;
using Models;
using Models.Ext;

namespace StudentManager
{
    public partial class FrmStudentManage : Form
    {
        private StudentClassService objClassService = new StudentClassService();
        private StudentService objStudentSerive = new StudentService();
        private List<StudentExt> list = null;
        public FrmStudentManage()
        {
            InitializeComponent();
            //��̬��Ӱ༶������
            this.cboClass.DataSource = objClassService.GetAllClass();
            this.cboClass.DisplayMember = "ClassName";
            this.cboClass.ValueMember = "ClassId";
            this.cboClass.SelectedIndex = -1;
        }
        //���հ༶��ѯ
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.cboClass.SelectedIndex == -1)
            {
                MessageBox.Show("��ѡ��һ���༶", "��ѯ��ʾ");
                this.cboClass.Focus();
                return;
            }
            list = objStudentSerive.GetStudentByClassId(this.cboClass.SelectedValue.ToString());
            this.dgvStudentList.AutoGenerateColumns = false;//��ֹ���ɲ���Ҫ����
            this.dgvStudentList.DataSource = list;
        }
        //����ѧ�Ų�ѯ
        private void btnQueryById_Click(object sender, EventArgs e)
        {
            if (this.txtStudentId.Text.Trim().Length == 0)
            {
                MessageBox.Show("������Ҫ��ѯ��ѧ��", "��ѯ��ʾ");
                this.txtStudentId.Focus();
                return;
            }
            //����ѧ�Ų�ѯѧԱ����
            StudentExt objStudent = objStudentSerive.GetStudentByStuId(this.txtStudentId.Text.Trim());
            if (objStudent == null)
            {
                MessageBox.Show("�������ѧ�Ų���ȷ��δ�ҵ���ѧԱ��Ϣ", "��ѯ��ʾ");
                this.txtStudentId.Focus();
                this.txtStudentId.SelectAll();
            }
            else
            {
                //����ѧԱ��Ϣ��ʾ����
                FrmStudentInfo objStudentForm = new FrmStudentInfo(objStudent);
                objStudentForm.Show();
            }

        }
        private void txtStudentId_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.txtStudentId.Text.Trim().Length == 0)
            {
                return;
            }
            if (e.KeyValue == 13)
            {
                btnQueryById_Click(null,null);
            }
        }
        //˫��ѡ�е�ѧԱ������ʾ��ϸ��Ϣ
        private void dgvStudentList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }
        //�޸�ѧԱ����
        private void btnEidt_Click(object sender, EventArgs e)
        {
            //�ж��Ƿ����޸ĵ���Ϣ
            if(this.dgvStudentList.RowCount==0 || this.dgvStudentList.CurrentRow == null)
            {
                MessageBox.Show("û��Ҫ�޸ĵ���Ϣ", "�޸���ʾ");
                return;
            }
            //��ȡҪ�޸ĵ�ѧԱѧ��
            string studentId = this.dgvStudentList.CurrentRow.Cells["StudentId"].Value.ToString();
            StudentExt objStudent = objStudentSerive.GetStudentByStuId(studentId);
            //��ʾ�޸Ĵ���
            FrmEditStudent objEditForm = new FrmEditStudent(objStudent);
            DialogResult result = objEditForm.ShowDialog();
            //�ж��Ƿ��޸ĳɹ�
            if (result == DialogResult.OK)
            {
                btnQuery_Click(null, null);
            }
        }
        private void tsmiModifyStu_Click(object sender, EventArgs e)
        {
            btnEidt_Click(null, null);
        }
        //ɾ��ѧԱ����
        private void btnDel_Click(object sender, EventArgs e)
        {
            //
            if (this.dgvStudentList.RowCount == 0)
            {
                MessageBox.Show("û��Ҫɾ���Ķ���","ɾ����ʾ");
                return;
            }
            if (this.dgvStudentList.CurrentRow == null)
            {
                MessageBox.Show("��ѡ��Ҫɾ����ѧԱ����","ɾ����ʾ");
                return;
            }
            //ɾ��ȷ��
            DialogResult result = MessageBox.Show("ȷ��Ҫɾ����", "ɾ��ȷ��", MessageBoxButtons.OKCancel,
               MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            //��ȡҪɾ����ѧ��
            string studentId = this.dgvStudentList.CurrentRow.Cells["StudentId"].Value.ToString();
            //����ѧ��ɾ��
            try
            {
                if (objStudentSerive.DeleteStudent(studentId) == 1)
                {
                    MessageBox.Show("ɾ���ɹ�","ɾ����ʾ");
                    btnQuery_Click(null, null);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"ɾ����Ϣ");
            }
        }     
        //��������
        private void btnNameDESC_Click(object sender, EventArgs e)
        {
            if(list==null||this.dgvStudentList.RowCount==0)
            {
                return;
            }
            this.list.Sort(new NameDESC());//����
            this.dgvStudentList.DataSource = null;
            this.dgvStudentList.DataSource = list;
        }
        //ѧ�Ž���
        private void btnStuIdDESC_Click(object sender, EventArgs e)
        {
            if (list == null || this.dgvStudentList.RowCount == 0)
            {
                return;
            }
            this.list.Sort(new StudentDESC());//����
            this.dgvStudentList.DataSource = null;
            this.dgvStudentList.DataSource = list;

        }
        //����к�
        private void dgvStudentList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvStudentList, e);
        }
        //��ӡ��ǰѧԱ��Ϣ
        private void btnPrint_Click(object sender, EventArgs e)
        {
         if(this.dgvStudentList.RowCount==0 || this.dgvStudentList.CurrentRow == null)
            {
                MessageBox.Show("û��Ҫ��ӡ����Ϣ", "��ӡ��ʾ");
                return;
            }
            //��ȡҪ��ӡ��ѧԱ����
            string studentId = this.dgvStudentList.CurrentRow.Cells["StudentId"].Value.ToString();
            StudentExt objStudent = objStudentSerive.GetStudentByStuId(studentId);
            //����Excelģ��ʵ�ִ�ӡ
            ExcelPrint.PrintStudent objPrint = new ExcelPrint.PrintStudent();
            objPrint.ExeutePrint(objStudent);

        }
     
        //�ر�
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmStudentManage_Load(object sender, EventArgs e)
        {

        }

        private void dgvStudentList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
    }
    /// <summary>
    /// ����������������
    /// </summary>
    class NameDESC : IComparer<StudentExt>
    {
        public int Compare(StudentExt x,StudentExt y)
        {
            return y.StudentName.CompareTo(x.StudentName); 
        }
    }
    /// <summary>
    /// ����ѧ�Ž�������
    /// </summary>
    class StudentDESC : IComparer<StudentExt>
    {
        public int Compare(StudentExt x, StudentExt y)
        {
            return y.StudentId.CompareTo(x.StudentId);
        }
    }
}
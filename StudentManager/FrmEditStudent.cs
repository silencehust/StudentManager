using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Models;
using Models.Ext;
using DAL;

namespace StudentManager
{
    public partial class FrmEditStudent : Form
    {
        private StudentClassService objClassService = new StudentClassService();
        private StudentService objStudentService = new StudentService();
        public FrmEditStudent(StudentExt objStudent)
        {
            InitializeComponent();
            //��ʼ���༶������
            this.cboClassName.DataSource = objClassService.GetAllClass();
            this.cboClassName.DisplayMember = "ClassName";
            this.cboClassName.ValueMember = "ClassId";

            this.txtStudentId.Text = objStudent.StudentId.ToString();
            this.txtStudentIdNo.Text = objStudent.StudentIdNo.ToString();
            this.txtStudentName.Text = objStudent.StudentName;
            this.txtPhoneNumber.Text = objStudent.PhoneNumber;
            this.txtAddress.Text = objStudent.StudentAddress;
            this.dtpBirthday.Text = objStudent.Birthday.ToShortDateString();
            this.txtCardNo.Text = objStudent.CardNo;
            if (objStudent.Gender == "��")
            {
                this.rdoMale.Checked = true;
            }
            else
            {
                this.rdoFemale.Checked = true;
            }
            this.cboClassName.Text = objStudent.ClassName;
            this.pbStu.Image = objStudent.StuImage.Length == 0 ? Image.FromFile("default.png") :
                (Image)new SerializeObjectToString().DeserializeObject(objStudent.StuImage);
        }
      
       
        //�ύ�޸�
        private void btnModify_Click(object sender, EventArgs e)
        {
            //������֤
            if (this.txtStudentName.Text.Trim().Length == 0)
            {
                MessageBox.Show("ѧ����������Ϊ��", "��ʾ��Ϣ");
                this.txtStudentName.Focus();
                return;
            }
            if (!this.rdoMale.Checked && !this.rdoFemale.Checked)
            {
                MessageBox.Show("��ѡ���Ա�", "��֤��ʾ");
                return;
            }
            if ((DateTime.Now.Year - Convert.ToDateTime(this.dtpBirthday.Text).Year) < 16)
            {
                MessageBox.Show("ѧ�����䲻��С��16��������ѡ��", "��֤��ʾ");
                return;
            }
            if (this.cboClassName.SelectedIndex == -1)
            {
                MessageBox.Show("��ѡ��༶", "��֤��ʾ");
                return;
            }
            //if (this.txtStudentIdNo.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("���������֤��", "��֤��ʾ");
            //    this.txtStudentIdNo.Focus();
            //    return;
            //}
            //if (this.txtCardNo.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("���Ų���Ϊ��", "��֤��ʾ");
            //    this.txtCardNo.Focus();
            //    return;
            //}
            //��֤���֤�Ÿ�ʽ�Ƿ����Ҫ��
            if (!DataValidate.IsIdentityCard(this.txtStudentIdNo.Text.Trim()))
            {
                MessageBox.Show("���֤��ʽ����ȷ������������", "��֤��ʾ");
                this.txtStudentIdNo.Focus();
                return;
            }
            //��֤���֤���Ƿ��ظ�
            if (objStudentService.IsIdNoExisted(this.txtStudentIdNo.Text.Trim(), this.txtStudentId.Text.Trim()))
            {
                MessageBox.Show("���֤�Ų��ܺ�����ѧԱ���֤�����ظ�","��֤��ʾ");
                this.txtStudentIdNo.Focus();
                this.txtStudentIdNo.SelectAll();
                return;
            }
            //��֤���֤���Ƿ�ͳ����������Ǻ�
            string month = string.Empty;
            string day = string.Empty;
            if (Convert.ToDateTime(this.dtpBirthday.Text).Month < 10)
            {
                month = "0" + Convert.ToDateTime(this.dtpBirthday.Text).Month;
            }
            else
            {
                month = Convert.ToDateTime(this.dtpBirthday.Text).Month.ToString();
            }
            if (Convert.ToDateTime(this.dtpBirthday.Text).Day < 10)
            {
                day = "0" + Convert.ToDateTime(this.dtpBirthday.Text).Day;
            }
            else
            {
                day = Convert.ToDateTime(this.dtpBirthday.Text).Day.ToString();
            }
            string birthday = Convert.ToDateTime(this.dtpBirthday.Text).Year.ToString() + month + day;
            if (!this.txtStudentIdNo.Text.Trim().Contains(birthday))
            {
                MessageBox.Show("���֤�źͳ������ڲ�ƥ��", "��֤��ʾ");
                this.txtStudentIdNo.Focus();
                this.txtStudentIdNo.SelectAll();
                return;
            }
            //// ? �ж����֤���Ƿ��Ѿ�����
            //if (this.objStudentService.IsIDCardExisted(this.txtStudentIdNo.Text.Trim()))
            //{
            //    MessageBox.Show("���֤���Ѵ���", "��֤��ʾ");
            //    this.txtStudentIdNo.Focus();
            //    this.txtStudentIdNo.SelectAll();
            //    return;
            //}
            //�ж�ѧ���Ƿ��Ѿ�����
            //if (this.objStudentService.IsCardNoExisted(this.txtCardNo.Text.Trim()))
            //{
            //    MessageBox.Show("ѧ���Ѵ���", "��֤��ʾ");
            //    this.txtCardNo.Focus();
            //    this.txtCardNo.SelectAll();
            //    return;
            //}

            //��װѧ������
            Student objStudent = new Student()
            {
                StudentName = this.txtStudentName.Text.Trim(),
                Gender = this.rdoMale.Checked ? "��" : "Ů",
                Birthday = Convert.ToDateTime(this.dtpBirthday.Text),
                Age = DateTime.Now.Year - Convert.ToDateTime(this.dtpBirthday.Text).Year,
                ClassId = Convert.ToInt32(this.cboClassName.SelectedValue),
                StudentIdNo = this.txtStudentIdNo.Text.Trim(),
                CardNo = this.txtCardNo.Text.Trim(),
                PhoneNumber = this.txtPhoneNumber.Text.Trim(),
                StudentAddress = this.txtAddress.Text.Trim()==""?"��ַ����":this.txtAddress.Text.Trim(),
                StudentId=Convert.ToInt32(this.txtStudentId.Text.Trim()),
                StuImage = this.pbStu.Image == null ? new SerializeObjectToString().SerializeObject(this.pbStu.Image) :""
            };
            //�ύ����
            try
            {
                if (objStudentService.ModifyStudent(objStudent) == 1)
                {
                    MessageBox.Show("ѧԱ��Ϣ�޸ĳɹ�", "��ʾ��Ϣ");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message) ;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //ѡ����Ƭ
        private void btnChoseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.pbStu.Image = Image.FromFile(fileDialog.FileName);
            }
        }

        private void FrmEditStudent_Load(object sender, EventArgs e)
        {

        }
    }
}
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

    public partial class FrmAddStudent : Form
    {
        private StudentClassService objClassService = new StudentClassService();
        private StudentService objStudentService = new StudentService();
        public FrmAddStudent()
        {
            InitializeComponent();
            //��ʼ���༶������
            this.cboClassName.DataSource = objClassService.GetAllClass();
            this.cboClassName.DisplayMember = "ClassName";
            this.cboClassName.ValueMember = "ClassId";
            this.cboClassName.SelectedIndex = -1;       
        }
        //�����ѧԱ
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //������֤
            if (this.txtStudentName.Text.Trim().Length == 0)
            {
                MessageBox.Show("������ѧ������","��֤��ʾ");
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
                this.dtpBirthday.Focus();
                return;
            }
            if (this.cboClassName.SelectedIndex == -1)
            {
                MessageBox.Show("��ѡ��༶", "��֤��ʾ");
                return;
            }
            if (this.txtStudentIdNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("���������֤��", "��֤��ʾ");
                this.txtStudentIdNo.Focus();
                return;
            }
            if (this.txtCardNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("���Ų���Ϊ��", "��֤��ʾ");
                this.txtCardNo.Focus();
                return;
            }
            //��֤���֤�Ÿ�ʽ�Ƿ����Ҫ��
            if (!DataValidate.IsIdentityCard(this.txtStudentIdNo.Text.Trim()))
            {
                MessageBox.Show("���֤��ʽ����ȷ������������","��֤��ʾ");
                this.txtStudentIdNo.Focus();
                this.txtStudentIdNo.SelectAll();
                return;
            }
            //��֤���֤���Ƿ�ͳ����������Ǻ�
            string month = string.Empty;
            string day = string.Empty;
            if (Convert.ToDateTime(this.dtpBirthday.Text).Month < 10)
            {
                month ="0"+ Convert.ToDateTime(this.dtpBirthday.Text).Month;
            }
            else
            {
                month = Convert.ToDateTime(this.dtpBirthday.Text).Month.ToString();
            }
            if (Convert.ToDateTime(this.dtpBirthday.Text).Day < 10)
            {
                day = "0"+Convert.ToDateTime(this.dtpBirthday.Text).Day;
            }
            else
            {
                day= Convert.ToDateTime(this.dtpBirthday.Text).Day.ToString();
            }
            string birthday= Convert.ToDateTime(this.dtpBirthday.Text).Year.ToString()+month+day;

            if (!this.txtStudentIdNo.Text.Trim().Contains(birthday))
            {
                MessageBox.Show("���֤�źͳ������ڲ�ƥ��", "��֤��ʾ");
                this.txtStudentIdNo.Focus();
                this.txtStudentIdNo.SelectAll();
                return;
            }
            //�ж����֤���Ƿ��Ѿ�����
            if (this.objStudentService.IsIDCardExisted(this.txtStudentIdNo.Text.Trim()))
            {
                MessageBox.Show("���֤���Ѵ���","��֤��ʾ");
                this.txtStudentIdNo.Focus();
                this.txtStudentIdNo.SelectAll();
                return;
            }
            //�ж�ѧ���Ƿ��Ѿ�����
            if (this.objStudentService.IsCardNoExisted(this.txtCardNo.Text.Trim()))
            {
                MessageBox.Show("ѧ���Ѵ���", "��֤��ʾ");
                this.txtCardNo.Focus();
                this.txtCardNo.SelectAll();
                return;
            }
            //��װѧԱ����
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
                StudentAddress = this.txtAddress.Text.Trim(),
                StuImage=this.pbStu.Image==null?"":new SerializeObjectToString().SerializeObject(this.pbStu.Image)
            };
            //�ύ����
            try
            {
                int result = objStudentService.AddStudent(objStudent);
                if (result == 1)
                {
                    DialogResult dresult = MessageBox.Show("��ӳɹ����Ƿ�������", "���ѯ��", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question);
                    if (dresult == DialogResult.OK)
                    {
                        //��յ�ǰ�ı���
                        foreach (Control item in gbStuInfo.Controls)
                        {
                            if(item is TextBox)
                            {
                                item.Text ="";
                            }
                            else if(item is RadioButton)
                            {
                                ((RadioButton)item).Checked = false;
                            }
                            else if(item is ComboBox)
                            {
                                ((ComboBox)item).SelectedIndex = -1;
                            }
                        }
                        this.pbStu.Image = null;
                        this.txtStudentName.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("���ʧ��", "�����ʾ");
                }
            }
            catch (Exception ex )
            {

                MessageBox.Show(ex.Message, "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
            }
            //�ж��Ƿ񱣴�ɹ�
        }
        //�رմ���
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }      
        //ѡ������Ƭ
        private void btnChoseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult result=fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.pbStu.Image = Image.FromFile(fileDialog.FileName);
            }
        }

        private void FrmAddStudent_Load(object sender, EventArgs e)
        {

        }
    }
}
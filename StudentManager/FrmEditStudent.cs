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
            //初始化班级下拉框
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
            if (objStudent.Gender == "男")
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
      
       
        //提交修改
        private void btnModify_Click(object sender, EventArgs e)
        {
            //数据验证
            if (this.txtStudentName.Text.Trim().Length == 0)
            {
                MessageBox.Show("学生姓名不能为空", "提示信息");
                this.txtStudentName.Focus();
                return;
            }
            if (!this.rdoMale.Checked && !this.rdoFemale.Checked)
            {
                MessageBox.Show("请选择性别", "验证提示");
                return;
            }
            if ((DateTime.Now.Year - Convert.ToDateTime(this.dtpBirthday.Text).Year) < 16)
            {
                MessageBox.Show("学生年龄不得小于16，请重新选择", "验证提示");
                return;
            }
            if (this.cboClassName.SelectedIndex == -1)
            {
                MessageBox.Show("请选择班级", "验证提示");
                return;
            }
            //if (this.txtStudentIdNo.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("请输入身份证号", "验证提示");
            //    this.txtStudentIdNo.Focus();
            //    return;
            //}
            //if (this.txtCardNo.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("卡号不得为空", "验证提示");
            //    this.txtCardNo.Focus();
            //    return;
            //}
            //验证身份证号格式是否符合要求
            if (!DataValidate.IsIdentityCard(this.txtStudentIdNo.Text.Trim()))
            {
                MessageBox.Show("身份证格式不正确，请重新输入", "验证提示");
                this.txtStudentIdNo.Focus();
                return;
            }
            //验证身份证号是否重复
            if (objStudentService.IsIdNoExisted(this.txtStudentIdNo.Text.Trim(), this.txtStudentId.Text.Trim()))
            {
                MessageBox.Show("身份证号不能和现有学员身份证号相重复","验证提示");
                this.txtStudentIdNo.Focus();
                this.txtStudentIdNo.SelectAll();
                return;
            }
            //验证身份证号是否和出生日期相吻合
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
                MessageBox.Show("身份证号和出生日期不匹配", "验证提示");
                this.txtStudentIdNo.Focus();
                this.txtStudentIdNo.SelectAll();
                return;
            }
            //// ? 判断身份证号是否已经存在
            //if (this.objStudentService.IsIDCardExisted(this.txtStudentIdNo.Text.Trim()))
            //{
            //    MessageBox.Show("身份证号已存在", "验证提示");
            //    this.txtStudentIdNo.Focus();
            //    this.txtStudentIdNo.SelectAll();
            //    return;
            //}
            //判断学号是否已经存在
            //if (this.objStudentService.IsCardNoExisted(this.txtCardNo.Text.Trim()))
            //{
            //    MessageBox.Show("学号已存在", "验证提示");
            //    this.txtCardNo.Focus();
            //    this.txtCardNo.SelectAll();
            //    return;
            //}

            //封装学生对象
            Student objStudent = new Student()
            {
                StudentName = this.txtStudentName.Text.Trim(),
                Gender = this.rdoMale.Checked ? "男" : "女",
                Birthday = Convert.ToDateTime(this.dtpBirthday.Text),
                Age = DateTime.Now.Year - Convert.ToDateTime(this.dtpBirthday.Text).Year,
                ClassId = Convert.ToInt32(this.cboClassName.SelectedValue),
                StudentIdNo = this.txtStudentIdNo.Text.Trim(),
                CardNo = this.txtCardNo.Text.Trim(),
                PhoneNumber = this.txtPhoneNumber.Text.Trim(),
                StudentAddress = this.txtAddress.Text.Trim()==""?"地址不详":this.txtAddress.Text.Trim(),
                StudentId=Convert.ToInt32(this.txtStudentId.Text.Trim()),
                StuImage = this.pbStu.Image == null ? new SerializeObjectToString().SerializeObject(this.pbStu.Image) :""
            };
            //提交对象
            try
            {
                if (objStudentService.ModifyStudent(objStudent) == 1)
                {
                    MessageBox.Show("学员信息修改成功", "提示信息");
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
        //选择照片
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
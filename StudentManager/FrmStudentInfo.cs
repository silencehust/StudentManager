using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Models.Ext;


namespace StudentManager
{
    public partial class FrmStudentInfo : Form
    {
        public FrmStudentInfo()
        {
            InitializeComponent();

        }
        /// <summary>
        /// ��ʾѧԱ��Ϣ
        /// </summary>
        /// <param name="objStudent"></param>
        public FrmStudentInfo(StudentExt objStudent)
        {
            InitializeComponent();
            this.lblStudentIdNo.Text = objStudent.StudentId.ToString();
            this.lblStudentName.Text = objStudent.StudentName;
            this.lblPhoneNumber.Text = objStudent.PhoneNumber;
            this.lblAddress.Text = objStudent.StudentAddress;
            this.lblBirthday.Text = objStudent.Birthday.ToShortDateString();
            this.lblCardNo.Text = objStudent.CardNo;
            this.lblClass.Text = objStudent.ClassName;
            this.lblGender.Text = objStudent.Gender;
            this.pbStu.Image = objStudent.StuImage.Length == 0 ? Image.FromFile("default.png"):
                (Image)new SerializeObjectToString().DeserializeObject(objStudent.StuImage);
            
        }

        //�ر�
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmStudentInfo_Load(object sender, EventArgs e)
        {

        }

        private void pbStu_Click(object sender, EventArgs e)
        {

        }
    }
}
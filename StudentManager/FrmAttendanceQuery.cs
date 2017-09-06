using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using Models;

namespace StudentManager
{
    public partial class FrmAttendanceQuery : Form
    {

        private AttendanceService objAttService = new AttendanceService();
        public FrmAttendanceQuery()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            //查询考勤结果
            DateTime dt1 = Convert.ToDateTime(this.dtpTime.Text);
            DateTime dt2 = dt1.AddDays(1.0);
            this.dgvStudentList.AutoGenerateColumns = false;
            this.dgvStudentList.DataSource = objAttService.GetStudentByDate(dt1, dt2, this.txtName.Text.Trim());
            
            //考勤学员总数
            this.lblCount.Text = objAttService.GetAllStudent().ToString();
            this.lblReal.Text = objAttService.GetAttendStudents(Convert.ToDateTime(this.dtpTime.Text), false).ToString();
            this.lblAbsenceCount.Text = (Convert.ToInt32(this.lblCount.Text) - Convert.ToInt32(this.lblReal.Text)).ToString();
        }
        //添加行号
        private void dgvStudentList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvStudentList, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

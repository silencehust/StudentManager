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
           
        }    

        #region 嵌入窗体显示

     
        //显示添加新学员窗体       
        private void tsmiAddStudent_Click(object sender, EventArgs e)
        {          
            FrmAddStudent objForm = new FrmAddStudent();
          
        }
        //考勤打卡      
        private void tsmi_Card_Click(object sender, EventArgs e)
        {
            //FrmAttendance objForm = new FrmAttendance();
            
        }
        //成绩快速查询【嵌入显示】
        private void tsmiQuery_Click(object sender, EventArgs e)
        {
          //  FrmScoreQuery objForm = new FrmScoreQuery();
            
        }
        //学员管理【嵌入显示】
        private void tsmiManageStudent_Click(object sender, EventArgs e)
        {
           // FrmStudentManage objForm = new FrmStudentManage();
           
        }
        //显示成绩查询与分析窗口    
        private void tsmiQueryAndAnalysis_Click(object sender, EventArgs e)
        {
            FrmScoreManage objForm = new FrmScoreManage();
            //
        }
        //考勤查询
        private void tsmi_AQuery_Click(object sender, EventArgs e)
        {
            FrmAttendanceQuery objForm = new FrmAttendanceQuery();
          //
        }
        #endregion

        #region 退出系统确认

        //退出系统
        private void tmiClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }      

        #endregion

        #region 其他

        //密码修改
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
     
        //访问官网
        private void tsmi_linkxkt_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "http://www.xiketang.com");
        }

        private void tsmi_about_Click(object sender, EventArgs e)
        {
           
        }

        #endregion 

    
    }
}
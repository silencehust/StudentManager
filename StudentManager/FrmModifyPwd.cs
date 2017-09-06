using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;

namespace StudentManager
{
    public partial class FrmModifyPwd : Form
    {
        private AdminService objAdminService = new AdminService();
        public FrmModifyPwd()
        {
            InitializeComponent();
        }
        //修改密码
        private void btnModify_Click(object sender, EventArgs e)
        {
            //校验输入的旧密码是否正确
            if (this.txtOldPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入原密码", "修改提示");
                this.txtOldPwd.Focus();
                return;
            }
            //判断原密码是否正确
            if (this.txtOldPwd.Text.Trim() != Program.CurrentAdmin.LoginPwd)
            {
                MessageBox.Show("原密码不正确", "修改提示");
                this.txtOldPwd.Focus();
                this.txtOldPwd.SelectAll();
                return; 
            }
            //判断是否已输入密码
            if (this.txtNewPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入新密码", "修改提示");
                this.txtNewPwd.Focus();
                return;
            }
            //验证密码长度
            if (this.txtNewPwd.Text.Trim().Length < 6 || this.txtNewPwd.Text.Trim().Length > 18)
            {
                MessageBox.Show("密码长度必须为6-18位之间","修改提示");
                this.txtOldPwd.Focus();
                this.txtOldPwd.SelectAll();
                return;
            }
            //判断两次输入的密码是否一致
            if (this.txtNewPwd.Text.Trim() != this.txtNewPwdConfirm.Text.Trim())
            {
                MessageBox.Show("两次输入的密码不一致，请重新输入","修改提示");
                return;
            }
            //将新密码提交到数据库
            int result = objAdminService.ModifyPwd(Program.CurrentAdmin.LoginId.ToString(),
                                                    this.txtNewPwd.Text.Trim());
            if (result == 1)
            {
                MessageBox.Show("修改成功","修改提示");
                Program.CurrentAdmin.LoginPwd = this.txtNewPwd.Text.Trim();
                this.Close();
            }

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Models;
using DAL;



namespace StudentManager
{
    public partial class FrmUserLogin : Form
    {
        //创建数据访问类对象
        private AdminService objAdminService = new AdminService();
        public FrmUserLogin()
        {
            InitializeComponent();
        }


        //登录
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //数据验证
            if(this.txtLoginId.Text.Trim().Length==0)
            {
                MessageBox.Show("请输入登录账号！", "登录提示");
                this.txtLoginId.Focus();
                return;
            }

            if (!DataValidate.IsInteger(this.txtLoginId.Text.Trim()))
            {
                MessageBox.Show("登录账号必须是正整数！", "登录提示");
                this.txtLoginId.Focus();
                this.txtLoginId.SelectAll();
                return;
            }

            if (this.txtLoginPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入登陆密码！", "登录提示");
                this.txtLoginPwd.Focus();
                return;
            }

            //封装用户信息到用户对象中
            Admin objAdmin = new Admin()
            {
                LoginId = Convert.ToInt32(this.txtLoginId.Text.Trim()),
                LoginPwd = this.txtLoginPwd.Text.Trim()
            };
            //提交用户信息
            
            try
            {
                objAdmin = objAdminService.AdminLogin(objAdmin);
                if (objAdmin == null)
                {
                    MessageBox.Show("用户名或密码错误！", "登录提示");
                }
                else
                {
                    Program.CurrentAdmin = objAdmin; //保存用户对象
                    this.DialogResult = DialogResult.OK;//设置登录成功信息提示
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"登录失败！");
            }
        }
          
        //关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //通过回车键使光标跳转
        private void txtLoginId_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.txtLoginId.Text.Trim().Length == 0)
                return;
            if (e.KeyValue == 13)
            {
                this.txtLoginPwd.Focus();
            }
        }

        private void txtLoginPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.txtLoginPwd.Text.Trim().Length == 0)
                return;
            if (e.KeyValue == 13)
            {
                btnLogin_Click(null, null);
            }
        }
    }
}

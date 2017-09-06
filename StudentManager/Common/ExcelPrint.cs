using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;

using Models;
using Models.Ext;

namespace ExcelPrint
{
    /// <summary>
    /// 在Excel中打印学生信息
    /// </summary>
    class PrintStudent
    {
        public void ExeutePrint(StudentExt objStudent)
        {
            //定义一个Excel工作簿
            Application excelApp = new Application();
            //获取已创建好的工作簿路径
            string excelBookPath = Environment.CurrentDirectory + "\\StudentInfo.xls";
            //将现有的工作簿加入已定义的工作簿集合
            excelApp.Workbooks.Add(excelBookPath);
            //获取第一个工作表
            Worksheet objSheet = (Worksheet)excelApp.Worksheets[1];
            //在当前的Excel中写入数据
            if (objStudent.StuImage.Length != 0)
            {
                //将图片保存在指定的位置
                Image objImage = (Image)new 
                    StudentManager.SerializeObjectToString().DeserializeObject(objStudent.StuImage);

                if (File.Exists(Environment.CurrentDirectory + "\\Studnent.jpg"))
                {
                    File.Delete(Environment.CurrentDirectory + "\\Student.jpg");
                }
                else
                {
                    //保存图片到系统目录（当前会保存在Debug或者Release文件夹啊中
                    objImage.Save(Environment.CurrentDirectory + "\\Student.jpg");
                    //将图片插入Excel
                    objSheet.Shapes.AddPicture(Environment.CurrentDirectory + "\\Student.jpg",
                        MsoTriState.msoFalse, MsoTriState.msoTrue, 10, 50, 70, 80);
                    //使用完毕后删除保存的图片
                    File.Delete(Environment.CurrentDirectory + "\\Student.jpg");
                }

            }
            //写入其他相关数据
            objSheet.Cells[4, 4] = objStudent.StudentId;//学号
            objSheet.Cells[4, 6] = objStudent.StudentName;//姓名
            objSheet.Cells[4, 8] = objStudent.Gender;//性别
            objSheet.Cells[6, 4] = objStudent.ClassName;//所在班级
            objSheet.Cells[6, 6] = objStudent.PhoneNumber;//联系电话
            objSheet.Cells[8, 4] = objStudent.StudentAddress;//家庭地址

            //打印预览
            excelApp.Visible = true;
            excelApp.Sheets.PrintPreview(true);
            //释放对象
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            excelApp = null;
        }
    }
}

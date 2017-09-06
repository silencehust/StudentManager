using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DAL
{
    /// <summary>
    /// 班级数据访问类
    /// </summary>
    public class StudentClassService
    {
        /// <summary>
        /// 获取所有班级对象
        /// </summary>
        /// <returns></returns>
        public List<StudentClass> GetAllClass()
        {
            string sql ="select ClassId,ClassName from StudentClass";
            SqlDataReader objReader = SqlHelper.GetReader(sql);
            List<StudentClass> list = new List<StudentClass>();
            while (objReader.Read())
            {
                list.Add(new StudentClass()
                {
                    ClassId = Convert.ToInt32(objReader["ClassId"]),
                    ClassName = objReader["ClassName"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        public DataSet GetAllStuClass()
        {
            string sql = "select ClassId,ClassName from StudentClass";
            return SqlHelper.GetDateSet(sql);
        }
    }
}

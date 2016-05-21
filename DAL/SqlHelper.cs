using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration; 

namespace DAL
{
    /// <summary>
    /// 通用数据访问类
    /// </summary>
    
    public class SqlHelper
    {
        private static string connstring = ConfigurationManager.ConnectionStrings["connString"].ToString();
        /// <summary>
        /// 实行增删改操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Update(string sql)
        {
            SqlConnection coon = new SqlConnection(connstring);
            SqlCommand cmd = new SqlCommand(sql, coon);
            try
            {
                coon.Open();
                int result = cmd.ExecuteNonQuery();  
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                coon.Close();
            }
        }
        /// <summary>
        /// 获取单一结果查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sql)
        {
            SqlConnection coon = new SqlConnection(connstring);
            SqlCommand cmd = new SqlCommand(sql, coon);
            try
            {
                coon.Open();
                object result = cmd.ExecuteScalar();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                coon.Close();
            }
        }
        /// <summary>
        /// 返回一个结果集的查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
        {
            SqlConnection coon = new SqlConnection(connstring);
            SqlCommand cmd = new SqlCommand(sql, coon);
            try
            {
                coon.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                coon.Close();
                throw;
            }
        }
    }
}

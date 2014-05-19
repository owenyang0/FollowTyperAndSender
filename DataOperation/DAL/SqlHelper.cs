using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace DataOperation.DAL
{
    class SqlHelper
    {
        //从App.Config读取数据库连接字符串
        private static readonly string connStr =
            ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        //使用长度可变参数
        public static int ExecuteNonQuery(string sql, params OleDbParameter[] parameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        //执行查询，返回结果集中的第一行第一列
        public static object ExecuteScalar(string sql, params OleDbParameter[] parameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        public static DataTable ExecuteDataTable(string sql, params OleDbParameter[] parameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    return dataSet.Tables[0];
                }
            }
        }

        #region 数据库DBNull与.NET的null转化
        public static object FromDBValue(object value)
        {
            if (value == DBNull.Value)
                return null;
            else
                return value;
        }
        public static object ToDbValue(object value)
        {
            if (value == null)
                return DBNull.Value;
            else
                return value;
        }

        #endregion
    }
}
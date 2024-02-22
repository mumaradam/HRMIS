using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.IO;

/// <summary>
/// Summary description for Module
/// </summary>
///
namespace HRMIS
{
    public static class Module
    {
        static OleDbConnection conn = new OleDbConnection();
        static OleDbCommand sqlComm = new OleDbCommand();
        static OleDbDataAdapter sqlDA = new OleDbDataAdapter();
        public static Boolean findWhereuser = false;
        public static Boolean flag = false;
        public static readonly object lockObject = new object();
        public static DataTable GetData(string strQuery)
        {
            DataTable dt = new DataTable();
            conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            string sql = strQuery;
            conn.Open();
            if (sql != "")
            {
                using (sqlComm = new OleDbCommand(sql, conn))
                {
                    sqlComm.Connection = conn;
                    using (sqlDA = new OleDbDataAdapter(sqlComm))
                    {
                        sqlDA.Fill(dt);
                    }
                }
            }
            conn.Close();
            return dt;
        }
        public static void gblInsert(string strSql)
        {
            conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            string sql = strSql;
            conn.Open();
            try
            {
                if (sql != "")
                {
                    lock (lockObject)
                    {
                        using (sqlComm = new OleDbCommand(sql, conn))
                        {

                            sqlComm.CommandText = strSql;
                            sqlComm.Connection = conn;
                            sqlComm.ExecuteNonQuery();
                            sqlComm.CommandTimeout = 0;
                            sqlComm = null;
                        }
                    }
                    
                }
                //conn.Close();

            }
            catch (System.Net.WebException ex)
            {
              HttpContext.Current.Response.Write(@"<script> alert('" + ex.Message + "') </script>");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        public static string GetField(string strQuery)
        {
            string sql = strQuery;
            string retAns = "";
            if (sql != "")
            {
                conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
                conn.Open();
                sqlComm = new OleDbCommand(sql, conn);
                sqlComm.CommandTimeout = 0;
                var firstColumn = sqlComm.ExecuteScalar();

                if (firstColumn != null)
                {
                    retAns = firstColumn.ToString();
                }
                conn.Close();
            }
            return retAns;

        }
        public static string GetCount(string strQuery)
        {
            string sql = strQuery;
            string retAns = "";
            if (sql != "")
            {
                conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
                conn.Open();
                sqlComm = new OleDbCommand(sql, conn);
                sqlComm.CommandTimeout = 0;
                retAns = sqlComm.ExecuteScalar().ToString();
                conn.Close();
            }
            return retAns;

        }
    }
}

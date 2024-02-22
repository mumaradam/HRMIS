using System;
using System.Collections;
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
using System.Text;
using System.Runtime.InteropServices;

namespace HRMIS
{
    public partial class Biomtrc : System.Web.UI.Page
    {
        private static DataTable dtQuery;
        private static string getEmpUser = "";
        private static string dtGetSENPIStr = "";
        private static string dtGetSGAStr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            getEmpUser = Session["Uname"] as string;
            getSENPILogs();
            getSGALogs();
        }
        protected void lblbtnDash_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dash");
        }
        protected void lblbtnDash1_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        protected void getSENPILogs()
        {
            try
            {
                string getUsrID = HRMIS.Module.GetField("Select TOP 1 Userid from Att2000.dbo.userinfo where SSN = '" + getEmpUser + "'");

                dtGetSENPIStr = "Select a.userid, b.BADGENUMBER, b.name, (Case when checktype = 'I' then 'In' ELSE 'Out' END) as inOUT," +
                                "convert(varchar(8), checktime, 114) as chkTime, convert(varchar, checktime, 20) as chkDate, " +
                                "DATENAME(MONTH, checktime) as Month, Day(checktime) as Day, Year(checktime) as Year " +
                                "from Att2000.dbo.CHECKINOUT a LEFT JOIN Att2000.dbo.USERINFO b On a.USERID = b.USERID where a.userID = '" + getUsrID + "' order by chkDate";
                
                dtQuery = HRMIS.Module.GetData(dtGetSENPIStr);
                if (dtQuery.Rows.Count > 0)
                {
                    int count = checked(dtQuery.Rows.Count - 1);
                    string sStatementFirst = "<table id='cell-select' class='table table-striped table-bordered break'><thead>" +
                                             "<tr><th>Date</th><th>Name</th><th>IN/OUT</th><th>Time</th><th>Month</th>" +
                                             "<th>Day</th><th>Year</th></tr></thead><tbody>";
                    Panel1.Controls.Add(new LiteralControl(sStatementFirst));
                    for (int x = 0; x <= count; x = checked(x + 1))
                    {
                        string Lstat = "";
                        if (dtQuery.Rows[x]["inOUT"].ToString().ToUpper() == "IN") { Lstat = "palegreen"; }
                        else if (dtQuery.Rows[x]["inOUT"].ToString().ToUpper() == "OUT") { Lstat = "lightcoral"; }
                        string sStatement1 = "<tr><td>" + dtQuery.Rows[x]["chkDate"].ToString() + "</td>" +
                                                 "<td>" + dtQuery.Rows[x]["name"].ToString() + "</td>" +
                                                 "<td style='background-color:" + Lstat + ";'>" + dtQuery.Rows[x]["inOUT"].ToString() + "</td>" +
                                                 "<td>" + dtQuery.Rows[x]["chkTime"].ToString() + "</td>" +
                                                 "<td>" + dtQuery.Rows[x]["Month"].ToString() + "</td>" +
                                                 "<td>" + dtQuery.Rows[x]["Day"].ToString() + "</td>" +
                                                 "<td>" + dtQuery.Rows[x]["Year"].ToString() + "</td></tr>";
                        Panel1.Controls.Add(new LiteralControl(sStatement1));
                    }
                    Panel1.Controls.Add(new LiteralControl("</tbody></table>"));
                }
            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void getSGALogs()
        {
            try
            {
                string ifFound = HRMIS.Module.GetField("Select USERID from SGA_Att200.dbo.USERINFO where SSN = '" + getEmpUser + "'");
                if(ifFound != "")
                {
                    //int countf = int.Parse(HRMIS.Module.GetCount("Select Count(*) from SGA_Att200.dbo.USERINFO where SSN = '" + getEmpUser + "'"));
                    dtGetSGAStr = "Select a.userid, b.BADGENUMBER, b.name, (Case when checktype = 'I' then 'In' ELSE 'Out' END) as inOUT," +
                                       "convert(varchar(8), checktime, 114) as chkTime, convert(varchar, checktime, 20) as chkDate, " +
                                       "DATENAME(MONTH, checktime) as Month, Day(checktime) as Day, Year(checktime) as Year " +
                                       "from SGA_Att200.dbo.CHECKINOUT a LEFT JOIN SGA_Att200.dbo.USERINFO b On a.USERID = b.USERID where b.SSN = '" + getEmpUser + "' order by chkDate";
                    dtQuery = HRMIS.Module.GetData(dtGetSGAStr);
                    if (dtQuery.Rows.Count > 0)
                    {
                        int count = checked(dtQuery.Rows.Count - 1);
                        string sStatementFirst = "<table id='cus-select' class='table table-striped table-bordered break'><thead>" +
                                                 "<tr><th>Date</th><th>Name</th><th>IN/OUT</th><th>Time</th><th>Month</th>" +
                                                 "<th>Day</th><th>Year</th></tr></thead><tbody>";
                        Panel2.Controls.Add(new LiteralControl(sStatementFirst));
                        for (int x = 0; x <= count; x = checked(x + 1))
                        {
                            string Lstat = "";
                            if (dtQuery.Rows[x]["inOUT"].ToString().ToUpper() == "IN") { Lstat = "palegreen"; }
                            else if (dtQuery.Rows[x]["inOUT"].ToString().ToUpper() == "OUT") { Lstat = "lightcoral"; }
                            string sStatement1 = "<tr><td>" + dtQuery.Rows[x]["chkDate"].ToString() + "</td>" +
                                                     "<td>" + dtQuery.Rows[x]["name"].ToString() + "</td>" +
                                                     "<td style='background-color:" + Lstat + ";'>" + dtQuery.Rows[x]["inOUT"].ToString() + "</td>" +
                                                     "<td>" + dtQuery.Rows[x]["chkTime"].ToString() + "</td>" +
                                                     "<td>" + dtQuery.Rows[x]["Month"].ToString() + "</td>" +
                                                     "<td>" + dtQuery.Rows[x]["Day"].ToString() + "</td>" +
                                                     "<td>" + dtQuery.Rows[x]["Year"].ToString() + "</td></tr>";
                            Panel2.Controls.Add(new LiteralControl(sStatement1));
                        }
                        Panel2.Controls.Add(new LiteralControl("</tbody></table>"));
                    }
                }
                else
                {
                    string getLastName = HRMIS.Module.GetField("Select TOP 1 EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = '" + getEmpUser + "'");
                    int cntLastName = int.Parse(HRMIS.Module.GetField("Select count(*) from SGA_Att200.dbo.USERINFO where NAME like '" + getLastName + "%'"));
                    if (cntLastName > 1)
                    {
                        dtGetSGAStr = "Select a.userid, b.BADGENUMBER, b.name, (Case when checktype = 'I' then 'In' ELSE 'Out' END) as inOUT," +
                                      "convert(varchar(8), checktime, 114) as chkTime, convert(varchar, checktime, 20) as chkDate, " +
                                      "DATENAME(MONTH, checktime) as Month, Day(checktime) as Day, Year(checktime) as Year " +
                                      "from SGA_Att200.dbo.CHECKINOUT a LEFT JOIN SGA_Att200.dbo.USERINFO b On a.USERID = b.USERID where b.SSN = '" + getEmpUser + "' order by chkDate";
                        dtQuery = HRMIS.Module.GetData(dtGetSGAStr);
                        if (dtQuery.Rows.Count > 0)
                        {
                            int count = checked(dtQuery.Rows.Count - 1);
                            string sStatementFirst = "<table id='cus-select' class='table table-striped table-bordered break'><thead>" +
                                                     "<tr><th>Date</th><th>Name</th><th>IN/OUT</th><th>Time</th><th>Month</th>" +
                                                     "<th>Day</th><th>Year</th></tr></thead><tbody>";
                            Panel2.Controls.Add(new LiteralControl(sStatementFirst));
                            for (int x = 0; x <= count; x = checked(x + 1))
                            {
                                string Lstat = "";
                                if (dtQuery.Rows[x]["inOUT"].ToString().ToUpper() == "IN") { Lstat = "palegreen"; }
                                else if (dtQuery.Rows[x]["inOUT"].ToString().ToUpper() == "OUT") { Lstat = "lightcoral"; }
                                string sStatement1 = "<tr><td>" + dtQuery.Rows[x]["chkDate"].ToString() + "</td>" +
                                                         "<td>" + dtQuery.Rows[x]["name"].ToString() + "</td>" +
                                                         "<td style='background-color:" + Lstat + ";'>" + dtQuery.Rows[x]["inOUT"].ToString() + "</td>" +
                                                         "<td>" + dtQuery.Rows[x]["chkTime"].ToString() + "</td>" +
                                                         "<td>" + dtQuery.Rows[x]["Month"].ToString() + "</td>" +
                                                         "<td>" + dtQuery.Rows[x]["Day"].ToString() + "</td>" +
                                                         "<td>" + dtQuery.Rows[x]["Year"].ToString() + "</td></tr>";
                                Panel2.Controls.Add(new LiteralControl(sStatement1));
                            }
                            Panel2.Controls.Add(new LiteralControl("</tbody></table>"));
                        }
                    }
                    else if (cntLastName == 1)
                    {

                        string getFullName = HRMIS.Module.GetField("Select TOP 1 (EmpLName + ', ' + EmpFName) from seihaHRMIS.dbo.HREmpInfo where empno = '" + getEmpUser + "'");

                        string findName = HRMIS.Module.GetField("Select TOP 1 Name from SGA_Att200.dbo.USERINFO where Name like '%" + getFullName + "%'");
                        if (findName == "")
                        {
                            dtGetSGAStr = "Select a.userid, b.BADGENUMBER, b.name, (Case when checktype = 'I' then 'In' ELSE 'Out' END) as inOUT," +
                                      "convert(varchar(8), checktime, 114) as chkTime, convert(varchar, checktime, 20) as chkDate, " +
                                      "DATENAME(MONTH, checktime) as Month, Day(checktime) as Day, Year(checktime) as Year " +
                                      "from SGA_Att200.dbo.CHECKINOUT a LEFT JOIN SGA_Att200.dbo.USERINFO b On a.USERID = b.USERID where b.SSN = '" + getEmpUser + "' order by chkDate";
                        }
                        else
                        {
                            dtGetSGAStr = "Select a.userid, b.BADGENUMBER, b.name, (Case when checktype = 'I' then 'In' ELSE 'Out' END) as inOUT," +
                                      "convert(varchar(8), checktime, 114) as chkTime, convert(varchar, checktime, 20) as chkDate, " +
                                      "DATENAME(MONTH, checktime) as Month, Day(checktime) as Day, Year(checktime) as Year " +
                                      "from SGA_Att200.dbo.CHECKINOUT a LEFT JOIN SGA_Att200.dbo.USERINFO b On a.USERID = b.USERID where b.Name like '%" + getFullName + "%' order by chkDate";
                        }
                        dtQuery = HRMIS.Module.GetData(dtGetSGAStr);
                        if (dtQuery.Rows.Count > 0)
                        {
                            int count = checked(dtQuery.Rows.Count - 1);
                            string sStatementFirst = "<table id='cus-select' class='table table-striped table-bordered break'><thead>" +
                                                     "<tr><th>Date</th><th>Name</th><th>IN/OUT</th><th>Time</th><th>Month</th>" +
                                                     "<th>Day</th><th>Year</th></tr></thead><tbody>";
                            Panel2.Controls.Add(new LiteralControl(sStatementFirst));
                            for (int x = 0; x <= count; x = checked(x + 1))
                            {
                                string Lstat = "";
                                if (dtQuery.Rows[x]["inOUT"].ToString().ToUpper() == "IN") { Lstat = "palegreen"; }
                                else if (dtQuery.Rows[x]["inOUT"].ToString().ToUpper() == "OUT") { Lstat = "lightcoral"; }
                                string sStatement1 = "<tr><td>" + dtQuery.Rows[x]["chkDate"].ToString() + "</td>" +
                                                         "<td>" + dtQuery.Rows[x]["name"].ToString() + "</td>" +
                                                         "<td style='background-color:" + Lstat + ";'>" + dtQuery.Rows[x]["inOUT"].ToString() + "</td>" +
                                                         "<td>" + dtQuery.Rows[x]["chkTime"].ToString() + "</td>" +
                                                         "<td>" + dtQuery.Rows[x]["Month"].ToString() + "</td>" +
                                                         "<td>" + dtQuery.Rows[x]["Day"].ToString() + "</td>" +
                                                         "<td>" + dtQuery.Rows[x]["Year"].ToString() + "</td></tr>";
                                Panel2.Controls.Add(new LiteralControl(sStatement1));
                            }
                            Panel2.Controls.Add(new LiteralControl("</tbody></table>"));
                        }
                    }
                }
                
                
            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}

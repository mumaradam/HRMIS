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
    public partial class EmployeeBiomtrc : System.Web.UI.Page
    {
        private static DataTable dtQuery;
        private static string getEmpUser = "";
        private static string dtGetSENPIStr = "";
        private static string dtGetSGAStr = "";
        private static string stTable = "excel-bg"; 
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dteFrm = DateTime.Now;
            DateTime dteTo = DateTime.Now;
            getEmpUser = Session["Uname"] as string;
            if (!Page.IsPostBack)
            {
                getSENPILogs(dteFrm, dteTo);
                getSGALogs(dteFrm, dteTo);
            }
            else
            {
                if(Module.flag != false)
                {
                    btnCalculate_Click(sender, e);
                }
                //getSENPILogs();
                //getSGALogs();
            }
            
        }
        protected void lblbtnDash_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dash");
        }
        protected void lblbtnDash1_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fromDate, toDate;
                DateTime TFromSENPI = Convert.ToDateTime(dateFrom.Text);
                DateTime TToSENPI = Convert.ToDateTime(dateTo.Text);
                DateTime TFromSGA = Convert.ToDateTime(dateFromSGA.Text);
                DateTime TToSGA = Convert.ToDateTime(dateToSGA.Text);
                if (DateTime.TryParse(dateFrom.Text, out fromDate) && DateTime.TryParse(dateTo.Text, out toDate))
                {
                    if (toDate >= fromDate)
                    {
                        getSENPILogs(TFromSENPI, TToSENPI);
                        getSGALogs(TFromSGA, TToSGA);
                        // Dates are valid and "Date To" is not less than "Date From"
                        // Proceed with your logic or further processing here
                    }
                    else
                    {
                        lblError.Visible = true;
                    }
                }
            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void btnCalculateSGA_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fromDate, toDate;
                DateTime TFromSENPI = Convert.ToDateTime(dateFrom.Text);
                DateTime TToSENPI = Convert.ToDateTime(dateTo.Text);
                DateTime TFromSGA = Convert.ToDateTime(dateFromSGA.Text);
                DateTime TToSGA = Convert.ToDateTime(dateToSGA.Text);
                if (DateTime.TryParse(dateFromSGA.Text, out fromDate) && DateTime.TryParse(dateToSGA.Text, out toDate))
                {
                    if (toDate >= fromDate)
                    {
                        getSENPILogs(TFromSENPI, TToSENPI);
                        getSGALogs(TFromSGA, TToSGA);
                        // Dates are valid and "Date To" is not less than "Date From"
                        // Proceed with your logic or further processing here
                    }
                    else
                    {
                        lblErrorSGA.Visible = true;
                    }
                }
            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void getSENPILogs(DateTime dtfrm, DateTime dtto)
        {
            try
            {
                Module.flag = false;
                dateFrom.Text = dtfrm.ToString("yyyy-MM-dd");
                dateTo.Text = dtto.ToString("yyyy-MM-dd");
                if (lblError.Visible == true){
                    lblError.Visible = false;
                }
                dtGetSENPIStr = "Select a.userid, b.SSN, b.BADGENUMBER, b.name, (Case when checktype = 'I' then 'In' ELSE 'Out' END) as inOUT," +
                                "convert(varchar(8), checktime, 114) as chkTime, convert(varchar, checktime, 20) as chkDate, " +
                                "DATENAME(MONTH, checktime) as Month, Day(checktime) as Day, Year(checktime) as Year " +
                                "from Att2000.dbo.CHECKINOUT a LEFT JOIN Att2000.dbo.USERINFO b On a.USERID = b.USERID where (convert(varchar, checktime, 101) between convert(varchar, '" + dtfrm.ToString("MM/dd/yyyy") + "', 101) and  convert(varchar, '" + dtto.ToString("MM/dd/yyyy") + "', 101)) " +
                                "and (Year(checktime) between year('" + dtfrm + "') and year('" + dtto + "'))   order by chkDate";

                dtQuery = HRMIS.Module.GetData(dtGetSENPIStr);
                if (dtQuery.Rows.Count > 0)
                {
                    int count = checked(dtQuery.Rows.Count - 1);
                    string sStatementFirst = "<table id='excel-bg' class='table table-striped table-bordered break'><thead>" +
                                             "<tr><th>Date</th><th>ID No</th><th>Name</th><th>IN/OUT</th><th>Time</th><th>Month</th>" +
                                             "<th>Day</th><th>Year</th></tr></thead><tbody>";
                    Panel1.Controls.Add(new LiteralControl(sStatementFirst));
                    for (int x = 0; x <= count; x = checked(x + 1))
                    {
                        string Lstat = "";
                        if (dtQuery.Rows[x]["inOUT"].ToString().ToUpper() == "IN") { Lstat = "palegreen"; }
                        else if (dtQuery.Rows[x]["inOUT"].ToString().ToUpper() == "OUT") { Lstat = "lightcoral"; }
                        string sStatement1 = "<tr><td>" + dtQuery.Rows[x]["chkDate"].ToString() + "</td>" +
                                                 "<td>" + dtQuery.Rows[x]["SSN"].ToString() + "</td>" +
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
        protected void getSGALogs(DateTime dtfrm, DateTime dtto)
        {
            try
            {
                Module.flag = false;
                dateFromSGA.Text = dtfrm.ToString("yyyy-MM-dd");
                dateToSGA.Text = dtto.ToString("yyyy-MM-dd");
                if (lblErrorSGA.Visible == true)
                {
                    lblErrorSGA.Visible = false;
                }
                dtGetSGAStr = "Select a.userid, b.SSN, b.BADGENUMBER, b.name, (Case when checktype = 'I' then 'In' ELSE 'Out' END) as inOUT," +
                               "convert(varchar(8), checktime, 114) as chkTime, convert(varchar, checktime, 20) as chkDate, " +
                               "DATENAME(MONTH, checktime) as Month, Day(checktime) as Day, Year(checktime) as Year " +
                               "from SGA_Att200.dbo.CHECKINOUT a LEFT JOIN SGA_Att200.dbo.USERINFO b On a.USERID = b.USERID where (convert(varchar, checktime, 101) between convert(varchar, '" + dtfrm.ToString("MM/dd/yyyy") + "', 101) and  convert(varchar, '" + dtto.ToString("MM/dd/yyyy") + "', 101)) " +
                               "and (Year(checktime) between year('" + dtfrm + "') and year('" + dtto + "')) order by chkDate";
                dtQuery = HRMIS.Module.GetData(dtGetSGAStr);
                if (dtQuery.Rows.Count > 0)
                {
                    int count = checked(dtQuery.Rows.Count - 1);
                    string sStatementFirst = "<table id='excel-sga' class='table table-striped table-bordered break'><thead>" +
                                             "<tr><th>Date</th><th>ID No</th><th>Name</th><th>IN/OUT</th><th>Time</th><th>Month</th>" +
                                             "<th>Day</th><th>Year</th></tr></thead><tbody>";
                    Panel2.Controls.Add(new LiteralControl(sStatementFirst));
                    for (int x = 0; x <= count; x = checked(x + 1))
                    {
                        string Lstat = "";
                        if (dtQuery.Rows[x]["inOUT"].ToString().ToUpper() == "IN") { Lstat = "palegreen"; }
                        else if (dtQuery.Rows[x]["inOUT"].ToString().ToUpper() == "OUT") { Lstat = "lightcoral"; }
                        string sStatement1 = "<tr><td>" + dtQuery.Rows[x]["chkDate"].ToString() + "</td>" +
                                                 "<td>" + dtQuery.Rows[x]["SSN"].ToString() + "</td>" +
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
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}

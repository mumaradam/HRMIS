using System;
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
using System.Web.UI.HtmlControls;

namespace HRMIS
{
    public partial class Emp : System.Web.UI.Page
    {
        private static DataTable dtQuery;
        private static string getEmpUser = "";
        private static string getUserAdmin = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            getEmpUser = Session["Uname"] as string;
            if (string.IsNullOrEmpty(getEmpUser))
            {
                Session.Abandon();
                Response.Redirect("~/Login");
            }
            else
            {
                if (!IsPostBack)
                {
                    getUserInfo();
                    getAllEmp();
                }

            }
        }
        protected void lblbtnDash_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dash");
        }
        protected void lblbtnDash1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Emp");
        }
        private void getUserInfo()
        {
            dtQuery = null;
            string sQuery = "Select * from seihaHRMIS.dbo.HREmpInfo where empno = '" + getEmpUser + "'";
            dtQuery = HRMIS.Module.GetData(sQuery);
            if (dtQuery.Rows.Count > 0)
            {
                getUserAdmin = dtQuery.Rows[0]["empadmin"].ToString();

            }
        }
        protected void getAllEmp()
        {
            try
            {
                dtQuery = null;
                string dtEmp = "";
                dtEmp = "Select EmpFName + ' ' + EmpLName as Name, convert(varchar, EmpDOH, 107) as HDate, * from seihaHRMIS.dbo.HREmpInfo where empStatus = 1 order by empno";
                dtQuery = HRMIS.Module.GetData(dtEmp);
                if (dtQuery.Rows.Count > 0)
                {
                    int count = checked(dtQuery.Rows.Count - 1);
                    for (int x = 0; x <= count; x = checked(x + 1))
                    {
                        string gen = "";
                        Button button = new Button();
                        Button button1 = new Button();
                        if (dtQuery.Rows[x]["EmpGen"].ToString() == "0") { gen = "Male"; } else { gen = "Female"; }
                        string sStatement = "<tr><td>" + dtQuery.Rows[x]["empno"].ToString() + "</td>" +
                                            "<td>" + dtQuery.Rows[x]["Name"].ToString() + "</td>" +
                                            "<td>" + getPosition(dtQuery.Rows[x]["EmpPos"].ToString()) + "</td>" +
                                            "<td>" + getDpart(dtQuery.Rows[x]["EmpDept"].ToString()) + "</td>" +
                                            "<td>" + gen + "</td>" +
                                            "<td>" + dtQuery.Rows[x]["HDate"].ToString() + "</td>" +
                                            "<td>" + dtQuery.Rows[x]["empEmail"].ToString() + "</td>" +
                                            "<td>";
                        Panel1.Controls.Add(new LiteralControl(sStatement));
                        button.ID = dtQuery.Rows[x]["empno"].ToString();
                        button.Text = "View Profile";
                        button.CssClass = "btn btn-out-dashed waves-effect waves-light btn-primary btn-square";
                        button.PostBackUrl = "~/user-profile?param=" + dtQuery.Rows[x]["empno"].ToString() + "";
                        Panel1.Controls.Add(button);
                        Panel1.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                        button1.ID = dtQuery.Rows[x]["empno"].ToString();
                        button1.Text = "Deactivate";
                        button1.CssClass = "btn btn-out-dashed waves-effect waves-light btn-danger btn-square";
                        button1.Attributes.Add("onclick", "_gaq.push(['_trackEvent', 'example', 'try', 'alert-success-cancel']);");
                        Panel1.Controls.Add(button1);
                        Panel1.Controls.Add(new LiteralControl("</td></tr>"));
                        
                    }
                    
                }


            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        private string getDpart(string eDpart)
        {
            string nameDepart = "";
            string dtQry = "";
            dtQry = "Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = '" + eDpart + "' and cspopupfor = 'DPT'";
            DataTable dt = HRMIS.Module.GetData(dtQry);
            if (dt.Rows.Count > 0)
            {
                nameDepart = dt.Rows[0]["cspopuptext"].ToString();
            }
            return nameDepart;

        }
        private string getPosition(string Gposition)
        {
            string pos = "";
            string dtQry = "";
            dtQry = "Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = '" + Gposition + "' and cspopupfor = 'POS'";
            DataTable dt = HRMIS.Module.GetData(dtQry);
            if (dt.Rows.Count > 0)
            {
                pos = dt.Rows[0]["cspopuptext"].ToString();
            }

            return pos;
        }
        
    }
}

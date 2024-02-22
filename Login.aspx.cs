using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
public partial class Login : System.Web.UI.Page
{
    OleDbConnection conn = new OleDbConnection();
    OleDbCommand sqlComm = new OleDbCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorMsg.Visible = false;
        lblUserRole.Visible = false;
        //if (!IsPostBack)
        //{
        //    if (Session["Uname"] != null)
        //    {
        //        Response.Redirect("~/DashPage.aspx");
        //    }
        //}
    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        conn.Open();
        String strQuery = "Select count(*) from seihaHRMIS.dbo.HRUserInfo where UserEmpNo = '" + txtUserName.Text + "' and UserPass = '" + txtPassword.Text + "'";
        sqlComm = new OleDbCommand(strQuery, conn);
        String output = sqlComm.ExecuteScalar().ToString();
        if (output == "1")
        {
            String strQuery1 = "Select UserStatus from seihaHRMIS.dbo.HRUserInfo where UserEmpNo = '" + txtUserName.Text + "' and UserPass = '" + txtPassword.Text + "'";
            sqlComm = new OleDbCommand(strQuery1, conn);
            String roleOutput = sqlComm.ExecuteScalar().ToString();
            if (roleOutput == "1")
            {
                Session["Uname"] = txtUserName.Text;
                Server.Transfer("~/loadingPage.aspx", true);
                //Response.AddHeader("REFRESH","0.5;loadingPage.aspx");
                //Response.Redirect("~/Dashboard.aspx");
            }
            else
            {
                lblUserRole.Visible = true;
            }

        }
        else
        {
            lblErrorMsg.Visible = true;
        }
        conn.Close();
    }
}
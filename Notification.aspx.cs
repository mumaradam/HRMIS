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

    public partial class Notification : System.Web.UI.Page
    {
        OleDbConnection conn = new OleDbConnection();
        OleDbCommand sqlComm = new OleDbCommand();
        OleDbDataAdapter sqlDA = new OleDbDataAdapter();
        private static string strcCon = "";
        private static DataTable dtQuery;
        private static DataTable dtEmpInfo;
        string dtEmpInfoQuery = "";
        private static string getEmpNo = "";
        private static string hldAdmnStat = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/404");
            //getEmpNo = Session["Uname"] as string;
            //if (string.IsNullOrEmpty(getEmpNo))
            //{
            //    Session.Abandon();
            //    Response.Redirect("~/Login");
            //}
            //else
            //{
            //    lblReg.Visible = false;
            //}
        }
        protected void lblDash_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard");
        }
        protected void lblLeaveA_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LeaveApplication");
        }
        protected void lblReg_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Registration");
        }
        protected void lblEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Employee");
        }
        protected void lblLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login");
        }
        protected void btnLeaveAll_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Leave");
        }
        protected void lblAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account?param=" + getEmpNo);
        }
        protected void lblLeave_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Leave");
        }
        protected void lblCalendar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Calendar");
        }
        protected void lblLeaveC_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LeaveCredits");
        }
        protected void lblNoti_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Notification");
        }
        protected void lblPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ChangePassword?accpass=" + getEmpNo);
        }

    }

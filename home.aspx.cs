using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lblDash_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("http://localhost:6704/Dashboard.aspx");
    }
    protected void lblReg_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://localhost:6704/Register.aspx");
    }
}
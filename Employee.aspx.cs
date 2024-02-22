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

public partial class Employee : System.Web.UI.Page
{
    OleDbConnection conn = new OleDbConnection();
    OleDbCommand sqlComm = new OleDbCommand();
    OleDbDataAdapter sqlDA = new OleDbDataAdapter();
    private static string strcCon = "";
    private static DataTable dtQuery;
    private static Boolean flag = false;
    string getEmpUser = "";
    string getDepart = "";
    string getPosUser = "";
    string getRolUser = "";
    string getUserAdmin = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/404");
        //getEmpUser = Session["Uname"] as string;
        //if (string.IsNullOrEmpty(getEmpUser))
        //{
        //    Session.Abandon();
        //    Response.Redirect("~/Login");
        //}
        //else
        //{
        //    getUserInfo(); 
        //}
        //if (!IsPostBack)
        //{
        //    populatePosDept();
        //    getAllEmp("", "", "");
        //    ListItem lst = new ListItem("Position", "", true);
        //    lst.Attributes.Add("hidden", "");
        //    position.Items.Insert(0, lst);
        //}
        
    }
    private void populatePosDept()
    {
        try
        {
            department.DataSource = null;
            string dtQry = "";
            dtQry = "Select cspopuptext, identity_column from seihaHRMIS.dbo.cspopup where cspopupfor = 'DPT' order by identity_column";
            DataTable dt = GetData(dtQry);
            if (dt.Rows.Count > 0)
            {
                department.DataSource = dt;
                department.DataTextField = "cspopuptext";
                department.DataValueField = "identity_column";
                department.DataBind();
                ListItem lst = new ListItem("Department", "", true);
                lst.Attributes.Add("hidden", "");
                department.Items.Insert(0, lst);
                
            }
           
            
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void dpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        populatePosPos(department.SelectedValue.ToString());
        department.Items.FindByValue("").Attributes.Add("hidden","");
    }
    private void populatePosPos(string gDept)
    {
        try
        {
            position.DataSource = null;
            string dtQry = "";
            dtQry = "Select cspopuptext, cspopupval from seihaHRMIS.dbo.cspopup where cspopupfor = 'POS' and cspopupconn= " + int.Parse(gDept) + " order by cspopupval";
            DataTable dt = GetData(dtQry);
            if (dt.Rows.Count > 0)
            {
                position.DataSource = dt;
                position.DataTextField = "cspopuptext";
                position.DataValueField = "cspopupval";
                position.DataBind();
                ListItem lst = new ListItem("Position", "", true);
                lst.Attributes.Add("hidden", "");
                position.Items.Insert(0, lst);
            }
            else
            {
                position.DataSource = null;
                position.Items.Clear();
                ListItem lst = new ListItem("Position", "", true);
                lst.Attributes.Add("hidden", "");
                position.Items.Insert(0, lst);
            }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    private void getUserInfo()
    {
        dtQuery = null;
        string sQuery = "Select * from seihaHRMIS.dbo.HREmpInfo where empno = '" + getEmpUser + "'";
        dtQuery = GetData(sQuery);
        if (dtQuery.Rows.Count > 0)
        {
            getDepart = dtQuery.Rows[0]["empdept"].ToString();
            getPosUser = dtQuery.Rows[0]["emppos"].ToString();
            getRolUser = dtQuery.Rows[0]["emprole"].ToString();
            getUserAdmin = dtQuery.Rows[0]["empadmin"].ToString();
            lblUser.Text = dtQuery.Rows[0]["empFname"].ToString();
            if (dtQuery.Rows[0]["empGen"].ToString() == "0")
            {
                UserPic.Attributes.Add("src", "images/img_avatar.png");
            }
            else
            {
                UserPic.Attributes.Add("src", "images/img_avatar2.png");
            }
            if (dtQuery.Rows[0]["empadmin"].ToString() != "1")
            {
                lblReg.Visible = false;
            }
            else
            {
                lblReg.Visible = true;
            }
            //if (getDepart == "MGR" || getDepart == "ADM" || getDepart == "IT")
            //{
            //    if (getRolUser == "RLD" || getRolUser == "RSL")
            //    {
            //        lblLeave.Visible = true;
            //    }
            //    else
            //    {
            //        if (getDepart == "ADM")
            //        {
            //            if (getPosUser == "DVR")
            //            {
            //                lblLeave.Visible = false;
            //            }
            //            else
            //            {
            //                lblLeave.Visible = true;
            //            }
            //        }
            //        else
            //        {
            //            lblLeave.Visible = false;
            //        }
                    
            //    }
            //}
            //else
            //{
            //    if (getPosUser == "ITM")
            //    {
            //        lblLeave.Visible = true;
            //    }
            //    else
            //    {
            //        if (getRolUser == "RLD" || getRolUser == "RSL")
            //        {
            //            lblLeave.Visible = true;
            //        }
            //        else
            //        {
            //            lblLeave.Visible = false;
            //        }
                    
            //    }
            //}
           
        }
    }
    protected void lblDash_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Dashboard");
    }
    protected void lblReg_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Registration");
    }
    protected void lblLeaveA_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/LeaveApplication");
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
    protected void lblLeave_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Leave");
    }
    protected void lblAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Account?param=" + getEmpUser);
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
        Response.Redirect("~/ChangePassword?accpass=" + getEmpUser);
    }
    protected void lblCalendar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Calendar");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            if (department.SelectedValue != "")
            {
                if (txtEmpSearch.Text != "")
                {
                    if (position.SelectedValue != "") 
                    {
                        getAllEmp(txtEmpSearch.Text, department.SelectedValue.ToString(), position.SelectedValue.ToString()); 
                    }
                    else
                    {
                        getAllEmp(txtEmpSearch.Text, department.SelectedValue.ToString(), "");
                    }
                    
                }
                else
                {
                    if (position.SelectedValue != "") { getAllEmp("", department.SelectedValue.ToString(), position.SelectedValue.ToString()); }
                    else{
                        getAllEmp("", department.SelectedValue.ToString(), "");
                    }
                }
                
            }
            else
            {
                if (txtEmpSearch.Text != "")
                {
                    getAllEmp(txtEmpSearch.Text, "", "");
                }
                else
                {
                    getAllEmp("", "", "");
                }
               
            }
            department.Items.FindByValue("").Attributes.Add("hidden", "");
            position.Items.FindByValue("").Attributes.Add("hidden", "");

        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    private string findDept(string gstat)
    {
        string fDept = "";
        string dtQry = "";
        dtQry = "Select cspopupval from seihaHRMIS.dbo.cspopup where cspopupfor = 'DPT' and identity_column = '" + gstat + "'";
        DataTable dt = GetData(dtQry);
        if (dt.Rows.Count > 0)
        {
            fDept = dt.Rows[0]["cspopupval"].ToString();
        }
        return fDept;
    }
    private void getAllEmp(string strQEmpName, string gDept, string gPos)
    {
        try
        {
            dtQuery = null;
            string dtEmp = "";
            gDept = findDept(gDept);
            if(strQEmpName == "")   
            {
                if (gDept != "")
                {

                    if (gPos != "")
                    {
                        dtEmp = "Select EmpFName + ' ' + EmpLName as Name, convert(varchar, EmpDOH, 107) as HDate, * from seihaHRMIS.dbo.HREmpInfo where empdept = '" + gDept + "' and emppos = '" + gPos + "' and empStatus = 1 order by empno";
                    }
                    else
                    {
                        dtEmp = "Select EmpFName + ' ' + EmpLName as Name, convert(varchar, EmpDOH, 107) as HDate, * from seihaHRMIS.dbo.HREmpInfo where empdept = '" + gDept + "' and empStatus = 1 order by empno";
                    }
                }
                else 
                {
                    if (gPos != "")
                    {
                        dtEmp = "Select EmpFName + ' ' + EmpLName as Name, convert(varchar, EmpDOH, 107) as HDate, * from seihaHRMIS.dbo.HREmpInfo where emppos = '" + gPos + "' and empStatus = 1 order by empno";
                    }
                    else
                    {
                        dtEmp = "Select EmpFName + ' ' + EmpLName as Name, convert(varchar, EmpDOH, 107) as HDate, * from seihaHRMIS.dbo.HREmpInfo where empStatus = 1 order by empno";
                    }
                }
                
            }
            else
            {
                if (gDept != "")
                {

                    if (gPos != "")
                    {
                        dtEmp = "Select EmpFName + ' ' + EmpLName as Name, convert(varchar, EmpDOH, 107) as HDate, * from seihaHRMIS.dbo.HREmpInfo where (EmpFName + ' ' + EmpLName) like '%" + strQEmpName + "%' and empdept = '" + gDept + "' and emppos = '" + gPos + "' and empStatus = 1 order by empno";
                    }
                    else
                    {
                        dtEmp = "Select EmpFName + ' ' + EmpLName as Name, convert(varchar, EmpDOH, 107) as HDate, * from seihaHRMIS.dbo.HREmpInfo where (EmpFName + ' ' + EmpLName) like '%" + strQEmpName + "%' and empdept = '" + gDept + "' and empStatus = 1 order by empno";
                    }
                }
                else
                {
                    if (gPos != "")
                    {
                        dtEmp = "Select EmpFName + ' ' + EmpLName as Name, convert(varchar, EmpDOH, 107) as HDate, * from seihaHRMIS.dbo.HREmpInfo where (EmpFName + ' ' + EmpLName) like '%" + strQEmpName + "%' and emppos = '" + gPos + "' and empStatus = 1 order by empno";
                    }
                    else
                    {
                        dtEmp = "Select EmpFName + ' ' + EmpLName as Name, convert(varchar, EmpDOH, 107) as HDate, * from seihaHRMIS.dbo.HREmpInfo where (EmpFName + ' ' + EmpLName) like '%" + strQEmpName + "%' and empStatus = 1 order by empno";
                    }
                }
                
            }
            dtQuery = GetData(dtEmp);
            if (dtQuery.Rows.Count > 0)
            {
                int count = checked(dtQuery.Rows.Count - 1);
                for (int x = 0; x <= count; x = checked(x + 1))
                {
                    
                    
                    if (dtQuery.Rows[x]["EmpGen"].ToString() == "0")
                    {
                        Button button = new Button();
                        LinkButton lbtn = new LinkButton();
                        string sStatement = "<div class='col-md-6 col-xl-4 col-12'><div class='card pmd-card user-info-card emphover'><div class='card-header pmd-card-border d-flex flex-row align-items-center'><a class='pmd-avatar-list-img' href='javascript:void(0);'> " +
                        "<img class='img-fluid' src='images/img_avatar.png' style='height: 40px; width: 40px;'></a>" +
                        "<div class='media-body'><h3 class='card-title' style='font-size: 1.7rem;'>" + dtQuery.Rows[x]["Name"].ToString() + "</h3><p class='card-subtitle' style='font-size: 1.2rem;'>" + getPosition(dtQuery.Rows[x]["EmpPos"].ToString()) + "</p></div>" +
                        "<li class='btn-group pmd-btn-fab btn-dark pmd-btn-flat btn btn-sm' style='display: flex; flex-direction: column;'> " +
                        "<a data-toggle='dropdown'><i class='material-icons pmd-icon-sm'>more_vert</i></a> <ul class='dropdown-menu dropdown-menu-right' style='left: 0px; will-change: transform; width: 130px; padding-left: 15px;'> ";
                        Panel3.Controls.Add(new LiteralControl(sStatement));
                        if (int.Parse(getUserAdmin) == 1)
                        {
                            
                            lbtn.ID = "Del" + dtQuery.Rows[x]["empno"].ToString();
                            lbtn.Text = "Delete";
                            lbtn.CssClass = "labelStyle pmd-btn-fab d-flex flex-row align-items-center";
                            lbtn.Attributes.Add("style", "color: #242526; font-size: 1.5rem;");
                            Panel3.Controls.Add(lbtn);
                        }
                        //"<li><asp:LinkButton ID='" + dtQuery.Rows[x]["empno"].ToString() + "' runat='server' class='labelStyle pmd-btn-fab d-flex flex-row align-items-center' style='color: #242526; font-size: 1.5rem;'><i class='material-icons md-dark pmd-icon-sm mr-3'>edit</i><span class='media-body'>Edit</span></asp:LinkButton></li> " +
                        //"<li><asp:LinkButton ID='" + dtQuery.Rows[x]["empno"].ToString() + "' runat='server' class='labelStyle pmd-btn-fab d-flex flex-row align-items-center' style='color: #242526; font-size: 1.5rem;'><i class='material-icons md-dark pmd-icon-sm mr-3'>delete</i><span class='media-body'>Delete</span></asp:LinkButton></li></ul> " +
                        string sStatement2 = "</li>" +
                        "</div> " +
                        "<ul class='list-group pmd-list'>" +
                                        "<li class='list-group-item d-flex flex-row'>" +
                                            "<i class='material-icons pmd-list-icon align-self-center pmd-md md-dark'>person_outline</i>" +
                                            "<div class='media-body'>" +
                                                "<label class='pmd-list-subtitle' style='font-size: 1.6rem;'>Employee ID</label>" +
                                                "<p class='pmd-list-title' style='font-size: 1.5rem;'>" + dtQuery.Rows[x]["empno"].ToString() + "</p>" +
                                            "</div>" +
                                        "</li>" +
                                        "<li class='list-group-item d-flex flex-row'>" +
                                            "<i class='material-icons pmd-list-icon align-self-center pmd-md md-dark'>call</i>" +
                                            "<div class='media-body'>" +
                                                "<label class='pmd-list-subtitle mb-0' style='font-size: 1.6rem;'>Phone No.</label>" +
                                                "<p class='pmd-list-title' style='font-size: 1.5rem;'>" + dtQuery.Rows[x]["empconno"].ToString() + "</p>" +
                                            "</div>" +
                                        "</li>" +
                                        "<li class='list-group-item d-flex flex-row'>" +
                                            "<i class='material-icons pmd-list-icon align-self-center pmd-md md-dark'>mail_outline</i>" +
                                            "<div class='media-body'>" +
                                                "<label class='pmd-list-subtitle' style='font-size: 1.6rem;'>Email Address</label>" +
                                                "<p class='pmd-list-title' style='font-size: 1.5rem;'><a href='mailto:" + dtQuery.Rows[x]["empemail"].ToString() + "'>" + dtQuery.Rows[x]["empemail"].ToString() + "</a></p>" +
                                            "</div>" +
                                        "</li>" +
                                        "<li class='list-group-item d-flex flex-row'>" +
                                            "<i class='material-icons pmd-list-icon align-self-center pmd-md md-dark'>people_outline</i>" +
                                            "<div class='media-body'>" +
                                                "<label class='pmd-list-subtitle' style='font-size: 1.6rem;'>Department</label>" +
                                                "<p class='pmd-list-title' style='font-size: 1.5rem;'>" + getDpart(dtQuery.Rows[x]["EmpDept"].ToString()) + "</p>" +
                                            "</div>" +
                                        "</li>" +
                                        "<li class='list-group-item d-flex flex-row'>" +
                                            "<i class='material-icons pmd-list-icon align-self-center pmd-md md-dark'>people_outline</i>" +
                                            "<div class='media-body'>" +
                                                "<label class='pmd-list-subtitle' style='font-size: 1.6rem;'>Position</label>" +
                                                "<p class='pmd-list-title' style='font-size: 1.5rem;'>" + getPosition(dtQuery.Rows[x]["EmpPos"].ToString()) + " " + getRole(dtQuery.Rows[x]["EmpRole"].ToString()) + "</p>" +
                                            "</div>" +
                                        "</li>" +
                                        "<li class='list-group-item d-flex flex-row'>" +
                                            "<i class='material-icons pmd-list-icon align-self-center pmd-md md-dark'>today</i>" +
                                            "<div class='media-body'>" +
                                                "<label class='pmd-list-subtitle' style='font-size: 1.6rem;'>Date of Joining</label>" +
                                                "<p class='pmd-list-title' style='font-size: 1.5rem;'>" + dtQuery.Rows[x]["HDate"].ToString() + "</p>" +
                                            "</div>" +
                                        "</li>" +
                                    "</ul>" +
                                    "<div class='card-footer pmd-card-border'>";
                            Panel3.Controls.Add(new LiteralControl(sStatement2));
                            if (int.Parse(getUserAdmin) == 1)
                            {
                                button.ID = dtQuery.Rows[x]["empno"].ToString();
                                button.Text = "View More";
                                button.CssClass = "btn pmd-btn-flat pmd-ripple-effect btn-primary";
                                button.Attributes.Add("style", "font-size: 1.5rem");
                                //button.CommandArgument = dtQuery.Rows[x]["empno"].ToString();
                                button.PostBackUrl = "~/EmployeeDetail?param=" + dtQuery.Rows[x]["empno"].ToString() + "";
                                //button.Click += btnView_Click;
                                Panel3.Controls.Add(button);
                            }
                            Panel3.Controls.Add(new LiteralControl("</div></div></div>"));
                    }
                    else
                    {
                        Button button = new Button();
                        LinkButton lbtn = new LinkButton();
                        string sStatement = "<div class='col-md-6 col-xl-4 col-12'><div class='card pmd-card user-info-card emphover'><div class='card-header pmd-card-border d-flex flex-row align-items-center'><a class='pmd-avatar-list-img' href='javascript:void(0);'> " +
                        "<img class='img-fluid' src='images/img_avatar2.png' style='height: 40px; width: 40px;'></a>" +
                        "<div class='media-body'><h3 class='card-title' style='font-size: 1.7rem;'>" + dtQuery.Rows[x]["Name"].ToString() + "</h3><p class='card-subtitle' style='font-size: 1.2rem;'>" + getPosition(dtQuery.Rows[x]["EmpPos"].ToString()) + "</p></div>" +
                        "<li class='btn-group pmd-btn-fab btn-dark pmd-btn-flat btn btn-sm' style='display: flex; flex-direction: column;'> " +
                        "<a data-toggle='dropdown'><i class='material-icons pmd-icon-sm'>more_vert</i></a> <ul class='dropdown-menu dropdown-menu-right' style='left: 0px; will-change: transform; width: 130px; padding-left: 15px;'> ";
                        Panel3.Controls.Add(new LiteralControl(sStatement));
                        if (int.Parse(getUserAdmin) == 1)
                        {
                            lbtn.ID = "Del" + dtQuery.Rows[x]["empno"].ToString();
                            lbtn.Text = "Delete";
                            lbtn.CssClass = "labelStyle pmd-btn-fab d-flex flex-row align-items-center";
                            lbtn.Attributes.Add("style", "color: #242526; font-size: 1.5rem;");
                            Panel3.Controls.Add(lbtn);
                        }
                        
                        string sStatement2 = "</li>" +
                        "</div> " +
                        "<ul class='list-group pmd-list'>" +
                                        "<li class='list-group-item d-flex flex-row'>" +
                                            "<i class='material-icons pmd-list-icon align-self-center pmd-md md-dark'>person_outline</i>" +
                                            "<div class='media-body'>" +
                                                "<label class='pmd-list-subtitle' style='font-size: 1.6rem;'>Employee ID</label>" +
                                                "<p class='pmd-list-title' style='font-size: 1.5rem;'>" + dtQuery.Rows[x]["empno"].ToString() + "</p>" +
                                            "</div>" +
                                        "</li>" +
                                        "<li class='list-group-item d-flex flex-row'>" +
                                            "<i class='material-icons pmd-list-icon align-self-center pmd-md md-dark'>call</i>" +
                                            "<div class='media-body'>" +
                                                "<label class='pmd-list-subtitle mb-0' style='font-size: 1.6rem;'>Phone No.</label>" +
                                                "<p class='pmd-list-title' style='font-size: 1.5rem;'>" + dtQuery.Rows[x]["empconno"].ToString() + "</p>" +
                                            "</div>" +
                                        "</li>" +
                                        "<li class='list-group-item d-flex flex-row'>" +
                                            "<i class='material-icons pmd-list-icon align-self-center pmd-md md-dark'>mail_outline</i>" +
                                            "<div class='media-body'>" +
                                                "<label class='pmd-list-subtitle' style='font-size: 1.6rem;'>Email Address</label>" +
                                                "<p class='pmd-list-title' style='font-size: 1.5rem;'><a href='mailto:" + dtQuery.Rows[x]["empemail"].ToString() + "'>" + dtQuery.Rows[x]["empemail"].ToString() + "</a></p>" +
                                            "</div>" +
                                        "</li>" +
                                        "<li class='list-group-item d-flex flex-row'>" +
                                            "<i class='material-icons pmd-list-icon align-self-center pmd-md md-dark'>people_outline</i>" +
                                            "<div class='media-body'>" +
                                                "<label class='pmd-list-subtitle' style='font-size: 1.6rem;'>Department</label>" +
                                                "<p class='pmd-list-title' style='font-size: 1.5rem;'>" + getDpart(dtQuery.Rows[x]["EmpDept"].ToString()) + "</p>" +
                                            "</div>" +
                                        "</li>" +
                                        "<li class='list-group-item d-flex flex-row'>" +
                                            "<i class='material-icons pmd-list-icon align-self-center pmd-md md-dark'>people_outline</i>" +
                                            "<div class='media-body'>" +
                                                "<label class='pmd-list-subtitle' style='font-size: 1.6rem;'>Position</label>" +
                                                "<p class='pmd-list-title' style='font-size: 1.5rem;'>" + getPosition(dtQuery.Rows[x]["EmpPos"].ToString()) + " " + getRole(dtQuery.Rows[x]["EmpRole"].ToString()) + "</p>" +
                                            "</div>" +
                                        "</li>" +
                                        "<li class='list-group-item d-flex flex-row'>" +
                                            "<i class='material-icons pmd-list-icon align-self-center pmd-md md-dark'>today</i>" +
                                            "<div class='media-body'>" +
                                                "<label class='pmd-list-subtitle' style='font-size: 1.6rem;'>Date of Joining</label>" +
                                                "<p class='pmd-list-title' style='font-size: 1.5rem;'>" + dtQuery.Rows[x]["HDate"].ToString() + "</p>" +
                                            "</div>" +
                                        "</li>" +
                                    "</ul>" +
                                    "<div class='card-footer pmd-card-border'>";
                        Panel3.Controls.Add(new LiteralControl(sStatement2));
                        if (int.Parse(getUserAdmin) == 1)
                        {
                            button.ID = dtQuery.Rows[x]["empno"].ToString();
                            button.Text = "View More";
                            button.CssClass = "btn pmd-btn-flat pmd-ripple-effect btn-primary";
                            button.Attributes.Add("style", "font-size: 1.5rem");
                            button.PostBackUrl = "~/EmployeeDetail?param=" + dtQuery.Rows[x]["empno"].ToString() + "";
                            Panel3.Controls.Add(button);
                        }
                        Panel3.Controls.Add(new LiteralControl("</div></div></div>"));
                    }
                    
                   
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
        DataTable dt = GetData(dtQry);
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
        DataTable dt = GetData(dtQry);
        if (dt.Rows.Count > 0)
        {
            pos = dt.Rows[0]["cspopuptext"].ToString();
        }
        
        return pos;
    }
    private string getRole(string gRole)
    {
        string nameRole = "";
        string dtQry = "";
        dtQry = "Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = '" + gRole + "' and cspopupfor = 'ROL'";
        DataTable dt = GetData(dtQry);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["cspopuptext"].ToString() == "None")
            {
                nameRole = "";
            }
            else
            {
                nameRole = dt.Rows[0]["cspopuptext"].ToString();
            }
        }
        return nameRole;
    }
    private DataTable GetData(string strQuery)
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
}
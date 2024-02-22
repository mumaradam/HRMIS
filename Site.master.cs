using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRMIS
{
    public partial class Site : System.Web.UI.MasterPage
    {
        private static DataTable dtQuery;
        private static string getEmpNo = "";
        private static string getUserAdmin = "";
        private static string getUserDept = "";
        private static string getUserPos = "";
        private static string getDateNow = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //getEmpNo = Session["Uname"] as string;
            getEmpNo = Session["Uname"] as string;
            if (string.IsNullOrEmpty(getEmpNo))
            {
                Session.Abandon();
                Response.Redirect("~/Login");
            }
            else
            {
                if(!IsPostBack){
                    getDateNow = DateTime.Now.ToString("MM/dd/yyyy");
                    getUserInfo();
                    getNavActive();
                    getNoti();
                }
                else
                {
                    Module.flag = true;
                    getNoti();
                }
                
            }
        }
        protected void getNavActive()
        {
            string currentPage = Request.Url.AbsolutePath;
            if (currentPage.EndsWith("Dash.aspx"))
            {
                navDash.Attributes["class"] = "active";//Dashboard Activated
                //Admin
                navAdmnDrop.Attributes["class"] = "pcoded-hasmenu";
                navEmp.Attributes["class"] = "";
                navEmpReg.Attributes["class"] = "";
                navCon.Attributes["class"] = "";
                navEmpBio.Attributes["class"] = "";
                //navEmpDet.Attributes["class"] = "";
                //Leave & Credits
                navLeave.Attributes["class"] = "";
                navLeaveApp.Attributes["class"] = "";
                navLeaveCred.Attributes["class"] = "";
                //Calendar & Biometric
                navCalendar.Attributes["class"] = "";
                navBiometric.Attributes["class"] = "";
                //Account
                navAccDrop.Attributes["class"] = "pcoded-hasmenu";
                navPro.Attributes["class"] = "";
            }
            else if (currentPage.EndsWith("Leaves.aspx") || currentPage.EndsWith("LeaveDet.aspx"))
            {
                navDash.Attributes["class"] = "";
                //Admin
                navAdmnDrop.Attributes["class"] = "pcoded-hasmenu";
                navEmp.Attributes["class"] = "";
                navEmpReg.Attributes["class"] = "";
                navCon.Attributes["class"] = "";
                navEmpBio.Attributes["class"] = "";
                //navEmpDet.Attributes["class"] = "";
                //Leave & Credits
                navLeave.Attributes["class"] = "active";
                navLeaveApp.Attributes["class"] = "";//Leave Application Activated
                navLeaveCred.Attributes["class"] = "";
                //Calendar & Biometric
                navCalendar.Attributes["class"] = "";
                navBiometric.Attributes["class"] = "";
                //Account
                navAccDrop.Attributes["class"] = "pcoded-hasmenu";
                navPro.Attributes["class"] = "";
            }
            else if(currentPage.EndsWith("LeaveApp.aspx"))
            {
                navDash.Attributes["class"] = "";
                //Admin
                navAdmnDrop.Attributes["class"] = "pcoded-hasmenu";
                navEmp.Attributes["class"] = "";
                navEmpReg.Attributes["class"] = "";
                navCon.Attributes["class"] = "";
                navEmpBio.Attributes["class"] = "";
                //navEmpDet.Attributes["class"] = "";
                //Leave & Credits
                navLeave.Attributes["class"] = "";
                navLeaveApp.Attributes["class"] = "active";//Leave Application Activated
                navLeaveCred.Attributes["class"] = "";
                //Calendar & Biometric
                navCalendar.Attributes["class"] = "";
                navBiometric.Attributes["class"] = "";
                //Account
                navAccDrop.Attributes["class"] = "pcoded-hasmenu";
                navPro.Attributes["class"] = "";
            }
            else if (currentPage.EndsWith("LeaveCred.aspx"))
            {
                navDash.Attributes["class"] = "";
                //Admin
                navAdmnDrop.Attributes["class"] = "pcoded-hasmenu";
                navEmp.Attributes["class"] = "";
                navEmpReg.Attributes["class"] = "";
                navCon.Attributes["class"] = "";
                navEmpBio.Attributes["class"] = "";
                //navEmpDet.Attributes["class"] = "";
                //Leave & Credits
                navLeave.Attributes["class"] = "";
                navLeaveApp.Attributes["class"] = "";//Leave Application Activated
                navLeaveCred.Attributes["class"] = "active";
                //Calendar & Biometric
                navCalendar.Attributes["class"] = "";
                navBiometric.Attributes["class"] = "";
                //Account
                navAccDrop.Attributes["class"] = "pcoded-hasmenu";
                navPro.Attributes["class"] = "";
            }
            else if (currentPage.EndsWith("user-profile.aspx"))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["param"]))
                {
                    //admin
                    navAdmnDrop.Attributes["class"] = "pcoded-hasmenu pcoded-trigger";
                    navEmp.Attributes["class"] = "active";
                    //account
                    navAccDrop.Attributes["class"] = "pcoded-hasmenu ";
                    navPro.Attributes["class"] = "";//user-profile Activated
                    Module.findWhereuser = false;
                }
                else 
                {
                    //admin
                    navAdmnDrop.Attributes["class"] = "pcoded-hasmenu";
                    navEmp.Attributes["class"] = "";
                    //account
                    navAccDrop.Attributes["class"] = "pcoded-hasmenu pcoded-trigger";
                    navPro.Attributes["class"] = "active";//user-profile Activated
                    Module.findWhereuser = true;
                    
                }

                navDash.Attributes["class"] = "";
                //Admin
                navEmpReg.Attributes["class"] = "";
                navCon.Attributes["class"] = "";
                navEmpBio.Attributes["class"] = "";
                //navEmpDet.Attributes["class"] = "";
                //Leave & Credits
                navLeave.Attributes["class"] = "";
                navLeaveApp.Attributes["class"] = "";
                navLeaveCred.Attributes["class"] = "";
                //Calendar & Biometric
                navCalendar.Attributes["class"] = "";
                navBiometric.Attributes["class"] = "";
                //Account
                
            }
            else if (currentPage.EndsWith("Emp.aspx"))
            {
                navDash.Attributes["class"] = "";
                //Admin
                navAdmnDrop.Attributes["class"] = "pcoded-hasmenu pcoded-trigger";
                navEmp.Attributes["class"] = "active";//Employee Activated
                navEmpReg.Attributes["class"] = "";
                navCon.Attributes["class"] = "";
                navEmpBio.Attributes["class"] = "";
                //navEmpDet.Attributes["class"] = "";
                //Leave & Credits
                navLeave.Attributes["class"] = "";
                navLeaveApp.Attributes["class"] = "";
                navLeaveCred.Attributes["class"] = "";
                //Calendar & Biometric
                navCalendar.Attributes["class"] = "";
                navBiometric.Attributes["class"] = "";
                //Account
                navAccDrop.Attributes["class"] = "pcoded-hasmenu";
                navPro.Attributes["class"] = "";
            }
            else if (currentPage.EndsWith("EmpReg.aspx"))
            {
                navDash.Attributes["class"] = "";
                //Admin
                navAdmnDrop.Attributes["class"] = "pcoded-hasmenu pcoded-trigger";
                navEmp.Attributes["class"] = "";
                navEmpReg.Attributes["class"] = "active";//Employee Reg Activated
                navCon.Attributes["class"] = "";
                navEmpBio.Attributes["class"] = "";
                //navEmpDet.Attributes["class"] = "";
                //Leave & Credits
                navLeave.Attributes["class"] = "";
                navLeaveApp.Attributes["class"] = "";
                navLeaveCred.Attributes["class"] = "";
                //Calendar & Biometric
                navCalendar.Attributes["class"] = "";
                navBiometric.Attributes["class"] = "";
                //Account
                navAccDrop.Attributes["class"] = "pcoded-hasmenu";
                navPro.Attributes["class"] = "";
            }
            else if (currentPage.EndsWith("EmployeeBiomtrc.aspx"))
            {
                navDash.Attributes["class"] = "";
                //Admin
                navAdmnDrop.Attributes["class"] = "pcoded-hasmenu pcoded-trigger";
                navEmp.Attributes["class"] = "";
                navEmpReg.Attributes["class"] = "";
                navCon.Attributes["class"] = "";
                navEmpBio.Attributes["class"] = "active"; //All Employee Biometric activated
                //navEmpDet.Attributes["class"] = "";
                //Leave & Credits
                navLeave.Attributes["class"] = "";
                navLeaveApp.Attributes["class"] = "";
                navLeaveCred.Attributes["class"] = "";
                //Calendar & Biometric
                navCalendar.Attributes["class"] = "";
                navBiometric.Attributes["class"] = "";
                //Account
                navAccDrop.Attributes["class"] = "pcoded-hasmenu";
                navPro.Attributes["class"] = "";
            }
            else if (currentPage.EndsWith("Biomtrc.aspx")) //Biometric
            {
                navDash.Attributes["class"] = "";
                //Admin
                navAdmnDrop.Attributes["class"] = "pcoded-hasmenu";
                navEmp.Attributes["class"] = "";//Employee Activated
                navEmpReg.Attributes["class"] = "";
                navCon.Attributes["class"] = "";
                navEmpBio.Attributes["class"] = "";
                //navEmpDet.Attributes["class"] = "";
                //Leave & Credits
                navLeave.Attributes["class"] = "";
                navLeaveApp.Attributes["class"] = "";
                navLeaveCred.Attributes["class"] = "";
                //Calendar & Biometric
                navCalendar.Attributes["class"] = "";
                navBiometric.Attributes["class"] = "active";
                //Account
                navAccDrop.Attributes["class"] = "pcoded-hasmenu";
                navPro.Attributes["class"] = "";
            }

        }
        protected void lblbtnDashSide_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dash");
        }
        protected void lblbtnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login");
        }
        protected void lnkbtnEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Emp");
        }
        protected void lnkbtnEmpReg_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EmpReg");
        }
        protected void lnkbtnBiometric_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EmployeeBiomtrc");
        }
        protected void lnkbtnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/user-profile");
        }
        protected void lnkbtnLeave_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Leaves");
        }
        protected void lnkbtnLeaveA_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LeaveApp?ep=" + getEmpNo + "&gid=" + Guid.NewGuid().ToString());
        }
        protected void lnkbtnLeaveCredit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LeaveCred");
        }
        protected void lnkbtnBio_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Biomtrc");
        }
        protected void lnkbtnCalen_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/404");
        }
        private void getUserInfo()
        {
            dtQuery = null;
            string sQuery = "Select * from seihaHRMIS.dbo.HREmpInfo a LEFT JOIN seihaHRMIS.dbo.emp_pic b ON a.empno = b.empno where a.empno = '" + getEmpNo + "'";
            dtQuery = HRMIS.Module.GetData(sQuery);
            if (dtQuery.Rows.Count > 0)
            {
                //getDepart = dtQuery.Rows[0]["empdept"].ToString();
                //getPosUser = dtQuery.Rows[0]["emppos"].ToString();
                // = dtQuery.Rows[0]["emprole"].ToString();
                getUserAdmin = dtQuery.Rows[0]["empadmin"].ToString();
                getUserPos = dtQuery.Rows[0]["empPos"].ToString();
                getUserDept = dtQuery.Rows[0]["empDept"].ToString();
                lblUser.Text = dtQuery.Rows[0]["empFname"].ToString();
                lblusernameSide.Text = dtQuery.Rows[0]["empFname"].ToString() + "<i class='fa fa-caret-down'></i>";
                if (dtQuery.Rows[0]["new_fileloc"].ToString() != "")
                {
                    string strFilePath = dtQuery.Rows[0]["new_fileloc"].ToString();
                    string base64String = GetBase64ImageString(strFilePath);
                    string dataUri = "data:image/jpeg;base64," + base64String;

                    UserPic.Attributes.Add("src", dataUri);
                    userpicSide.Attributes.Add("src", dataUri);
                }
                else
                {
                    if (dtQuery.Rows[0]["empGen"].ToString() == "0")
                    {
                        UserPic.Attributes.Add("src", "images/img_avatar.png");
                        userpicSide.Attributes.Add("src", "images/img_avatar.png");
                    }
                    else
                    {
                        UserPic.Attributes.Add("src", "images/img_avatar2.png");
                        userpicSide.Attributes.Add("src", "images/img_avatar2.png");
                    }
                }

                
                if (getUserAdmin == "1")
                {
                    lblbtnAdmin.Visible = true;
                    lnkbtnEmpReg.Visible = true;
                    //lnkbtnEmpDet.Visible = true;
                    lnkbtnControl.Visible = true;
                    //lblReg.Visible = false;
                }
                else
                {
                    if (getUserPos == "ITM" || getUserPos == "TMR" || getUserPos == "SMR" || getUserPos == "TOM" || getUserDept == "MGR")
                    {
                        lblbtnAdmin.Visible = true;
                        lnkbtnEmpReg.Visible = false;
                        //lnkbtnEmpDet.Visible = false;
                        lnkbtnControl.Visible = false;
                    }
                    else
                    {
                        lblbtnAdmin.Visible = false;
                        lnkbtnEmpReg.Visible = false;
                        //lnkbtnEmpDet.Visible = false;
                        lnkbtnControl.Visible = false;
                    }
                    //lblReg.Visible = true;
                }

            }
        }
        private string GetBase64ImageString(string imagePath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            // Your code to determine the new label text
            //Module.flag = true;
            getNoti();
            // Update the UpdatePanel to refresh the label
            UpdatePanel1.Update();
           
        }
        protected void getNoti()
        {
            //Module.flag = false;
            string sQuery = "";
            if (getUserAdmin == "1")
            {
                sQuery = "Select a.notEmpno, (Select EmpFName from seihaHRMIS.dbo.HREmpInfo where empno = b.empNo) as Name, (Select EmpFName from seihaHRMIS.dbo.HREmpInfo where empno = a.notwho) as Who, " +
                            "(Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=b.empTOL) as Type, " +
                            "b.empReason,  a.notType, a.notLeaveNo from seihaHRMIS.dbo.notInfo a LEFT JOIN seihaHRMIS.dbo.HRLeaveInfo b ON a.notLeaveNo = b.identity_column " +
                            "where a.notView = 1 and convert(varchar, a.notDate, 101) = convert(varchar, getdate(), 101) order by a.notDate desc ";
            }
            else
            {
                if (getUserPos == "ITM" || getUserPos == "TMR" || getUserPos == "SMR" || getUserPos == "TOM")
                {
                    sQuery = "Select a.notEmpno, c.EmpFName as Name,  " +
                            "(Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=b.empTOL) as Type, (Select EmpFName from seihaHRMIS.dbo.HREmpInfo where empno = a.notwho) as Who, " +
                            "b.empReason,  a.notType, a.notLeaveNo from seihaHRMIS.dbo.notInfo a LEFT JOIN seihaHRMIS.dbo.HRLeaveInfo b ON a.notLeaveNo = b.identity_column " +
                            "LEFT JOIN seihaHRMIS.dbo.HREmpInfo c On b.empNo = c.EmpNo " +
                            "where a.notView = 1 and convert(varchar, a.notDate, 101) = convert(varchar, getdate(), 101) and c.EmpDept = '" + getUserDept + "' order by a.notDate desc ";

                }
                else if (getUserDept == "MGR")
                {
                    sQuery = "Select a.notEmpno, (Select EmpFName from seihaHRMIS.dbo.HREmpInfo where empno = b.empNo) as Name, (Select EmpFName from seihaHRMIS.dbo.HREmpInfo where empno = a.notwho) as Who, " +
                            "(Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=b.empTOL) as Type, " +
                            "b.empReason,  a.notType, a.notLeaveNo, a.notType from seihaHRMIS.dbo.notInfo a LEFT JOIN seihaHRMIS.dbo.HRLeaveInfo b ON a.notLeaveNo = b.identity_column " +
                            "where a.notView = 1 and convert(varchar, a.notDate, 101) = convert(varchar, getdate(), 101) order by a.notDate desc ";
                }
                else
                {
                    sQuery = "Select a.notEmpno, (Select EmpFName from seihaHRMIS.dbo.HREmpInfo where empno = b.empNo) as Name, (Select EmpFName from seihaHRMIS.dbo.HREmpInfo where empno = a.notwho) as Who, " +
                            "(Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=b.empTOL) as Type, " +
                            "b.empReason,  a.notType, a.notLeaveNo from seihaHRMIS.dbo.notInfo a LEFT JOIN seihaHRMIS.dbo.HRLeaveInfo b ON a.notLeaveNo = b.identity_column " +
                            "where a.notView = 1 and convert(varchar, a.notDate, 101) = convert(varchar, getdate(), 101) and a.notEmpno = '" + getEmpNo + "' order by a.notDate desc ";
                }
            }
            
            dtQuery = HRMIS.Module.GetData(sQuery);
            if (dtQuery.Rows.Count > 0)
            {
                showBadge.Attributes["class"] = "badge bg-c-red";
                lblMessage.Visible = true;
                int count = checked(dtQuery.Rows.Count - 1);
                for (int x = 0; x <= count; x = checked(x + 1))
                {
                    string sStatement1 = "";
                    string chngOf = "";
                    if (dtQuery.Rows[x]["Type"].ToString().ToUpper() == "CHANGE OFF") { chngOf = ""; } else { chngOf = "Leave"; }
                    if (getEmpNo == dtQuery.Rows[x]["notEmpno"].ToString())
                    {
                        if (dtQuery.Rows[x]["notType"].ToString() != "")
                        {
                            LinkButton lnk = new LinkButton();
                            sStatement1 = "<li class='waves-effect waves-light'><div class='media'>";
                            Panel1.Controls.Add(new LiteralControl(sStatement1));
                            string lnkTxt = "<h5 class='notification-user'>" + dtQuery.Rows[x]["Who"].ToString() + "</h5>" +
                                            "<p class='notification-msg'>Has <strong style='font-size:14px;'>" + dtQuery.Rows[x]["notType"].ToString() + "</strong> <strong> Your </strong>'s</p>" +
                                            "<p class='notification-msg'>" + dtQuery.Rows[x]["Type"].ToString() + " " + chngOf + " Request</p>";
                            lnk.ID = "UM" + dtQuery.Rows[x]["notLeaveNo"].ToString();
                            lnk.Text = lnkTxt;
                            lnk.CssClass = "media-body";
                            //lnk.PostBackUrl = "~/LeaveDet?param=" + dtQuery.Rows[x]["notLeaveNo"].ToString() + "";
                            //lnk.Attributes["target"] = "_blank";
                            lnk.Attributes["onclick"] = "window.open('" + ResolveUrl("~/LeaveDet?param=" + dtQuery.Rows[x]["notLeaveNo"].ToString()) + "', '_blank'); return false;";
                            Panel1.Controls.Add(lnk);
                            Panel1.Controls.Add(new LiteralControl("</div></li>"));                 
                                            //"</div></div></li>";
                        }
                        else
                        {
                            LinkButton lnk = new LinkButton();
                            sStatement1 = "<li class='waves-effect waves-light'><div class='media'>";
                            Panel1.Controls.Add(new LiteralControl(sStatement1));
                            string lnkTxt = "<h5 class='notification-user'>You</h5>" +
                                            "<p class='notification-msg'>Had filed a <strong style='font-size:14px;'>" + dtQuery.Rows[x]["Type"].ToString() + "</strong> " + chngOf + "</p>" +
                                            "<p class='notification-msg'>Reason " + dtQuery.Rows[x]["empReason"].ToString() + " </p>";
                            lnk.ID = "U" + dtQuery.Rows[x]["notLeaveNo"].ToString();
                            lnk.Text = lnkTxt;
                            lnk.CssClass = "media-body";
                            //lnk.PostBackUrl = "~/LeaveDet?param=" + dtQuery.Rows[x]["notLeaveNo"].ToString() + "";
                            //lnk.Attributes["target"] = "_blank";
                            lnk.Attributes["onclick"] = "window.open('" + ResolveUrl("~/LeaveDet?param=" + dtQuery.Rows[x]["notLeaveNo"].ToString()) + "', '_blank'); return false;";
                            Panel1.Controls.Add(lnk);
                            Panel1.Controls.Add(new LiteralControl("</div></li>"));
                                            //"</div></div></li>";
                        }
                    }
                    else
                    {
                        if (dtQuery.Rows[x]["notType"].ToString() != "")
                        {
                            LinkButton lnk = new LinkButton();
                            sStatement1 = "<li class='waves-effect waves-light'><div class='media'>";
                            Panel1.Controls.Add(new LiteralControl(sStatement1));
                            string lnkTxt = "<h5 class='notification-user'>" + dtQuery.Rows[x]["Who"].ToString() + "</h5>" +
                                             "<p class='notification-msg'>Has <strong style='font-size:14px;'>" + dtQuery.Rows[x]["notType"].ToString() + "</strong> <strong>" + dtQuery.Rows[x]["Name"].ToString() + "</strong>'s</p>" +
                                             "<p class='notification-msg'>" + dtQuery.Rows[x]["Type"].ToString() + " " + chngOf + " Request</p>";
                            if (dtQuery.Rows[x]["notType"].ToString().ToUpper().Contains("ADMIN"))
                            {
                                lnk.ID = "AM" + dtQuery.Rows[x]["notLeaveNo"].ToString();
                            }
                            else
                            {
                                lnk.ID = "M" + dtQuery.Rows[x]["notLeaveNo"].ToString();
                            }
                            
                            lnk.Text = lnkTxt;
                            lnk.CssClass = "media-body";
                            //lnk.PostBackUrl = "~/LeaveDet?param=" + dtQuery.Rows[x]["notLeaveNo"].ToString() + "";
                            //lnk.Attributes["target"] = "_blank";
                            lnk.Attributes["onclick"] = "window.open('" + ResolveUrl("~/LeaveDet?param=" + dtQuery.Rows[x]["notLeaveNo"].ToString()) + "', '_blank'); return false;";
                            Panel1.Controls.Add(lnk);
                            Panel1.Controls.Add(new LiteralControl("</div></li>")); 
                                             //"</div></div></li>";
                        }
                        else
                        {
                            LinkButton lnk = new LinkButton();
                            sStatement1 = "<li class='waves-effect waves-light'><div class='media'>";
                            Panel1.Controls.Add(new LiteralControl(sStatement1));
                            string lnkTxt = "<h5 class='notification-user'>" + dtQuery.Rows[x]["Name"].ToString() + "</h5>" +
                                            "<p class='notification-msg'>Has filed a <strong style='font-size:14px;'>" + dtQuery.Rows[x]["Type"].ToString() + "</strong> " + chngOf + "</p>" +
                                            "<p class='notification-msg'>Reason " + dtQuery.Rows[x]["empReason"].ToString() + " </p>";
                            lnk.ID ="F" + dtQuery.Rows[x]["notLeaveNo"].ToString();
                            lnk.Text = lnkTxt;
                            lnk.CssClass = "media-body";
                            //lnk.PostBackUrl = "~/LeaveDet?param=" + dtQuery.Rows[x]["notLeaveNo"].ToString() + "";
                            //lnk.Attributes["target"] = "_blank";
                            lnk.Attributes["onclick"] = "window.open('" + ResolveUrl("~/LeaveDet?param=" + dtQuery.Rows[x]["notLeaveNo"].ToString()) + "', '_blank'); return false;";
                            Panel1.Controls.Add(lnk);
                            Panel1.Controls.Add(new LiteralControl("</div></li>")); 
                                            //"</div></div></li>";
                        }
                    }
                   
                    
                    //Panel1.Controls.Add(new LiteralControl(sStatement1));
                }


            }
            else
            {
                showBadge.Attributes["class"] = "";
                lblMessage.Visible = false;

            }
        }
    }
}


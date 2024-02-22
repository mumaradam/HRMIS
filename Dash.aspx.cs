using System;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRMIS
{
    public partial class Dash : System.Web.UI.Page
    {
        private static DataTable dtQuery;
        int TMonth = 0;
        int TYear = 0;
        int Nmonth = 0;
        private static string getEmpNo = "";
        private static string getUserAdmin = "";
        private static string getPosUser = "";
        private static string getDepart = "";
        private static int getDNow = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            getEmpNo = Session["Uname"] as string;
            TMonth = DateTime.Now.Month;
            TYear = DateTime.Now.Year;
            getDNow = DateTime.Now.Day;
            Nmonth = DateTime.Now.AddMonths(1).Month;
            DateTime Ndt = DateTime.Now.AddMonths(1);
            if(!IsPostBack){
                getInitInfo();
                if (getEmpNo == "0169")
                {
                    getUpBirthday();
                    getToBirthday();
                    getAnniv();
                    //getAllLeave();
                    lblTmonth.InnerHtml = "<img src='images/balloons.gif' style='height: 25px; width: 25px;'>" + "Upcoming Birthdays this " + Ndt.ToString("MMMM");
                }
                else
                {
                    getUpBirthday();
                    getToBirthday();
                    getAnniv();
                    //getAllLeave();
                    //getTodayBDay();
                    lblTmonth.InnerHtml = "<img src='images/balloons.gif' style='height: 25px; width: 25px;'>" + "Upcoming Birthdays this " + Ndt.ToString("MMMM");
                }
              
                
            }
            else
            {
                //getAllLeave();
            }
            //lblTmonth.InnerText ="Upcoming Birthdays this " + Ndt.ToString("MMMM");
        }
        protected void lblbtnDash_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dash");
        }
        protected void btnRefreshBirth_Click(object sender, EventArgs e)
        {
            getUpBirthday();
        }
        protected void lnkbtnViewLeave_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Leaves");
        }
        private void getInitInfo()
        {
            dtQuery = null;
            string sQuery = "Select * from seihaHRMIS.dbo.HREmpInfo where empno = '" + getEmpNo + "'";
            dtQuery = HRMIS.Module.GetData(sQuery);
            if (dtQuery.Rows.Count > 0)
            {
                getDepart = dtQuery.Rows[0]["empdept"].ToString();
                getUserAdmin = dtQuery.Rows[0]["empadmin"].ToString();
                getPosUser = dtQuery.Rows[0]["emppos"].ToString();
                getNumbers(getEmpNo, getUserAdmin);
            }
        }
        private void getAllLeave()
        {
            try
            {
                dtQuery = null;
                string dtLeaveList = "";
                if (getUserAdmin == "1")
                {
                    dtLeaveList = "select top 10 b.empno, (b.EmpFName + ' ' + b.EmpLName) as Name, (select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = b.empPos and cspopupfor = 'POS') as Position, " +
                                  "(convert(varchar, a.empdatefrom, 107) + ' - ' +  convert(varchar, a.empdateto, 107)) as LDate , DATEDIFF(day, a.empdatefrom, a.empdateto) + 1 AS days, b.empgen, " +
                                  "(Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = a.empTOL and cspopupfor = 'TOL') as Type, a.empReason, a.identity_column as ID, " +
                                  "(Select top 1 new_fileloc from seihaHRMIS.dbo.emp_pic where empno = b.empno) as 'picloc' from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empNo = b.EmpNo where a.empStatus = 0 order by empdate desc";
                }
                else
                {
                    if (getPosUser == "ITM" || getPosUser == "TMR" || getPosUser == "SMR")
                    {
                        dtLeaveList = "select top 10 b.empno, (b.EmpFName + ' ' + b.EmpLName) as Name, (select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = b.empPos and cspopupfor = 'POS') as Position, " +
                                      "(convert(varchar, a.empdatefrom, 107) + ' - ' +  convert(varchar, a.empdateto, 107)) as LDate , DATEDIFF(day, a.empdatefrom, a.empdateto) + 1 AS days, b.empgen, " +
                                      "(Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = a.empTOL and cspopupfor = 'TOL') as Type, a.empReason, a.identity_column as ID, " +
                                      "(Select top 1 new_fileloc from seihaHRMIS.dbo.emp_pic where empno = b.empno) as 'picloc' from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empNo = b.EmpNo where a.empStatus = 0 and b.empDept = '" + getDepart + "' order by empdate desc";
                    }
                    else if (getDepart == "MGR")
                    {
                        dtLeaveList = "select top 10 b.empno, (b.EmpFName + ' ' + b.EmpLName) as Name, (select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = b.empPos and cspopupfor = 'POS') as Position, " +
                                  "(convert(varchar, a.empdatefrom, 107) + ' - ' +  convert(varchar, a.empdateto, 107)) as LDate , DATEDIFF(day, a.empdatefrom, a.empdateto) + 1 AS days, b.empgen, " +
                                  "(Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = a.empTOL and cspopupfor = 'TOL') as Type, a.empReason, a.identity_column as ID, " +
                                  "(Select top 1 new_fileloc from seihaHRMIS.dbo.emp_pic where empno = b.empno) as 'picloc' from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empNo = b.EmpNo where a.empStatus = 0 order by empdate desc";
                    }
                    else
                    {
                        dtLeaveList = "select top 10 b.empno, (b.EmpFName + ' ' + b.EmpLName) as Name, (select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = b.empPos and cspopupfor = 'POS') as Position, " +
                                      "(convert(varchar, a.empdatefrom, 107) + ' - ' +  convert(varchar, a.empdateto, 107)) as LDate , DATEDIFF(day, a.empdatefrom, a.empdateto) + 1 AS days, b.empgen, " +
                                      "(Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = a.empTOL and cspopupfor = 'TOL') as Type, a.empReason, a.identity_column as ID, " +
                                      "(Select top 1 new_fileloc from seihaHRMIS.dbo.emp_pic where empno = b.empno) as 'picloc' from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empNo = b.EmpNo where a.empStatus = 0 and b.empNo = '" + getEmpNo + "' order by empdate desc";
                    }

                }
                dtQuery = HRMIS.Module.GetData(dtLeaveList);
                if (dtQuery.Rows.Count > 0)
                {
                    int count = checked(dtQuery.Rows.Count - 1);
                    string sStatement1 = "<div class='table'><table class='table table-hover'><thead><tr>" +
                                         "<th>Name</th><th>Leave Type</th><th>Leave Date</th><th class='text-center'>Action</th></tr></thead><tbody>";
                    Panel4.Controls.Add(new LiteralControl(sStatement1));
                    for (int x = 0; x <= count; x = checked(x + 1))
                    {
                        string tol = "";
                        string PicGen = "";
                        Button button = new Button();
                        tol = dtQuery.Rows[x]["Type"].ToString();
                        if (dtQuery.Rows[x]["picloc"].ToString() != "")
                        {
                            string strFilePath = dtQuery.Rows[x]["picloc"].ToString();
                            string base64String = GetBase64ImageString(strFilePath);
                            string dataUri = "data:image/jpeg;base64," + base64String;
                            PicGen = dataUri;
                        }
                        else
                        {
                            if (dtQuery.Rows[x]["empgen"].ToString() == "0") { PicGen = "images/img_avatar.png"; } else { PicGen = "images/img_avatar2.png"; }
                        }
                        
                        
                        string sStatement = "<tr><td><div class='d-inline-block align-middle'>"+
                                            "<img src='" + PicGen + "' alt='user image' class='img-radius img-40 align-top m-r-15' style='height: 40px;'>" +
                                            "<div class='d-inline-block'><h6>" + dtQuery.Rows[x]["Name"].ToString() + "</h6>"+
                                            "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["Position"].ToString() + "</p></div></div></td>" +
                                                        "<td>" + tol + "</td>" +
                                                        "<td class='mytooltip tooltip-effect-5'><span class='tooltip-item' style='padding: 1.25rem 0.75rem; background: none;'>" + dtQuery.Rows[x]["LDate"].ToString() + "</span><span class='tooltip-content clearfix' style='margin-left: -450px; width: 550px; height: 100px;'><p class='tooltip-text' style='font-size:14px; white-space: normal; height:100px;'>" + dtQuery.Rows[x]["empReason"].ToString() + "</p></span></td>" +
                                                        "<td class'text-right>";
                        Panel4.Controls.Add(new LiteralControl(sStatement));
                        button.ID = dtQuery.Rows[x]["ID"].ToString();
                        button.Text = "View More";
                        button.CssClass = "btn btn-info btn-round waves-effect waves-light";
                        button.PostBackUrl = "~/LeaveDet?param=" + dtQuery.Rows[x]["ID"].ToString() + "";
                        Panel4.Controls.Add(button);

                    }
                    Panel4.Controls.Add(new LiteralControl("</td></tr></tbody></table><div class='text-right m-r-20'><a href='#!' class='b-b-primary text-primary'>View all Leave</a></div>"));
                }
                else
                {
                    string sStatement1 = "<div class='table'><table class='table table-hover'><thead><tr><th>Name</th>" +
                                         "<th>Leave Type</th><th>Leave Date</th><th class='text-right'>Action</th></tr></thead><tbody>" +
                                         "</tbody></table><div class='text-right m-r-20'><a href='#!' class='b-b-primary text-primary'>View all Leave</a></div></div>";
                    Panel4.Controls.Add(new LiteralControl(sStatement1));
                }
            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        private string GetBase64ImageString(string imagePath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
        private void getUpBirthday()
        {
            try
            {
                dtQuery = null;
                string dtUpBirth = "";
                dtUpBirth = "Select EmpFName + ' ' + EmpLName as Name, convert(varchar(6), EmpDOB, 7) as BDate, EmpGen, new_fileloc from seihaHRMIS.dbo.HREmpInfo a LEFT JOIN " +
                            "seihaHRMIS.dbo.emp_pic b ON a.empno = b.empno where month(EmpDOB) = " + Nmonth + " order by day(EmpDOB)";
                dtQuery = HRMIS.Module.GetData(dtUpBirth);
                if (dtQuery.Rows.Count > 0)
                {
                    int count = checked(dtQuery.Rows.Count - 1);
                    for (int x = 0; x <= count; x = checked(x + 1))
                    {

                        if (dtQuery.Rows[x]["EmpGen"].ToString() == "0")
                        {
                            Literal1.Text = Literal1.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='images/img_avatar.png'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></div>";

                        }
                        else
                        {
                            Literal1.Text = Literal1.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='images/img_avatar2.png'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></div>";

                        }


                        //if (dtQuery.Rows[x]["new_fileloc"].ToString() != "")
                        //{
                        //    string strFilePath = dtQuery.Rows[x]["new_fileloc"].ToString();
                        //    string base64String = GetBase64ImageString(strFilePath);
                        //    string dataUri = "data:image/jpeg;base64," + base64String;

                        //    Literal1.Text = Literal1.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='" + dataUri + "' style='height: 40px;'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></div>";
                        //}
                        //else
                        //{
                        //    if (dtQuery.Rows[x]["EmpGen"].ToString() == "0")
                        //    {
                        //        Literal1.Text = Literal1.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='images/img_avatar.png'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></div>";

                        //    }
                        //    else
                        //    {
                        //        Literal1.Text = Literal1.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='images/img_avatar2.png'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></div>";

                        //    }
                        //}
                        
                    }
                }

            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        private void getToBirthday()
        {
            try
            {
                dtQuery = null;
                string dtUpBirth = "";
                dtUpBirth = "Select EmpFName + ' ' + EmpLName as Name, convert(varchar(6), EmpDOB, 7) as BDate, EmpGen, new_fileloc from seihaHRMIS.dbo.HREmpInfo a LEFT JOIN " +
                            " seihaHRMIS.dbo.emp_pic b ON a.empno = b.empno where month(EmpDOB) = " + TMonth + " order by day(EmpDOB)";
                dtQuery = HRMIS.Module.GetData(dtUpBirth);
                if (dtQuery.Rows.Count > 0)
                {
                    int count = checked(dtQuery.Rows.Count - 1);
                    for (int x = 0; x <= count; x = checked(x + 1))
                    {

                        if (dtQuery.Rows[x]["EmpGen"].ToString() == "0")
                        {
                            Literal3.Text = Literal3.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='images/img_avatar.png'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></div>";
                        }
                        else
                        {
                            Literal3.Text = Literal3.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='images/img_avatar2.png'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></div>";
                        }

                        //if (dtQuery.Rows[x]["new_fileloc"].ToString() != "")
                        //{
                        //    string strFilePath = dtQuery.Rows[x]["new_fileloc"].ToString();
                        //    string base64String = GetBase64ImageString(strFilePath);
                        //    string dataUri = "data:image/jpeg;base64," + base64String;

                        //    Literal3.Text = Literal3.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='" + dataUri + "' style='height: 40px;'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></div>";
                        //}
                        //else
                        //{
                        //    if (dtQuery.Rows[x]["EmpGen"].ToString() == "0")
                        //    {
                        //        Literal3.Text = Literal3.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='images/img_avatar.png'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></div>";
                        //    }
                        //    else
                        //    {
                        //        Literal3.Text = Literal3.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='images/img_avatar2.png'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></div>";
                        //    }
                        //}
                       
                    }
                }

            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        private void getAnniv()
        {
            try
            {
                dtQuery = null;
                string dtUpBirth = "";
                //dtUpBirth = "Select EmpFName + ' ' + EmpLName as Name, EmpDOH, EmpGen, " +
                //            "(CASE WHEN (CAST((DATEDIFF(DD, EmpDOH, getdate())) / 365.25 as int)) = 0  THEN ((CAST((DATEDIFF(DD, EmpDOH, getdate())) / 365.25 as int)) + 1) ELSE (CAST((DATEDIFF(DD, EmpDOH, getdate())) / 365.25 as int)) END) as ADate " +
                //            "from seihaHRMIS.dbo.HREmpInfo where month(EmpDOH) = " + TMonth + " order by ADate";
                dtUpBirth = "Select EmpFName + ' ' + EmpLName as Name, EmpDOH, EmpGen, " +
                            "(year(GETDATE()) - year(EmpDOH)) as ADate, new_fileloc " +
                            "from seihaHRMIS.dbo.HREmpInfo a LEFT JOIN seihaHRMIS.dbo.emp_pic b ON a.empno = b.empno where month(EmpDOH) = " + TMonth + " and (year(GETDATE()) - year(EmpDOH)) >= 1 order by ADate";
                dtQuery = HRMIS.Module.GetData(dtUpBirth);
                if (dtQuery.Rows.Count > 0)
                {
                    int count = checked(dtQuery.Rows.Count - 1);
                    for (int x = 0; x <= count; x = checked(x + 1))
                    {
                        if (dtQuery.Rows[x]["EmpGen"].ToString() == "0")
                        {
                            Literal2.Text = Literal2.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='images/img_avatar.png'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["ADate"].ToString() + " year(s)</p></div></div>";
                        }
                        else
                        {
                            Literal2.Text = Literal2.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='images/img_avatar2.png'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["ADate"].ToString() + " year(s)</p></div></div>";
                        }

                        //if (dtQuery.Rows[x]["new_fileloc"].ToString() != "")
                        //{
                        //    string strFilePath = dtQuery.Rows[x]["new_fileloc"].ToString();
                        //    string base64String = GetBase64ImageString(strFilePath);
                        //    string dataUri = "data:image/jpeg;base64," + base64String;
                        //    Literal2.Text = Literal2.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='" + dataUri + "' style='height: 40px;'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["ADate"].ToString() + " year(s)</p></div></div>";
                        //}
                        //else
                        //{
                        //    if (dtQuery.Rows[x]["EmpGen"].ToString() == "0")
                        //    {
                        //        Literal2.Text = Literal2.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='images/img_avatar.png'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["ADate"].ToString() + " year(s)</p></div></div>";
                        //    }
                        //    else
                        //    {
                        //        Literal2.Text = Literal2.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='images/img_avatar2.png'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["ADate"].ToString() + " year(s)</p></div></div>";
                        //    }
                        //}
                        
                    }
                }

            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        private void getTodayBDay()
        {
            try
            {
                dtQuery = null;
                string dtBirthNow = "";
                dtBirthNow = "Select EmpFName + ' ' + EmpLName as Name, convert(varchar(6), EmpDOB, 7) as BDate, EmpGen, new_fileloc from seihaHRMIS.dbo.HREmpInfo a LEFT JOIN seihaHRMIS.dbo.emp_pic b ON a.empno = b.empno where month(EmpDOB) = " + TMonth + " and day(EmpDOB) = " + getDNow + " order by day(EmpDOB)";
                dtQuery = HRMIS.Module.GetData(dtBirthNow);
                if (dtQuery.Rows.Count > 0)
                {
                    int count = checked(dtQuery.Rows.Count - 1);
                    for (int x = 0; x <= count; x = checked(x + 1))
                    {
                        if (dtQuery.Rows[x]["EmpGen"].ToString() == "0")
                        {
                            Literal5.Text = Literal5.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='images/img_avatar.png'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></div>";
                        }
                        else
                        {
                            Literal5.Text = Literal5.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='images/img_avatar2.png'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></div>";
                        }

                        //if (dtQuery.Rows[x]["new_fileloc"].ToString() != "")
                        //{
                        //    string strFilePath = dtQuery.Rows[x]["new_fileloc"].ToString();
                        //    string base64String = GetBase64ImageString(strFilePath);
                        //    string dataUri = "data:image/jpeg;base64," + base64String;
                        //    Literal5.Text = Literal5.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='" + dataUri + "' style='height: 40px;'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></div>";
                        //}
                        //else
                        //{
                        //    if (dtQuery.Rows[x]["EmpGen"].ToString() == "0")
                        //    {
                        //        Literal5.Text = Literal5.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='images/img_avatar.png'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></div>";
                        //    }
                        //    else
                        //    {
                        //        Literal5.Text = Literal5.Text + "<div class='align-middle m-b-30'><img alt='user image' class='img-radius img-40 align-top m-r-15' src='images/img_avatar2.png'><div class='d-inline-block'>" + dtQuery.Rows[x]["Name"].ToString() + "<p class='text-muted m-b-0'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></div>";
                        //    }
                        //}
                    }
                }

            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        private void getNumbers(string strEID, string strAdmin)
        {
            try
            {
                if (strAdmin == "1")
                {
                    int countFind = 0;
                    string findEmpNo = "";
                    findEmpNo = "Select count(*) from seihaHRMIS.dbo.HREmpInfo where empstatus=1";
                    countFind = int.Parse(HRMIS.Module.GetCount(findEmpNo));
                    if (countFind > 0)
                    {
                        totEmp.Text = countFind.ToString();
                    }
                    //--------------------
                    int countLeave = 0;
                    string findLeave = "";
                    findLeave = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where month(empdate) = " + DateTime.Now.Month + " and year(empdate) = " + DateTime.Now.Year + "";
                    countLeave = int.Parse(HRMIS.Module.GetCount(findLeave));
                    if (countLeave > 0)
                    {
                        totLeave.Text = countLeave.ToString();
                    }

                    int countLeaveM = 0;
                    string findLeaveM = "";
                    findLeaveM = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where convert(varchar, empdate, 101) = '" + DateTime.Now.ToString("MM/dd/yyyy") + "'";
                    countLeaveM = int.Parse(HRMIS.Module.GetCount(findLeaveM));
                    if (countLeaveM > 0)
                    {
                        totLeaveM.Text = countLeaveM.ToString();
                    }

                    int countLeaveA = 0;
                    string findLeaveA = "";
                    findLeaveA = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where empstatus = 1 and month(empdate) = " + DateTime.Now.Month + " and year(empdate) = " + DateTime.Now.Year + "";
                    countLeaveA = int.Parse(HRMIS.Module.GetCount(findLeaveA));
                    if (countLeaveA > 0)
                    {
                        totLeaveApp.Text = countLeaveA.ToString();
                    }
                }
                else
                {
                    empCount.Visible = false;
                    stayedComp.Visible = true;
                    int countLeave = 0;
                    string findLeave = "";
                    findLeave = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where empNo = '" + strEID + "'";
                    countLeave = int.Parse(HRMIS.Module.GetCount(findLeave));
                    if (countLeave > 0)
                    {
                        totLeave.Text = countLeave.ToString();
                        leavfiled.Visible = false;
                    }

                    int countLeaveM = 0;
                    string findLeaveM = "";
                    findLeaveM = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where empNo = '" + strEID + "' and convert(varchar, empdate, 101) = '" + DateTime.Now.ToString("MM/dd/yyyy") + "'";
                    countLeaveM = int.Parse(HRMIS.Module.GetCount(findLeaveM));
                    if (countLeaveM > 0)
                    {
                        totLeaveM.Text = countLeaveM.ToString();
                    }

                    int countLeaveA = 0;
                    string findLeaveA = "";
                    findLeaveA = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where empNo = '" + strEID + "' and empstatus = 1";
                    countLeaveA = int.Parse(HRMIS.Module.GetCount(findLeaveA));
                    if (countLeaveA > 0)
                    {
                        totLeaveApp.Text = countLeaveA.ToString();
                        apprLeave.Visible = false;
                    }

                    int countyrsStayd = 0;
                    string findyrsStayd = "";
                    findyrsStayd = "Select (DATEDIFF(YEAR, empDOH, GETDATE()) - (CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, empDOH, GETDATE()), empDOH) > GETDATE() THEN 1 ELSE 0 END)) AS years_stayed from seihaHRMIS.dbo.HREmpInfo where empNo = '" + strEID + "'";
                    countyrsStayd = int.Parse(HRMIS.Module.GetCount(findyrsStayd));
                    if (countyrsStayd > 0)
                    {
                        dayCalc.Text = countyrsStayd.ToString();
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

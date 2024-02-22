using System;
using System.Collections;
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
using System.Web.Services;

namespace HRMIS
{
    public partial class user_profile : System.Web.UI.Page
    {
        private static DataTable dtQuery;
        private static DataTable dtGetEmp;
        private static string getEmpUser = "";
        private static string getUserAdmin = "";
        private static string getUserDept = "";
        private static string getUserPos = "";
        private static string getUserRole = "";
        private static string DirecPath = "";
        private string getParam = "";
        private static string hldEmpNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            getEmpUser = Session["Uname"] as string;
            getParam = Request.QueryString["param"] as string;
            if (string.IsNullOrEmpty(getEmpUser))
            {
                Session.Abandon();
                Response.Redirect("~/Login");
            }
            else
            {
                getUserInfo();
                getUserLeaves();
                if (getEmpUser != hldEmpNo)
                {
                    breadEmp.Visible = true;
                }
                else if (getEmpUser == hldEmpNo)
                {
                    breadEmp.Visible = false;
                }
                //if (IsPostBack)
                //{
                //    string fileName = Path.GetFileName(FileUploadControl.PostedFile.FileName.ToString());
                //    if (fileName !="")
                //    {
                //        uploadPic();
                //    }
                //}
            }
        }
        public void savePic_Click(object sender, EventArgs e)
        {
                string fileName = Path.GetFileName(FileUploadControl.PostedFile.FileName.ToString());
                DirecPath = "//10.110.60.240/Database/HRMIS Profile_Pic/" + hldEmpNo;
                if (fileName != "")
                {
                    if (!Directory.Exists(DirecPath))
                    {
                       
                        Directory.CreateDirectory(DirecPath);
                        FileUploadControl.PostedFile.SaveAs(DirecPath + "/" + fileName);
                        savePic_data(DirecPath, true);
                    }
                    else
                    {
                        
                        FileUploadControl.PostedFile.SaveAs(DirecPath + "/" + fileName);
                        savePic_data(DirecPath, false);
                    }

                }
                //Response.Redirect(Request.Url.AbsoluteUri);
        }
        protected void uploadPic_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = Path.GetFileName(FileUploadControl.PostedFile.FileName.ToString());
                DirecPath = "//10.110.60.240/Database/HRMIS Profile_Pic/" + hldEmpNo;
                if (fileName != "")
                {
                    if (!Directory.Exists(DirecPath))
                    {
                        savePic_data(DirecPath, true);
                        Directory.CreateDirectory(DirecPath);
                        FileUploadControl.PostedFile.SaveAs(DirecPath + "/" + fileName);

                    }
                    else
                    {
                        savePic_data(DirecPath, false);
                        FileUploadControl.PostedFile.SaveAs(DirecPath + "/" + fileName);
                    }

                }
                //Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void lblbtnDash1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dash");
        }
        protected void lblbtnDash2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Emp");
        }
        protected void lblbtnDash_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        private void getUserInfo()
        {
            dtQuery = null;
            string sQuery = "Select * from seihaHRMIS.dbo.HREmpInfo where empno = '" + getEmpUser + "'";
            dtQuery = HRMIS.Module.GetData(sQuery);
            if (dtQuery.Rows.Count > 0)
            {
                getUserDept = dtQuery.Rows[0]["empDept"].ToString();
                getUserPos = dtQuery.Rows[0]["empPos"].ToString();
                getUserAdmin = dtQuery.Rows[0]["empadmin"].ToString();
                getUserRole = dtQuery.Rows[0]["emprole"].ToString();
                
                if (getParam != null)//if view from employee page
                {
                    dtGetEmp = null;
                    string sQuery1 = "Select * from seihaHRMIS.dbo.HREmpInfo where empno = '" + getParam + "'";
                    dtGetEmp = HRMIS.Module.GetData(sQuery1);
                    if (dtGetEmp.Rows.Count > 0)
                    {
                        lblFullName.Text = dtGetEmp.Rows[0]["empfname"].ToString() + ' ' + dtGetEmp.Rows[0]["emplname"].ToString();
                        lblPosition.Text = getPosition(dtGetEmp.Rows[0]["empPos"].ToString());
                        hldEmpNo = dtGetEmp.Rows[0]["empno"].ToString();
                        if (getUserAdmin == "1")
                        {
                            //allLeav.Visible = true;
                            edit_btn.Visible = true;
                        }
                        else
                        {
                            edit_btn.Visible = false;
                        }
                        displayPersonalInfo(dtGetEmp.Rows[0]["empno"].ToString());
                    }
                    
                }
                else//if vidw from account profile
                {
                    hldEmpNo = dtQuery.Rows[0]["empno"].ToString();
                    lblFullName.Text = dtQuery.Rows[0]["empfname"].ToString() + ' ' + dtQuery.Rows[0]["emplname"].ToString();
                    lblPosition.Text = getPosition(dtQuery.Rows[0]["empPos"].ToString());
                    displayPersonalInfo(dtQuery.Rows[0]["empno"].ToString());
                }
            }
            
        }
        private void displayPersonalInfo(string strID)
        {
            try
            {
                string sQuery = "Select (EmpFName + ' ' + EmpLName) as Name, convert(varchar, EmpDOB, 107) as BDate, * from seihaHRMIS.dbo.HREmpInfo a LEFT JOIN " +
                                " seihaHRMIS.dbo.emp_pic b ON a.empno = b.empno where a.empno = '" + strID + "'";
                dtQuery = HRMIS.Module.GetData(sQuery);
                if (dtQuery.Rows.Count > 0)
                {
                    txtFname.Text = dtQuery.Rows[0]["empfname"].ToString();
                    txtLname.Text = dtQuery.Rows[0]["emplname"].ToString();
                    txtMIname.Text = dtQuery.Rows[0]["empMI"].ToString();
                    txtDOB.Text = Convert.ToDateTime(dtQuery.Rows[0]["empDOB"]).ToString("yyyy-MM-dd");
                    int count = checked(dtQuery.Rows.Count - 1);
                    for (int x = 0; x <= count; x = checked(x + 1))
                    {
                        string gen = "";
                        if (dtQuery.Rows[0]["new_fileloc"].ToString() != "")
                        {
                            string strFilePath = dtQuery.Rows[0]["new_fileloc"].ToString();
                            string base64String = GetBase64ImageString(strFilePath);
                            string dataUri = "data:image/jpeg;base64," + base64String;

                            userpicSide2.ImageUrl = dataUri;
                        }
                        else
                        {
                            if (dtQuery.Rows[0]["empgen"].ToString() == "0")
                            {
                                gen = "Male";

                                userpicSide2.ImageUrl = "images/img_avatar.png";
                            }
                            else
                            {
                                gen = "Female";
                                userpicSide2.ImageUrl = "images/img_avatar2.png";
                            }
                        }
                        
                        string sStatement1 = "<tr><th scope='row'>Full Name</th><td>" + dtQuery.Rows[0]["Name"].ToString() + "</td></tr> " +
                                             "<tr><th scope='row'>Gender</th><td>" + gen + "</td></tr> " +
                                             "<tr><th scope='row'>Birth Date</th><td>" + dtQuery.Rows[0]["BDate"].ToString() + "</td></tr> " +
                                             "<tr><th scope='row'>Age</th><td></td></tr> " +
                                             "<tr><th scope='row'>Marital Status</th><td></td></tr> " +
                                             "<tr><th scope='row'>Mobile Number</th><td>" + dtQuery.Rows[0]["empconno"].ToString() + "</td></tr> " +
                                             "<tr><th scope='row'>Email</th><td>" + dtQuery.Rows[0]["empemail"].ToString() + "</td></tr>";
                        AboutMePanel.Controls.Add(new LiteralControl(sStatement1));
                        string sStatement2 = "<tr><th scope='row'>Home/Blk No</th><td></td></tr> " +
                                             "<tr><th scope='row'>Street/Subdivision</th><td></td></tr> " +
                                             "<tr><th scope='row'>Barangay</th><td></td></tr> " +
                                             "<tr><th scope='row'>City/Municipality</th><td></td></tr> " +
                                             "<tr><th scope='row'>Country</th><td></td></tr>" +
                                             "<tr><th scope='row'>Zip Code</th><td></td></tr>" +
                                             "<tr><th scope='row'>Provincial Address</th><td>" + dtQuery.Rows[0]["empadd"].ToString() + "</td></tr>";
                        AboutMePanel2.Controls.Add(new LiteralControl(sStatement2));
                    }
                }
               
            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        private void displayEmployerInfo(string strID)
        {

        }
        private string GetBase64ImageString(string imagePath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
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
        
        private void savePic_data(string strPath, Boolean flg)
        {
            string[] filePaths = Directory.GetFiles(strPath);
            foreach (string filePath in filePaths)
            {
                if (filePath != "")
                {
                    if(flg == false)
                    {
                        string strhldOld = HRMIS.Module.GetField("Select new_fileloc from seihaHRMIS.dbo.emp_pic where empno = '" + hldEmpNo + "'");
                        string sQueryDetail = "UPDATE seihaHRMIS.dbo.emp_pic set file_name ='" + Path.GetFileName(filePath) + "', new_fileloc = '" + filePath + "', " +
                                              "old_fileloc = '" + strhldOld + "' where empno = '" + hldEmpNo + "')";
                        HRMIS.Module.gblInsert(sQueryDetail);
                        Response.Redirect(Request.RawUrl);

                    }
                    else
                    {
                        string sQueryDetail = "Insert into seihaHRMIS.dbo.emp_pic(empNo, file_name, new_fileloc, old_fileloc) " +
                                    "values('" + hldEmpNo + "', '" + Path.GetFileName(filePath) + "', '" + filePath + "', '')";

                        HRMIS.Module.gblInsert(sQueryDetail);
                        Response.Redirect(Request.RawUrl);
                    }
                    
                }
            }
        }
        protected void getUserLeaves()
        {
            try
            {
                string dtLeaveList = "";
                if (getParam != null)//if view from employee page
                {
                    dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                             " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=empTOL) as Type, (select empPos from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Pos, " +
                             "(select empDept from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Dep, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=empStatus) as Stat, " +
                             "convert(varchar, empdate, 107) as DateFiled, *  from seihaHRMIS.dbo.HRLeaveInfo where empNo = '" + getParam + "' order by identity_column desc";
                }
                else
                {
                    dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                             " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=empTOL) as Type, (select empPos from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Pos, " +
                             "(select empDept from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Dep, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=empStatus) as Stat, " +
                             "convert(varchar, empdate, 107) as DateFiled, *  from seihaHRMIS.dbo.HRLeaveInfo where empNo = '" + getEmpUser + "' order by identity_column desc";
                }
                
                dtQuery = HRMIS.Module.GetData(dtLeaveList);
                if (dtQuery.Rows.Count > 0)
                {
                    int count = checked(dtQuery.Rows.Count - 1);
                    for (int x = 0; x <= count; x = checked(x + 1))
                    {
                        Button button = new Button();
                        string Lstat = "";
                        if (dtQuery.Rows[x]["Stat"].ToString() == "Pending") { Lstat = "lightgray"; }
                        else if (dtQuery.Rows[x]["Stat"].ToString() == "Approved") { Lstat = "palegreen"; }
                        else if (dtQuery.Rows[x]["Stat"].ToString() == "Denied") { Lstat = "palevioletred"; }
                        else if (dtQuery.Rows[x]["Stat"].ToString() == "Cancelled") { Lstat = "red"; }
                        string sStatement1 = "<tr><td>" + dtQuery.Rows[x]["DateFiled"].ToString() + "</td>" +
                                         "<td>" + dtQuery.Rows[x]["Type"].ToString() + "</td>" +
                                         "<td>" + dtQuery.Rows[x]["FDate"].ToString() + " - " + dtQuery.Rows[x]["TDate"].ToString() + "</td>" +
                                         "<td>" + dtQuery.Rows[x]["days"].ToString() + "</td>" +
                                         "<td style='background-color:" + Lstat + ";'>" + dtQuery.Rows[x]["Stat"].ToString() + "</td>" +
                                         "<td style='text-wrap: wrap;'>" + dtQuery.Rows[x]["empReason"].ToString() + "</td>";
                                         //"<td>";
                        Panel1.Controls.Add(new LiteralControl(sStatement1));
                        //button.ID = dtQuery.Rows[x]["identity_column"].ToString();
                        //button.Text = "View";
                        //button.ClientIDMode = ClientIDMode.Static;
                        //button.CssClass = "btn btn-out-dashed waves-effect waves-light btn-primary btn-square";
                        //button.Click += Button_Click;
                        //button.PostBackUrl = "~/LeaveDet.aspx?param=" + dtQuery.Rows[x]["identity_column"].ToString() + "";
                        //Panel1.Controls.Add(button);
                        Panel1.Controls.Add(new LiteralControl("</tr>"));
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

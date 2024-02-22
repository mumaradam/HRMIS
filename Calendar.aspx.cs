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
using System.Globalization;
public class Day
{
    public int DayNumber { get; set; }
    public string CssClass { get; set; }
    public string Class { get; set; }
}
public partial class Calendar : System.Web.UI.Page
{
    OleDbConnection conn = new OleDbConnection();
    OleDbCommand sqlComm = new OleDbCommand();
    OleDbDataAdapter sqlDA = new OleDbDataAdapter();
    string getEmpNo = "";
    private static DataTable dtQuery;
    DateTime today = DateTime.Today;
    string dateString = "";
    protected List<Day> Days;
    static DateTime currentMonth;
    static string strDateNow = "";
    static string strNowDT = "";
    static string toDat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/404");
        //getEmpNo = Session["Uname"] as string;
        //lblReg.Visible = false;
        //getUserInfo();
        //strNowDT = DateTime.Now.Day.ToString();
        ////DateTime currentMonth = DateTime.ParseExact(dateString, "MMMM yyyy", CultureInfo.CurrentCulture);
        ////Days = GetDays(DateTime.Today);
        ////BindCalendar();
        ////displayCalen();
        //toDat = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + strNowDT;
        //if (!IsPostBack)
        //{
            
        //    dateString = DateTime.Now.ToString("MMMM yyyy");
        //    MonthYear.Text = dateString;
        //    //Days = GetDays(DateTime.Today);
        //    PopulateCalendar();
            
        //}
    }
    private void getUserInfo()
    {
        dtQuery = null;
        string sQuery = "Select * from seihaHRMIS.dbo.HREmpInfo where empno = '" + getEmpNo + "'";
        dtQuery = GetData(sQuery);
        if (dtQuery.Rows.Count > 0)
        {
            lblUser.Text = dtQuery.Rows[0]["empFname"].ToString();
            if (dtQuery.Rows[0]["empGen"].ToString() == "0")
            {
                UserPic.Attributes.Add("src", "images/img_avatar.png");
            }
            else
            {
                UserPic.Attributes.Add("src", "images/img_avatar2.png");
            }
        }
    }
    protected void PrevMonth_Click(object sender, EventArgs e)
    {
        currentMonth = DateTime.ParseExact(MonthYear.Text, "MMMM yyyy", CultureInfo.CurrentCulture);
        dateString = currentMonth.AddMonths(-1).ToString("MMMM yyyy");
        strDateNow = currentMonth.AddMonths(-1).ToString("MMMM yyyy");
        MonthYear.Text = dateString;
        //Days = GetDays(currentMonth.AddMonths(-1));
        PopulateCalendar();
    }

    protected void NextMonth_Click(object sender, EventArgs e)
    {
        currentMonth = DateTime.ParseExact(MonthYear.Text, "MMMM yyyy", CultureInfo.CurrentCulture);
        dateString = currentMonth.AddMonths(1).ToString("MMMM yyyy");
        strDateNow = currentMonth.AddMonths(1).ToString("MMMM yyyy");
        MonthYear.Text = dateString;
        //Days = GetDays(currentMonth.AddMonths(1));
        PopulateCalendar();
    }

    //protected List<Day> GetDays(DateTime month)
    //{
    //    var days = new List<Day>();

    //    // Get the first day and last day of the current month
    //    var firstDay = new DateTime(month.Year, month.Month, 1);
    //    var lastDay = new DateTime(month.Year, month.Month, DateTime.DaysInMonth(month.Year, month.Month));

    //    // Add the days to the calendar
    //    for (var i = 0; i < firstDay.DayOfWeek.GetHashCode(); i++)
    //    {
    //        days.Add(new Day
    //        {
    //            DayNumber = new DateTime(firstDay.Year, firstDay.Month, 1).AddDays(-firstDay.DayOfWeek.GetHashCode() + i +1 -1).Day,
    //            CssClass = "fc-day-top fc-other-month fc-past"
    //        });
    //    }

    //    for (var i = 1; i <= lastDay.Day; i++)
    //    {
    //        days.Add(new Day
    //        {
    //            DayNumber = i,
    //            CssClass = i == DateTime.Today.Day && month.Month == DateTime.Today.Month && month.Year == DateTime.Today.Year ? "fc-day-top fc-today alert alert-info" : ""
    //        });
    //    }

    //    var daysToAdd = 7 * days.Count % 41;
    //    if (daysToAdd < 7)
    //    {
    //        for (var i = 1; i <= daysToAdd; i++)
    //        {
    //            days.Add(new Day
    //            {
    //                DayNumber = new DateTime(lastDay.Year, lastDay.Month, lastDay.Day).AddDays(i).Day,
    //                CssClass = "fc-day-top fc-other-month fc-future"
    //            });
    //        }
    //    }

    //    return days;
    //}
    protected void PopulateCalendar()
    {
        TableRow headerRow = new TableRow();

        // Create the header cells and add them to the header row
        TableCell headerCell1 = new TableCell();
        headerCell1.Text = "SUN";
        headerRow.Cells.Add(headerCell1);
        headerCell1.Attributes.Add("class", "fc-day-header  fc-sun");
        headerCell1.Attributes.Add("style", "padding: 0.75rem; font-size:1.5rem;");

        TableCell headerCell2 = new TableCell();
        headerCell2.Text = "MON";
        headerRow.Cells.Add(headerCell2);
        headerCell2.Attributes.Add("class", "fc-day-header  fc-mon");
        headerCell2.Attributes.Add("style", "padding: 0.75rem; font-size:1.5rem;");

        TableCell headerCell3 = new TableCell();
        headerCell3.Text = "TUE";
        headerRow.Cells.Add(headerCell3);
        headerCell3.Attributes.Add("class", "fc-day-header  fc-tue");
        headerCell3.Attributes.Add("style", "padding: 0.75rem; font-size:1.5rem;");

        TableCell headerCell4 = new TableCell();
        headerCell4.Text = "WED";
        headerRow.Cells.Add(headerCell4);
        headerCell4.Attributes.Add("class", "fc-day-header  fc-wed");
        headerCell4.Attributes.Add("style", "padding: 0.75rem; font-size:1.5rem;");

        TableCell headerCell5 = new TableCell();
        headerCell5.Text = "THU";
        headerRow.Cells.Add(headerCell5);
        headerCell5.Attributes.Add("class", "fc-day-header  fc-thu");
        headerCell5.Attributes.Add("style", "padding: 0.75rem; font-size:1.5rem;");

        TableCell headerCell6 = new TableCell();
        headerCell6.Text = "FRI";
        headerRow.Cells.Add(headerCell6);
        headerCell6.Attributes.Add("class", "fc-day-header  fc-fri");
        headerCell6.Attributes.Add("style", "padding: 0.75rem; font-size:1.5rem;");

        TableCell headerCell7 = new TableCell();
        headerCell7.Text = "SAT";
        headerRow.Cells.Add(headerCell7);
        headerCell7.Attributes.Add("class", "fc-day-header  fc-sat");
        headerCell7.Attributes.Add("style", "padding: 0.75rem; font-size:1.5rem;");

        // Add the header row to the table's HeaderRow property
        CalendarTable.Rows.Add(headerRow);
        // Get the current date and the first day of the current month
        DateTime currentDate = DateTime.ParseExact(dateString, "MMMM yyyy", null);

        DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

        
        // Calculate the number of days in the current month
        
        int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
        var firstDay = new DateTime(currentDate.Year, currentDate.Month, 1);
        var lastDay = new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
        // Calculate the day of the week for the first day of the month
        int lstday = lastDay.Day;
        DayOfWeek firstDayOfWeek = firstDayOfMonth.DayOfWeek;
        int hlddy = 0;
        // Loop through each cell in the table and populate it with the appropriate date
        int dayOfMonth = 1;
        for (int i = 0; i < 6; i++)
        {
            
            TableRow row = new TableRow();
            for (int j = 0; j < 7; j++)
            {
                
                TableCell cell = new TableCell();
                if (i == 0 && j < (int)firstDayOfWeek)
                {
                    // Empty cell before the first day of the month
                    int dtfrst = new DateTime(firstDay.Year, firstDay.Month, 1).AddDays(-firstDay.DayOfWeek.GetHashCode() + j + 1 - 1).Day;
                    cell.Text = dtfrst.ToString();
                    cell.Attributes.Add("class", "fc-day-top fc-other-month fc-past");
                    cell.Attributes.Add("style", "padding-bottom: 83px;");
                }
                else if (dayOfMonth > daysInMonth)
                {
                    // Empty cell after the last day of the month
                    //DateTime firstDayOfNextMonth = new DateTime(lastDay.Year, lastDay.Month, lastDay.Day).AddDays(1);
                    DateTime date1 = new DateTime(firstDay.Year, firstDay.Month, 1);
                    DateTime date2 = new DateTime(lastDay.Year, lastDay.Month, lastDay.Day);
                    TimeSpan diff = date2 - date1;
                    int smpl = int.Parse(diff.TotalDays.ToString());
                    int dtst = date2.AddDays((smpl / 7 * 7 % 41) - 1).Day;
                    if(hlddy < dtst)
                    {
                        hlddy = hlddy + 1;
                        cell.Text = hlddy.ToString();
                        cell.Attributes.Add("class", "fc-day-top fc-other-month fc-past");
                        cell.Attributes.Add("style", "padding-bottom: 83px;");
                    }
                  
                        //int lastDayOfMonth = firstDayOfNextMonth.AddDays(1).Day;
                        // Add the last day of the month to the dictionary
                    
                }
                else
                {
                    // Cell with a date
                    string nxtDayCnt = currentDate.Year + "/"+ currentDate.Month + "/" +  dayOfMonth.ToString();
                    if (nxtDayCnt == toDat)
                    {
                        cell.Text = dayOfMonth.ToString();
                        cell.Attributes.Add("class", "fc-day fc-today alert alert-info");
                        cell.Attributes.Add("style", "padding-bottom: 83px;");
                    }
                    else
                    {
                        cell.Text = dayOfMonth.ToString();
                        cell.Attributes.Add("class", "fc-day-top fc-past");
                        cell.Attributes.Add("style", "padding-bottom: 83px;");
                    }
                    
                    dayOfMonth++;
                }
                row.Attributes.Add("style", "overflow: hidden;");
                row.Cells.Add(cell);
            }
            CalendarTable.Rows.Add(row);
        }
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
    protected void lblPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ChangePassword?accpass=" + getEmpNo);
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
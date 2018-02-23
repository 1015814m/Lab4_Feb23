using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using database;

public partial class AdminCreate : System.Web.UI.Page
{
    public string LastUpdatedBy = "Sean Marley";
    public static string year = DateTime.Now.ToString("yyyy");
    public static string month = DateTime.Now.Month.ToString("d2");
    public static string day = DateTime.Now.ToString("dd");
    public static string LastUpdate = year + "-" + month + "-" + day;
    public NewUser currentUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["employeeLoggedIn"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (Session["employeeLoggedIn"].ToString() != "True")
        {
            Response.Redirect("Login.aspx");
        }

        Employee user = (Employee)Session["user"];

        if (user.Admin != true)
        {
            Response.Redirect("HomePage.aspx");
        }
    }
    protected void Submit_Click(object sender, EventArgs e)
    {



        //------------------------------------INSERT INTO EMPLOYEELOGIN------------------------------------------------------------------------------------
        try
        {
            SqlConnection conn = ProjectDB.connectToDB();
            try
            {


                System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand();
                insert.Connection = conn;

                //-----------------------------GETS MAX EMPLOYEELOGINID--------------------------------------------------------------------------------------
                System.Data.SqlClient.SqlCommand select = new System.Data.SqlClient.SqlCommand();
                select.Connection = conn;
                select.CommandText = "(select max(([EmpLoginID]) +1) from[dbo].[EmployeeLogin])";
                var temp = select.ExecuteScalar();
                string maxID = temp.ToString();
                currentUser = new NewUser(FirstNameText.Text, LastNameText.Text, MiddleText.Text, EmailText.Text, maxID, LastUpdatedBy, LastUpdate);
                //-----------------------------GETS MAX EMPLOYEELOGINID--------------------------------------------------------------------------------------

                string username = (currentUser.getLastName() + currentUser.getFirstName().Substring(0, 1) + currentUser.getMiddleName()).ToLower();
                string password = "testPassword57";



                insert.CommandText = "INSERT INTO [dbo].[EmployeeLogin] ([UserName],[PasswordHash],[LastLogin],[LastUpdatedBy],[LastUpdated]) VALUES (@userName, @password, @lastLogin, @lastUpdatedBy, @lastUpdate)";

                insert.Parameters.AddWithValue("@userName", username);
                insert.Parameters.AddWithValue("@password", SimpleHash.ComputeHash(password, "MD5", null));
                insert.Parameters.AddWithValue("@lastLogin", DateTime.Now.ToString());
                insert.Parameters.AddWithValue("@lastUpdatedBy", LastUpdatedBy);
                insert.Parameters.AddWithValue("@lastUpdate", LastUpdate);

                insert.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('error inserting into DB for employeelogin Boiiiii')", true);
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('error connecting to DB')", true);
        }
        //--------------------------------------------END OF INSERT INTO EMPLOYEELOGIN-------------------------------------------------------


        //--------------------------------------------INSERTING INTO EMPLOYEE TABLE----------------------------------------------------------



        try
        {
            SqlConnection conn = ProjectDB.connectToDB();
            try
            {

                string points = "0";
                System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand();
                insert.Connection = conn;

                //------------------------------------------------------INSERTS WITH PARAMETERIZED QUERIES----------------------------------------------------
                insert.CommandText = "INSERT INTO[dbo].[Employee]([FirstName],[LastName],[Email],[LastUpdatedBy],[LastUpdated],[EmpLoginID],[Points]) VALUES(@firstName, @lastName, @email, @lastUpdatedBy, @lastUpdated, @empLoginID, @points)";



                insert.Parameters.AddWithValue("@firstName", currentUser.getFirstName());
                insert.Parameters.AddWithValue("@lastName", currentUser.getLastName());
                insert.Parameters.AddWithValue("@email", currentUser.getEmail());
                insert.Parameters.AddWithValue("@lastUpdatedBy", currentUser.getLastUpdatedBy());
                insert.Parameters.AddWithValue("@lastUpdated", currentUser.getLastUpdate());
                insert.Parameters.AddWithValue("@empLoginID", currentUser.getEmployeeLoginID());
                insert.Parameters.AddWithValue("@points", points);

                insert.ExecuteNonQuery();
                conn.Close();
                SuccessLabel.Text = "*User Successfully Created";
            }
            //------------------------------------------------------INSERTS WITH PARAMETERIZED QUERIES----------------------------------------------------
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('error inserting into DB for employee')", true);
            }

        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('error connecting to DB')", true);
        }
    }

    protected void EditButton_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection conn = ProjectDB.connectToDB();
            System.Data.SqlClient.SqlCommand joinTables = new System.Data.SqlClient.SqlCommand();
            joinTables.Connection = conn;

            joinTables.CommandText = "SELECT * FROM Employee as Current_Employees";
            GridView1.DataSource = joinTables.ExecuteReader();
            GridView1.DataBind();

            conn.Close();
        }

        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('lol you thought')", true);
        }
    }
    //------------------------------------------------------------------------
    //------------------------------------------------------------------------
    //------------------------------------------------------------------------


}  




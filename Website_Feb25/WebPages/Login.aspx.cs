using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using database;
using System.Data.SqlClient;



public partial class _Default : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["databaseName"] = "Lab4";

        
    }

    protected void EmployeeLogin_Authenticate(object sender, AuthenticateEventArgs e)
    {
        try
        {
            //the Login object has both UserName and Password properties
            string userName = employeeLogin.UserName;
            string password = employeeLogin.Password;

            //the authenticated property of the AutheticateEventArgs object is what
            //determines whether to authenticate the login or not...here we assume no
            e.Authenticated = false;

            //setting up SqlConnection and SqlCommand
            SqlConnection conn = ProjectDB.connectToDB();
            if (conn != null)
            {
                string commandText = "SELECT TOP 1 UserName, PasswordHash FROM [dbo].[EmployeeLogin] WHERE UserName = @UserName";

                SqlCommand select = new SqlCommand(commandText, conn);

                select.Parameters.AddWithValue("@UserName", userName);

                SqlDataReader reader = select.ExecuteReader();

                //if there is such a record, read it
                if (reader.HasRows)
                {
                    reader.Read();
                    String pwHash = reader["PasswordHash"].ToString(); //retrieve the password hash

                    String user = reader["UserName"].ToString();
                    Session["loggedInAs"] = user;

                    //user the SimpleHash object to verify the user's entered password
                    bool verify = SimpleHash.VerifyHash(password, "MD5", pwHash);

                    //the result of the VerifyHash is boolean; we use this to determine authentication
                    e.Authenticated = verify;
                    if (e.Authenticated == true)
                    {
                        getUserInfo(getLoginID(userName));
                        
                    }
                }

                conn.Close();

                Session["employeeLoggedIn"] = e.Authenticated.ToString();
            }
            else
            {
                errorMessage.Text += "\nThe connection to the database failed: " + conn;
            }

            if (e.Authenticated == false)
            {
                employeeLogin.FailureText = "Incorrect Login/Password";
            }
        }
        catch (Exception ex)
        {
            employeeLogin.FailureText = ex.ToString();
        }
    }

    protected void employeeLogin_LoggedIn(object sender, EventArgs e)
    {

        Response.Redirect("HomePage.aspx");   
    }

    protected void getUserInfo(int empLoginID)
    {
        try
        {
            int count = 0;
            string commandText = "SELECT TOP 1 EmployeeID, FirstName, LastName, Email, LastUpdated, LastUpdatedBy, Points " +
                "FROM [DBO].[EMPLOYEE] WHERE EmpLoginID = @EmpLoginID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);
            select.Parameters.AddWithValue("@EmpLoginID", empLoginID);

            SqlDataReader reader = select.ExecuteReader();


            //if there is data for the user read it
            if(reader.HasRows)
            {
                reader.Read();
                count++;
                int employeeID = (int)reader["EmployeeID"];
                String firstName = reader["FirstName"].ToString();
                String lastName = reader["LastName"].ToString();
                String email = reader["Email"].ToString();
                DateTime lastUpdated = (DateTime)reader["LastUpdated"];
                String lastUpdatedBy = reader["LastUpdatedBy"].ToString();
                Decimal points = (Decimal)reader["Points"];

                Employee user = new Employee(firstName, lastName, email, lastUpdated, lastUpdatedBy, empLoginID, false, points);
                Session["user"] = user;


                //Employee user2 = (Employee)Session["user"];
            }
            else
            {
                reader.Close();
            }
            if(count == 0) 
            {
                //if the user does not exist in the employee table check the administrator table for the employee
                commandText = "SELECT TOP 1 AdminID, FirstName, LastName, Email, LastUpdated, LastUpdatedBy" +
                    " FROM [DBO].[ADMINISTRATOR] WHERE EmpLoginID = @EmpLoginID";
                select = new SqlCommand(commandText, conn);
                select.Parameters.AddWithValue("@EmpLoginID", empLoginID);

                SqlDataReader adminReader = select.ExecuteReader();


                //if the administrator exists then read the data
                
                if (adminReader.HasRows)
                {
                    adminReader.Read();
                    int adminID = (int)adminReader["AdminID"];
                    String firstName = adminReader["FirstName"].ToString();
                    String lastName = adminReader["LastName"].ToString();
                    String email = adminReader["Email"].ToString();
                    DateTime lastUpdated = (DateTime)adminReader["LastUpdated"];
                    String lastUpdatedBy = adminReader["LastUpdatedBy"].ToString();

                    //create an employee object that is specific to the administrator i.e. the admin boolean is true
                    Employee user = new Employee(firstName, lastName, email, lastUpdated, lastUpdatedBy, empLoginID, true);

                    //create a session variable for the user logged in
                    Session["user"] = user;


                }
                else
                {
                    adminReader.Close();
                }
            }

            
            conn.Close();

        }
        catch (Exception ex)
        {
            
            errorMessage.Text += "\n" + ex;

        }
    }

    protected int getLoginID(string userName)
    {
        try
        {
            string commandText = "SELECT TOP 1 EmpLoginID FROM [DBO].[EMPLOYEELOGIN] WHERE UserName = @UserName";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);
            select.Parameters.AddWithValue("@UserName", userName);

            SqlDataReader reader = select.ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();
                int empLoginID = (int)reader["EmpLoginID"];
                conn.Close();
                return empLoginID;
            }
            else
            {
                return -1;
            }


        }
        catch (Exception ex)
        {
            errorMessage.Text += "\n" + ex;
            return -1;
        }
    }

    protected void ForgotPass_Click(object sender, EventArgs e)
    {
        Response.Redirect("PasswordReset.aspx");
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using database;
using System.Configuration;
using System.Data;

public partial class AdminCreate : System.Web.UI.Page
{
    Employee user;
    Control[] ctrlArray;
    public static int employeeID;
    
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

        user = (Employee)Session["user"];

        if (user.Admin != true)
        {
            Response.Redirect("HomePage.aspx");
        }
        ctrlArray = new Control[] { lblEditFirstName, lblEditLastName, lblEditEmail, txtEditFirstName, txtEditLastName, txtEditEmail, btnSubmit, btnCancel };

    }

    
    /// <summary>
    /// When the user clicks the submit button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Submit_Click(object sender, EventArgs e)
    {

        try
        {
            Boolean check = true;
            //validation that all entries are filled in
            if (txtFirstName.Text == "")
            {
                check = false;
                lblSuccess.Text = "Please enter a first name.";
                txtFirstName.Focus();
            }
            if (txtLastName.Text == "")
            {
                check = false;
                lblSuccess.Text = "Please enter a last name.";
                txtLastName.Focus();
            }
            if (txtEmail.Text == "")
            {
                check = false;
                lblSuccess.Text = "Please enter an email address.";
                txtEmail.Focus();
            }

            if (check)
            {
                Employee newEmployee = new Employee(txtFirstName.Text, txtLastName.Text, txtEmail.Text, DateTime.Now, (user.FirstName + " " + user.LastName));

                insertLogin(newEmployee);
                insertEmployee(newEmployee);
                lblSuccess.Text = "User successfully created! <br/>";
            }
            else
            {
                lblSuccess.Text += "User was not created. Please ensure all entries are valid.";
            }
        }
        catch (Exception)
        {

        }
    }

    /// <summary>
    /// Generates a random password between 6 and 12 characters
    /// </summary>
    /// <returns>Returns a string of characters</returns>
    protected string generatePassword()
    {
        
        string password = "";
        Random rnd = new Random();

        int charLength = rnd.Next(6, 12);
        string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";

        for (int i = 0; i < charLength; i++)
        {
            int a = rnd.Next(0, chars.Length - 1);
            password += chars.Substring(a, 1);
        }

        return password;
    }

    /// <summary>
    /// Checks the database for the user login
    /// </summary>
    /// <param name="username">Enter the username that you want to check</param>
    /// <returns>Returns a boolean(True = the user does not exist, False = the user already exists)</returns>
    protected Boolean checkForUser(string username)
    {
        int result = -1;
        try
        {
            string commandText = "SELECT COUNT(USERNAME) as Result FROM [DBO].[EMPLOYEELOGIN] WHERE USERNAME = @UserName";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@UserName", username);

            SqlDataReader reader = select.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                result = (int)reader["Result"];
            }

            conn.Close();
        }
        catch (Exception ex)
        {
            errorMessage.Text += "Error checking username" + ex;
        }

        if (result == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Finds the max employee login id to be used to enter the next
    /// </summary>
    /// <returns>Returns the max employee login id</returns>
    protected int findMaxLoginID()
    {
        int max = -1;
        try
        {
            string commandText = "SELECT MAX(EmpLoginID) as Result FROM [DBO].[EMPLOYEELOGIN]";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            SqlDataReader reader = select.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                max = (int)reader["Result"];

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            errorMessage.Text += "Error Finding Max " + ex;

        }
        return max;
    }

    /// <summary>
    /// Generates a user login from the first 5 of their lastname and the first letter of their first name
    /// </summary>
    /// <param name="employee">Pass the employee that was created</param>
    /// <returns>Returns a username</returns>
    protected string generateLogin(Employee employee)
    {
        string username = "";
        if (employee.LastName.Length < 5)
        {
            username += employee.LastName;
        }
        else
        {
            username += employee.LastName.Substring(0, 5);
        }
        username += employee.FirstName.Substring(0, 1);
        return username;
    }

    /// <summary>
    /// Insert employee login information into the database
    /// </summary>
    /// <param name="employee"></param>
    /// <param name="admin"></param>
    protected void insertLogin(Employee employee)
    {
        string username = "";
        string password = "";

        try
        {
            string commandText = "INSERT INTO [DBO].[EMPLOYEELOGIN] ([USERNAME],[PASSWORDHASH],[LASTLOGIN],[LASTUPDATEDBY],[LASTUPDATED]) " +
                "VALUES (@UserName, @PasswordHash, @LastLogin, @LastUpdatedBy, @LastUpdated)";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand insert = new SqlCommand(commandText, conn);

            if (checkForUser(generateLogin(employee)))
            {
                //if the username doesnt already exist then create a username
                username = generateLogin(employee);
                insert.Parameters.AddWithValue("@UserName", username);
            }
            else
            {
                //If the username already exists add a number onto the end of it
                Random rnd = new Random();
                username = generateLogin(employee) + rnd.Next(0, 9);
                insert.Parameters.AddWithValue("@UserName", username);
            }
            //generate a new password for the user
            password = generatePassword();
            lblPassword.Text = "Password: " + password;
            lblUserName.Text = "UserName: " + username;


            insert.Parameters.AddWithValue("@PasswordHash", SimpleHash.ComputeHash(password, "MD5", null));
            insert.Parameters.AddWithValue("@LastLogin", DateTime.Now);
            insert.Parameters.AddWithValue("@LastUpdatedBy", user.FirstName + " " + user.LastName);
            insert.Parameters.AddWithValue("@LastUpdated", DateTime.Now);

            insert.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception ex)
        {
            errorMessage.Text += "Error Inserting Login to DB " + ex;
        }
    }

    /// <summary>
    /// This method inserts the employee information into the database
    /// </summary>
    /// <param name="employee">This is the employee that we are committing to the database</param>
    /// <param name="admin">This is the user logged in</param>
    protected void insertEmployee(Employee employee)
    {
        try
        {
            string commandText = "INSERT INTO [DBO].[EMPLOYEE] ([FIRSTNAME],[LASTNAME],[EMAIL],[LASTUPDATEDBY],[LASTUPDATED],[EMPLOGINID],[POINTS]) " +
                "VALUES (@FirstName, @LastName, @Email, @LastUpdatedBy, @LastUpdated, @EmpLoginID, @Points)";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand insert = new SqlCommand(commandText, conn);

            //add the information via parameters
            insert.Parameters.AddWithValue("@FirstName", employee.FirstName);
            insert.Parameters.AddWithValue("@LastName", employee.LastName);
            insert.Parameters.AddWithValue("@Email", employee.Email);
            insert.Parameters.AddWithValue("@LastUpdatedBy", user.FirstName + " " + user.LastName);
            insert.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
            insert.Parameters.AddWithValue("@EmpLoginID", findMaxLoginID());
            insert.Parameters.AddWithValue("@Points", 0);

            //execute the insert statement
            insert.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception ex)
        {
            //if the insert statement fails then display the message
            errorMessage.Text += "Error Inserting Employee into DB" + ex;
        }
    }



    protected void employeeGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = employeeGrid.SelectedRow;
            employeeID = Int32.Parse(row.Cells[1].Text);
            string firstName = row.Cells[2].Text;
            string lastName = row.Cells[3].Text;
            string email = row.Cells[4].Text;

            txtEditFirstName.Text = firstName;
            txtEditLastName.Text = lastName;
            txtEditEmail.Text = email;
        }
        catch (Exception ex)
        {
            errorMessage.Text += "Error in GridView Selection " + ex;
        }

        editMode();
    }

    protected void editMode()
    {
        employeeGrid.Visible = false;

        foreach(Control element in ctrlArray )
        {
            element.Visible = true;
        }
        btnSubmit.Enabled = true;
        btnCancel.Enabled = true;
        txtEditFirstName.Focus();
    }

    protected void selectMode()
    {
        //this mode hides the text fields and buttons and show the gridview again
        employeeGrid.Visible = true;

        foreach(Control element in ctrlArray)
        {
            element.Visible = false;
        }
        btnSubmit.Enabled = false;
        btnCancel.Enabled = false;
        employeeGrid.DataBind();
        employeeGrid.Focus();
    }

    /// <summary>
    /// Commit the changes made to the text boxes populated after the gridview is selected
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void commitChanges(object sender, EventArgs e)
    {
        try
        {
            //establish connection
            string commandText = "UPDATE [DBO].[EMPLOYEE] set [FirstName] = @FirstName, [LastName] = @LastName, [Email] = @Email WHERE [EmployeeID] = @EmployeeID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand update = new SqlCommand(commandText, conn);

            //add values to the parameters
            update.Parameters.AddWithValue("@FirstName", txtEditFirstName.Text);
            update.Parameters.AddWithValue("@LastName", txtEditLastName.Text);
            update.Parameters.AddWithValue("@Email", txtEditEmail.Text);
            update.Parameters.AddWithValue("@EmployeeID", employeeID);

            //execute query, close connection, and refresh the gridview
            update.ExecuteNonQuery();
            conn.Close();
            employeeGrid.DataBind();
            selectMode();
        }
        catch (Exception ex)
        {
            errorMessage.Text += "Error committing edited user to the database. " + ex;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        selectMode();
    }
}  




using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using database;
using System.Data.SqlClient;

public partial class PasswordReset : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void resetPassword()
    {
        string newPass = "";
        if (txtUserForgot.Text != "")
        {
            try
            {
                string commandText = "UPDATE [dbo].[EmployeeLogin] set [PasswordHash] = @PasswordHash WHERE [UserName] = @UserName";
                SqlConnection conn = ProjectDB.connectToDB();
                SqlCommand update = new SqlCommand(commandText, conn);
                newPass = generatePassword();

                update.Parameters.AddWithValue("@PasswordHash", SimpleHash.ComputeHash(newPass, "MD5", null));
                update.Parameters.AddWithValue("@UserName", txtUserForgot.Text);

                update.ExecuteNonQuery();
                conn.Close();

                Email resetEmail = new Email(findEmail(findEmployeeID(findLoginID(txtUserForgot.Text))), "Your password has been reset <br />Your new password is: " + newPass, "Password Change");
                resetEmail.sendEmail();
            }
            catch (Exception ex)
            {
                errorMessage.Text += "" + ex;
            }
        }
        
    }

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

    protected int findEmployeeID(int id)
    {
        int empID = -1;
        try
        {
            string commandText = "SELECT TOP 1 EmployeeID FROM [dbo].[Employee] WHERE EmpLoginID = @EmpLoginID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@EmpLoginID", id);

            SqlDataReader reader = select.ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();
                empID = (int)reader["EmployeeID"];
            }
            conn.Close();
            return empID;
        }
        catch (Exception ex)
        {
            errorMessage.Text += "" + ex;
            return empID;
        }
    }

    protected string findEmail(int id)
    {
        string email = "";
        try
        {
            string commandText = "SELECT TOP 1 Email FROM [dbo].[Employee] WHERE EmployeeID = @EmployeeID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@EmployeeID", id);

            SqlDataReader reader = select.ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();
                email = reader["Email"].ToString();
            }
            conn.Close();
            return email;
        }
        catch (Exception ex)
        {
            errorMessage.Text += "" + ex;
            return email;
        }
    }

    protected int findLoginID(string username)
    {
        int returnID = -1;
        try
        {
            string commandText = "SELECT TOP 1 EmpLoginID FROM [dbo].[EmployeeLogin] WHERE UserName = @UserName";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@UserName", username);

            SqlDataReader reader = select.ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();
                returnID = (int)reader["EmpLoginID"];
            }
            conn.Close();
            return returnID;
        }
        catch (Exception ex)
        {
            errorMessage.Text += "" + ex;
            return returnID;
        }
    }

    protected void btnForgot_Click(object sender, EventArgs e)
    {
        if (txtUserForgot.Text != "")
        {
            resetPassword();
        }
    }

    protected void changePassword()
    {
        try
        {
            string username = txtUserChange.Text;
            string password = txtPassOld.Text;
            string hash = "";

            if (username != "" && password != "")
            {
                string commandText = "SELECT [PasswordHash] FROM [dbo].[EmployeeLogin] WHERE [UserName] = @UserName";
                SqlConnection conn = ProjectDB.connectToDB();
                SqlCommand select = new SqlCommand(commandText, conn);

                select.Parameters.AddWithValue("@UserName", username);

                SqlDataReader reader = select.ExecuteReader();

                if(reader.HasRows)
                {
                    reader.Read();
                    hash = reader["PasswordHash"].ToString();
                }
                reader.Close();
                if (SimpleHash.VerifyHash(password,"MD5",hash))
                {
                    string newPassOne = txtPassNewOne.Text;
                    string newPassTwo = txtPassNewTwo.Text;
                    if (newPassOne != "" && newPassTwo != "")
                    {
                        if(newPassOne == newPassTwo)
                        {
                            commandText = "UPDATE [dbo].[EmployeeLogin] SET [PasswordHash] = @PasswordHash WHERE [UserName] = @UserName";
                            SqlCommand update = new SqlCommand(commandText, conn);

                            update.Parameters.AddWithValue("@PasswordHash", SimpleHash.ComputeHash(txtPassNewOne.Text, "MD5", null));
                            update.Parameters.AddWithValue("@UserName", txtUserChange.Text);

                            update.ExecuteNonQuery();
                        }
                        else
                        {
                            errorMessage.Text = "Your new password must match.";
                        }
                    }
                    else
                    {
                        errorMessage.Text = "Please ensure that all entries are completed.";
                    }
                }
                else
                {
                    errorMessage.Text = "Incorrect Password";
                }

                conn.Close();

            }
            else
            {
                errorMessage.Text = "Please ensure that all entries are completed";
            }
        }
        catch (Exception ex)
        {
            errorMessage.Text += "" + ex;
        }
    }

    protected void btnConfirmPass_Click(object sender, EventArgs e)
    {
        changePassword();
    }
}
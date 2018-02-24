using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using database;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class GivePoints : System.Web.UI.Page
{
    public static int valueIndex;
    public static int pointIndex;
    public static int applaudIndex;
    public static int selectedEmployeeIndex;
    static Employee user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.SearchEmployee();

        }
        if (Session["employeeLoggedIn"] == null)
        {
            Response.Redirect("Login.aspx"); //check that the filepath is correct
        }
        if (Session["employeeLoggedIn"].ToString() != "True")
        {
            Response.Redirect("Login.aspx"); //check that the filepath works
        }

        user = (Employee)Session["user"];

        if (user.Admin == true)
        {
            Response.Redirect("Admin.aspx");
        }



        if (DropDownApplaud.SelectedIndex > 0)
        {
            valueIndex = DropDownApplaud.SelectedIndex;
        }

        if (DropDownCompanyValue.SelectedIndex > 0)
        {
            applaudIndex = DropDownCompanyValue.SelectedIndex;
        }


        if (DropDownPointsGiven.SelectedIndex > 0)
        {
            pointIndex = int.Parse(DropDownPointsGiven.SelectedValue);
        }

        DropDownPointsGiven.SelectedIndex = 0;
        selectValue();
        selectApplaud();
    }
    private void SearchEmployee()
    {
        if (txtSearchTeamMember.Text != "")
        {
            GVTeamMember.Visible = true;
        }
        SqlConnection conn = ProjectDB.connectToDB();
        using (System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand())
        {
            string commandText = "SELECT EmployeeID, (FirstName + ' ' + LastName) as FullName FROM Employee";
            if (!string.IsNullOrEmpty(txtSearchTeamMember.Text.Trim()))
            {
                commandText += " WHERE FirstName LIKE '%' + @FirstName + '%' OR LastName LIKE '%' + @LastName + '%'";
                insert.Parameters.AddWithValue("@FirstName", txtSearchTeamMember.Text.Trim());
                insert.Parameters.AddWithValue("@LastName", txtSearchTeamMember.Text.Trim());
            }
            insert.CommandText = commandText;
            insert.Connection = conn;
            using (SqlDataAdapter sda = new SqlDataAdapter(insert))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GVTeamMember.DataSource = dt;
                GVTeamMember.DataBind();
            }


            conn.Close();
        }
    }

    protected void Search(object sender, EventArgs e)
    {
        this.SearchEmployee();
    }
    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {
        GVTeamMember.PageIndex = e.NewPageIndex;
        this.SearchEmployee();
    }
    //protected void SelectEmployeeIndex(object sender, EventArgs e)
    //{

    //    //selectedEmployeeIndex = int.Parse(GVTeamMember.SelectedRow.Cells[1].Text);
    //}
    private void selectValue()
    {

        try
        {
            SqlConnection conn = ProjectDB.connectToDB();
            string commandText = "select Name from [dbo].[Value]";
            System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand(commandText, conn);
            DropDownCompanyValue.DataSource = insert.ExecuteReader();
            DropDownCompanyValue.DataTextField = "Name";
            DropDownCompanyValue.DataBind();
            DropDownCompanyValue.Items.Insert(0, "Select");

            conn.Close();
        }
        catch (Exception s)
        {
            Label.Text += "Value Error";
            Label.Text += s.Message;
        }
    }

    private void selectApplaud()
    {

        try
        {
            SqlConnection conn = ProjectDB.connectToDB();
            string commandText = "select Name from [dbo].[Applaud]";
            System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand(commandText, conn);
            DropDownApplaud.DataSource = insert.ExecuteReader();
            DropDownApplaud.DataTextField = "Name";
            DropDownApplaud.DataBind();
            DropDownApplaud.Items.Insert(0, "Select");

            conn.Close();
        }
        catch (Exception s)
        {
            Label.Text += "Applaud Error";
            Label.Text += s.Message;
        }
    }
    protected void SubmitGivePointsBtn_Click(object sender, EventArgs e)
    {
        bool working = true;
        if (pointIndex == 0)
        {
            working = false;
            Error.Text += "Please select from Points" + "<br>";

        }
        if (valueIndex == 0)
        {
            working = false;
            Error.Text += "Please select from Values" + "<br>";
        }
        if (applaudIndex == 0)
        {
            working = false;
            Error.Text += "Please select from Applaud For Being" + "<br>";
        }

        if (working == true)
        {
            CommittToDBPoints();
        }
    }

    private void CommittToDBPoints()
    {

        try
        {
            SqlConnection conn = ProjectDB.connectToDB();
            string commandText = "INSERT INTO [dbo].[Achievement] (Description, Date, PointsAmount, EmployeeID, ValueID, RecEmployee, ApplaudID) " +
                "VALUES (@Description, @Date, @PointsAmount, @EmployeeID, @ValueID, @RecEmployee, @ApplaudID)";
            System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand(commandText, conn);
            insert.Parameters.AddWithValue("@Description", txtDescription.Value);
            insert.Parameters.AddWithValue("@Date", DateTime.Parse(txtDate.Value));
            insert.Parameters.AddWithValue("@PointsAmount", pointIndex);
            insert.Parameters.AddWithValue("@EmployeeID", findEmployeeID(user.EmpLoginID));
            insert.Parameters.AddWithValue("@ValueID", valueIndex);
            insert.Parameters.AddWithValue("@RecEmployee", int.Parse(GVTeamMember.SelectedRow.Cells[1].Text));
            insert.Parameters.AddWithValue("@ApplaudID", applaudIndex);

            insert.ExecuteNonQuery();
            Label.Text += insert.CommandText;
            conn.Close();

            SqlConnection add = ProjectDB.connectToDB();
            string addPoints = "SELECT TOP 1 Points FROM [dbo].[Employee] WHERE EmployeeID = @RecEmployee";
            System.Data.SqlClient.SqlCommand select = new System.Data.SqlClient.SqlCommand(addPoints, add);
            select.Parameters.AddWithValue("@RecEmployee", int.Parse(GVTeamMember.SelectedRow.Cells[1].Text));

            SqlDataReader reader = select.ExecuteReader();
            Decimal points = 0;
            if (reader.HasRows)
            {
                reader.Read();
                points = (Decimal)reader["Points"];
            }
            add.Close();

            SqlConnection addTo = ProjectDB.connectToDB();
            string addToTable = "UPDATE [dbo].[Employee] SET Points = @PointTotal + @PointAdded WHERE EmployeeID = @RecEmployee";
            System.Data.SqlClient.SqlCommand update = new System.Data.SqlClient.SqlCommand(addToTable, addTo);
            update.Parameters.AddWithValue("@PointTotal", points);
            update.Parameters.AddWithValue("@PointAdded", pointIndex);
            update.Parameters.AddWithValue("@RecEmployee", int.Parse(GVTeamMember.SelectedRow.Cells[1].Text));
            update.ExecuteNonQuery();
            addTo.Close();



        }
        catch (Exception ea)
        {
            Label.Text += ea.Message + valueIndex;

        }

    }

    protected int findEmployeeID(int id)
    {
        int employeeID = 0;
        try
        {
            string commandText = "SELECT TOP 1 EmployeeID FROM [DBO].[EMPLOYEE] WHERE EmpLoginID = @EmpLoginID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@EmpLoginID", id);

            SqlDataReader reader = select.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();

                employeeID = (int)reader["EmployeeID"];

            }
            conn.Close();
            return employeeID;
        }
        catch (Exception ex)
        {
            Error.Text += "Error finding employee ID" + ex;
            return -1;
        }

    }
}
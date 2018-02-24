using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using database;
using System.Data.SqlClient;




public partial class HomePage : System.Web.UI.Page
{
    static Employee user;
    static int numOfPosts = 20;
    static Posts[] postsArray = new Posts[numOfPosts];

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

        if (user.Admin == true)
        {
            Response.Redirect("Admin.aspx");
        }

        welcomeMessage.Text = "Welcome " + user.FirstName + " " + user.LastName + " You currently have " + Decimal.Round(user.Points, 2) + " points!";
        form1.Controls.Add(new LiteralControl("<br />"));

        Image[] imgArray = new Image[numOfPosts];
        TextBox[] txtArray = new TextBox[numOfPosts];
        Button[] btnArray = new Button[numOfPosts];
        Label[] lblArray = new Label[numOfPosts];




        for (int i = 0; i < numOfPosts; i++)
        {
            imgArray[i] = new Image();
            imgArray[i].Height = 100;
            imgArray[i].Width = 100;
            imgArray[i].BorderStyle = BorderStyle.Solid;
            imgArray[i].CssClass = "images";

            txtArray[i] = new TextBox();
            txtArray[i].Height = 100;
            txtArray[i].Width = 500;
            txtArray[i].TextMode = TextBoxMode.MultiLine;
            txtArray[i].CssClass = "w3-light-grey";

            btnArray[i] = new Button();
            btnArray[i].Text = "Like :)";

            lblArray[i] = new Label();
            
        }

        postInformation();
        try
        {
            if (postsArray != null)
            {
                for (int a = 0; a < numOfPosts; a++)
                {
                    form1.Controls.Add(imgArray[a]);
                    
                    form1.Controls.Add(txtArray[a]);
                    //determine if the post is an achievement or a transaction
                    if (postsArray[a].AchievementID == -1)
                    {
                        txtArray[a].Text += findEmployee(findTransaction(postsArray[a].TransactionID).EmployeeID) + " used their points to purchase: "
                            + findTransaction(postsArray[a].TransactionID).RewardID + " for $" + findTransaction(postsArray[a].TransactionID).Cost;
                        //add their image from s3
                        imgArray[a].ImageUrl = findImage(findTransaction(postsArray[a].TransactionID).EmployeeID);
                    }
                    //determine if the post is an achievement or transaction
                    if (postsArray[a].TransactionID == -1)
                    {
                        txtArray[a].Text += findEmployee(findAchievement(postsArray[a].AchievementID).RecEmployee) + " received kudos for "
                            + findAchievement(postsArray[a].AchievementID).Description + " from "
                            + findEmployee(findAchievement(postsArray[a].AchievementID).EmployeeID);
                        //add their image from s3
                        imgArray[a].ImageUrl = findImage(findAchievement(postsArray[a].AchievementID).RecEmployee);
                    }
                    form1.Controls.Add(new LiteralControl("<br />"));
                    form1.Controls.Add(btnArray[a]);
                    
                    //to get the number to change set the button ID equal to the number control it is therefore we can set the proper like button
                    //to update the right label with likes
                    btnArray[a].ID = a.ToString();
                    btnArray[a].Click += getControl;

                    form1.Controls.Add(lblArray[a]);
                    lblArray[a].Text = findLikes(postsArray[a].PostID) + " Likes!";
                    form1.Controls.Add(new LiteralControl("<br />"));
                    form1.Controls.Add(new LiteralControl("<br />"));
                    //LoginView1.Controls.Add()
                }
            }
            else
            {
                errorMessage.Text += "Here";
            }
        }
        catch (Exception ex)
        {
            errorMessage.Text += ex + "post error";
        }

    }

    protected void likeClick(object sender, EventArgs e, Control cntrl)
    {

    }

    protected void postInformation()
    {
        try
        {
            string commandText = "SELECT TOP (" + numOfPosts + ") * FROM [dbo].[FeedInformation] ORDER BY PostID DESC";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            SqlDataReader reader = select.ExecuteReader();
            int a = 0;
            
            while (reader.Read())
            {
                int achievementID, transactionID = -1;
                int postID = (int)reader[0];
                DateTime postTime = (DateTime)reader[1];
                int numOfLikes = (int)reader[2];

                if (!reader.IsDBNull(3))
                {
                    achievementID = (int)reader[3];
                }
                else
                {
                    achievementID = -1;
                }
                if (!reader.IsDBNull(4))
                {
                    transactionID = (int)reader[4];
                }
                else
                {
                    transactionID = -1;
                }

                postsArray[a] = new Posts(postID, postTime, numOfLikes, achievementID, transactionID);
                a++;
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            errorMessage.Text += "\n" + ex;
        }

    }

    /// <summary>
    /// Finds the information related to the specified achievement id
    ///
    /// </summary>
    /// <param name="id">This is the achievement id that you are trying to find</param>
    /// <returns>Returns an array of objects that is equal to the achievement information in the database</returns>
    protected Achievement findAchievement(int id)
    {

        try
        {
            string commandText = "SELECT TOP 1 Description, Date, PointsAmount, EmployeeID, ValueID, RecEmployee FROM [DBO].[ACHIEVEMENT] WHERE AchievementID = @AchievementID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@AchievementID", id);

            SqlDataReader reader = select.ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();
                string description = reader["Description"].ToString();
                DateTime date = (DateTime)reader["Date"];
                int points = (int)reader["PointsAmount"];
                int employeeID = (int)reader["EmployeeID"];
                int valueID = (int)reader["ValueID"];
                int recEmployee = (int)reader["RecEmployee"];
                conn.Close();
                Achievement a = new Achievement(id, description, date, points, employeeID, valueID, recEmployee);
                return a;
            }

            conn.Close(); //remove later
        }
        catch (Exception ex)
        {
            
            errorMessage.Text += "Error Finding Achievement " + ex;
        }
        errorMessage.Text += "Reached end of Achievement";
        return null;
    }

    protected string findEmployee(int id)
    {
        string employeeName = "";
        try
        {
            string commandText = "SELECT TOP 1 FirstName, LastName FROM [DBO].[EMPLOYEE] WHERE EmployeeID = @EmployeeID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@EmployeeID", id);

            SqlDataReader reader = select.ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();
                string firstName = reader["FirstName"].ToString();
                string lastName = reader["LastName"].ToString();
                employeeName = firstName + " " + lastName;
                conn.Close();
                return employeeName;
            }
            conn.Close(); //remove later
        }
        catch (Exception ex)
        {
            errorMessage.Text += "Error Finding Employee" + ex;
        }
        return employeeName;
    }

    //
    protected Transaction findTransaction(int id)
    {
        try
        {
            string commandText = "SELECT TOP 1 Cost, PurchaseTime, EmployeeID, RewardID FROM [DBO].[TRANSACTION] WHERE TransactionID = @TransactionID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@TransactionID", id);

            SqlDataReader reader = select.ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();
                Decimal cost = (Decimal)reader["Cost"];
                DateTime purchaseTime = (DateTime)reader["PurchaseTime"];
                int employeeID = (int)reader["EmployeeID"];
                int rewardID = (int)reader["RewardID"];

                Transaction tran = new Transaction(id, cost, purchaseTime, employeeID, rewardID);
                conn.Close();
                return tran;
            }
            
        }
        catch (Exception ex)
        {
            errorMessage.Text += "Error Finding Transaction " + ex;
        }
        errorMessage.Text += "Reached end of transaction";
        return null;
    }

    protected int findLikes(int postID)
    {
        int likes = -1;
        try
        {
            string commandText = "SELECT TOP 1 NumOfLikes FROM [dbo].[FeedInformation] WHERE PostID = @PostID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@PostID", postID);

            SqlDataReader reader = select.ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();
                likes = (int)reader["NumOfLikes"];
            }
            conn.Close();
            return likes;
        }
        catch (Exception ex)
        {
            errorMessage.Text += "Error finding number of likes " + ex;
            return likes;
        }
    }

    protected void liked(int postID)
    {
        try
        {
            string commandText = "UPDATE [dbo].[FeedInformation] set [NumOfLikes] = @NumOfLikes WHERE [PostID] = @PostID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand update = new SqlCommand(commandText, conn);

            update.Parameters.AddWithValue("@NumOfLikes", findLikes(postID) + 1);
            update.Parameters.AddWithValue("@PostID", postID);

            update.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception ex)
        {
            errorMessage.Text += "Error Liking Post " + ex;
        }
    }

    //Gets the ID of the control which will be equal to the post number
    protected void getControl(object sender, EventArgs e)
    {
        int num = 0;
        try
        {
            string b = (sender as Button).ID.ToString();
            num = Int32.Parse(b);
            liked(postsArray[num].PostID);
            Response.Redirect("HomePage.aspx");
        }
        catch (Exception)
        {
            
        }
    }

    protected string findImage(int id)
    {
        string img = "~/Images/DefaultImg.jpg";
        try
        {
            string commandText = "SELECT TOP 1 ImageURL from [dbo].[Image] WHERE EmployeeID = @EmployeeID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@EmployeeID", id);

            SqlDataReader reader = select.ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();
                img = reader["ImageURL"].ToString();
            }

            conn.Close();
            return img;
        }
        catch (Exception ex)
        {
            errorMessage.Text = "Error Finding Image " + ex;
            return img;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using database;
using System.Data.SqlClient;

public partial class Rewards : System.Web.UI.Page
{
    static Employee user;
    static int index;
    static int num;
    static RewardItem[] itemArray;
    static string error = "";
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
        user.Points = getPoints(findEmployeeID(user.EmpLoginID));
        Session["user"] = user;

        num = countRewards();
        itemArray = new RewardItem[num];

        lblPoints.Text = "Welcome " + user.FirstName + " " + user.LastName + " <br /> You currently have " + user.Points + " Points!";
        errorMessage.Text = error;


        populateArray();

        Image[] imgArray = new Image[num];
        TextBox[] txtArray = new TextBox[num];
        Button[] btnArray = new Button[num];

        for (int i = 0; i < num; i++)
        {
            imgArray[i] = new Image();
            imgArray[i].Height = 150;
            imgArray[i].Width = 100;
            imgArray[i].BorderStyle = BorderStyle.Solid;
            
            

            txtArray[i] = new TextBox();
            txtArray[i].Height = 200;
            txtArray[i].Width = 500;
            txtArray[i].TextMode = TextBoxMode.MultiLine;

            btnArray[i] = new Button();
            btnArray[i].Text = "Buy";
            btnArray[i].ID = i.ToString();
            btnArray[i].Click += getControl;

            feed.Controls.Add(imgArray[i]);
            imgArray[i].ImageUrl = findImage(itemArray[i].RewardID);
            feed.Controls.Add(txtArray[i]);
            feed.Controls.Add(new LiteralControl("<br />"));
            feed.Controls.Add(btnArray[i]);
            feed.Controls.Add(new LiteralControl("<br />"));
        }

        for (int i = 0; i < itemArray.Length; i++)
        {
            txtArray[i].Text += "Reward Name: " + itemArray[i].Name + Environment.NewLine;
            txtArray[i].Text += "Reward Description: " + itemArray[i].Description;
            txtArray[i].Text += Environment.NewLine + "Price: $" + Decimal.Round(itemArray[i].Price) + Environment.NewLine;
            txtArray[i].Text += "Offer Start Date: " + (itemArray[i].StartDate).ToShortDateString() + Environment.NewLine;
            txtArray[i].Text += "Offer End Date: " + (itemArray[i].EndDate).ToShortDateString() + Environment.NewLine;
            txtArray[i].Text += "Quantity Remaining: " + itemArray[i].Quantity + Environment.NewLine;
        }
        
    }

    protected int countRewards()
    {
        int count = 0;
        try
        {
            string commandText = "SELECT COUNT(RewardID) as Result FROM [dbo].[RewardItem] WHERE [EndDate] >= @EndDate AND [StartDate] <= @StartDate AND [Quantity] > @Quantity";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@EndDate", DateTime.Now);
            select.Parameters.AddWithValue("@StartDate", DateTime.Now);
            select.Parameters.AddWithValue("@Quantity", 0);

            SqlDataReader reader = select.ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();
                count = (int)reader["Result"];
            }
            conn.Close();

        }
        catch (Exception)
        {
            error += "<br/>There are no reward available.";
        }
        return count;
    }

    protected void populateArray()
    {
        try
        {
            string commandText = "SELECT [RewardID],[Name],[Description],[Price],[StartDate],[EndDate],[Quantity],[ProviderID],[CategoryID]" +
                "FROM [dbo].[RewardItem] WHERE [EndDate] >= @EndDate AND [StartDate] <= @StartDate AND [Quantity] > @Quantity";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@EndDate", DateTime.Now);
            select.Parameters.AddWithValue("@StartDate", DateTime.Now);
            select.Parameters.AddWithValue("@Quantity", 0);

            SqlDataReader reader = select.ExecuteReader();
            int a = 0;

            while(reader.Read())
            {
                int rewardID, quantity, providerID, categoryID;
                string name, description;
                DateTime startDate, endDate;
                Decimal price;

                rewardID = (int)reader[0];
                name = reader[1].ToString();
                description = reader[2].ToString();
                price = (Decimal)reader[3];
                startDate = (DateTime)reader[4];
                endDate = (DateTime)reader[5];
                quantity = (int)reader[6];
                providerID = (int)reader[7];
                categoryID = (int)reader[8];

                itemArray[a] = new RewardItem(rewardID, name, description, price, startDate, endDate, quantity, providerID, categoryID);

                a++;
            }
            conn.Close();
        }
        catch (Exception)
        {
            error += "<br/>Error Populating Rewards.";
        }
    }

    protected void getControl(object sender, EventArgs e)
    {
        int b = 0;
        try
        {
            string k = (sender as Button).ID.ToString();
            num = Int32.Parse(k);
            if (user.Points < itemArray[num].Price)
            {
                error = "<br/>You do not have enough points to make this purchase.";
            }
            else
            {
                buyReward(num);
            }
            Response.Redirect("Rewards.aspx");
        }
        catch (Exception)
        {

        }
    }

    protected void buyReward(int id)
    {
        if (user.Points >= itemArray[id].Price)
        {
            try
            {
                string commandText = "INSERT INTO [dbo].[Transaction] values (@Cost, @PurchaseTime, @EmployeeID, @RewardID)";
                SqlConnection conn = ProjectDB.connectToDB();
                SqlCommand insert = new SqlCommand(commandText, conn);

                insert.Parameters.AddWithValue("@Cost", itemArray[id].Price);
                insert.Parameters.AddWithValue("@PurchaseTime", DateTime.Now);
                insert.Parameters.AddWithValue("@EmployeeID", findEmployeeID(user.EmpLoginID));
                insert.Parameters.AddWithValue("@RewardID", itemArray[id].RewardID);

                insert.ExecuteNonQuery();

                conn.Close();

                subtractPoints(itemArray[id].Price);

                subtractQuantity(1, itemArray[id].RewardID);

                updateFeed();

                sendEmail(user.Email, id);
            }
            catch (Exception)
            {
                error += "<br/>Error Purchasing Reward.";
            }
        }
        else
        {
            error += "<br/>You do not have enough points to make this purchase.";
        }
    }

    protected void subtractPoints(Decimal cost)
    {

        try
        {
            string commandText = "UPDATE [dbo].[Employee] set [Points] = @Points WHERE [EmployeeID] = @EmployeeID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand update = new SqlCommand(commandText, conn);

            update.Parameters.AddWithValue("@Points", user.Points - cost);
            update.Parameters.AddWithValue("@EmployeeID", findEmployeeID(user.EmpLoginID));

            user.Points = user.Points - cost;

            Session["user"] = user;

            update.ExecuteNonQuery();

            conn.Close();
        }
        catch (Exception)
        {
            error += "<br/>Error Subtracting Points";
        }
    }

    

    protected int findEmployeeID(int id)
    {
        int employeeID = -1;
        try
        {
            String commandText = "Select EmployeeID from [dbo].[EMPLOYEE] WHERE EmpLoginID = @EmpLoginID";
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
        catch (Exception)
        {
            error += "<br/>Error Finding EmployeeID";
            return employeeID;
        }
    }

    protected void updateFeed()
    {

        try
        {
            string commandText = "INSERT INTO [dbo].[FeedInformation] ([PostTime],[NumOfLikes],[TransactionID]) Values (@PostTime, @NumOfLikes, @TransactionID)";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand insert = new SqlCommand(commandText, conn);

            insert.Parameters.AddWithValue("@PostTime", DateTime.Now);
            insert.Parameters.AddWithValue("@NumOfLikes", 0);
            insert.Parameters.AddWithValue("@TransactionID", findRecentTransaction());

            insert.ExecuteNonQuery();

            conn.Close();
        }
        catch (Exception)
        {
            error += "<br/>Error Updating Feed.";
        }
    }

    protected int findRecentTransaction()
    {
        int a = -1;
        try
        {
            string commandText = "SELECT MAX(TransactionID) as Result FROM [dbo].[Transaction]";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            SqlDataReader reader = select.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                a = (int)reader["Result"];
            }
            conn.Close();
        }
        catch (Exception)
        {
            error += "<br/>Error Finding Recent Transactions.";
        }
        return a;
    }

    protected void subtractQuantity(int bought, int rewardID)
    {

        try
        {
            string commandText = "UPDATE [dbo].[RewardItem] SET [Quantity] = @Quantity WHERE [RewardID] = @RewardID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand update = new SqlCommand(commandText, conn);

            update.Parameters.AddWithValue("@Quantity", getQuantity(rewardID) - bought);
            update.Parameters.AddWithValue("@RewardID", rewardID);

            update.ExecuteNonQuery();

            conn.Close();
        }
        catch (Exception ex)
        {
            error += "<br/>Error Subtracting Quantity";
        }
    }

    protected int getQuantity(int id)
    {
        int quantity = 0;
        try
        {
            string commandText = "SELECT [Quantity] FROM [dbo].[RewardItem] WHERE [RewardID] = @RewardID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@RewardID", id);

            SqlDataReader reader = select.ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();
                quantity = (int)reader["Quantity"];
            }
            conn.Close();
        }
        catch (Exception)
        {
            error += "<br/>Error Getting Quantity.";
        }
        return quantity;
    }

    protected string purchaseCode()
    {

        string password = "";
        Random rnd = new Random();

        int charLength = rnd.Next(12, 20);
        string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";

        for (int i = 0; i < charLength; i++)
        {
            int a = rnd.Next(0, chars.Length - 1);
            password += chars.Substring(a, 1);
        }

        return password;
    }

    protected void sendEmail(string emailTo, int id)
    {
        try
        {
            string body = "You just purchased: " + itemArray[id].Name + "<br />Your purchase code is: " + purchaseCode() + "<br />Enjoy (:<br /> This is an automatic email from Elk Logistics Reward System";
            string subject = "Receipt for: " + itemArray[id].Name;
            Email purchaseEmail = new Email(emailTo, body, subject);
            purchaseEmail.sendEmail();
        }
        catch (Exception)
        {

        }
    }

    protected string findImage(int id)
    {
        string img = "https://s3.amazonaws.com/484imagescourtney/default.jpg";

        try
        {
            string commandText = "SELECT TOP 1 ImageURL from [dbo].[Image] WHERE RewardID = @RewardID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@RewardID", id);

            SqlDataReader reader = select.ExecuteReader();

            if (reader.HasRows)
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
    protected Decimal getPoints(int id)
    {
        Decimal points = 0;
        try
        {
            string commandText = "SELECT [Points] FROM [dbo].[Employee] WHERE [EmployeeID] = @EmployeeID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@EmployeeID", id);

            SqlDataReader reader = select.ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();
                points = (Decimal)reader["Points"];
            }
            conn.Close();
        }
        catch (Exception)
        {

        }
        return points;
    }





}
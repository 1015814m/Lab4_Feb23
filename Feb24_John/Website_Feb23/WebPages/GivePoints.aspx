<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GivePoints.aspx.cs" Inherits="GivePoints" %>

<!DOCTYPE html>
<html>
    <head>
        <title>W3.CSS Template</title>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
        <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins">
        <style>
        body,h1,h2,h3,h4,h5 {font-family: "Poppins", sans-serif}
        body {font-size:16px;}
        .w3-half img{margin-bottom:-6px;margin-top:16px;opacity:0.8;cursor:pointer}
        .w3-half img:hover{opacity:1}

        input[type=text], .ddl, textarea {
            width: 100%;
            padding: 12px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
            margin-top: 6px;
            margin-bottom: 16px;
            resize: vertical;
        }

        .button {
            background-color: #4CAF50;
            color: white;
            padding: 12px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        input[type=submit]:hover {
            background-color: #45a049;
        }

        .container {
            border-radius: 5px;
            background-color: bisque;
            padding: 20px;
        }

            .hiddencol { 
                display: none; 

            }
        </style>
    </head>
    <body>

    <!-- Sidebar/menu -->
    <nav class="w3-sidebar w3-red w3-collapse w3-top w3-large w3-padding" style="z-index:3;width:300px;font-weight:bold;" id="mySidebar"><br>
      <a href="javascript:void(0)" onclick="w3_close()" class="w3-button w3-hide-large w3-display-topleft" style="width:100%;font-size:22px">Close Menu</a>
      <div class="w3-container">
        <h3 class="w3-padding-64"><b>Top 10<br>Solutions</b></h3>
      </div>
      <div class="w3-bar-block">
        <a href="HomePage.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">Home</a> 
        <a href="GivePoints.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">Give Points</a> 
        <a href="Rewards.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">View Rewards</a> 
        
      </div>
    </nav>

    <!-- Top menu on small screens -->
    <header class="w3-container w3-top w3-hide-large w3-red w3-xlarge w3-padding">
      <a href="javascript:void(0)" class="w3-button w3-red w3-margin-right" onclick="w3_open()">☰</a>
      <span>Top 10 Solutions</span>
    </header>

    <!-- Overlay effect when opening sidebar on small screens -->
    <div class="w3-overlay w3-hide-large" onclick="w3_close()" style="cursor:pointer" title="close side menu" id="myOverlay"></div>

    <!-- !PAGE CONTENT! -->
    <div class="w3-main" style="margin-left:340px;margin-right:40px">

      <!-- Header -->
      <div class="w3-container" style="margin-top:80px" id="showcase">
        <h1 class="w3-jumbo"><b>Reward Team Members</b></h1>
        <h1 class="w3-xxxlarge w3-text-red"><b>Give Kudos:</b></h1>
        <hr style="width:50px;border:5px solid red; float: left;" class="w3-round">
      </div>

    <div class="w3-container" id="givepoints" style="margin-top: 75px;">
        <form id="feed" runat="server">
            <asp:Label ID="Error" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <label >Team Member</label>
            <br />
            <asp:TextBox ID="txtSearchTeamMember" runat="server" required="" OnTextChanged="Search" AutoPostBack="true"></asp:TextBox>
            <asp:GridView ID="GVTeamMember" runat="server" Visible="false" AutoGenerateColumns="false" OnPageIndexChanging="OnPaging" AutoGenerateSelectButton="true" SelectedRowStyle-BackColor="#DCDCDC">
                <Columns>
                    <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" ItemStyle-Width="150" ItemStyle-CssClass="hiddencol"  HeaderStyle-CssClass="hiddencol"/>
                    <asp:BoundField DataField="FullName" HeaderText="Name" ItemStyle-Width="150" />
                     
                </Columns>
            </asp:GridView>
            <br />
            <label>Give Kudos For:</label>
            <asp:DropDownList ID="DropDownCompanyValue" required= "" runat="server" CssClass="ddl"></asp:DropDownList>
            <label>Date:</label>
            <input id="txtDate" runat="server" type="text" required= "" placeholder="YYYY-MM-DD" />

            <label>Description:</label>
            <textarea id="txtDescription" required= "" runat="server" placeholder="Write a description of the activity completed..."
                style="height:200px"></textarea>

            <label>Points Given:</label>
            <asp:DropDownList ID="DropDownPointsGiven" required= "" runat="server" CssClass="ddl">
                <asp:ListItem Text="Select"></asp:ListItem>
                <asp:ListItem Text="10"></asp:ListItem>
                <asp:ListItem Text="25"></asp:ListItem>
                <asp:ListItem Text="50"></asp:ListItem>
            </asp:DropDownList>

            <label>Applaud For Being:</label>
            <asp:DropDownList ID="DropDownApplaud" required= "" runat="server" CssClass="ddl"></asp:DropDownList>

            <asp:Button ID="SubmitGivePoints" runat="server" Text="Submit" OnClick="SubmitGivePointsBtn_Click" CssClass="button" />
            <br />
            <asp:Label ID="Label" runat="server"></asp:Label>
        </form>
    </div>
  

    <!-- End page content -->
    </div>

    <!-- W3.CSS Container -->
    <div class="w3-light-grey w3-container w3-padding-32" style="margin-top:75px;padding-right:58px"><p class="w3-right">Powered by <a href="https://www.w3schools.com/w3css/default.asp" title="W3.CSS" target="_blank" class="w3-hover-opacity">w3.css</a></p></div>

    <script>
        // Script to open and close sidebar
        function w3_open() {
            document.getElementById("mySidebar").style.display = "block";
            document.getElementById("myOverlay").style.display = "block";
        }

        function w3_close() {
            document.getElementById("mySidebar").style.display = "none";
            document.getElementById("myOverlay").style.display = "none";
        }

        // Modal Image Gallery
        function onClick(element) {
            document.getElementById("img01").src = element.src;
            document.getElementById("modal01").style.display = "block";
            var captionText = document.getElementById("caption");
            captionText.innerHTML = element.alt;
        }
    </script>

    </body>
</html>
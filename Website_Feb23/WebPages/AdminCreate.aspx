<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminCreate.aspx.cs" Inherits="AdminCreate" %>

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
            .auto-style16 {
                width: 947px;
                height: 70px;
            }
            .auto-style52 {
                height: 25px;
                width: 650px;
            }
            .auto-style53 {
                height: 41px;
                width: 650px;
            }
            .auto-style54 {
                height: 25px;
                width: 68px;
            }
            .auto-style55 {
                height: 41px;
                width: 68px;
            }
            .auto-style56 {
                height: 25px;
                width: 69px;
            }
            .auto-style57 {
                height: 41px;
                width: 69px;
            }
            .auto-style61 {
                width: 100%;
            }
            .auto-style62 {
                width: 754px;
            }
            .auto-style63 {
                width: 344px;
            }
            .auto-style64 {
                width: 344px;
                height: 28px;
            }
            .auto-style65 {
                height: 28px;
            }
            
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
        <a href="Admin.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">Home</a>  
        <a href="AdminRewards.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">Add Rewards</a> 
        <a href="AdminCreate.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">Create/Edit Users</a> 
        <a href="AdminAnalytics.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">View Analytics</a>  
        <a href="AdminAddFunds.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">Add Funds</a>
        
      </div>
    </nav>

    <!-- Top menu on small screens -->
    <header class="w3-container w3-top w3-hide-large w3-red w3-xlarge w3-padding">
      <a href="javascript:void(0)" class="w3-button w3-red w3-margin-right" onclick="w3_open()">?</a>
      <span>Top 10 Solutions</span>
    </header>

    <!-- Overlay effect when opening sidebar on small screens -->
    <div class="w3-overlay w3-hide-large" onclick="w3_close()" style="cursor:pointer" title="close side menu" id="myOverlay"></div>

    <!-- !PAGE CONTENT! -->
    <div class="w3-main" style="margin-left:340px;margin-right:40px">

      <!-- Header -->
      <div class="w3-container" style="margin-top:80px" id="showcase">
        <h1 class="w3-jumbo"><b>Administration</b></h1>
        <h1 class="w3-xxxlarge w3-text-red">Create Users</h1>
        <hr style="width:50px;border:5px solid red; float: left;" class="w3-round">
      </div>

    <div class="w3-container" id="administration" style="margin-top: 75px;">

    <!-- Actual Form Code -->
        <form id="feed" runat="server">
                
                
                <asp:Label id="lblFirstName" text="First Name:" runat="server"></asp:Label>
                <asp:TextBox id="txtFirstName" runat="server" required="" Type="text"></asp:TextBox>
                <asp:Label id="lblLastName" runat="server" text="Last Name:"></asp:Label>
                <asp:TextBox id="txtLastName" type="text" runat="server" required=""></asp:TextBox>
                <asp:Label id="Email" runat="server" text="E-Mail:"></asp:Label>
                <asp:TextBox id="txtEmail" type="text" runat="server" required=""></asp:TextBox>
                <asp:Button runat="server" id="Submit" CssClass=".button" Text="Submit" type="submit" OnClick="Submit_Click" formnovalidate=""/>
                <br />
                <asp:Label id="lblSuccess" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="lblPassword" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="errorMessage" runat="server" Text=""></asp:Label>
            
            
                <div class="w3-container" style="margin-top:80px" id="editshowcase">       
                    <h1 class="w3-xxxlarge w3-text-red">Edit Users</h1>
                    <hr style="width:50px;border:5px solid red; float: left;" class="w3-round">
                </div>

                
             <div>

                 <asp:GridView ID="employeeGrid" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="EmployeeID" DataSourceID="SqlDataSource" OnSelectedIndexChanged="employeeGrid_SelectedIndexChanged">
                     <Columns>
                         <asp:CommandField ShowSelectButton="True" />
                         <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" InsertVisible="False" ReadOnly="True" SortExpression="EmployeeID" />
                         <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                         <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                         <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                         <asp:BoundField DataField="Points" HeaderText="Points" SortExpression="Points" />
                     </Columns>
                 </asp:GridView>
                 <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Lab4ConnectionString %>" SelectCommand="SELECT [EmployeeID], [FirstName], [LastName], [Email], [Points] FROM [Employee]"></asp:SqlDataSource>


                 <asp:Label ID="lblEditFirstName" runat="server" Text="First Name: " Visible="false"></asp:Label>
                 <asp:TextBox ID="txtEditFirstName" runat="server" Visible="false" Type="text"></asp:TextBox>
                 <asp:Label ID="lblEditLastName" runat="server" Text="Last Name: " Visible="false"></asp:Label>
                 <asp:TextBox ID="txtEditLastName" runat="server" Visible="false" Type="text"></asp:TextBox>
                 <asp:Label ID="lblEditEmail" runat="server" Text="Email: " Visible="false"></asp:Label>
                 <asp:TextBox ID="txtEditEmail" runat="server" Visible="false" Type="text"></asp:TextBox>
                 <asp:Button ID="btnSubmit" runat="server" Text="Submit Changes" formnovalidate="" OnClick="commitChanges" />
                 <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate="" OnClick="btnCancel_Click"/>
             </div>       
            

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
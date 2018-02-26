<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminAddFunds.aspx.cs" Inherits="AdminAddFunds" %>

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

                input[type=text],textarea {
            width: 100%;
            padding: 12px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
            margin-top: 6px;
            margin-bottom: 16px;
            resize: vertical;
        }

        .ddl{
            padding: 12px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
            margin-top: 6px;
            margin-bottom: 16px;
            resize: vertical;
            display: inline-block;
            width: 100px;
            
        }
        .table{
            font-size:x-large;
        }
        .button {
            background-color: #4CAF50;
            color: white;
            padding: 12px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }
        .row:after {
            content: "";
            display: table;
            clear: both;
        }
            .column {
                float: left;
                width: 46%;
                padding: 10px;
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
        <a href="Logout.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">Logout</a>
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
        <h1 class="w3-jumbo"><b>Administration</b></h1>
        <h1 class="w3-xxxlarge w3-text-red"><b>Add Funds</b></h1>
        <hr style="width:50px;border:5px solid red; float: left;" class="w3-round">
      </div>

    <div class="w3-container" id="administration" style="margin-top: 75px;">
        <form id="feed" runat="server">
             <asp:Label runat="server" ID="CurrentAdminPoints" Text="Current Admin Points:" Font-Bold="True" Font-Underline="True" CssClass="table"></asp:Label>
             
            <table class="table"> 
                 <tr>
                     <td>
                         <asp:Label ID="CurrentFunds" Text="&#9899Current Funds:" runat="server"></asp:Label> 
                     </td>
                     <td>
                         <asp:Label ID="lblCurrenFundsNum" Text="" runat="server"></asp:Label>
                     </td>
                 </tr>
                 
                 <tr>
                     <td>
                         <asp:Label ID="TotalPoints" Text="&#9899Total Points Earned <br/>By Employees:" runat="server"></asp:Label>
                     </td>
                     <td>
                         <asp:Label ID="lblTotalPoints" Text="" runat="server"></asp:Label>
                     </td>
                 </tr>
                
                 <tr>
                     <td>
                         <asp:Label ID="RemainingFunds" Text="&#9899Remaining Funds:" runat="server"></asp:Label>
                     </td>
                     <td>
                         <asp:Label ID="lblRemainingFunds" Text="" runat="server"></asp:Label>
                     </td>
                 </tr>
              </table>
                
              <br />
              <asp:Label ID="WithdrawFrom" Text="Withdraw From:" runat="server"></asp:Label>
              <br />
              <asp:TextBox ID="txtWithdrawFrom" runat="server"></asp:TextBox>
              <br />
              <asp:Label ID="depositTo" Text="Deposit To:" runat="server"></asp:Label>
              <br />
              <asp:TextBox ID="txtDepositTo" runat="server"></asp:TextBox>
              <br />
              <asp:Label ID="Amount" Text="Amount:" runat="server"></asp:Label>
              <br />
              <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
              <br />
              <asp:Button ID="btnSubmitAddFunds" runat="server" Text="Submit" OnClick="SubmitFunds_OnClick" CssClass="button" />

              
 
            





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
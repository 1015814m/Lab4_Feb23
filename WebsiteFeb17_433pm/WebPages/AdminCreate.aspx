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
        <a href="Admin.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">Administration</a> 
        <a href="CreateEmp.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">Create/Edit Users</a> 
        <a href="Analytics.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">View Analytics</a> 
        <a href="Settings.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">Settings</a> 
        
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
        <h1 class="w3-xxxlarge w3-text-red">Create/Edit Users</h1>
        <hr style="width:50px;border:5px solid red; float: left;" class="w3-round">
      </div>

    <div class="w3-container" id="administration" style="margin-top: 75px;">

    <!-- Actual Form Code -->
        <form id="feed" runat="server">
    <div>
    
        <table class="auto-style16" style="border-bottom-style: solid; border-color: #DFC39B" >
            <tr>
                <td class="auto-style52">
                    <asp:Label ID="Label18" runat="server" Font-Bold="True" ForeColor="#005480" Text="Employee Name - "></asp:Label>
                </td>
                <td class="auto-style54">
                    &nbsp;</td>
                <td class="auto-style52"></td>
                <td class="auto-style56">&nbsp;</td>
                <td class="auto-style52"></td>
            </tr>
            <tr>
                <td class="auto-style53">
                    <asp:Label ID="Label19" runat="server" Text="First Name: "></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    <asp:TextBox ID="FirstNameText" runat="server" Width="228px"></asp:TextBox>
                </td>
                <td class="auto-style55">
                    &nbsp;</td>
                <td class="auto-style53">
                    <asp:Label ID="Label20" runat="server" Text="Middle Initial: "></asp:Label>
                    <br />
                    <asp:TextBox ID="MiddleText" runat="server" Width="39px" MaxLength="1"></asp:TextBox>
                </td>
                <td class="auto-style57">
                    &nbsp;</td>
                <td class="auto-style53">
                    <asp:Label ID="Label21" runat="server" Text="Last Name: "></asp:Label>
                    <br />
                    <asp:TextBox ID="LastNameText" runat="server" Width="209px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style52">
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="(First Name Required)" Font-Italic="True" ForeColor="Red" ControlToValidate="FirstNameText" Font-Size="Small"></asp:RequiredFieldValidator>
                </td>
                <td class="auto-style54">
                    &nbsp;</td>
                <td class="auto-style52">
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="(Middle Initial Required)" Font-Italic="True" ForeColor="Red" ControlToValidate="MiddleText" Font-Size="Small"></asp:RequiredFieldValidator>
                </td>
                <td class="auto-style56">
                    &nbsp;</td>
                <td class="auto-style52">
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="(Last Name Required)" Font-Italic="True" ForeColor="Red" ControlToValidate="LastNameText" Font-Size="Small"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <br />
        <table class="auto-style16" style="border-bottom-style: solid; border-color: #DFC39B">
            <tr>
                <td class="auto-style63">
                    <asp:Label ID="Label22" runat="server" Font-Bold="True" ForeColor="#005480" Text="Employee Information - "></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style64">
                    <asp:Label ID="Label23" runat="server" Text="Email: "></asp:Label>
                    <br />
                    <asp:TextBox ID="EmailText" runat="server" Width="205px"></asp:TextBox>
                </td>
                <td class="auto-style65">
                    <br />
                </td>
            </tr>
            <tr>
                <td class="auto-style64">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="(Email Required)" ControlToValidate="EmailText" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td class="auto-style65">
                    <asp:GridView ID="GridView1" runat="server" Width="577px" AutoGenerateEditButton="True">
                    </asp:GridView>
                    </td>
            </tr>
        </table>
        <br />
        <table class="auto-style61">
            <tr>
                <td class="auto-style62">&nbsp;<asp:Button ID="Button1" runat="server" Text="Submit New User" Width="160px" OnClick="Submit_Click" />
                    <asp:Label ID="SuccessLabel" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="EditButton" runat="server" Text="Edit Users" Width="160px" CausesValidation="False" OnClick="EditButton_Click" />
                </td>
            </tr>
            <tr>
                <td class="auto-style62">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
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
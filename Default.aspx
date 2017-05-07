<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Merge</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="https://fonts.googleapis.com/css?family=Roboto+Mono" rel="stylesheet">
</head>
<body>
    <form runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see http://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="respond" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>
        <div class="container">

            <!-- NAVBAR -->
            <div class="navbar navbar-inverse navbar-fixed-top">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand site-brand" runat="server" href="~/">Merge</a>
                    </div>
                </div>
            </div>
            
            <center>
                <h1>The Best of Both Worlds</h1>
                <h5>Introducing Merge - the social media for programmers.</h5> <br />
                <a href ="Register.aspx"><asp:Button ID="SignUpButton" Text="Sign Up Now!" CssClass="btn btn-primary" OnClick ="SignUpButton_Click" runat="server"/> </a> <br /> <br />
                <p><em>Already a member? <a href="Login.aspx">Sign in!</a></em></p> <br /> <br /> <br />
                <h3>Chronological Github Integration</h3> <br /> <br />
                <h3>Post Pictures and Code</h3> <br /> <br />
                <h3>Connect With Your On-Campus Clubs and Groups</h3>
                <h3>Chat With Friends and Co-Workers</h3> <br /> <br />
                <h3>Merge your Worlds Today!</h3>
            </center>

            <footer class="site-brand" style="text-align: center;">
                <p>&copy; <%: DateTime.Now.Year %> Merge</p>
                <p>the computer science &quot;social&quot; network</p>
                <p><a href="https://github.com/o080o">Alex Durville</a> | <a href="https://www.linkedin.com/in/kayleehartmanokstate/">Kaylee Hartman</a> | <a href="https://github.com/BenDMyers">Ben Myers</a> | <a href="https://github.com/Dadadah">Jacob Schlecht</a> | <a href="https://github.com/starkeyandhutch">Drew Starkey</a></p>
            </footer>
        </div>
    </form>
</body>
</html>

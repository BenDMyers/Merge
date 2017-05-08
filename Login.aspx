<%@ Page Title="Log In" Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %> | Merge</title>

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
						
                    <div class="nav navbar-nav navbar-right">
                        <asp:LinkButton ID="Contact" CssClass="btn btn-primary navbar-btn btn-margins" OnClick="contactclick" runat="server" Text="Contact Us" />
                    </div>
                    </div>
                </div>
            </div>

		<asp:SqlDataSource ID="LoginUserData" runat="server" ProviderName = "System.Data.SqlClient" 
            ConnectionString = "<%$ ConnectionStrings:DBConnection %>"
            SelectCommand="select u.username, u.userpassword from users u where (u.username = @username and u.password = @password);">
			<SelectParameters>
				<asp:ControlParameter ControlID="Username" Name="username" PropertyName="Text" />
				<asp:ControlParameter ControlID="Password" Name="password" PropertyName="Text" />
			</SelectParameters>
		</asp:SqlDataSource>
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-4">
                    <br />
					<asp:Label ID="LogOutLabel" runat="server" Text=""></asp:Label><br /><br />
                    <p>Username*</p>
                    <asp:TextBox ID="Username" runat="server" CssClass="form-control"></asp:TextBox> <br />
                    <asp:RequiredFieldValidator ID="UsernameVal" ControlToValidate="Username" runat="server">Username is required</asp:RequiredFieldValidator> <br />
                    <p>Password*</p>
                    <asp:TextBox ID="Password" Text="Password" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox> <br />
                    <asp:RequiredFieldValidator ID="PasswordVal" ControlToValidate="Password" runat="server">Password is required</asp:RequiredFieldValidator> <br /><br />
					<asp:Label ID="ErrorLabel" runat="server" Text=""></asp:Label><br /><br />
					<asp:Button ID="LoginButton" Text="Log In" OnClick="loginclick" CssClass="btn btn-primary" runat="server" /> <br /><br />
                    <p><em>Not a member yet? <a href="Register.aspx">Sign up!</a></em></p>
				</div>
                <div class="col-md-4"></div>
            </div>

            <hr />
            <footer class="site-brand" style="text-align: center;">
                <p>&copy; <%: DateTime.Now.Year %> Merge</p>
                <p>the computer science &quot;social&quot; network</p>
                <p><a href="https://github.com/o080o">Alex Durville</a> | <a href="https://www.linkedin.com/in/kayleehartmanokstate/">Kaylee Hartman</a> | <a href="https://github.com/BenDMyers">Ben Myers</a> | <a href="https://github.com/Dadadah">Jacob Schlecht</a> | <a href="https://github.com/starkeyandhutch">Drew Starkey</a></p>
            </footer>
        </div>
    </form>
</body>
</html>

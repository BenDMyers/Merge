<%@ Page Title="Register" Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

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
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <a class="navbar-brand site-brand" runat="server" href="~/">Merge</a>
                    </div>
                </div>
            </div>

		<asp:SqlDataSource ID="RegisterUserData" runat="server"
			ProviderName = "System.Data.SqlClient"
			ConnectionString = "<%$ ConnectionStrings:DBConnection %>"
			InsertCommand="insert into users (userrealname, useravatar, useremail, username, userpassword, usergitname) values (@name, @avatar, @email, @username, @password, @git);"
			>
			<InsertParameters>
				<asp:ControlParameter ControlID="Name" Name="name" PropertyName="Text" />
				<asp:ControlParameter ControlID="Avatar" Name="avatar" PropertyName="FileName" />
				<asp:ControlParameter ControlID="Email" Name="email" PropertyName="Text" />
				<asp:ControlParameter ControlID="Username" Name="username" PropertyName="Text" />
				<asp:ControlParameter ControlID="Git" Name="git" PropertyName="Text" />
				<asp:ControlParameter ControlID="Password" Name="password" PropertyName="Text" />
			</InsertParameters>
		</asp:SqlDataSource>
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-4 form-group">
                    <br />
                    <asp:ValidationSummary ID="ValSum" DisplayMode="BulletList" HeaderText="The following errors have occured: " runat="server" />
                    <asp:Label ID="ErrorLabel" runat="server"></asp:Label><br /><br />
                    <p>OSU Email<asp:RegularExpressionValidator ID="EmailRegexVal" ControlToValidate="Email" ErrorMessage="Email must be valid OSU email." ValidationExpression="([0-9]|[A-Z]|[a-z]|\.|[!#$%&'*\+\-\/=?^_`{|}~])+@(okstate\.edu)" runat="server">*</asp:RegularExpressionValidator></p>
                    <asp:TextBox ID="Email" runat="server" CssClass="form-control"></asp:TextBox> <br />
                    <p>Full Name<asp:RequiredFieldValidator ID="NameVal" ControlToValidate="Name" ErrorMessage="Full name is required." runat="server">*</asp:RequiredFieldValidator></p>
                    <asp:TextBox ID="Name" runat="server" CssClass="form-control"></asp:TextBox> <br />
					<p>Username<asp:RequiredFieldValidator ID="UsernameVal" ControlToValidate="Username" ErrorMessage="Username is required." runat="server">*</asp:RequiredFieldValidator></p>
                    <asp:TextBox ID="Username" runat="server" CssClass="form-control"></asp:TextBox> <br />
					<p>GitHub Username</p>
                    <asp:TextBox ID="Git" runat="server" CssClass="form-control"></asp:TextBox> <br />
                    <p>Password<asp:RequiredFieldValidator ID="PasswordVal" ControlToValidate="Password" ErrorMessage="Password is required." runat="server">*</asp:RequiredFieldValidator></p>
                    <asp:TextBox ID="Password" Text="Password" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox> <br />
                    <p>Re-enter Password<asp:RequiredFieldValidator ID="PasswordAgainVal" ControlToValidate="PasswordAgain" ErrorMessage="Verify your password." runat="server">*</asp:RequiredFieldValidator></p>
                    <asp:TextBox ID="PasswordAgain" Text="Re-enter Password" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox> <br />
                    <p>Avatar</p>
                    <asp:FileUpload ID="Avatar" AllowMultiple="false" runat="server" CssClass="form-control-file" type="file"/> <br />
                </div>
                <div class="col-md-4">
                </div>
            </div>
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-4">
                    <asp:Button ID="Submit" Text="Submit" OnClick="submitclick" CssClass="btn btn-primary" runat="server" /> <br /><br />
                    <p><em>Already a member? <a href="Login.aspx">Sign in!</a></em></p>
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

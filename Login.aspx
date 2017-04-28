<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %>Register</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

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
		<asp:SqlDataSource ID="RegisterUserData" runat="server"
			ProviderName = "System.Data.SqlClient"
			ConnectionString = "<%$ ConnectionStrings:DBConnection %>"
			SelectCommand="select u.username, u.userpassword from users u where (u.username = @username and u.password = @password);"
			>
			<SelectParameters>
				<asp:ControlParameter ControlID="Username" Name="username" PropertyName="Text" />
				<asp:ControlParameter ControlID="Password" Name="password" PropertyName="Text" />
			</SelectParameters>
		</asp:SqlDataSource>
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-4">
                    <p>Username*</p>
                    <asp:TextBox ID="Username" runat="server"></asp:TextBox> <br />
                    <asp:RequiredFieldValidator ID="UsernameVal" ControlToValidate="Username" runat="server">Username is required</asp:RequiredFieldValidator> <br />
                    <p>Password*</p>
                    <asp:TextBox ID="Password" Text="Password" TextMode="Password" runat="server"></asp:TextBox> <br />
                    <asp:RequiredFieldValidator ID="PasswordVal" ControlToValidate="Password" runat="server">Password is required</asp:RequiredFieldValidator> <br />
					<asp:Button ID="LoginButton" Text="Login" OnClick="loginclick" CssClass="btn-primary" runat="server" /> <br />
				</div>
                <div class="col-md-4"></div>
            </div>
        </div>
    </form>
</body>
</html>

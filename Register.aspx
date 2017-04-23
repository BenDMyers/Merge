<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div id="registerForm">
                <asp:TextBox ID="Email" Text="OSU Email" runat="server"></asp:TextBox> <br />
                <asp:RequiredFieldValidator ID="EmailVal" ControlToValidate="Email" runat="server">Email is required</asp:RequiredFieldValidator> 
                <asp:RegularExpressionValidator ID="EmailRegexVal" ControlToValidate="Email" ValidationExpression="([0-9]|[a-z]|\.|[!#$%&'*\+\-\/=?^_`{|}~])+@((okstate\.edu)|(ostatemail\.okstate\.edu))" runat="server">Email must be valid OSU email</asp:RegularExpressionValidator> <br />
                <asp:TextBox ID="Name" Text="Full Name" runat="server"></asp:TextBox> <br />
                <asp:RequiredFieldValidator ID="NameVal" ControlToValidate="Name" runat="server">Full name is required</asp:RequiredFieldValidator> <br />
                <asp:TextBox ID="Password" Text="Password" TextMode="Password" runat="server"></asp:TextBox> <br />
                <asp:RequiredFieldValidator ID="PasswordVal" ControlToValidate="Password" runat="server">Password is required</asp:RequiredFieldValidator> <br />
                <asp:TextBox ID="PasswordAgain" Text="Re-enter Password" TextMode="Password" runat="server"></asp:TextBox> <br />
                <asp:RequiredFieldValidator ID="PasswordAgainVal" ControlToValidate="PasswordAgain" runat="server">Verify your password</asp:RequiredFieldValidator> <br />
                <asp:FileUpload ID="Avatar" AllowMultiple="false" runat="server" /> <br />
                <asp:Button ID="Submit" Text="Submit" CssClass="btn-primary" runat="server" /> <br />
            </div>
        </div>
        <asp:SqlDataSource ID="UserEntryDS" InsertCommand="" runat="server"></asp:SqlDataSource>
    </form>
</body>
</html>

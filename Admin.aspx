<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

<!--comments are bollocks
to-do:
admin stuff
database stuffs Kaylee-->

    <asp:SqlDataSource ID="SqlUserData" 
             Runat="server" 
             SelectCommand="SELECT [userid], [username], 
               [userrealname], [usergitname], [useravatar], [useremail] FROM 
               [users]"
         ConnectionString="<%$ ConnectionStrings:DBConnection %>" 
         DataSourceMode="DataReader">
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="SqlGroupData" 
             Runat="server" 
             SelectCommand="SELECT [groupid], [groupname], 
               [groupavatar], [gabout] FROM 
               [groups]"
         ConnectionString="<%$ ConnectionStrings:DBConnection %>" 
         DataSourceMode="DataReader">
    </asp:SqlDataSource>


    <asp:SqlDataSource ID="RegisterGroupData" runat="server"
			ProviderName = "System.Data.SqlClient"
			ConnectionString = "<%$ ConnectionStrings:DBConnection %>"
			InsertCommand="insert into groups (groupname, groupavatar, gabout) values (@name, @avatar, @about);"
			>
			<InsertParameters>
				<asp:ControlParameter ControlID="NewGroupName" Name="name" PropertyName="Text" />
				<asp:ControlParameter ControlID="NewGroupAbout" Name="about" PropertyName="Text" />
				<asp:ControlParameter ControlID="NewGroupAvatar" Name="avatar" PropertyName="FileName" />
			</InsertParameters>
		</asp:SqlDataSource>

    <asp:GridView ID="UserView" runat="server" DataKeyNames="userid" DataSourceID="SqlUserData" AutoGenerateColumns="false" OnRowCommand="UserView_OnRowCommand">
        <Columns>
            <asp:BoundField DataField="userid" HeaderText="userid" />
            <asp:BoundField DataField="username" HeaderText="username" />
            <asp:BoundField DataField="userrealname" HeaderText="real name" />
            <asp:BoundField DataField="useravatar" HeaderText="avatar" />
            <asp:BoundField DataField="useremail" HeaderText="email" />

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="RemoveUserButton" CssClass="btn btn-default" runat="server" CausesValidation="false" CommandName="RemoveUser"
                        Text="Remove" CommandArgument='<%# Eval("userid") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="EditUserButton" CssClass="btn btn-default" runat="server" CausesValidation="false" CommandName="EditUser"
                        Text="Edit" CommandArgument='<%# Eval("userid") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:GridView ID="GroupsView" runat="server" DataKeyNames="groupid" DataSourceID="SqlGroupData" AutoGenerateColumns="false" OnRowCommand="GroupView_OnRowCommand">
        <Columns>
            <asp:BoundField DataField="groupid" HeaderText="groupid" />
            <asp:BoundField DataField="groupname" HeaderText="name" />
            <asp:BoundField DataField="groupavatar" HeaderText="avatar" />
            <asp:BoundField DataField="gabout" HeaderText="description" />

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="RemoveGroupButton" CssClass="btn btn-default" runat="server" CausesValidation="false" CommandName="RemoveGroup"
                        Text="Remove" CommandArgument='<%# Eval("groupid") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="EditGroupButton" CssClass="btn btn-default" runat="server" CausesValidation="false" CommandName="EditGroup"
                        Text="Edit" CommandArgument='<%# Eval("groupid") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    

    <h1>Add a new group</h1>
    <p> group name:
    <asp:TextBox ID="NewGroupName" CssClass="form-control" runat="server" /> </p>
    <p> description:
    <asp:TextBox ID="NewGroupAbout" CssClass="form-control" runat="server" /> </p>
    <p>avatar: <asp:FileUpload ID="NewGroupAvatar" CssClass="form-control-file" AllowMultiple="false" runat="server" type="file"/></p>
    <asp:Button ID="NewGroup" CssClass="btn btn-default" runat="server" OnClick="NewGroup_OnClick" Text="Add Group"/>

    <asp:Panel ID="debugr" runat="server"></asp:Panel>

</asp:Content>
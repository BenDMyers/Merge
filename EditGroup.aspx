<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EditGroup.aspx.cs" Inherits="EditGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <asp:SqlDataSource ID="SqlMemberData" 
             Runat="server" 
             SelectCommand="SELECT [muserid], [mgroupid], [madmin], [username]
                FROM 
               [member]
                JOIN users ON [muserid] = [userid]
                WHERE [mgroupid] = @groupid"
         ConnectionString="<%$ ConnectionStrings:DBConnection %>" 
         DataSourceMode="DataReader">
        
			<SelectParameters>
				<asp:QueryStringParameter QueryStringField="gid" Name="groupid"/>
            </SelectParameters>
    </asp:SqlDataSource>


     <asp:GridView ID="MemberView" runat="server" DataKeyNames="muserid" DataSourceID="SqlMemberData" AutoGenerateColumns="false" OnRowCommand="MemberView_OnRowCommand">
        <Columns>
            <asp:BoundField DataField="muserid" HeaderText="userid" />
            <asp:BoundField DataField="username" HeaderText="username" />
            <asp:BoundField DataField="madmin" HeaderText="is admin" />

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="RemoveMember" CssClass="btn btn-default" runat="server" CausesValidation="false" CommandName="RemoveMember"
                        Text="Remove" CommandArgument='<%# Eval("muserid") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="Adminify" CssClass="btn btn-default" runat="server" CausesValidation="false" CommandName="AdminifyMember"
                        Text="Make Admin" CommandArgument='<%# Eval("muserid") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="DeAdminify" CssClass="btn btn-default" runat="server" CausesValidation="false" CommandName="DeAdminifyMember"
                        Text="Remove Admin" CommandArgument='<%# Eval("muserid") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    
    <h1>Add a member</h1>
    <p> username:
    <asp:TextBox ID="MemberUsername" CssClass="form-control" runat="server" /> </p>
    <p> userid: 
    <asp:TextBox ID="MemberUserid" CssClass="form-control" runat="server" /> </p>    
    <asp:Button ID="NewMember" CssClass="btn btn-default" runat="server" OnClick="NewMember_OnClick" Text="Add Member"/>

    <asp:Panel ID="debugr" runat="server"></asp:Panel>

</asp:Content>

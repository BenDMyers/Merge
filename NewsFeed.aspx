<%@ Page Title="News Feed" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="NewsFeed.aspx.cs" Inherits="NewsFeed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<!--comments are bollocks
to-do:
GitHub stuff
post modal
post repeater-ish thing
see Alex and Ben
database stuffs Kaylee-->


    <asp:Panel ID="Panel" runat="server"></asp:Panel>
	<%--This is the select statement is for displaying last 20 posts made
	<asp:SqlDataSource ID="PosttData" runat="server" ProviderName = "System.Data.SqlClient" 
            ConnectionString = "<%$ ConnectionStrings:DBConnection %>"
            SelectCommand="
			select TOP(20) * 
				from postt p
				left join users u on u.userid = p.puserid
				left join groups g on g.groupid = p.pgroupid
				order by p.postid desc;">
	</asp:SqlDataSource>--%>

    <br /><br />
</asp:Content>


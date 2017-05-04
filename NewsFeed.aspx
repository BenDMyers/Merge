<%@ Page Title="News Feed" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="NewsFeed.aspx.cs" Inherits="NewsFeed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<!--comments are bollocks
to-do:
GitHub stuff
post modal
post repeater-ish thing
see Alex and Ben
database stuffs Kaylee-->


    <button id="ModalTestToggler" type="button" class="btn btn-primary" onclick="$('#PostModal').modal('toggle')">Toggle Modal</button>
    <asp:Panel ID="Panel" runat="server"></asp:Panel>
	<%--This is the select statement is for displaying last 20 posts made --%>
	<%--<asp:SqlDataSource ID="PosttData" runat="server" ProviderName = "System.Data.SqlClient" 
            ConnectionString = "<%$ ConnectionStrings:DBConnection %>"
            SelectCommand="
			select TOP(20) * 
				from postt p
				left join users u on u.userid = p.puserid
				left join groups g on g.groupid = p.pgroupid
				order by p.postid desc;">
			<SelectParameters>
				<asp:ControlParameter ControlID="Stuff" Name="put stuffs here" PropertyName="Text" />
			</SelectParameters>
	</asp:SqlDataSource>--%>

    <br /><br />

    <ul class="fa-ul">
        <li class="li-chatbox li-chatbox-folder">Users<ul class="fa-ul">
            <li class="li-chatbox li-chatbox-user"><a href="">Ben.usr</a></li>
            <li class="li-chatbox li-chatbox-user"><a href="">Jacob.usr</a></li></ul></li>
        <li class="li-chatbox li-chatbox-folder">Groups<ul class="fa-ul">
            <li class="li-chatbox li-chatbox-group"><a href="">Association_for_Computing_Machinery.grp</a></li>
            <li class="li-chatbox li-chatbox-group"><a href="">Video_Game_Developers_Club.grp</a></li></ul></li>
    </ul>
</asp:Content>


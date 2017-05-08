<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
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
    <form id="form1" runat="server">
	<!-- NAVBAR -->
    <div class="navbar navbar-inverse navbar-fixed-top">
		<div class="container">
			<div class="navbar-header">
				<a class="navbar-brand site-brand" runat="server" href="~/Default">Merge</a>
			</div>
		</div>
    </div>
	<div class="site-brand" style="text-align: center;">
        <h2>Merge</h2>
        <h3>the computer science &quot;social&quot; network</h3>
    <br />
		<p>Email or visit our developers at:</p>
		<ul style="list-style: none;">
            <li>Alex Durville: <a href="mailto:alex.durville@okstate.edu">Alex's Email</a> || <a href="https://github.com/o080o">Alex's GitHub</a></li>
            <li>Kaylee Hartman: <a href="mailto:kaylee.hartman@okstate.edu">Kaylee's Email</a> || <a href="https://www.linkedin.com/in/kayleehartmanokstate/">Kaylee's LinkedIn</a></li>
            <li>Ben Myers: <a href="mailto:ben.myers@okstate.edu">Ben's Email</a> || <a href="https://github.com/BenDMyers">Ben's GitHub</a></li>
            <li>Jacob Schlecht: <a href="mailto:jacob.schlecht@okstate.edu">Jacob's Email</a> || <a href="https://github.com/Dadadah">Jacob's GitHub</a></li>
            <li>Drew Starkey: <a href="mailto:andrew.starkey@okstate.edu">Drew's Email</a> || <a href="https://github.com/starkeyandhutch">Drew's GitHub</a></li>
        </ul>
    </div>
	<footer class="site-brand" style="text-align: center;">
		<p>&copy; <%: DateTime.Now.Year %> Merge</p>
		<p>the computer science &quot;social&quot; network</p>
    </footer>
    </form>
</body>
</html>

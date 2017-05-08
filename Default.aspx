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
		<div id="BackgroundPic" style="background-position: center; left: 0; background-image: url('pictures/DefaultBackground.jpg'); "> 

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
            <div id="SignUp" style="width: 100vw; text-align: center;">
                <div id="HeaderPic" style="background-position: center; left: 0; width: 100vw; position: absolute; background-image: url('pictures/HeaderImage.jpg'); "> 
                <h1>The Best of Both Worlds</h1>
                <h3>Introducing Merge - the social media for programmers.</h3> <br />
                <a href ="Register.aspx"><asp:Button ID="SignUpButton" Text="Merge Your Worlds Today!" CssClass="btn btn-primary" OnClick ="SignUpButton_Click" runat="server"/> </a> <br /> <br />
                <p><em>Already a member? <a href="Login.aspx">Sign in!</a></em></p> <br /> <br /> <br />
                </div>
            </div>
            <div class="container" id="features">                
					<div class="clearfix" id="GithubDescription" style="border-bottom-style: solid; border-bottom-width: 1px; margin-top: 300px"> 
                    <img alt="GithubPic" src="https://assets-cdn.github.com/images/modules/logos_page/GitHub-Mark.png" style="display: inline-block; text-align: left; width: 20vw; height: 15vw; position: relative" />
                    <div class="text" style="display: inline-block; text-align: left;">
                        <h3>Chronological Github Integration</h3>
                        <p>Never miss a push with Github notifications showing up in your newsfeed.</p>
                    </div>
                    <br /> <br /> <br />
                </div> 
                <br /> <br />
                <div class="clearfix" id="PicturesNCode" style="text-align: right; border-bottom-style: solid; border-bottom-width: 1px;">
                    <div class="text" style="display: inline-block; text-align: right;">
                        <h3>Post Pictures and Code</h3>
                        <p>Show your code, display, and comments all in one post!</p>
                    </div>
                    <img alt="PicturesNCodePic" src="http://media.istockphoto.com/photos/coding-picture-id519161626?k=6&m=519161626&s=612x612&w=0&h=vtPo_EoYkP6EmbWIlZ7bpPF9tJbmP40W7mjKPOdQC5w=" style="width: 20vw; height: 15vw; display: inline-block" />
                    <br /> <br /> <br />
                </div>
                <br /> <br /> 
                <div class="clearfix" id="ClubsnGroups" style="border-bottom-style: solid; border-bottom-width: 1px">
                    <img alt="GroupsPic" src="https://avatars3.githubusercontent.com/u/21269750?v=3&s=280style=" style="display: inline-block; width: 20vw; height: 15vw;" />
                    <div class="text" style="display: inline-block; text-align: left;">
                        <h3>Connect With Your On-Campus Clubs and Groups</h3>
                        <p>Sign up your club and communicate, post code, and chat with your members.</p>
                    </div>
                    <br /> <br /> <br />
                </div>
                <br /> <br />
                <div class="clearfix" id="ChatFeature" style="text-align: right; border-bottom-style: solid; border-bottom-width: 1px;">
                    <div class="text" style="display: inline-block; text-align: right;">
                        <h3>Chat With Friends and Co-Workers</h3> 
                        <p>Merge allows you to chat with any user </p>
                    </div>
                    <img alt="ChatPic" src="https://ak9.picdn.net/shutterstock/videos/6685271/thumb/1.jpg" style="display: inline-block; width: 20vw; height: 15vw;" />
                <br /> <br /> <br />
                </div>
        
                <br /> <br />
              
        </div>
            <footer class="site-brand" style="text-align: center;">
                <p>&copy; <%: DateTime.Now.Year %> Merge</p>
                <p>the computer science &quot;social&quot; network</p>
                <p><a href="https://github.com/o080o">Alex Durville</a> | <a href="https://www.linkedin.com/in/kayleehartmanokstate/">Kaylee Hartman</a> | <a href="https://github.com/BenDMyers">Ben Myers</a> | <a href="https://github.com/Dadadah">Jacob Schlecht</a> | <a href="https://github.com/starkeyandhutch">Drew Starkey</a></p>
            </footer>
        </div>
		</div>
    </form>
</body>
</html>

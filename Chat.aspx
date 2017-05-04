<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Chat.aspx.cs" Inherits="Chat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <div id="chatContainer">
        <label id="outgoingUser"></label>
        <div id="chatIncoming" style="overflow-y:auto;max-height:70vh;">
            <em>Please wait while your chat partner conencts....</em><br />
        </div>
        <input id="chatEntry" style="width:70%" type="text" />
        <script type="text/javascript">
            $(function () {

                var parseForUserName = window.location.href;
                var parsed = parseForUserName.split("=");
                var target = parsed[1];
                $("#outgoingUser").text(target);

                $("#chatEntry").keydown(function (event) {
                    var pmchat = $.connection.pMHub;
                    if (event.keyCode == 13) {
                        if ($(this).val() == null || $(this).val() == "") return false;
                        pmchat.server.send($("#outgoingUser").text(), $(this).val());
                        $('#chatIncoming').stop().animate({
                            scrollTop: $('#chatIncoming')[0].scrollHeight
                        }, 800);
                        $(this).val("");
                        return false;
                    }
                });
            });
        </script>
    </div>

<!--comments are bollocks
to-do:
chat stuff
see Jacob-->
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ChatGroup.aspx.cs" Inherits="ChatGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <div id="chatContainer">
        <label id="outgoingUser"></label>
        <div id="chatIncoming" style="overflow-y:auto;height:70vh;">
            <span class="joinedchat">Please wait while we connect you to the group chat...</span><br />
        </div>
        <input id="chatEntry" class="form-control" style="width:70%" type="text" />
        <script type="text/javascript">
            $(function () {

                var groupchat = $.connection.groupChatHub;

                var target = <%: Session["TempGroupChatName"] %>
                $("#outgoingUser").text(target);

                $("#chatEntry").keydown(function (event) {
                    var groupchat = $.connection.groupChatHub;
                    if (event.keyCode == 13) {
                        if ($(this).val() == null || $(this).val() == "") return false;
                        groupchat.server.sendGroupMessage($(this).val());
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

</asp:Content>


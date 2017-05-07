using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

public class GroupChatHub : Hub
{
    
    public async Task ConnectToGroupChat(string userName, string group)
    {
        var user = PMHub.ConnectedUsers.FirstOrDefault(x => x.GetID() == Context.ConnectionId);
        if (user == null)
        {
            user = new UserDetail(Context.ConnectionId, userName);
            PMHub.ConnectedUsers.Add(user);
            Clients.Others.updateOnlineUsers(userName, true);
        }
        user.SetGroup(group);
        await Groups.Add(Context.ConnectionId, group);
        Clients.Group(group).sendMessageFromGroup(userName, "<span class='joinedchat'>Has joined the chat.</span>");
    }

    public override Task OnDisconnected()
    {
        var user = PMHub.ConnectedUsers.FirstOrDefault(x => x.GetID() == Context.ConnectionId);
        if (user == null || user.GetGroup() == null) return base.OnDisconnected();
        Groups.Remove(Context.ConnectionId, user.GetGroup());
        Clients.OthersInGroup(user.GetGroup()).sendMessageFromGroup(user.GetUserName(), "<span class='joinedchat'>Has left the chat.</span>");
        return base.OnDisconnected();
    }

    public void SendGroupMessage(string message)
    {
        var user = PMHub.ConnectedUsers.FirstOrDefault(x => x.GetID() == Context.ConnectionId);
        if (user == null || user.GetGroup() == null) return;
        Clients.Group(user.GetGroup()).sendMessageFromGroup(user.GetUserName(), message);
    }

    public void WhosInGroup(string group)
    {
        List<string> ConnectedUserNames = new List<string>();
        foreach (UserDetail user in PMHub.ConnectedUsers)
        {
            if (user.GetGroup() == group && !ConnectedUserNames.Contains(user.GetUserName()))
                ConnectedUserNames.Add(user.GetUserName());
        }
        Clients.Caller.onlineGroupUsers(ConnectedUserNames);
    }

}

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
        if (user == null) return;
        user.SetGroup(group);
        await Groups.Add(Context.ConnectionId, group);
        Clients.Group(group).sendMessage(userName, "<span class='joinedchat'>Has joined the channel.</span>");
    }

    public override Task OnDisconnected()
    {
        var user = PMHub.ConnectedUsers.FirstOrDefault(x => x.GetID() == Context.ConnectionId);
        if (user == null) return base.OnDisconnected();
        Groups.Remove(Context.ConnectionId, user.GetGroup());
        Clients.OthersInGroup(user.GetGroup()).sendMessage(user.GetUserName(), "<span class='joinedchat'>Has left the chat.</span>");
        return base.OnDisconnected();
    }

    public void SendGroupMessage(string message)
    {
        var user = PMHub.ConnectedUsers.FirstOrDefault(x => x.GetID() == Context.ConnectionId);
        if (user == null) return;
        Clients.Group(user.GetGroup()).sendMessage(user.GetUserName(), message);
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

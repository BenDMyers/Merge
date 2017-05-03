using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

public class PMHub : Hub
{
    private List<UserDetail> ConnectedUsers = new List<UserDetail>();

    public void Connect(string userName)
    {
        UserDetail newUser = new UserDetail(Context.ConnectionId, userName);
        if (!ConnectedUsers.Contains(newUser))
        ConnectedUsers.Add(newUser);
        Clients.All.updateOnlineUsers(userName, true);
        WhosOnline();
    }

    public void Disconnect(string userName)
    {
        ConnectedUsers.Remove(new UserDetail(Context.ConnectionId, userName));
        Clients.All.updateOnlineUsers(userName, false);
    }

    public void Send(string target, string message)
    {
        UserDetail messageTarget = FindUserByUserName(target);
        UserDetail messageSender = FindUserById(Context.ConnectionId);
        if (messageTarget == null || messageSender == null) return;
        var name = messageSender.GetUserName();
        Clients.Client(messageTarget.GetID()).sendMessage(name, message);
    }

    public void WhosOnline()
    {
        List<string> ConnectedUserNames = new List<string>();
        foreach (UserDetail user in ConnectedUsers)
        {
            ConnectedUserNames.Add(user.GetUserName());
        }
        Clients.Caller.onlineUsers(ConnectedUserNames.ToArray());
    }

    private UserDetail FindUserByUserName(string userName)
    {
        return ConnectedUsers.Find(x => x.GetUserName() == userName);
    }

    private UserDetail FindUserById(string id)
    {
        return ConnectedUsers.Find(x => x.GetID() == id);
    }

    internal class UserDetail
    {
        string ConnectionId;
        string UserName;
        public UserDetail(string id, string uname)
        {
            ConnectionId = id;
            UserName = uname;
        }

        public string GetUserName()
        {
            return UserName;
        }

        public string GetID()
        {
            return ConnectionId;
        }
    }
}

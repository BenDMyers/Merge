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
    }

    public void Disconnect(string userName)
    {
        ConnectedUsers.Remove(new UserDetail(Context.ConnectionId, userName));
    }

    public void Send(string name, string target, string message)
    {
        Clients.Client(FindUserByUserName(target).GetID()).sendMessage(name, message);
    }

    private UserDetail FindUserByUserName(string userName)
    {
        return ConnectedUsers.Find(x => x.GetUserName() == userName);
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
            return UserName;
        }
    }
}

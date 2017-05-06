using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserDetail
/// </summary>
public class UserDetail
{
    private string connectionId;
    private string userName;
    private string curGroup;

    public UserDetail(string id, string uname)
    {
        connectionId = id;
        userName = uname;
    }

    public string GetUserName()
    {
        return userName;
    }

    public string GetID()
    {
        return connectionId;
    }

    public void SetGroup(string group)
    {
        curGroup = group;
    }

    public string GetGroup()
    {
        return curGroup;
    }
}
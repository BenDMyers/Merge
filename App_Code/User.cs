using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
// simple container class for user info.
/// </summary>

public class User
{
    public String username;
    public String avatar;
    public String gitname;
	public int userid;

    public User(String curusername, String curavatar, int curuserid)
    {
        username = curusername;   // lulz these names are terrible
        avatar = curavatar;       // lulz these names are terrible
		userid = curuserid;
    }
}
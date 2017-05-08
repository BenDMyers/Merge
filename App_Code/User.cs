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
	public int admin;

    public User(String curusername, String curavatar, int curuserid, int curadmin)
    {
        username = curusername;   // lulz these names are terrible
        avatar = "/pictures/avatars/" + curavatar;       // lulz these names are terrible
		userid = curuserid;
		admin = curadmin;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LogOut : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		Session["LogOut"] = "1";
		Session["Username"] = null;
		Session["UserId"] = null;
		Response.Redirect("Login.aspx");
	}
}
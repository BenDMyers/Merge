using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["Username"] != null) Response.Redirect("NewsFeed.aspx");
    }

    protected void SignUpButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register.aspx");
	}

	protected void contactclick(object sender, EventArgs e)
	{
		Response.Redirect("Contact.aspx");
	}

}
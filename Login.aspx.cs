using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
        if ((string)Session["Username"] != null) Response.Redirect("NewsFeed.aspx");
		if ((string)Session["LogOut"] == "1")
		{
			LogOutLabel.Text = "Sucessfully logged out.";
			Session["LogOut"] = null;
		}
	}

	public void loginclick(object sender, EventArgs e) {
		loginUser();
	}

	public void loginUser() {
		//Connect to the database and check to see if user already exists, if it does, compare the password
		string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
		SqlConnection conn = new SqlConnection(connectionString);
		string query = "SELECT * FROM users WHERE username = '" + Username.Text + "'";
		SqlCommand com = new SqlCommand(query, conn);
		conn.Open();

		//Actually execute the query and return the results
		SqlDataReader dr = com.ExecuteReader();

		//Loop through results and output to the page
		if (dr.HasRows)
		{
			//Check the user information
			while (dr.Read())
			{
				//do some things

				//Check to see if the password is the same
				if (dr["userpassword"].ToString() == Password.Text)
				{
					//login the user
					Session["Username"] = dr["Username"].ToString();
					Session["UserId"] = dr["UserId"].ToString();

					Response.Redirect("NewsFeed.aspx");
				}
				else
				{
					//passwords dont match
					//give the user a warning.
					ErrorLabel.Text = "Please enter the correct password!";
				}
			}
		}
		else
		{
			//Tell the user to register
			ErrorLabel.Text = "Please register!";

		}

		conn.Close();
	}

	protected void contactclick(object sender, EventArgs e)
	{
		Response.Redirect("Contact.aspx");
	}
}
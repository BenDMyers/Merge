using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

	public void submitclick(object sender, EventArgs e) {
		string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
		SqlConnection con = new SqlConnection(connectionString);
		con.Open();

		SqlCommand usernamecmd = new SqlCommand("select * from users where username=@Name", con);
		SqlCommand emailcmd = new SqlCommand("select * from users where useremail=@Email", con);
		usernamecmd.Parameters.AddWithValue("@Name", Username.Text);
		emailcmd.Parameters.AddWithValue("@Email", Email.Text);
		SqlDataReader undr = usernamecmd.ExecuteReader();
		var usernameTaken = undr.HasRows;
		undr.Close();
		SqlDataReader edr = emailcmd.ExecuteReader();
		var emailTaken = edr.HasRows;
		edr.Close();

		if (emailTaken)
		{
			ErrorLabel.Text = "Email already registered, please login.";
		}
		else if (usernameTaken)
		{
			ErrorLabel.Text = "Username already taken.";
		}
		else
		{
			if (Avatar.HasFile)
			{
				//Using a try statement allows us to output debugging problems.
				try
				{
					string filename = Avatar.FileName;
					Avatar.SaveAs(Server.MapPath("~/pictures/avatars/") + filename);
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine("Upload status: The file could not be uploaded. The following error occured: " + ex.Message);
				}
			}
			// Username is available
			RegisterUserData.Insert();
			Response.Redirect("Login.aspx");
		}
	}

}
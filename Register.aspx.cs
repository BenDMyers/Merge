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
		if (Avatar.HasFile)
		{
			//Using a try statement allows us to output debugging problems.
			try
			{
				string filename = Avatar.FileName;
				Avatar.SaveAs(Server.MapPath("~/avatars/") + filename);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Upload status: The file could not be uploaded. The following error occured: " + ex.Message);
			}
		}
		else
		{
			try
			{
				Avatar.SaveAs(Server.MapPath("~/images/img_avatar3.png"));
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("There was a problem creating your profile. Error: " + ex.Message);
			}
		}

		RegisterPersonaData.Insert();

		RegisterUserData.Update();
	}
}
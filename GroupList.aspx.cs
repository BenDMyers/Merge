using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class GroupList : System.Web.UI.Page
{
	// this is a shortcut for your connection string
	static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;


	// simple container class for user info.
	private class Group
	{
		public String groupname;
		public String avatar;

		public Group(String curgroupname, String curavatar)
		{
			groupname = curgroupname;   // lulz these names are terrible
			avatar = curavatar;       // lulz these names are terrible
		}
	}

	private DataTable testSql()
	{
		// do stuff
		using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
		{
			SqlCommand cmd = new SqlCommand("SELECT * FROM group", conn);
			cmd.Connection.Open();
			DataTable TempTable = new DataTable();
			TempTable.Load(cmd.ExecuteReader());
			return TempTable;
		}
	}


	protected void Page_Load(object sender, EventArgs e)
	{
		//Check to see if the user has logged in, if not disable ability to post a car
		if (Session["Username"] == null)
		{
			Response.Redirect("Login.aspx");
		}

		//Connect to the database and check to see if user already exists, if it does, compare the password
		string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
		SqlConnection conn = new SqlConnection(connectionString);
		string query = "select TOP(20) * from member m left join users u on u.userid = m.muserid left join groups g on g.groupid = m.mgroupid where m.muserid = " +  Session["UserId"] + ";";
		SqlCommand cmd = new SqlCommand(query, conn);

		conn.Open();

		//Actually execute the query and return the results
		SqlDataReader reader = cmd.ExecuteReader();
		string[] checkarray = new string[14];
		while (reader.Read())
		{
			checkarray[0] = reader[0].ToString();       //muserid
			checkarray[1] = reader[1].ToString();       //mgroupid
			checkarray[2] = reader[2].ToString();       //madmin
			checkarray[3] = reader[3].ToString();       //userid
			checkarray[4] = reader[4].ToString();       //username
			checkarray[5] = reader[5].ToString();       //userrealname
			checkarray[6] = reader[6].ToString();       //usergitname
			checkarray[7] = reader[7].ToString();       //useravatar
			checkarray[8] = reader[8].ToString();       //useremail
			checkarray[9] = reader[9].ToString();       //userpassword
			checkarray[10] = reader[10].ToString();     //groupid
			checkarray[11] = reader[11].ToString();     //groupname
			checkarray[12] = reader[12].ToString();     //groupavatar
			checkarray[13] = reader[13].ToString();     //gabout

			Session["GroupId"] = Int32.Parse(checkarray[10]);
			Group group = new Group(checkarray[11], checkarray[12]);
			AddPost(Panel, group);
		}
		reader.Close();
		conn.Close();
	}

	private void AddPost(Panel panel, Group group)
	{
		// layout of this code block should resemble the layout of the generated HTML!
		// ( i.e. outer elements are created first, and added after - creation is open tag, adding is close tag
		Panel post = new Panel();
		post.CssClass = "post-container";
		Panel block = new Panel();
		block.CssClass = "post-block";

		// make the username and avatar container
		Panel userContainer = new Panel();
		userContainer.CssClass = "user-info";

		if (group.avatar != "NULL")
		{
			Image userAvatar = new Image();
			userAvatar.CssClass = "avatar";
			userAvatar.ImageUrl = "/pictures/avatars/" + group.avatar;
			// and finally add them to the container
			userContainer.Controls.Add(userAvatar);
		}

		Label groupText = new Label();
		groupText.Text = group.groupname;

		// and finally add them to the container
		userContainer.Controls.Add(groupText);

		// and add the container to the outer block
		block.Controls.Add(userContainer);

		post.Controls.Add(block);
		panel.Controls.Add(post);
	}
}
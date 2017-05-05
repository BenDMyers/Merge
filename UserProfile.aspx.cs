using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class UserProfile : System.Web.UI.Page
{
	// this is a shortcut for your connection string
	static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;


	// simple container class for user info.
	private class User
	{
		public String username;
		public String avatar;

		public User(String curusername, String curavatar)
		{
			username = curusername;   // lulz these names are terrible
			avatar = curavatar;       // lulz these names are terrible
		}
	}

	private DataTable testSql()
	{
		// do stuff
		using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
		{
			SqlCommand cmd = new SqlCommand("SELECT * FROM POSTT", conn);
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
		string query = "select TOP(20) * from postt p left join users u on u.userid = p.puserid where p.puserid = " + Session["UserId"] + " order by p.ptimestamp desc;";
		SqlCommand cmd = new SqlCommand(query, conn);

		conn.Open();

		//Actually execute the query and return the results
		SqlDataReader reader = cmd.ExecuteReader();
		string[] checkarray = new string[21];
		while (reader.Read())
		{
			checkarray[0] = reader[0].ToString();       //postid
			checkarray[1] = reader[1].ToString();       //ptext
			checkarray[2] = reader[2].ToString();       //ptimestamp
			checkarray[3] = reader[3].ToString();       //phascom
			checkarray[4] = reader[4].ToString();       //commentid
			checkarray[5] = reader[5].ToString();       //puserid
			checkarray[6] = reader[6].ToString();       //pgroupid
			checkarray[7] = reader[7].ToString();       //pcode
			checkarray[8] = reader[8].ToString();       //ppicfile
			checkarray[9] = reader[9].ToString();       //pedate
			checkarray[10] = reader[10].ToString();     //petime
			checkarray[11] = reader[11].ToString();     //peinfo
			checkarray[12] = reader[12].ToString();     //userid
			checkarray[13] = reader[13].ToString();     //username
			checkarray[14] = reader[14].ToString();     //userrealname
			checkarray[15] = reader[15].ToString();     //useravatar
			checkarray[16] = reader[16].ToString();     //useremail
			checkarray[17] = reader[17].ToString();     //userpassword

			User user = new User(checkarray[13], checkarray[15]);
			AddPost(Panel, user, checkarray[1], checkarray[8], checkarray[7]);
		}
		reader.Close();
		conn.Close();
	}

	private void AddPost(Panel panel, User user, String text, String imgSrc, String codeSrc)
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

		if (user.avatar != "NULL")
		{
			Image userAvatar = new Image();
			userAvatar.CssClass = "avatar";
			userAvatar.ImageUrl = "/pictures/avatars/" + user.avatar;
			// and finally add them to the container
			userContainer.Controls.Add(userAvatar);
		}

		Label userText = new Label();
		userText.Text = user.username;

		// and finally add them to the container
		userContainer.Controls.Add(userText);

		// and add the container to the outer block
		block.Controls.Add(userContainer);


		// add the text container
		Panel textContainer = new Panel();
		textContainer.CssClass = "content-container text-content";

		Label textLabel = new Label();
		textLabel.Text = text;
		textContainer.Controls.Add(textLabel);

		//and finally add the container to the outer block
		block.Controls.Add(textContainer);

		if (codeSrc != null && codeSrc != "")
		{
			//Literal code = new Literal();
			//code.Text = "<pre><code>" + "</code></pre>"; //ROFL HOW FAST CAN YOU SAY CODE INJECTION!

			Panel codePanel = new Panel();
			codePanel.CssClass = "content-container code-content";

			// make the code element - this involves some special html tags that don't exist in asp
			// so F**K ASP.NET and let's write literal HTML
			Literal codePre = new Literal();
			codePre.Text = "<pre><code>";
			Label code = new Label();
			code.Text = HttpUtility.HtmlEncode(codeSrc); // maybe not code injection? 
			Literal codePost = new Literal();
			codePost.Text = "</pre></code>";

			// and add the code to the container
			codePanel.Controls.Add(codePre);
			codePanel.Controls.Add(code);
			codePanel.Controls.Add(codePost);

			block.Controls.Add(codePanel);
		}
		if (imgSrc != null && imgSrc != "" && imgSrc != "System.Web.UI.WebControls.FileUpload")
		{
			Panel imageContainer = new Panel();
			imageContainer.CssClass = "content-container img-content";

			Image img = new Image();
			img.CssClass = "post-image";
			img.ImageUrl = "~/pictures/postpics/" + user.username + "/" + imgSrc;
			imageContainer.Controls.Add(img);

			//and finally add the container to the outer block
			block.Controls.Add(imageContainer);
		}

		post.Controls.Add(block);
		panel.Controls.Add(post);
	}
}

﻿using System;
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
    
    private void makeProfilePanel(int uid)
    {
        //Connect to the database and check to see if user already exists, if it does, compare the password
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        string query = "select * from users where userid = " + uid + ";";
        SqlCommand cmd = new SqlCommand(query, conn);

        conn.Open();

        //Actually execute the query and return the results
        SqlDataReader reader = cmd.ExecuteReader();
        string[] checkarray = new string[10];

        List<Post> posts = new List<Post>();
        reader.Read();
        checkarray[0]    = reader[0].ToString();     //userid
        string username  = reader[1].ToString();     //username
        string name      = reader[2].ToString();     //userrealname
        string gitname   = reader[3].ToString();     //usergitname
        string avatar    = reader[4].ToString();     //useravatar
        checkarray[5]    = reader[5].ToString();     //useremail
        checkarray[6]    = reader[6].ToString();     //userpassword

        //we're going to build a User object to let it take care of server file paths for our avatar.
        // in the future, maybe User would do something more useful, idk.
        User user = new User(name, avatar, uid);

        reader.Close();
        conn.Close();

        // now build a sweet panel!
        Image avatarImg = new Image();
        avatarImg.ImageUrl = user.avatar;
        avatarImg.CssClass = "info-biopic";
        InfoPanel.Controls.Add(avatarImg);

        Panel info = new Panel();
        info.CssClass = "info-sidepanel";

        Label nameLabel = new Label();
        nameLabel.Text = name;
        nameLabel.CssClass = "info-realname";
        info.Controls.Add(nameLabel);

        Label usernameLabel = new Label();
        usernameLabel.Text = username;
        usernameLabel.CssClass = "info-username";
        info.Controls.Add(usernameLabel);

        // we need this to add this to the page - so we use a Label which *should* render as a simple span
        // <span class="fa fa-github/>
        Label gitLogo = new Label();
        gitLogo.CssClass = "fa fa-github info-git-logo";
        info.Controls.Add(gitLogo);

        Label aboutLabel = new Label();
        aboutLabel.Text = gitname;
        aboutLabel.CssClass = "info-gitname";
        info.Controls.Add(aboutLabel);

        InfoPanel.Controls.Add(info);

    }

	protected void Page_Load(object sender, EventArgs e)
	{
		//Check to see if the user has logged in, if not disable ability to post a car
		if (Session["Username"] == null)
		{
			Response.Redirect("Login.aspx");
		}
		if (Int32.Parse(Session["UserId"].ToString()) % 2 != 0)
		{
			Session["Username"] = Session["TempUsername"];
			Session["UserId"] = Session["TempUserId"];
		}

        // Get queried user, default to viewer
        string queriedUser;
        if(Request.QueryString[""] != null)
        {
            queriedUser = Request.QueryString[""];
        }
        else
        {
            queriedUser = Session["UserId"].ToString();
        }

        makeProfilePanel(Int32.Parse(queriedUser));

        // Get queried user info
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);

        //string userInfoQuery = "select * from users where userid = " + 

        // Fetch posts
        string query = "select TOP(20) * from postt p left join users u on u.userid = p.puserid where p.puserid = " + Int32.Parse(queriedUser) + " order by p.ptimestamp desc;";
		SqlCommand cmd = new SqlCommand(query, conn);

		conn.Open();

		//Actually execute the query and return the results
		SqlDataReader reader = cmd.ExecuteReader();
		string[] checkarray = new string[19];
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
			checkarray[16] = reader[16].ToString();     //useravatar
			checkarray[17] = reader[17].ToString();     //useremail
			checkarray[18] = reader[18].ToString();     //userpassword

			User user = new User(checkarray[13], checkarray[15], Int32.Parse(queriedUser));
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

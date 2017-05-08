using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class GroupProfile : System.Web.UI.Page
{
	// this is a shortcut for your connection string
	static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

	string sqlDatetime = "M/d h:mm:ss tt";
	string outputTimestamp = "M/d h:mm:ss tt";

	// WHY THIS NO PARSE??   "5/5 9:03:42 PM"

	private List<Post> getPosts()
	{
		int tempGroupID = Int32.Parse(Request.QueryString["groupid"]);
		string tempGroupName = Request.QueryString["groupname"];

		//Connect to the database and check to see if user already exists, if it does, compare the password
		string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
		SqlConnection conn = new SqlConnection(connectionString);
		string query = "select TOP(20) * from postt p join groups g on g.groupid = p.pgroupid where p.commentid IS NULL and p.pgroupid = " + tempGroupID + " order by p.postid desc;";
		SqlCommand cmd = new SqlCommand(query, conn);

		conn.Open();

		//Actually execute the query and return the results
		SqlDataReader reader = cmd.ExecuteReader();
		string[] checkarray = new string[22];

		List<Post> posts = new List<Post>();
		while (reader.Read())
		{
			checkarray[0] = reader[0].ToString();     //postid
			checkarray[1] = reader[1].ToString();     //ptext
			checkarray[2] = reader[2].ToString();     //ptimestamp
			checkarray[3] = reader[3].ToString();     //phascom
			checkarray[4] = reader[4].ToString();     //commentid
			checkarray[5] = reader[5].ToString();     //puserid
			checkarray[6] = reader[6].ToString();     //pgroupid
			checkarray[7] = reader[7].ToString();     //pcode
			checkarray[8] = reader[8].ToString();     //ppicfile
			checkarray[9] = reader[9].ToString();     //pedate
			checkarray[10] = reader[10].ToString();     //petime
			checkarray[11] = reader[11].ToString();     //peinfo
			checkarray[12] = reader[12].ToString();     //groupid
			checkarray[13] = reader[13].ToString();     //groupname
			checkarray[14] = reader[14].ToString();     //groupavatar


			int id = Int32.Parse(checkarray[0]);
			DateTime time = DateTime.ParseExact(checkarray[2], sqlDatetime, null);
			time = DateTime.SpecifyKind(time, DateTimeKind.Local);
			bool hasComments = bool.Parse(checkarray[3]);
			
			User user = new User(tempGroupName, checkarray[20], tempGroupID);
			Control post = addFooter(UserPost.makePost(user, checkarray[1], checkarray[8], checkarray[7], time, false), time, id, hasComments);
			post.ID = "post" + id;
			post.ClientIDMode = System.Web.UI.ClientIDMode.Static; // this supposedly makes client ID's the same as ASP ID's
			posts.Add(new Post(post, time));
		}
		reader.Close();
		conn.Close();
		return posts;
	}


	private List<Post> getComments(int postId)
	{
		int tempGroupID = Int32.Parse(Request.QueryString["groupid"]);
		string tempGroupName = Request.QueryString["groupname"];

		//Connect to the database and check to see if user already exists, if it does, compare the password
		string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
		SqlConnection conn = new SqlConnection(connectionString);
		string query = "select TOP(20) * from postt p left join users u on u.userid = p.puserid left join groups g on g.groupid = p.pgroupid  where p.commentid = " + postId.ToString() + " order by p.postid desc;";
		SqlCommand cmd = new SqlCommand(query, conn);

		conn.Open();

		//Actually execute the query and return the results
		SqlDataReader reader = cmd.ExecuteReader();
		string[] checkarray = new string[22];

		List<Post> posts = new List<Post>();
		while (reader.Read())
		{
			checkarray[0] = reader[0].ToString();     //postid
			checkarray[1] = reader[1].ToString();     //ptext
			checkarray[2] = reader[2].ToString();     //ptimestamp
			checkarray[3] = reader[3].ToString();     //phascom
			checkarray[4] = reader[4].ToString();     //commentid
			checkarray[5] = reader[5].ToString();     //puserid
			checkarray[6] = reader[6].ToString();     //pgroupid
			checkarray[7] = reader[7].ToString();     //pcode
			checkarray[8] = reader[8].ToString();     //ppicfile
			checkarray[9] = reader[9].ToString();     //pedate
			checkarray[10] = reader[10].ToString();     //petime
			checkarray[11] = reader[11].ToString();     //peinfo
			checkarray[12] = reader[12].ToString();     //userid
			checkarray[13] = reader[13].ToString();     //username
			checkarray[14] = reader[14].ToString();     //userrealname
			checkarray[15] = reader[15].ToString();     //usergitname
			checkarray[16] = reader[16].ToString();     //useravatar
			checkarray[17] = reader[17].ToString();     //useremail
			checkarray[18] = reader[18].ToString();     //userpassword
			checkarray[19] = reader[19].ToString();     //groupid
			checkarray[20] = reader[20].ToString();     //groupname
			checkarray[21] = reader[21].ToString();     //groupavatar

			int id = Int32.Parse(checkarray[0]);
			DateTime time = DateTime.ParseExact(checkarray[2], sqlDatetime, null);
			time = DateTime.SpecifyKind(time, DateTimeKind.Local);
			bool hasComments = bool.Parse(checkarray[3]);

			User user = new User(tempGroupName, checkarray[20], tempGroupID);
			Control post = addFooter(UserPost.makePost(user, checkarray[1], checkarray[8], checkarray[7], time, false), time, id, hasComments);
			post.ID = "post" + id;
			post.ClientIDMode = System.Web.UI.ClientIDMode.Static; // this supposedly makes client ID's the same as ASP ID's
			posts.Add(new Post(post, time));
		}
		reader.Close();
		conn.Close();
		return posts;
	}


	// so, using just ASP the whole page reloads, and then a modal pops up.....
	// that is a terrible UX. so TO JAVASCRIPT WE GOOOOOO!!!
	// ^ javascript saved our day here.
	public void onReply(object sender, EventArgs evt)
	{
		Page.ClientScript.RegisterStartupScript(this.GetType(), "derpyderp", "$('#PostModal').modal('toggle')", true);
	}

	// so, the whole page reloads, and then a modal pops up.....
	// that is a terrible UX. so TO JAVASCRIPT WE GOOOOOO
	public void loadComments(object sender, EventArgs evt)
	{
		int postId = Int32.Parse((sender as Button).Attributes["postid"]);
		List<Post> comments = getComments(postId);

		// find a post with the matching postID in the DOM...... !!!!!!!!!!! FML! WHY C#! WHY!!!!
		Control Post = GroupPanel.FindControl("post" + postId);
		int i = GroupPanel.Controls.IndexOf(Post) + 1; // then find that element in the list of controls, so we can add stuff after it.

		foreach (Post post in comments)
		{
			GroupPanel.Controls.AddAt(i, post.control);
			i++;
		}

	}

	// add a footer block to the posts!
	private Control addFooter(Control original, DateTime time, int postID, bool hasComments)
	{

		// now for the footer

		Panel footer = new Panel();
		footer.CssClass = "post-footer";

		// add timestamp
		Label timestampLabel = new Label();
		timestampLabel.CssClass = "timestamp-label";
		timestampLabel.Text = time.ToString();
		footer.Controls.Add(timestampLabel);

		if (hasComments)
		{
			//add the load comment control
			Button loadComments = new Button();
			loadComments.CssClass = "load-comments-button";
			loadComments.Text = "load comments";
			loadComments.Attributes["postid"] = postID.ToString();
			loadComments.Click += new EventHandler(this.loadComments);
			footer.Controls.Add(loadComments);
		}


		//add the load comment control
		Button replyButton = new Button();
		replyButton.CssClass = "reply-button";
		replyButton.Text = "reply";
		// this sneaky bit of javascript opens a modal window to reply to a particular post. wooooooo
		replyButton.OnClientClick = "$('#PostModal').modal('toggle'); $('#PostButton').attr('replyPost', " + postID + "); document.getElementById('HiddenThing').value=" + postID + "; return false;";
		footer.Controls.Add(replyButton);

		original.Controls.Add(footer);
		return original;
	}


	// add a footer block to the posts!
	private Control addCommentFooter(Control original, DateTime time, int postID)
	{

		// now for the footer

		Panel footer = new Panel();
		footer.CssClass = "post-footer";

		// add timestamp
		Label timestampLabel = new Label();
		timestampLabel.CssClass = "timestamp-label";
		timestampLabel.Text = time.ToString();
		footer.Controls.Add(timestampLabel);

		original.Controls.Add(footer);
		return original;
	}


	private List<User> allUsers()
	{
		//Connect to the database and check to see if user already exists, if it does, compare the password
		string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
		SqlConnection conn = new SqlConnection(connectionString);
		string query = "select * from users";
		SqlCommand cmd = new SqlCommand(query, conn);

		conn.Open();

		//Actually execute the query and return the results
		SqlDataReader reader = cmd.ExecuteReader();
		string[] checkarray = new string[7]; // GAHKLJSHDFLKJHDSGKLJDSF I HATE ASP I HATE ASP I HATE ASP I HATE ASP I HATE ASP I HATE ASP I HATE ASP I HATE ASP I HATE ASP

		List<User> users = new List<User>();

		while (reader.Read())
		{
			checkarray[0] = reader[0].ToString(); //userid
			checkarray[1] = reader[1].ToString(); //username
			checkarray[2] = reader[2].ToString(); //userrealname
			checkarray[3] = reader[3].ToString(); //usergitname
			checkarray[4] = reader[4].ToString(); //useravatar
			checkarray[5] = reader[5].ToString(); //useremail
												  //checkarray[9] = reader[9].ToString(); //userpassword

			User user = new User(checkarray[1], checkarray[4], Int32.Parse(checkarray[0]));
			user.gitname = checkarray[3];
			users.Add(user);
		}
		reader.Close();
		conn.Close();
		return users;
	}

	// merge the list of list of posts into a single list - aka flattening by one dimension.
	private List<Post> flatten(List<List<Post>> ALLTHELISTS)
	{
		List<Post> finalList = new List<Post>();

		foreach (List<Post> list in ALLTHELISTS)
		{
			finalList.AddRange(list);
		}

		return finalList;
	}


	protected void Page_Load(object sender, EventArgs e)
	{
		//Check to see if the user has logged in, if not disable ability to post a car
		if (Session["Username"] == null)
		{
			Response.Redirect("Login.aspx");
		}

		int tempGroupAdmin = Int32.Parse(Request.QueryString["groupadmin"]);
		int tempGroupID = Int32.Parse(Request.QueryString["groupid"]);
		string tempGroupName = Request.QueryString["groupname"];

		//Check to see if the user is a group admin
		if (tempGroupAdmin == 1)
		{
			if (Int32.Parse(Session["UserId"].ToString()) % 2 == 0)
			{
				Session["TempUsername"] = Session["Username"];
				Session["TempUserId"] = Session["UserId"];
				Session["UserName"] = tempGroupName;
				Session["UserId"] = tempGroupID;
			}
			else {
				Session["UserName"] = tempGroupName;
				Session["UserId"] = tempGroupID;
			}
		}
		else
		{
			// hides the post button on the navbar
			Page.ClientScript.RegisterStartupScript(this.GetType(), "notgroupadmin", "$('#PostModalToggler').hide()", true);
		}

		List<List<Post>> ALLTHEPOSTS = new List<List<Post>>(); // place to put all our lists
		List<Post> userPosts = getPosts(); // get user posts from database
		ALLTHEPOSTS.Add(userPosts); // add the user psot list to the listy list

		// finally, flatten all the lists into one list, and sort it.
		List<Post> posts = flatten(ALLTHEPOSTS);
		posts.Sort();
		posts.Reverse();
		// then add them to the page!
		foreach (Post post in posts)
		{
			GroupPanel.Controls.Add(post.control);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class NewsFeed : System.Web.UI.Page
{
    // this is a shortcut for your connection string
    static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

    //string sqlDatetime = "M/d h:mm:ss tt";

    List<String> sqlDatetimeFormats = new List<string>(new String[]{"M/d/yyyy h:mm:ss tt", "M/d h:mm:ss tt"});
    string outputTimestamp = "M/d h:mm:ss tt";

    private List<Post> getPosts()
	{
		//Connect to the database and check to see if user already exists, if it does, compare the password
		string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        string query = "select TOP(20) * from postt p left join users u on u.userid = p.puserid left join groups g on g.groupid = p.pgroupid left join member m on m.mgroupid = g.groupid where u.userid = p.puserid or m.muserid =" + Session["UserId"] + ";";
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
			checkarray[9] = reader[9].ToString();     //userid
			checkarray[10] = reader[10].ToString();     //username
			checkarray[11] = reader[11].ToString();     //userrealname
			checkarray[12] = reader[12].ToString();     //usergitname
			checkarray[13] = reader[13].ToString();     //useravatar
			checkarray[14] = reader[14].ToString();     //useremail
			checkarray[15] = reader[15].ToString();     //userpassword
			checkarray[16] = reader[16].ToString();     //groupid
			checkarray[17] = reader[17].ToString();     //groupname
			checkarray[18] = reader[18].ToString();     //groupavatar
			checkarray[19] = reader[19].ToString();     //muserid
			checkarray[20] = reader[20].ToString();     //mgroupid
			checkarray[21] = reader[21].ToString();     //madmin

			int id = Int32.Parse(checkarray[0]);
			DateTime time = SqlDateHelper.parseSqlDate(checkarray[2]);
			bool hasComments = bool.Parse(checkarray[3]);
			int admin = 0;
			if (checkarray[21] == "1")
			{
				admin = 1;
			}

			//Check to see if the post is group or user post
			if (checkarray[13] == "")
			{
				if (admin == 1)
				{
					User user = new User(checkarray[17], checkarray[18], Int32.Parse(checkarray[16]), admin);
					Control post = addFooter(UserPost.makePost(user, checkarray[1], checkarray[8], checkarray[7], time, false), time, id, hasComments);
					post.ID = "post" + id;
					post.ClientIDMode = System.Web.UI.ClientIDMode.Static; // this supposedly makes client ID's the same as ASP ID's
					posts.Add(new Post(post, time));
				}
				else
				{
					User user = new User(checkarray[17], checkarray[18], Int32.Parse(checkarray[16]), admin);
					Control post = addFooter(UserPost.makePost(user, checkarray[1], checkarray[8], checkarray[7], time, false), time, id, hasComments);
					post.ID = "post" + id;
					post.ClientIDMode = System.Web.UI.ClientIDMode.Static; // this supposedly makes client ID's the same as ASP ID's
					posts.Add(new Post(post, time));
				}
			}
			else
			{
				if (Int32.Parse(checkarray[9]) == Int32.Parse(Session["UserId"].ToString()))
				{
					User user = new User(checkarray[10], checkarray[13], Int32.Parse(checkarray[9]), 1);
					Control post = addFooter(UserPost.makePost(user, checkarray[1], checkarray[8], checkarray[7], time, false), time, id, hasComments);
					post.ID = "post" + id;
					post.ClientIDMode = System.Web.UI.ClientIDMode.Static; // this supposedly makes client ID's the same as ASP ID's
					posts.Add(new Post(post, time));
				}
				else
				{
					User user = new User(checkarray[10], checkarray[13], Int32.Parse(checkarray[9]), 0);
					Control post = addFooter(UserPost.makePost(user, checkarray[1], checkarray[8], checkarray[7], time, false), time, id, hasComments);
					post.ID = "post" + id;
					post.ClientIDMode = System.Web.UI.ClientIDMode.Static; // this supposedly makes client ID's the same as ASP ID's
					posts.Add(new Post(post, time));
				}
			}

		}
        reader.Close();
        conn.Close();
        return posts;
    }

    private List<Post> getComments(int postId)
    {

        //Connect to the database and check to see if user already exists, if it does, compare the password
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        string query = "select TOP(20) * from postt p left join users u on u.userid = p.puserid left join groups g on g.groupid = p.pgroupid left join member m on m.mgroupid = g.groupid where (u.userid = p.puserid or m.muserid =" + Session["UserId"] + ") and p.commentid = " + Int32.Parse(postId.ToString()) + ";";
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
			checkarray[9] = reader[9].ToString();     //userid
			checkarray[10] = reader[10].ToString();     //username
			checkarray[11] = reader[11].ToString();     //userrealname
			checkarray[12] = reader[12].ToString();     //usergitname
			checkarray[13] = reader[13].ToString();     //useravatar
			checkarray[14] = reader[14].ToString();     //useremail
			checkarray[15] = reader[15].ToString();     //userpassword
			checkarray[16] = reader[16].ToString();     //groupid
			checkarray[17] = reader[17].ToString();     //groupname
			checkarray[18] = reader[18].ToString();     //groupavatar
			checkarray[19] = reader[19].ToString();     //muserid
			checkarray[20] = reader[20].ToString();     //mgroupid
			checkarray[21] = reader[21].ToString();     //madmin

			int id = Int32.Parse(checkarray[0]);
            DateTime time = SqlDateHelper.parseSqlDate(checkarray[2]);
            bool hasComments = bool.Parse(checkarray[3]);
			bool admin = false;
			if (checkarray[21] != "")
			{
				admin = bool.Parse(checkarray[21]);
			}
			int zero = 0;
			int one = 1;

			//Check to see if the user is a group admin
			if (checkarray[13] == "")
			{
				if (admin)
				{
					User user = new User(checkarray[17], checkarray[18], Int32.Parse(checkarray[16]), one);
					Control post = addFooter(UserPost.makePost(user, checkarray[1], checkarray[8], checkarray[7], time, false), time, id, hasComments);
					post.ID = "post" + id;
					post.ClientIDMode = System.Web.UI.ClientIDMode.Static; // this supposedly makes client ID's the same as ASP ID's
					posts.Add(new Post(post, time));
				}
				else
				{
					User user = new User(checkarray[17], checkarray[18], Int32.Parse(checkarray[16]), zero);
					Control post = addFooter(UserPost.makePost(user, checkarray[1], checkarray[8], checkarray[7], time, false), time, id, hasComments);
					post.ID = "post" + id;
					post.ClientIDMode = System.Web.UI.ClientIDMode.Static; // this supposedly makes client ID's the same as ASP ID's
					posts.Add(new Post(post, time));
				}
			}
			else
			{
				if (Int32.Parse(checkarray[9]) == Int32.Parse(Session["UserId"].ToString()))
				{
					User user = new User(checkarray[10], checkarray[13], Int32.Parse(checkarray[9]), one);
					Control post = addFooter(UserPost.makePost(user, checkarray[1], checkarray[8], checkarray[7], time, false), time, id, hasComments);
					post.ID = "post" + id;
					post.ClientIDMode = System.Web.UI.ClientIDMode.Static; // this supposedly makes client ID's the same as ASP ID's
					posts.Add(new Post(post, time));
				}
				else
				{
					User user = new User(checkarray[10], checkarray[13], Int32.Parse(checkarray[9]), zero);
					Control post = addFooter(UserPost.makePost(user, checkarray[1], checkarray[8], checkarray[7], time, false), time, id, hasComments);
					post.ID = "post" + id;
					post.ClientIDMode = System.Web.UI.ClientIDMode.Static; // this supposedly makes client ID's the same as ASP ID's
					posts.Add(new Post(post, time));
				}
			}
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
        int postId = Int32.Parse( (sender as LinkButton).Attributes["postid"] );
        List<Post> comments = getComments(postId);

        // find a post with the matching postID in the DOM...... !!!!!!!!!!! FML! WHY C#! WHY!!!!
        Control Post = NewsFeedPanel.FindControl("post" + postId);
        int i = NewsFeedPanel.Controls.IndexOf(Post) + 1; // then find that element in the list of controls, so we can add stuff after it.
        
        foreach ( Post post in comments)
        {
            NewsFeedPanel.Controls.AddAt(i, post.control);
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
            LinkButton loadComments = new LinkButton();
            loadComments.CssClass = "load-comments-button btn btn-info";
            loadComments.Text = "<span style='font-weight: bold; letter-spacing: -4px;'>//</span> Load Comments";
            loadComments.Attributes["postid"] = postID.ToString();
            loadComments.Click += new EventHandler(this.loadComments);
            footer.Controls.Add(loadComments);
        }


        //add the load comment control
        LinkButton replyButton = new LinkButton();
        replyButton.CssClass = "reply-button btn btn-primary";
        replyButton.Text = "<span class='fa fa-reply'></span> Reply";
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
        string query = "select * from users u left join member m on m.muserid = u.userid";
        SqlCommand cmd = new SqlCommand(query, conn);

        conn.Open();

        //Actually execute the query and return the results
        SqlDataReader reader = cmd.ExecuteReader();
        string[] checkarray = new string[10]; // GAHKLJSHDFLKJHDSGKLJDSF I HATE ASP I HATE ASP I HATE ASP I HATE ASP I HATE ASP I HATE ASP I HATE ASP I HATE ASP I HATE ASP

        List<User> users = new List<User>();

        while (reader.Read())
        {
            checkarray[0] = reader[0].ToString();		//userid
            checkarray[1] = reader[1].ToString();		//username
            checkarray[2] = reader[2].ToString();		//userrealname
            checkarray[3] = reader[3].ToString();		//usergitname
            checkarray[4] = reader[4].ToString();		//useravatar
            checkarray[5] = reader[5].ToString();		//useremail
			checkarray[6] = reader[6].ToString();	    //userpassword
			checkarray[7] = reader[7].ToString();       //muserid
			checkarray[8] = reader[8].ToString();       //mgroupid
			checkarray[9] = reader[9].ToString();       //madmin

			bool admin = false;
			if (checkarray[9] != "")
			{
				admin = bool.Parse(checkarray[9]);
			}
			if (admin)
			{
				User user = new User(checkarray[1], checkarray[4], Int32.Parse(checkarray[0]), 1);
				user.gitname = checkarray[3];
				users.Add(user);
			}
			else
			{
				User user = new User(checkarray[1], checkarray[4], Int32.Parse(checkarray[0]), 1);
				user.gitname = checkarray[3];
				users.Add(user);

			}
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
		if (Int32.Parse(Session["UserId"].ToString()) % 2 != 0)
		{
			Session["Username"] = Session["TempUsername"];
			Session["UserId"] = Session["TempUserId"];
		}


        List<List<Post>> ALLTHEPOSTS = new List<List<Post>>(); // place to put all our lists
        List<Post> userPosts = getPosts(); // get user posts from database
        ALLTHEPOSTS.Add(userPosts); // add the user psot list to the listy list

        List<User> users = allUsers(); // get all the users - we will then fetch each of their git feeds.
        if (users.Count <=0 ) {
            Post error = new Post(GithubPosts.errorPost("woops. no github users"), DateTime.UtcNow);
            userPosts.Add(error);
        }
        foreach (User user in users) {
            if (user.gitname != null && user.gitname != "NULL" && user.gitname != "")
            {
                List<Post> githubPosts = GithubPosts.gitPosts(user.gitname); // if they have a github name, get their feed as a seperate list
                ALLTHEPOSTS.Add(githubPosts);
                if (githubPosts.Count <=0 )
                {
                    Post error = new Post(GithubPosts.errorPost("woops. no events for this user"), DateTime.UtcNow);
                    // we couldn't get git info... soooooo
                    githubPosts.Add(error);
                }
            }
        }

        // finally, flatten all the lists into one list, and sort it.
        List<Post> posts = flatten(ALLTHEPOSTS);
        posts.Sort();
        posts.Reverse();
        // then add them to the page!
        foreach ( Post post in posts)
        {
            NewsFeedPanel.Controls.Add(post.control);
        }
	}

}

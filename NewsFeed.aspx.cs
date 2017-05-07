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

    string sqlDatetime = "M/d h:mm:ss tt";
    string outputTimestamp = "M/d h:mm:ss tt";

    // WHY THIS NO PASRE??   "5/5 9:03:42 PM"


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


    private List<Post> getPosts()
    {

        //Connect to the database and check to see if user already exists, if it does, compare the password
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        string query = "select TOP(20) * from postt p left join users u on u.userid = p.puserid left join groups g on g.groupid = p.pgroupid where p.commentid IS NULL order by p.postid desc;";
        SqlCommand cmd = new SqlCommand(query, conn);

        conn.Open();

        //Actually execute the query and return the results
        SqlDataReader reader = cmd.ExecuteReader();
        string[] checkarray = new string[21];

        List<Post> posts = new List<Post>();
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
            checkarray[18] = reader[18].ToString();     //groupid
            checkarray[19] = reader[19].ToString();     //groupname
            checkarray[20] = reader[20].ToString();     //groupavatar

            int id = Int32.Parse(checkarray[0]);
            User user = new User(checkarray[13], checkarray[15]);
            DateTime time = DateTime.ParseExact(checkarray[2], sqlDatetime, null);
            time = DateTime.SpecifyKind(time, DateTimeKind.Local);
            bool hasComments = bool.Parse(checkarray[3]);
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

        //Connect to the database and check to see if user already exists, if it does, compare the password
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        string query = "select TOP(20) * from postt p left join users u on u.userid = p.puserid left join groups g on g.groupid = p.pgroupid  where p.commentid = " + postId.ToString() + " order by p.postid desc;";
        SqlCommand cmd = new SqlCommand(query, conn);

        conn.Open();

        //Actually execute the query and return the results
        SqlDataReader reader = cmd.ExecuteReader();
        string[] checkarray = new string[21];

        List<Post> posts = new List<Post>();
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
            checkarray[18] = reader[18].ToString();     //groupid
            checkarray[19] = reader[19].ToString();     //groupname
            checkarray[20] = reader[20].ToString();     //groupavatar

            int id = Int32.Parse(checkarray[0]);
            User user = new User(checkarray[13], checkarray[15]);
            DateTime time = DateTime.ParseExact(checkarray[2], sqlDatetime, null);
            time = DateTime.SpecifyKind(time, DateTimeKind.Local);
            Control post = addCommentFooter(UserPost.makePost(user, checkarray[1], checkarray[8], checkarray[7], time, true), time, id);
            

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
        int postId = Int32.Parse( (sender as Button).Attributes["postid"] );
        List<Post> comments = getComments(postId);

        // find a post with the matching postID in the DOM...... !!!!!!!!!!! FML! WHY C#! WHY!!!!
        Control Post = Panel.FindControl("post" + postId);
        int i = Panel.Controls.IndexOf(Post) + 1; // then find that element in the list of controls, so we can add stuff after it.
        
        foreach ( Post post in comments)
        {
            Panel.Controls.AddAt(i, post.control);
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

            User user = new User(checkarray[1], checkarray[4]);
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
		if (Int32.Parse(Session["UserId"].ToString()) % 2 != 0)
		{
			Session["Username"] = Session["TempUsername"];
			Session["UserId"] = Session["TempUserId"];
		}


        List<List<Post>> ALLTHEPOSTS = new List<List<Post>>(); // place to put all our lists
        List<Post> userPosts = getPosts(); // get user posts from database
        ALLTHEPOSTS.Add(userPosts); // add the user psot list to the listy list

        List<User> users = allUsers(); // get all the users - we will then fetch each of their git feeds.
        foreach (User user in users) {
            if (user.gitname != null && user.gitname != "NULL" && user.gitname != "")
            {
                List<Post> githubPosts = GithubPosts.gitPosts(user.gitname); // if they have a github name, get their feed as a seperate list
                ALLTHEPOSTS.Add(githubPosts);
            }
        }

        // finally, flatten all the lists into one list, and sort it.
        List<Post> posts = flatten(ALLTHEPOSTS);
        posts.Sort();
        posts.Reverse();
        // then add them to the page!
        foreach ( Post post in posts)
        {
            Panel.Controls.Add(post.control);
        }
	}

}

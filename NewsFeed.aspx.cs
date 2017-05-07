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

    string sqlDatetime = "M/d/yyyy h:mm:ss tt";
    
           // WHY THIS NO PASRE??   "5/5 9:03:42 PM"

    // simple container class for user info.
    private class User
    {
        public String username;
        public String avatar;
        public String gitname;

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


    private List<Post> getPosts()
    {

        //Connect to the database and check to see if user already exists, if it does, compare the password
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        string query = "select TOP(20) * from postt p left join users u on u.userid = p.puserid left join groups g on g.groupid = p.pgroupid order by p.postid desc;";
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

            User user = new User(checkarray[13], checkarray[15]);
            posts.Add(makePost(user, checkarray[1], checkarray[8], checkarray[7], DateTime.ParseExact(checkarray[2], sqlDatetime, null)));

        }
        reader.Close();
        conn.Close();
        return posts;
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
	else if (Int32.Parse(Session["UserId"].ToString()) % 2 == 1)
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
        // then add them to the page!
        foreach ( Post post in posts)
        {
            Panel.Controls.Add(post.control);
        }
	}

    private Post makePost(User user, String text, String imgSrc, String codeSrc, DateTime timestamp)
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
            img.ImageUrl = "~/pictures/postpics/" + imgSrc;
            imageContainer.Controls.Add(img);

            //and finally add the container to the outer block
            block.Controls.Add(imageContainer);
        }

        post.Controls.Add(block);
        return new Post(post, timestamp) ;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class NewsFeed : System.Web.UI.Page
{

    // simple container class for user info.
    private class User
    {
        public String username;
        public String avatar;

        public User(String cusername, String cavatar)
        {
            username = cusername;   // lulx these names are terrible
            avatar = cavatar;       // lulz these names are terriblz
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
		/*Connect to the database and check to see if user already exists, if it does, compare the password
		string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
		SqlConnection conn = new SqlConnection(connectionString);
		string query = "create or alter function countcom() ";
		SqlCommand com = new SqlCommand(query, conn);
		conn.Open();

		//Actually execute the query and return the results
		com.ExecuteNonQuery();

		conn.Close();*/
	}

    private void AddPost(Panel panel, User user, String text, String imgSrc=null, String codeSrc=null)
    {
        Panel post = new Panel();
        Label userBox = new Label();
        Label box = new Label();
        
        box.Text = text;
        userBox.Text = user.username;
        post.Controls.Add(box);
        post.Controls.Add(userBox);


        if (codeSrc != null)
        {
            //Literal code = new Literal();
            //code.Text = "<pre><code>" + "</code></pre>"; //ROFL HOW FAST CAN YOU SAY CODE INJECTION!


            Literal codePre = new Literal();
            codePre.Text = "<pre><code>";
            Label code = new Label();
            code.Text = HttpUtility.HtmlEncode(codeSrc); // maybe not code injection? 
            Literal codePost = new Literal();
            codePost.Text = "</pre></code>";

            post.Controls.Add(codePre);
            post.Controls.Add(code);
            post.Controls.Add(codePost);
        }
        if (imgSrc != null)
        {
            Image img = new Image();
            img.ImageUrl = imgSrc;
            post.Controls.Add(img);
        }

        panel.Controls.Add(post);
    }
}
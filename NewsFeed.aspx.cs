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
        User user = new User("Bob", "n/a");
        AddPost(Panel, user,"Hello yhere!z", imgSrc: "http://placekitten.com.s3.amazonaws.com/homepage-samples/200/286.jpg");
        AddPost(Panel, user, "asdfadfg");
        AddPost(Panel, user, "zomg posting is fun", imgSrc: "http://placekitten.com.s3.amazonaws.com/homepage-samples/200/286.jpg", codeSrc:"function(){ return 'this is some javascript' }");
        AddPost(Panel, user, "Hedddllo yhere!z", codeSrc:"<div><input type=button></input></div>");
        
    }

    private void AddPost(Panel panel, User user, String text, String imgSrc=null, String codeSrc=null)
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

        Image userAvatar = new Image();
        userAvatar.CssClass = "avatar";
        userAvatar.ImageUrl = user.avatar;

        Label userText = new Label();
        userText.Text = user.username;

        // and finally add them to the container
        userContainer.Controls.Add(userAvatar);
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

        if (codeSrc != null)
        {
            //Literal code = new Literal();
            //code.Text = "<pre><code>" + "</code></pre>"; //ROFL HOW FAST CAN YOU SAY CODE INJECTION!

            Panel codePanel = new Panel();
            codePanel.CssClass = "content-container code-content";

            // make the code element - this involves some special html tags that don't exist in asp
            // so FUCK ASP.NET and let's write literal HTML
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
        if (imgSrc != null)
        {
            Panel imageContainer = new Panel();
            imageContainer.CssClass = "content-container img-content";

            Image img = new Image();
            img.CssClass = "post-image";
            img.ImageUrl = imgSrc;
            imageContainer.Controls.Add(img);

            //and finally add the container to the outer block
            block.Controls.Add(imageContainer);
        }

        post.Controls.Add(block);
        panel.Controls.Add(post);
    }
}
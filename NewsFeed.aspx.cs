using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        User user = new User("Bob", "n/a");
        AddPost(Panel, user,"Hello yhere!z", imgSrc: "http://placekitten.com.s3.amazonaws.com/homepage-samples/200/286.jpg");
        AddPost(Panel, user, "asdfadfg");
        AddPost(Panel, user, "zomg posting is fun", imgSrc: "http://placekitten.com.s3.amazonaws.com/homepage-samples/200/286.jpg", codeSrc:"function(){ return 'this is some javascript' }");
        AddPost(Panel, user, "Hedddllo yhere!z", codeSrc:"<div><input type=button></input></div>");
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
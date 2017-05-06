using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestSharp;
using RestSharp.Authenticators;


public partial class GithubTest : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        List<Post> githubPosts = GithubPosts.gitPosts("bendmyers");
        foreach ( Post post in githubPosts)
        {
            debugr.Controls.Add(post.control);
        }
    }

    

    private void bug(String thing)
    {
        Panel container = new Panel();
        Label text = new Label();
        text.Text = thing;
        container.Controls.Add(text);
        debugr.Controls.Add(container);

    }
}
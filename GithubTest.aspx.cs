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
    private class Repo
    {
        public String name { get; set; }
    }
    private class Actor
    {
        public String login { get; set; }
    }
    private class Payload
    {
        // EVERY field fo ALL the payloads should be here..... grrr....
        public String login { get; set; }
        public String ref_type {get; set; }

    }
    private class Event
    {
        public string type { get; set; }
        //public Dictionary<String, Payload> payload { get; set; } //gah, can we do more than string payloads?
        public Payload payload { get; set; }
        public Actor actor { get; set; }
        public Repo repo { get; set; }
        public String created_at { get; set; }

        public String getUsername()
        {
            return actor.login;
        }

        public String getRepoName()
        {
            return repo.name;
        }
    }

    private abstract class EventPost
    {
        public abstract Control generateInner(Event evt);
        public Control generate(Event evt)
        {
           
            Control inner = generateInner(evt); // this should be a simple Label/div/etc.

            // layout of this code block should resemble the layout of the generated HTML!
            // ( i.e. outer elements are created first, and added after - creation is open tag, adding is close tag
            Panel post = new Panel();
            post.CssClass = "post-container";
            Panel block = new Panel();
            block.CssClass = "post-block";

            /*
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
            */


            // add the inner container
            Panel gitContainer = new Panel();
            gitContainer.CssClass = "content-container git-content";

            gitContainer.Controls.Add(generateInner(evt));
            

            //and finally add the container to the outer block
            block.Controls.Add(gitContainer);

            post.Controls.Add(block);
            return post;
        }

    }

    private class ActionEventPost : EventPost
    {
        private String action;

        public ActionEventPost(String action)
        {
            this.action = action;
        }

        public override Control generateInner(Event evt)
        {
            Panel container = new Panel();
            Label action = new Label();
            String username = evt.getUsername();
            String repo = evt.getRepoName();
            action.Text = username + " " + this.action + " " + repo;
            container.Controls.Add(action);
            return container;
        }
    }


    // we need logic to decide what was created... soooo
    private class CreateEventPost : EventPost
    {
        public override Control generateInner(Event evt)
        {
            Panel container = new Panel();
            Label action = new Label();
            String username = evt.getUsername();
            String repo = evt.getRepoName(); // the repository this happened in

            switch (evt.payload.ref_type)
            {
                case "repository":
                    action.Text = username + " " + "created a new repository: " + repo;
                    break;
                case "branch":
                    action.Text = username + " " + "created a new branch in " + repo;
                    break;
                case "tag":
                    action.Text = username + " " + "created a new tag in " + repo;
                    break;
            }
            container.Controls.Add(action);
            return container;
        }
    }

    private Dictionary<String, EventPost> eventPostFactories = new Dictionary<String, EventPost>();
    //events["PushEvent"] = new EventPost();
    
    public List<Control> gitPosts(String githubUsername)
    {
        // define the post relationship
        eventPostFactories["PushEvent"] = new ActionEventPost("pushed to");
        eventPostFactories["CreateEvent"] = new ActionEventPost("created a new repository,");


        // do a call to github events and hope, hope HOPE
        var client = new RestClient(); // var?!?
        client.BaseUrl = new Uri("https://api.github.com");

        var request = new RestRequest();
        request.Resource = "users/" + githubUsername + "/events"; // what could possibly go wrong????

        var response = client.Execute<List<Event>>(request);

        if (response.ErrorException != null)
        {
            // we couldn't get git info... soooooo
            return new List<Control>();
        }
        else
        {
            List<Control> controls = new List<Control>();
            List<Event> events = response.Data;
            foreach (Event evnt in events)
            {
                // bug(evnt.type);
                if (eventPostFactories.ContainsKey(evnt.type))
                {
                    EventPost postType = eventPostFactories[evnt.type];
                    if (postType != null)
                    {
                        controls.Add(postType.generate(evnt));
                    }
                }
            }
            // put it in a flat array because... why?
            return controls;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        List<Control> githubPosts = gitPosts("bendmyers");
        foreach ( Control control in githubPosts)
        {
            debugr.Controls.Add(control);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestSharp;
using RestSharp.Authenticators;
using System.Globalization;

public class GithubPosts
{

    public static string githubDatetime = "yyyy-MM-dd'T'HH:mm:ss"; //format of github event timestamps

    // some classes to match the heirarchy of the github JSON response for events
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
        public String ref_type { get; set; }

    }

    // basic class that contains all the information of an event from github - fields **must match the names in the JSON response object**
    private class Event
    {
        public string type { get; set; }
        //public Dictionary<String, Payload> payload { get; set; } //gah, can we do more than string payloads?
        public Payload payload { get; set; }
        public Actor actor { get; set; }
        public Repo repo { get; set; }
        public String created_at { get; set; }

        // implement some getters for basic info
        public DateTime getTimestamp()
        {
            DateTime utcTime =  DateTime.Parse(this.created_at, null, DateTimeStyles.RoundtripKind);
            // now...... make it into a certain timezone!
            var myTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            DateTime currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, myTimeZone);

            // we could also do some datetime timezone stuffs to convert everythin into UTC.
            return currentDateTime;
        }
        public String getUsername()
        {
            return actor.login;
        }

        public String getRepoName()
        {
            return repo.name;
        }
    }

    // the superclass for other github event post types
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

            // add the inner container
            Panel gitContainer = new Panel();
            gitContainer.CssClass = "content-container git-content";

            gitContainer.Controls.Add(inner);


            //and finally add the container to the outer block
            block.Controls.Add(gitContainer);

            post.Controls.Add(block);



            Panel footer = new Panel();
            footer.CssClass = "post-footer";

            // add timestamp
            Label timestampLabel = new Label();
            timestampLabel.CssClass = "timestamp-label";
            timestampLabel.Text = evt.getTimestamp().ToString();
            footer.Controls.Add(timestampLabel);

            /*
            //add the load comment control
            Button loadComments = new Button();
            loadComments.CssClass = "load-comments-button";
            loadComments.Text = "load comments";
            footer.Controls.Add(loadComments);


            //add the load comment control
            Button replyButton = new Button();
            replyButton.CssClass = "reply-button";
            replyButton.Text = "load comments";
            footer.Controls.Add(replyButton);
            */

            post.Controls.Add(footer);

            return post;
        }

    }

    // creates an error post for developers, and to let users know that github might be down or something.
    private class ActionEventPost : EventPost
    {
        private String action;

        public ActionEventPost(String action)
        {
            this.action = action;
        }

        // generate the post content - this gets put insode containers later.
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


    // we need logic to decide what was created... soooo this class exists.
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

    // this holds the generators for all the github event types. initially empty :(
    private static Dictionary<String, EventPost> eventPostFactories = new Dictionary<String, EventPost>();

    public static Control errorPost(String oops)
    {

        Label inner = new Label();
        inner.Text = oops;

        // layout of this code block should resemble the layout of the generated HTML!
        // ( i.e. outer elements are created first, and added after - creation is open tag, adding is close tag
        Panel post = new Panel();
        post.CssClass = "post-container";
        Panel block = new Panel();
        block.CssClass = "post-block";

        // add the inner container
        Panel gitContainer = new Panel();
        gitContainer.CssClass = "content-container git-content git-error";

        gitContainer.Controls.Add(inner);


        //and finally add the container to the outer block
        block.Controls.Add(gitContainer);

        post.Controls.Add(block);



        Panel footer = new Panel();
        footer.CssClass = "post-footer";

        // add timestamp
        Label timestampLabel = new Label();
        timestampLabel.CssClass = "timestamp-label";
        timestampLabel.Text = DateTime.UtcNow.ToString();
        footer.Controls.Add(timestampLabel);
        post.Controls.Add(footer);

        return post;
    }

    public static List<Post> gitPosts(String githubUsername)
    {
        // define the post relationship between event types and their generators
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

            Post error = new Post(errorPost(response.ErrorException.Message), DateTime.UtcNow);
            // we couldn't get git info... soooooo
            return new List<Post>(new Post[] { error });
        }
        else
        {
            List<Post> posts = new List<Post>();
            List<Event> events = response.Data;
            if (events.Count > 0)
            {
                foreach (Event evnt in events)
                {
                    // bug(evnt.type);
                    if (evnt.type != null && eventPostFactories.ContainsKey(evnt.type))
                    {
                        EventPost postType = eventPostFactories[evnt.type];
                        if (postType != null)
                        {
                            posts.Add(new Post(postType.generate(evnt), evnt.getTimestamp()));
                        }
                    }
                }
            }
            return posts;
        }
    }
}
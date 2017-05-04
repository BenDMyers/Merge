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
    private class Event
    {
        public string type { get; set; }
    }

    private class EventPost
    {
        public Control generate(Event evt)
        {
            return null;
        }
    }

    private class ActionEventPost : EventPost
    {
        private String action;

        public ActionEventPost(String action)
        {
            this.action = action;
        }

        public Control generate(Event evt)
        {
            Panel container = new Panel();
            Label action = new Label();
            action.Text = "someone" + action;
            container.Controls.Add(action);
            return container;
        }
    }

    private Dictionary<String, EventPost> events = new Dictionary<String, EventPost>();
    //events["PushEvent"] = new EventPost();
    


    private class EventRenderer
    {
        // here is the hookup between events and their respective generators.
     //   events["PushEvent"] = this.PushEvent;

        private Control PushEvent(Event evnt)
        {
            return null;
        }
    }
    

    protected void Page_Load(object sender, EventArgs e)
    {
        // define the post relationship
        events["PushPost"] = new ActionEventPost("Pushed something!");


        // do a call to github events and hope, hope HOPE
        var client = new RestClient(); // var?!?
        client.BaseUrl = new Uri("https://api.github.com");
        //client.Authenticator = new HttpBasicAuthenticator("username", "password");

        var request = new RestRequest();
        request.Resource = "users/o080o/events";


        var response = client.Execute<List<Event>>(request);

        if (response.ErrorException != null)
        {
            // we couldn't get git info... soooooo
            bug("no git!");
        }
        else
        {
            List<Event> events = response.Data;
            foreach( Event evnt in events)
            {
                // bug(evnt.type);
                EventPost postType = events[evnt.type];
                if (postType != null)
                {
                    debugr.Controls.Add(postType.generate(evnt));
                }
            }
        }


        bug(response.ToString());

    }

    

    private void bug(String thing)
    {
        Label text = new Label();
        text.Text = thing;
        debugr.Controls.Add(text);

    }
}
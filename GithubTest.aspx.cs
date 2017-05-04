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

    protected void Page_Load(object sender, EventArgs e)
    {
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
                bug(evnt.type);
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
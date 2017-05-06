using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// A single post object - can contain user posts, github posts, or any other sort of post.
/// </summary>
public class Post : IComparable<Post>
{

    public Control control;
    public DateTime timestamp;

    // fall back to DateTime's CompareTo
    public int CompareTo(Post other)
    {
        return this.timestamp.CompareTo(other.timestamp);
    }

    // basic constructor..
    public Post(Control control, DateTime timestamp )
    {
        this.control = control;
        this.timestamp = timestamp;
        // this is a hack
        Label timestampLabel = new Label();
        timestampLabel.Text = this.timestamp.ToString();
        control.Controls.Add(timestampLabel);
    }
    
}
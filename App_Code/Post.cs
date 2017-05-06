using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Post
/// </summary>
public class Post : IComparable<Post>
{

    public Control control;
    public DateTime timestamp;

    //this function should be useless....
    //public bool after(Post that)
    //{
    //    // CompareTo returns < 0 when this instance *proceeds* it.
    //    return that.timestamp.CompareTo(this.timestamp) < 0;
    //}

    // fall back to DateTime's CompareTo
    public int CompareTo(Post other)
    {
        return this.timestamp.CompareTo(other.timestamp);
    }

    public Post(Control control, DateTime timestamp )
    {
        if (control == null) {
            Label lbl = new Label(); lbl.Text = "nuuuuu"; // just some debugging. should never happen - right??.....
            control = lbl;
        }
        this.control = control;
        this.timestamp = timestamp;
    }
    
}
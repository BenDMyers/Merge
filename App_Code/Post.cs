using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Post
/// </summary>
public class Post
{

    public Control control;
    public DateTime timestamp;


    public bool after(Post that)
    {
        // CompareTo returns < 0 when this instance *proceeds* it.
        return that.timestamp.CompareTo(this.timestamp) < 0;
    }
    public Post(Control control, DateTime timestamp )
    {
        if (control == null) {
            Label lbl = new Label(); lbl.Text = "nuuuuu";
            control = lbl;
        }
        this.control = control;
        this.timestamp = timestamp;
    }
}
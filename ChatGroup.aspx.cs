using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class ChatGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Grab the queried groupid
        var groupid = HttpContext.Current.Request.Url.Query;
        groupid = groupid.Split('=')[1];

        // Here check if the user is a member of the group
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);

        string query = "select  g.groupname,g.groupid from member m left join users u on u.userid = m.muserid left join groups g on g.groupid = m.mgroupid where m.muserid = " + Session["UserId"] + " and m.mgroupid = " + groupid + ";";
        SqlCommand getGroups = new SqlCommand(query, conn);
        conn.Open();

        SqlDataReader dr = getGroups.ExecuteReader();

        if (!dr.HasRows)
        {
            Response.Redirect("NewsFeed.aspx");
        }
        else
        {
            dr.Read();
            outgoingUser.InnerText = dr["groupname"].ToString();
        }

        conn.Close();
    }
}
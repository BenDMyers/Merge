using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditGroup : System.Web.UI.Page
{
    private void bug(string oops)
    {
        Label lbl = new Label();
        lbl.Text = oops;
        debugr.Controls.Add(lbl);
    }

    private void doQuery(string query)
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }
    private void removeMember(int uid, int gid)
    {
        string query = "delete from member where muserid=" + uid.ToString() + " AND mgroupid=" + gid.ToString() + "; ";
        doQuery(query);
        bug(query);
    }
    private void adminifyMember(int uid, int gid)
    {
        doQuery("update member set madmin=1 where muserid=" + uid.ToString() + " AND mgroupid=" + gid.ToString() + "; ");
        //doQuery("delete from member where muserid=" + uid.ToString() + " AND mgroupid=" + uid.ToString() + "; ");
    }
    private void deAdminifyMember(int uid, int gid)
    {
        doQuery("update member set madmin=0 where muserid=" + uid.ToString() + " AND mgroupid=" + gid.ToString() + "; ");
        //doQuery("delete from member where muserid=" + uid.ToString() + " AND mgroupid=" + uid.ToString() + "; ");
    }
    protected void MemberView_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandArgument == null) return;
        int uid = Convert.ToInt32(e.CommandArgument);
        int gid = Int32.Parse(Request.QueryString["gid"]);
        bug("GRRRRRRR");
        bug(e.CommandName);
        bug("||");
        switch (e.CommandName)
        {
            case "RemoveMember":
                removeMember(uid, gid);
                break;
            case "AdminifyMember":
                adminifyMember(uid, gid);
                break;
            case "DeAdminifyMember":
                deAdminifyMember(uid, gid);
                break;
        }
        Response.Redirect("/EditGroup.aspx?gid=" + gid); // force page reload ...
    }

    protected void NewMember_OnClick(object sender, EventArgs e)
    {
        int gid = Int32.Parse(Request.QueryString["gid"]);
        if (MemberUserid.Text != null || MemberUserid.Text != "")
        {
            int uid = Int32.Parse(MemberUserid.Text);
            doQuery("insert into member (muserid, mgroupid, madmin) values (" + uid.ToString() + "," + gid.ToString() + ",0 )");
        }
        else if (MemberUsername.Text != null && MemberUsername.Text != "")
        { // uid not specified, try to lookup based on username - should fail if there is more than one user with same name
            //doQuery("");
            bug("NOOOOOOOOOOO!!!!!");
        }
        Response.Redirect("/EditGroup.aspx?gid=" + gid); // force page reload...
    }
        protected void Page_Load(object sender, EventArgs e)
    {
        bug("derp.");

    }
}
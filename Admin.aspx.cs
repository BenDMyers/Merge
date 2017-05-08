using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Admin : System.Web.UI.Page
{
    private void doQuery(string query)
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);
        con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
        con.Close();
    }
    private void removeUser(int uid)
    {
        // do something
        Label lbl = new Label();
        lbl.Text = "remove user " + uid.ToString();
        debugr.Controls.Add(lbl);

        doQuery("delete from users where userid=" + uid.ToString() + ";");
        
    }
    private void editUser(int uid)
    {
        // do something
        Label lbl = new Label();
        lbl.Text = "edit user " + uid.ToString();
        debugr.Controls.Add(lbl);

        Response.Redirect("/EditUser.aspx?uid=" + uid);
    }
    private void removeGroup(int gid)
    {
        // do something
        Label lbl = new Label();
        lbl.Text = "remove group " + gid.ToString();
        debugr.Controls.Add(lbl);

        doQuery("delete from member where mgroupid=" + gid + "; delete from groups where groupid=" + gid + ";");
    }
    private void editGroup(int gid)
    {
        // do something
        Label lbl = new Label();
        lbl.Text = "edit group " + gid.ToString();
        debugr.Controls.Add(lbl);
        Response.Redirect("/EditGroup.aspx?gid=" + gid);
    }

    protected void UserView_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "RemoveUser" && e.CommandName != "EditUser") return;
        int uid = Convert.ToInt32(e.CommandArgument);

        switch (e.CommandName)
        {
            case "RemoveUser":
                removeUser(uid);
                Response.Redirect("/Admin.aspx"); // force whole page reload
                break;
            case "EditUser":
                editUser(uid);
                break;
        }

    }
    protected void GroupView_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName != "RemoveGroup" && e.CommandName != "EditGroup") return;
        int gid = Convert.ToInt32(e.CommandArgument);

        switch (e.CommandName)
        {
            case "RemoveGroup":
                removeGroup(gid);
                Response.Redirect("/Admin.aspx"); // force whole page reload
                break;
            case "EditGroup":
                editGroup(gid);
                break;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
	{

	}

    protected void NewGroup_OnClick(object sender, EventArgs e)
    {
        string name = NewGroupName.Text;
        string about = NewGroupName.Text;
        string filename;
        if (NewGroupAvatar.HasFile)
        {
            //Using a try statement allows us to output debugging problems.
            try
            {
                filename = NewGroupAvatar.FileName;
                NewGroupAvatar.SaveAs(Server.MapPath("~/pictures/avatars/") + filename);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Upload status: The file could not be uploaded. The following error occured: " + ex.Message);
            }
        }
        // Username is available
        RegisterGroupData.Insert();
        Response.Redirect("/Admin.aspx"); // force whole page reload
    }
}
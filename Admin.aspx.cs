using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin : System.Web.UI.Page
{
    private void removeUser(int uid)
    {
        // do something
        Label lbl = new Label();
        lbl.Text = "remove user " + uid.ToString();
        debugr.Controls.Add(lbl);
    }
    private void editUser(int uid)
    {
        // do something
        Label lbl = new Label();
        lbl.Text = "edit user " + uid.ToString();
        debugr.Controls.Add(lbl);
    }
    private void removeGroup(int gid)
    {
        // do something
        Label lbl = new Label();
        lbl.Text = "remove group " + gid.ToString();
        debugr.Controls.Add(lbl);
    }
    private void editGroup(int gid)
    {
        // do something
        Label lbl = new Label();
        lbl.Text = "edit group " + gid.ToString();
        debugr.Controls.Add(lbl);
    }

    protected void UserView_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "RemoveUser" && e.CommandName != "EditUser") return;
        int uid = Convert.ToInt32(e.CommandArgument);

        switch (e.CommandName)
        {
            case "RemoveUser":
                removeUser(uid);
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
    }
}
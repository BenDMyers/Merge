using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class SiteMaster : MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;
    private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

    protected void Page_Init(object sender, EventArgs e)
    {
        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;
    }

    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        }
        else
        {
            // Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }
        }

        // If they aren't logged in, redirect

        if (Session["Username"] == null)
            Response.Redirect("Login.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HiddenNameLabel.Text = (string)Session["Username"];

        // Here we populate the group chat list
        SqlConnection conn = new SqlConnection(connectionString);
        string query = "select g.groupname,g.groupid from member m left join users u on u.userid = m.muserid left join groups g on g.groupid = m.mgroupid where m.muserid = " + Session["UserId"] + ";";

        SqlCommand getGroups = new SqlCommand(query, conn);
        conn.Open();

        SqlDataReader dr = getGroups.ExecuteReader();

        if (dr.HasRows)
        {
            while (dr.Read())
            {
                ListItem GroupItem = new ListItem();
                GroupItem.Attributes.Add("class", "li-chatbox li-chatbox-group");
                GroupItem.Value = "ChatGroup.aspx?group=" + dr["groupid"];
                GroupItem.Text = dr["groupname"].ToString().Replace(" ", "_" + "\n") + ".grp";
                GroupList.Items.Add(GroupItem);
            }
        }

        conn.Close();
    }

	public void postClick(object sender, EventArgs e)
	{

        Control button = sender as Control;
        DateTime myDateTime = DateTime.UtcNow;

        if (PostPic.HasFile)
		{
			//Using a try statement allows us to output debugging problems.
			try
			{
				string filename = PostPic.FileName;
				PostPic.SaveAs(Server.MapPath("~/pictures/postpics/") + filename);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Upload status: The file could not be uploaded. The following error occured: " + ex.Message);
			}
		}

        if (Int32.Parse(Session["UserId"].ToString())%2 == 0)
		{
            String replyPost = HiddenThing.Value;
			if (replyPost != null && replyPost != "") // this would be a comment on another post
			{
				int parentId = Int32.Parse(replyPost);
				//Connect to the database and check to see if user already exists, if it does, compare the password
				string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
				SqlConnection conn = new SqlConnection(connectionString);
				string query = "insert into postt (ptext, ptimestamp, phascom, puserid, ppicfile, pcode, commentid) values (@posttext, @timestamp, @hascom, @userid, @picfile, @codetext, @parentid);";
				SqlCommand com = new SqlCommand(query, conn);
				conn.Open();

				com.Parameters.AddWithValue("@posttext", WriteTextBox.Text);
				com.Parameters.AddWithValue("@timestamp", myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				com.Parameters.AddWithValue("@parentid", parentId); // set the parent field of this post
				com.Parameters.AddWithValue("@hascom", 0);
				com.Parameters.AddWithValue("@picfile", PostPic.FileName);
				com.Parameters.AddWithValue("@codetext", WriteCodeBox.Text);
				com.Parameters.AddWithValue("@userid", Int32.Parse(Session["UserId"].ToString()));

				com.ExecuteNonQuery();

				// now update the parent posts hascom field to indicate it has a comment
				string updateQuery = "update postt set phascom=1 where postid=" + parentId + ";";
				com = new SqlCommand(updateQuery, conn);

				com.ExecuteNonQuery();
				conn.Close();

			}
			else
			{
				//Connect to the database and check to see if user already exists, if it does, compare the password
				string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
				SqlConnection conn = new SqlConnection(connectionString);
				string query = "insert into postt (ptext, ptimestamp, phascom, puserid, ppicfile, pcode) values (@posttext, @timestamp, @hascom, @userid, @picfile, @codetext);";
				SqlCommand com = new SqlCommand(query, conn);
				conn.Open();

				com.Parameters.AddWithValue("@posttext", WriteTextBox.Text);
				com.Parameters.AddWithValue("@timestamp", myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				com.Parameters.AddWithValue("@hascom", 0);
				com.Parameters.AddWithValue("@picfile", PostPic.FileName);
				com.Parameters.AddWithValue("@codetext", WriteCodeBox.Text);
				com.Parameters.AddWithValue("@userid", Int32.Parse(Session["UserId"].ToString()));

                com.ExecuteNonQuery();

                conn.Close();
            }
		}
		else
		{
			//Connect to the database and check to see if user already exists, if it does, compare the password
			SqlConnection conn = new SqlConnection(connectionString);
			string query = "insert into postt (ptext, ptimestamp, phascom, pgroupid, ppicfile, pcode) values (@posttext, @timestamp, @hascom, @groupid, @picfile, @codetext);";
			SqlCommand com = new SqlCommand(query, conn);
			conn.Open();

			com.Parameters.AddWithValue("@posttext", WriteTextBox.Text);
			com.Parameters.AddWithValue("@timestamp", myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
			com.Parameters.AddWithValue("@hascom", 0);
			com.Parameters.AddWithValue("@picfile", PostPic.FileName);
			com.Parameters.AddWithValue("@codetext", WriteCodeBox.Text);
			com.Parameters.AddWithValue("@groupid", Int32.Parse(Session["UserId"].ToString()));

			com.ExecuteNonQuery();

			conn.Close();
		}

        WriteTextBox.Text = "";
        WriteCodeBox.Text = "";
        Response.Redirect(Request.RawUrl);
	}

	protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Context.GetOwinContext().Authentication.SignOut();
    }

	public void logoutclick(object sender, EventArgs e)
	{
		Session["LogOut"] = "1";
		Session["Username"] = null;
		Session["UserId"] = null;
		Response.Redirect("Login.aspx");

	}

	//protected void Update_Code_Preview()
	//{
	//    Literal codePre = new Literal();
	//    codePre.Text = "<pre><code>";
	//    Label code = new Label();
	//    code.Text = HttpUtility.HtmlEncode(WriteCodeBox.Text); // maybe not code injection? 
	//    Literal codePost = new Literal();
	//    codePost.Text = "</pre></code>";

	//    CodePreviewPanel.Controls.Add(codePre);
	//    CodePreviewPanel.Controls.Add(code);
	//    CodePreviewPanel.Controls.Add(codePost);
	//}
}

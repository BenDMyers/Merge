using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for UserPost
/// </summary>
public class UserPost
{
    
    public static Control makePost(User user, String text, String imgSrc, String codeSrc, DateTime timestamp, bool isComment)
    {
        // layout of this code block should resemble the layout of the generated HTML!
        // ( i.e. outer elements are created first, and added after - creation is open tag, adding is close tag
        Panel post = new Panel();

        if (isComment)
        {
            post.CssClass = "post-container comment-post";
            Panel comment = new Panel();
            comment.CssClass = "comment-block";

            Label commentIndicator = new Label();
            commentIndicator.Text = "//";

            comment.Controls.Add(commentIndicator);
            post.Controls.Add(comment);
        } else
        {
            post.CssClass = "post-container";
        }


        Panel block = new Panel();
        block.CssClass = "post-block";

        // make the username and avatar container
        Panel userContainer = new Panel();
        userContainer.CssClass = "user-info";

        Image userAvatar = new Image();
        userAvatar.CssClass = "avatar";
        userAvatar.ImageUrl = user.avatar; // user.avatar contains server path info as well, so its all in one place
        // and finally add them to the container
        userContainer.Controls.Add(userAvatar);

		if (user.userid % 2 != 0)
		{
			HyperLink userText = new HyperLink();
			userText.Text = user.username;
			userText.NavigateUrl = "~/GroupProfile.aspx?groupid=" + user.userid + "&groupname=" + user.username + "&admin=" + user.admin;
			// and finally add them to the container
			userContainer.Controls.Add(userText);
		}
		else
		{
			HyperLink userText = new HyperLink();
			userText.Text = user.username;
			userText.NavigateUrl = "~/UserProfile.aspx?userid=" + user.userid + "&username=" + user.username + "&admin=" + user.admin;
			// and finally add them to the container
			userContainer.Controls.Add(userText);
		}

        // and add the container to the outer block
        block.Controls.Add(userContainer);

        // add the text container
        Panel textContainer = new Panel();
        textContainer.CssClass = "content-container text-content";

        Label textLabel = new Label();
        textLabel.Text = text;
        textContainer.Controls.Add(textLabel);

        //and finally add the container to the outer block
        block.Controls.Add(textContainer);

        if (codeSrc != null && codeSrc != "")
        {
            //Literal code = new Literal();
            //code.Text = "<pre><code>" + "</code></pre>"; //ROFL HOW FAST CAN YOU SAY CODE INJECTION!

            Panel codePanel = new Panel();
            codePanel.CssClass = "content-container code-content";

            // make the code element - this involves some special html tags that don't exist in asp
            // so F**K ASP.NET and let's write literal HTML
            Literal codePre = new Literal();
            codePre.Text = "<pre><code>";
            Label code = new Label();
            code.Text = HttpUtility.HtmlEncode(codeSrc); // maybe not code injection? 
            Literal codePost = new Literal();
            codePost.Text = "</pre></code>";

            // and add the code to the container
            codePanel.Controls.Add(codePre);
            codePanel.Controls.Add(code);
            codePanel.Controls.Add(codePost);

            block.Controls.Add(codePanel);
        }
        if (imgSrc != null && imgSrc != "" && imgSrc != "System.Web.UI.WebControls.FileUpload")
        {
            Panel imageContainer = new Panel();
            imageContainer.CssClass = "content-container img-content";

            Image img = new Image();
            img.CssClass = "post-image";
            img.ImageUrl = "~/pictures/postpics/" + imgSrc;
            imageContainer.Controls.Add(img);

            //and finally add the container to the outer block
            block.Controls.Add(imageContainer);
        }

        post.Controls.Add(block);
		
        return post;
    }
}
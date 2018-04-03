using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AppSession.GetUserProfile() != null)
            {
                UserProfile userProfile = new UserProfile();
                userProfile = AppSession.GetUserProfile();

                //2 = Judge
                if (userProfile.UserLevel != 1)
                    Response.Redirect("Login.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Menu_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            switch (linkButton.ID)
            {
                case "lbtnAddEvent":
                    Response.Redirect("AddEvent.aspx");
                    break;
                case "lbtnManageJudge":
                    Response.Redirect("ManageJudge.aspx");
                    break;
                case "lbtnUpdateEvent":
                    Response.Redirect("UpdateEvent.aspx");
                    break;
                case "lbtnAddCategory":
                    Response.Redirect("AddCategory.aspx");
                    break;
                case "lbtnUpdateCategory":
                    Response.Redirect("UpdateCategory.aspx");
                    break;
                case "lbtnInviteJudge":
                    Response.Redirect("InviteJudge.aspx");
                    break;
                default:
                    break;
            }
        }

    }
}
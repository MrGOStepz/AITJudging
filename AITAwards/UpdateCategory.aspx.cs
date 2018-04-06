using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class UpdateCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AppSession.GetUserProfile() != null)
            {
                UserProfile userProfile = new UserProfile();
                userProfile = AppSession.GetUserProfile();

                //1 = Admin
                if (userProfile.UserLevel != 1)
                    Response.Redirect("Login.aspx");

            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            if (AppSession.GetUpdateCategoryState() == "EditCategory")
            {
                divControl.Controls.Clear();

                PlaceHolder phControl = new PlaceHolder();
                UpdateCategoryDetailControl updateCategoryDetailControl = (UpdateCategoryDetailControl)LoadControl("UpdateCategoryDetailControl.ascx");
                updateCategoryDetailControl.ID = "updateDetailControl";
                updateCategoryDetailControl.EventID = AppSession.GetUpdateEventID();
                phControl.Controls.Add(updateCategoryDetailControl);
                divControl.Controls.Add(phControl);
                return;
            }

            InitializePage();
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

        private void InitializePage()
        {
            List<AITEvent> lstAitEvent = new List<AITEvent>();
            AITEvent aitEvent = new AITEvent();

            IAdminDatabase adminDB = new AdminDB();
            lstAitEvent = adminDB.GetListEvent();

            for (int i = 0; i < lstAitEvent.Count; i++)
            {
                ImageButton image = new ImageButton();
                image.ID = "img" + lstAitEvent[i].EventID;
                image.ImageUrl = "Images/Event/" + lstAitEvent[i].PathFile;
                image.CssClass = "image-background";

                image.Click += Image_Click;

                divControl.Controls.Add(new LiteralControl("<div class='col-lg-3'>"));
                divControl.Controls.Add(new LiteralControl("<div class='social-box facebook'>"));
                divControl.Controls.Add(image);
                divControl.Controls.Add(new LiteralControl("</div>"));
                divControl.Controls.Add(new LiteralControl("</div>"));
            }
        }

        private void Image_Click(object sender, ImageClickEventArgs e)
        {
            int eventID;
            ImageButton imageButton = sender as ImageButton;
            try
            {
                eventID = int.Parse(imageButton.ID.Substring(3));
            }
            catch (Exception)
            {
                eventID = -1;
            }


            Response.Redirect("UpdateCategoryDetail.aspx?eventID=" + eventID);

        }
    }
}
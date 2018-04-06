using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class BestScore : System.Web.UI.Page
    {
        public UserProfile _userProfile = new UserProfile();
        public JudgeCategory _judgeCategory = new JudgeCategory();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (AppSession.GetUserProfile() != null && AppSession.GetJudgeAndCategory() != null)
            {

                _userProfile = new UserProfile();
                _judgeCategory = new JudgeCategory();

                _userProfile = AppSession.GetUserProfile();
                _judgeCategory = AppSession.GetJudgeAndCategory();

                AppSession.SetUserProfile(_userProfile);
                AppSession.SetJudgeAndCategory(_judgeCategory);
                //2 = Judge
                if (_userProfile.UserLevel != 2)
                    Response.Redirect("Login.aspx");
                if (_judgeCategory.CategoryID != 1)
                    Response.Redirect("Categories.aspx");

                InitializePage();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void InitializePage()
        {
            IAdminDatabase adminDB = new AdminDB();
            List<AITCategories> lstAitCategories = new List<AITCategories>();

            lstAitCategories = new List<AITCategories>();
            lstAitCategories = adminDB.GetListCategoryByEventID(1);

            contentControl.Controls.Clear();

            contentControl.Controls.Add(new LiteralControl("<div class='row padding-10'>"));
            ImageButton imageButton;

            for (int i = 2; i < lstAitCategories.Count + 1; i++)
            {

                imageButton = new ImageButton();
                imageButton.ID = "cat" + lstAitCategories[i - 1].CategoryID;
                imageButton.CssClass = "rounded image-width";
                //TODO Change Image
                imageButton.ImageUrl = "Images/Categories/" + lstAitCategories[i - 1].PathFile;
                imageButton.Click += ImageButton_Click;

                contentControl.Controls.Add(new LiteralControl("<div class='col text-center'>"));
                contentControl.Controls.Add(imageButton);
                contentControl.Controls.Add(new LiteralControl("</div>"));

                if (i % 3 == 0)
                {
                    contentControl.Controls.Add(new LiteralControl("</div>"));
                    contentControl.Controls.Add(new LiteralControl("<div class='row'>"));
                }
            }

            contentControl.Controls.Add(new LiteralControl("</div>"));
        }

        private void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            string imageID;
            ImageButton imageButton = sender as ImageButton;
            imageID = imageButton.ID;
            imageID = imageID.Substring(3);

            Response.Redirect("BestScoreCategory.aspx?categoryID=" + imageID);
        }
    }
}
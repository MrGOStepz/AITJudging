using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class Categories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AppSession.GetUserProfile() != null)
            {
                UserProfile userProfile = new UserProfile();
                userProfile = AppSession.GetUserProfile();

                //2 = Judge
                if (userProfile.UserLevel != 2)
                    Response.Redirect("Index.aspx");

                InitializePage(userProfile);
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }

        protected void InitializePage(UserProfile userProfile)
        {
            List<JudgeCategory> lstJudgeCategories = new List<JudgeCategory>();

            //TODO Setting EventID
            IJudgeDatabase judgeDatabase = new JudgeDB();
            lstJudgeCategories = judgeDatabase.GetListOfCategoriesByUserIDAndEventID(userProfile.UserID, 1);

            contentControl.Controls.Clear();

            contentControl.Controls.Add(new LiteralControl("<div class='row padding-10'>"));
            ImageButton imageButton;

            for (int i = 1; i < lstJudgeCategories.Count + 1; i++)
            {

                imageButton = new ImageButton();
                imageButton.ID = "cat" + lstJudgeCategories[i - 1].CategoryID;
                imageButton.CssClass = "rounded image-width";
                //TODO Change Image
                imageButton.ImageUrl = "Images/Temp/CategoryImage.png";
                imageButton.Click += ImageButton_Click;

                contentControl.Controls.Add(new LiteralControl("<div class='col text-center'>"));
                contentControl.Controls.Add(imageButton);
                contentControl.Controls.Add(new LiteralControl("</div>"));

                if (i % 5 == 0)
                {
                    contentControl.Controls.Add(new LiteralControl("</div>"));
                    contentControl.Controls.Add(new LiteralControl("<div class='row'>"));
                }
            }

            contentControl.Controls.Add(new LiteralControl("</div>"));
        }

        private void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            JudgeCategory judgeCategory = new JudgeCategory();

            string imageID;
            ImageButton imageButton = sender as ImageButton;
            imageID = imageButton.ID;
            imageID = imageID.Substring(3);
            judgeCategory.CategoryID = int.Parse(imageID);
            judgeCategory.UserID = AppSession.GetUserID();

            AppSession.SetJudgeAndCategory(judgeCategory);
            Response.Redirect("Project.aspx");
        }
    }
}
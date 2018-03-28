using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class StudentWork : System.Web.UI.Page
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AppSession.GetUserProfile() != null && AppSession.GetJudgeAndCategory() != null)
            {
                UserProfile userProfile = new UserProfile();
                JudgeCategory judgeCategory = new JudgeCategory();

                userProfile = AppSession.GetUserProfile();
                judgeCategory = AppSession.GetJudgeAndCategory();
        
                //2 = Judge
                if (userProfile.UserLevel != 2)
                    Response.Redirect("Index.aspx");
        
                InitializePage(userProfile, judgeCategory);
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        
        }

        protected void InitializePage(UserProfile userProfile, JudgeCategory judgeCategory)
        {
            List<ProjectDetail> lstProject = new List<ProjectDetail>();
            List<int> lstProjecDone = new List<int>();
        
            //TODO Setting EventID
            IJudgeDatabase judgeDatabase = new JudgeDB();
            lstProject = judgeDatabase.GetListOfProjectByCategory(judgeCategory.CategoryID);
            lstProjecDone = judgeDatabase.GetListProjectDoneByJudgeIDAndCategoryID(userProfile.UserID, judgeCategory.CategoryID);
            contentControl.Controls.Clear();
        
            contentControl.Controls.Add(new LiteralControl("<div class='row padding-10'>"));
            ImageButton imageButton;
            

            for (int i = 1; i < lstProject.Count + 1; i++)
            { 
                imageButton = new ImageButton();
                imageButton.ID = "project" + lstProject[i -1].ProjectID;
                

                for (int j = 0; j < lstProjecDone.Count; j++)
                {
                    if(lstProject[i - 1].ProjectID == lstProjecDone[j])
                    {
                        imageButton.Enabled = false;
                        imageButton.BorderColor = System.Drawing.Color.Red;
                        imageButton.BorderWidth = 5;
                        break;
                    }
                }

                //TODO Change Image
                imageButton.ImageUrl = "Images/Projects/"+ lstProject[i-1].PathFile;

                System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(imageButton.ImageUrl));

                if (image.Height > image.Width)
                {
                    imageButton.CssClass = "rounded image-wa";
                }
                else
                {
                    imageButton.CssClass = "rounded image-ha";
                }

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
            ProjectDetail projectDetail = new ProjectDetail();
        
            string imageID;
            ImageButton imageButton = sender as ImageButton;
            imageID = imageButton.ID;
            imageID = imageID.Substring(7);

            AppSession.SetProjectID(int.Parse(imageID));
            Response.Redirect("Rubric.aspx");
        }
            
     }
}
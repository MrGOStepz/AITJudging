using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class Marking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AppSession.GetUserProfile() != null && AppSession.GetRubric() != null)
            {
                UserProfile userProfile = new UserProfile();
                JudgeCategory judgeCategory = new JudgeCategory();

                userProfile = AppSession.GetUserProfile();

                //2 = Judge
                if (userProfile.UserLevel != 2)
                    Response.Redirect("Index.aspx");

                InitializePage(AppSession.GetProjectID());
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }

        protected void InitializePage(int projectID)
        {
            ProjectDetail projectDetail = new ProjectDetail();

            QuestionControl questionControl = (QuestionControl)LoadControl("UserControl/QuestionControl.ascx");
            questionControl.ID = "questionControl";

            IJudgeDatabase judgeDatabase = new JudgeDB();
            projectDetail = judgeDatabase.GetProjectbyProjectID(projectID);
            imgProject.ImageUrl = "Images/Projects/" + projectDetail.PathFile;
            imgProject.Attributes.Add("style", "width: auto; height: 50vh;");

            phControl.Controls.Clear();
            phControl.Controls.Add(questionControl);

        }

    }
}
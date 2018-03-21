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

                if(AppSession.GetListAnswer() == null)
                {
                    AppSession.SetQuestionNo(0);
                }
                else
                {
                    
                }

                userProfile = AppSession.GetUserProfile();
                judgeCategory = AppSession.GetJudgeAndCategory();

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
            imgProject.ImageUrl = projectDetail.PathFile;
            imgProject.Attributes.Add("style", "width: auto; height: 50vh;");

            phControl.Controls.Clear();
            phControl.Controls.Add(questionControl);

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            RadioButton radioButton = new RadioButton();
            AnswerDetail lstAnswer = new AnswerDetail();

            QuestionControl questionControl = (QuestionControl)phControl.FindControl("questionControl");

            for (int i = 0; i < questionControl.ListBtnRadioID.Count; i++)
            {
                radioButton = (RadioButton)questionControl.RubricControl.FindControl("btnRadio" + questionControl.ListBtnRadioID[i]);
                if(radioButton.Checked)
                {

                }
            }
            

            AppSession.SetQuestionNo(AppSession.GetQuestionNo() + 1);
            RubricDetail rubricDetail = new RubricDetail();
            rubricDetail = AppSession.GetRubric();

            if(AppSession.GetQuestionNo() > rubricDetail.ListCriteriaDetail.Count)
            {
                Response.Redirect("Result.aspx");
            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (AppSession.GetQuestionNo() == 0)
            {
                Response.Redirect("Rubric.aspx");
            }
            else
            {
                AppSession.SetQuestionNo(AppSession.GetQuestionNo() - 1);
            }
        }

        protected void ReloadRubricQuestion()
        {

        }

    }
}
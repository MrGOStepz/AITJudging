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

                //if(AppSession.GetListAnswer() == null)
                //{
                //    AppSession.SetQuestionNo(0);
                //}

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
            imgProject.ImageUrl = projectDetail.PathFile;
            imgProject.Attributes.Add("style", "width: auto; height: 50vh;");

            phControl.Controls.Clear();
            phControl.Controls.Add(questionControl);

        }

        //protected void btnNext_Click(object sender, EventArgs e)
        //{
        //    RadioButton radioButton = new RadioButton();
        //    AnswerDetail answerDetail = new AnswerDetail();

        //    QuestionControl questionControl = (QuestionControl)phControl.FindControl("questionControl");
        //    int s = questionControl.TempQuestionON;
        //    for (int i = 0; i < questionControl.ListBtnRadioID.Count; i++)
        //    {
        //        radioButton = (RadioButton)questionControl.RubricControl.FindControl("btnRadio" + (10 * AppSession.GetQuestionNo() + questionControl.ListBtnRadioID[i]));
        //        if (radioButton.Checked)
        //        {
        //            answerDetail.Answer = questionControl.ListBtnRadioID[i];
        //            answerDetail.Description = questionControl.TextBoxDescription.Text;

        //            List<AnswerDetail> lstAnswerDetail = new List<AnswerDetail>();
        //            lstAnswerDetail = AppSession.GetListAnswer();

        //            if (lstAnswerDetail != null)
        //            {
        //                for (int j = 0; j < lstAnswerDetail.Count; j++)
        //                {
        //                    if (lstAnswerDetail[j].Question == AppSession.GetQuestionNo())
        //                    {
        //                        lstAnswerDetail[j].Answer = answerDetail.Answer;
        //                        lstAnswerDetail[j].Description = answerDetail.Description;
        //                        AppSession.SetListAnswer(lstAnswerDetail);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                List<AnswerDetail> lstAnswer = new List<AnswerDetail>();
        //                lstAnswer.Add(answerDetail);
        //                AppSession.SetListAnswer(lstAnswer);
        //            }
        //        }
        //    }

        //    questionControl.RubricControl.Controls.Clear();

        //    RubricDetail rubricDetail = new RubricDetail();
        //    rubricDetail = AppSession.GetRubric();

        //    if(AppSession.GetQuestionNo() > rubricDetail.ListCriteriaDetail.Count)
        //    {
        //        Response.Redirect("Result.aspx");
        //    }
        //    else
        //    {
        //        AppSession.SetQuestionNo(AppSession.GetQuestionNo() + 1);
        //        ReloadRubricQuestion();
        //    }

        //}

        //protected void btnBack_Click(object sender, EventArgs e)
        //{
        //    if (AppSession.GetQuestionNo() == 0)
        //    {
        //        Response.Redirect("Rubric.aspx");
        //    }
        //    else
        //    {
        //        AppSession.SetQuestionNo(AppSession.GetQuestionNo() - 1);
        //        ReloadRubricQuestion();
        //    }
        //}

        protected void ReloadRubricQuestion()
        {
            phControl.Controls.Clear();
            QuestionControl questionControl = (QuestionControl)LoadControl("UserControl/QuestionControl.ascx");
            phControl.Controls.Add(questionControl);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class Result : System.Web.UI.Page
    {
        public UserProfile _userProfile = new UserProfile();
        public JudgeCategory _judgeCategory = new JudgeCategory();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (AppSession.GetUserProfile() != null && AppSession.GetJudgeAndCategory() != null && AppSession.GetListAnswer() != null)
            {
                _userProfile = new UserProfile();
                _judgeCategory = new JudgeCategory();

                _userProfile = AppSession.GetUserProfile();
                _judgeCategory = AppSession.GetJudgeAndCategory();

                List<AnswerDetail> lstAnswer = new List<AnswerDetail>();
                lstAnswer = AppSession.GetListAnswer();
                lstAnswer = lstAnswer.OrderBy(o => o.Question).ToList();

                AppSession.SetUserProfile(_userProfile);
                AppSession.SetJudgeAndCategory(_judgeCategory);

                //2 = Judge
                if (_userProfile.UserLevel != 2)
                    Response.Redirect("Index.aspx");

                InitializePage(lstAnswer, AppSession.GetProjectID());
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }

        protected void InitializePage(List<AnswerDetail> lstAnswerDetail, int projectID)
        {
            ProjectDetail projectDetail = new ProjectDetail();
            RubricDetail rubricDetail = new RubricDetail();

            List<CriteriaDetail> lstCriteriaDetail = new List<CriteriaDetail>();
            List<LevelCriteria> lstLevelCriteria = new List<LevelCriteria>();



            IJudgeDatabase judgeDatabase = new JudgeDB();
            projectDetail = judgeDatabase.GetProjectbyProjectID(projectID);

            if (projectDetail.TypeFileID == 1)
            {
                vdoCon.Visible = false;
                imgProject.Visible = true;
                lrURL.Visible = false;
                imgProject.ImageUrl = "Images/Projects/" + projectDetail.CategoryID + "/" + projectDetail.PathFile;
                //imgProject.Attributes.Add("style", "width: auto; height: 50vh;");
                System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(imgProject.ImageUrl));
                if (image.Height > image.Width)
                {
                    imgProject.Attributes.Add("style", "width: auto; height: 50vh;");
                }
                else
                {
                    imgProject.Attributes.Add("style", "width: 100%; height: auto;");
                }
            }
            else
            {
                vdoCon.Visible = true;
                imgProject.Visible = false;
                lrURL.Visible = true;
                lrURL.Text = projectDetail.PathFile;
            }


            //txtDescription.Text = projectDetail.Description;
            //txtName.Text = projectDetail.Name;

            lstCriteriaDetail = judgeDatabase.GetCriteriaByCategoryID(projectDetail.CategoryID);

            for (int i = 0; i < lstCriteriaDetail.Count; i++)
            {
                lstCriteriaDetail[i].LevelCritieria = judgeDatabase.GetListOfLevelCriteriaByCriteriaID(lstCriteriaDetail[i].CriteriaID);
            }

            rubricDetail.CategoryID = projectDetail.CategoryID;
            rubricDetail.ListCriteriaDetail = lstCriteriaDetail;

            rubricTB.Controls.Clear();

            //Create Rubic
            rubricTB.Controls.Add(new LiteralControl("<tr>"));
            string criteriaName = null;
            float criterScore = 0.0f;
            float score = 0.0f;
            string comment = "";
            float totalscore = 0.0f;
            float fullscore = 0.0f;

            for (int i = 0; i < rubricDetail.ListCriteriaDetail.Count; i++)
            {
                criterScore = 0.0f;
                criteriaName = rubricDetail.ListCriteriaDetail[i].Name;
                rubricTB.Controls.Add(new LiteralControl("<td style='font-size:13px;'>" + criteriaName + "</td>"));
                rubricTB.Controls.Add(new LiteralControl("<td> <table class='table table-bordered'> <tr>"));

                for (int j = 0; j < rubricDetail.ListCriteriaDetail[i].LevelCritieria.Count; j++)
                {
                    if(lstAnswerDetail[i].Answer == j)
                    {
                        rubricTB.Controls.Add(new LiteralControl("<td bgcolor='red' style='font-size:11px;'>"));
                        rubricTB.Controls.Add(new LiteralControl(rubricDetail.ListCriteriaDetail[i].LevelCritieria[j].Description));
                        rubricTB.Controls.Add(new LiteralControl("</td>"));
                        score = rubricDetail.ListCriteriaDetail[i].LevelCritieria[j].ValueScore;
                        comment = lstAnswerDetail[i].Description;
                    }
                    else
                    {
                        rubricTB.Controls.Add(new LiteralControl("<td style='font-size:11px;'>"));
                        rubricTB.Controls.Add(new LiteralControl(rubricDetail.ListCriteriaDetail[i].LevelCritieria[j].Description));
                        rubricTB.Controls.Add(new LiteralControl("</td>"));
                    }

                    if (criterScore < rubricDetail.ListCriteriaDetail[i].LevelCritieria[j].ValueScore)
                        criterScore = rubricDetail.ListCriteriaDetail[i].LevelCritieria[j].ValueScore;

                }


                rubricTB.Controls.Add(new LiteralControl("</tr> <tr>"));
                rubricTB.Controls.Add(new LiteralControl("<td colspan='" + rubricDetail.ListCriteriaDetail[i].LevelCritieria.Count + "'>")); 
                TextBox textBox = new TextBox();
                textBox.ID = "txtDescription";
                textBox.TextMode = TextBoxMode.MultiLine;
                textBox.CssClass = "form-control";
                textBox.Text = comment;
                textBox.Enabled = false;

                rubricTB.Controls.Add(textBox);
                rubricTB.Controls.Add(new LiteralControl("</td></tr>"));

                rubricTB.Controls.Add(new LiteralControl("</tr> </table> </td> <td>"));
                rubricTB.Controls.Add(new LiteralControl(score.ToString("0.0") + " out of " + criterScore.ToString("0.0")));
                rubricTB.Controls.Add(new LiteralControl("</td> </tr>"));

                totalscore += score;
                fullscore += criterScore;

            }
            txtTotal.Text = totalscore.ToString("0.0") + " out of " + fullscore;
            rubricTB.Controls.Add(new LiteralControl("</tr>"));
            AppSession.SetTotalScore(totalscore);
            AppSession.SetRubric(rubricDetail);
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            List<ScoreModel> lstScoreModel = new List<ScoreModel>();
            ScoreModel scoreModel;
            RubricDetail rubricDetail = new RubricDetail();
            List<AnswerDetail> lstAnswerDetail = new List<AnswerDetail>();
            lstAnswerDetail = AppSession.GetListAnswer();
            rubricDetail = AppSession.GetRubric();

            

            for (int i = 0; i < lstAnswerDetail.Count; i++)
            {
                scoreModel = new ScoreModel();
                scoreModel.ProjectID = AppSession.GetProjectID();
                scoreModel.CriteriaID = rubricDetail.ListCriteriaDetail[lstAnswerDetail[i].Question].CriteriaID;
                scoreModel.Score = rubricDetail.ListCriteriaDetail[lstAnswerDetail[i].Question].LevelCritieria[lstAnswerDetail[i].Answer].ValueScore;
                scoreModel.Comment = lstAnswerDetail[i].Description;
                scoreModel.UserID = AppSession.GetUserID();
                lstScoreModel.Add(scoreModel);
            }

            float totalScore = AppSession.GetTotalScore();
            Console.WriteLine();
            IJudgeDatabase judgeDatabase = new JudgeDB();
            if (judgeDatabase.InsertProjectScore(lstScoreModel) > 0)
            {
                if (judgeDatabase.AddTotalScoreProjectByJudge(AppSession.GetProjectID(), AppSession.GetUserID(), totalScore) > 0)
                {
                    AppSession.SetListAnswer(null);
                    AppSession.SetQuestionNo(0);
                    Response.Redirect("StudentWork.aspx");
                }
            }
            else
            {
                //TODO show somthing wrong!
            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Marking.aspx");
        }
    }
}
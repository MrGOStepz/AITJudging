using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class Rubric : System.Web.UI.Page
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
            RubricDetail rubricDetail = new RubricDetail();

            List<CriteriaDetail> lstCriteriaDetail = new List<CriteriaDetail>();
            List<LevelCriteria> lstLevelCriteria = new List<LevelCriteria>();

            IJudgeDatabase judgeDatabase = new JudgeDB();
            projectDetail = judgeDatabase.GetProjectbyProjectID(projectID);
            txtName.Text = projectDetail.Name;
            txtDescription.Text = projectDetail.Description;
            imgProject.ImageUrl = "Images/Projects/" + projectDetail.PathFile;
            System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(imgProject.ImageUrl));

            if (image.Height > image.Width)
            {
                imgProject.Attributes.Add("style", "width: 50vh; height: auto; max-height:90vh;");
                lbSetCol.Text = "<div class='col text-center'>";
                lbCDiv.Text = "";
                lbCDiv2.Text = "</div>";
            }
            else
            {
                imgProject.Attributes.Add("style", "width: auto; height: 50vh;");
                lbSetCol.Text = "</div> <div class='row padding-10'>";
                lbCDiv.Text = "</div>";
                lbCDiv2.Text = "";
            }
            
                

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

            for (int i = 0; i < rubricDetail.ListCriteriaDetail.Count; i++)
            {
                criterScore = 0.0f;
                criteriaName = rubricDetail.ListCriteriaDetail[i].Name;
                rubricTB.Controls.Add(new LiteralControl("<th scope = 'row'>" + criteriaName + "</th>"));
                rubricTB.Controls.Add(new LiteralControl("<td> <table class='table table-bordered'> <tr>"));

                for (int j = 0; j < rubricDetail.ListCriteriaDetail[i].LevelCritieria.Count; j++)
                {
                    rubricTB.Controls.Add(new LiteralControl("<td>"));
                    rubricTB.Controls.Add(new LiteralControl(rubricDetail.ListCriteriaDetail[i].LevelCritieria[j].Description));
                    rubricTB.Controls.Add(new LiteralControl("</td>"));

                    if(criterScore < rubricDetail.ListCriteriaDetail[i].LevelCritieria[j].ValueScore)
                        criterScore = rubricDetail.ListCriteriaDetail[i].LevelCritieria[j].ValueScore;
                }

                rubricTB.Controls.Add(new LiteralControl("</tr> </table> </td> <td>"));
                rubricTB.Controls.Add(new LiteralControl(criterScore.ToString("0.0")));
                rubricTB.Controls.Add(new LiteralControl("</td> </tr>"));
                
            }
                
            rubricTB.Controls.Add(new LiteralControl("</tr>"));

            AppSession.SetRubric(rubricDetail);

        }


        protected void btnMark_Click(object sender, EventArgs e)
        {
            Response.Redirect("Marking.aspx");
        }

        protected void btnProject_Click(object sender, EventArgs e)
        {
            AppSession.SetListAnswer(null);
            Response.Redirect("StudentWork.aspx");
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class StudentWorkDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["projectid"] != null)
                {
                    int projectid = 0;
                    try
                    {
                        projectid = int.Parse(Request.QueryString["projectid"]);
                    }
                    catch (Exception)
                    {
                        Response.Redirect("ScoreStudentWork.aspx");
                    }

                    InitializePage(projectid);
                }
                else
                {
                    Response.Redirect("ScoreStudentWork.aspx");
                }
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
            double avgScore = judgeDatabase.GetTotalScoreByProjectID(projectID);
            //txtName.Text = projectDetail.Name;
            //txtDescription.Text = projectDetail.Description;
            txtScore.Text = avgScore.ToString("0.0");
            if (projectDetail.TypeFileID == 1)
            {
                vdoCon.Visible = false;
                imgProject.Visible = true;
                lrURL.Visible = false;

                imgProject.ImageUrl = "Images/Projects/" + projectDetail.CategoryID + "/" + projectDetail.PathFile;
                System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(imgProject.ImageUrl));

                //if (image.Height > image.Width)
                //{
                //    imgProject.Attributes.Add("style", "width: 50vh; height: auto; max-height:90vh;");
                //    lbSetCol.Text = "<div class='col text-center'>";
                //    lbCDiv.Text = "";
                //    lbCDiv2.Text = "</div>";
                //}
                //else
                //{
                //    imgProject.Attributes.Add("style", "width: auto; height: 50vh;");
                //    lbSetCol.Text = "</div> <div class='row padding-10'>";
                //    lbCDiv.Text = "</div>";
                //    lbCDiv2.Text = "";
                //}

                imgProject.Attributes.Add("style", "width: auto; height: 50vh;");
            }
            else
            {
                vdoCon.Visible = true;
                imgProject.Visible = false;
                lrURL.Visible = true;
                lrURL.Text = projectDetail.PathFile;
            }

            lbSetCol.Text = "</div> <div class='row padding-10'>";
            lbCDiv.Text = "</div>";
            lbCDiv2.Text = "";

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
                rubricTB.Controls.Add(new LiteralControl("<th scope = 'row' style='font-size:13px;'>" + criteriaName + "</th>"));
                rubricTB.Controls.Add(new LiteralControl("<td> <table class='table table-bordered'> <tr>"));

                for (int j = 0; j < rubricDetail.ListCriteriaDetail[i].LevelCritieria.Count; j++)
                {
                    rubricTB.Controls.Add(new LiteralControl("<td style='font-size:11px;'>"));
                    rubricTB.Controls.Add(new LiteralControl(rubricDetail.ListCriteriaDetail[i].LevelCritieria[j].Description));
                    rubricTB.Controls.Add(new LiteralControl("</td>"));

                    if (criterScore < rubricDetail.ListCriteriaDetail[i].LevelCritieria[j].ValueScore)
                        criterScore = rubricDetail.ListCriteriaDetail[i].LevelCritieria[j].ValueScore;
                }

                rubricTB.Controls.Add(new LiteralControl("</tr> </table> </td> <td style='font-size:11px;'>"));
                rubricTB.Controls.Add(new LiteralControl(criterScore.ToString("0.0")));
                rubricTB.Controls.Add(new LiteralControl("</td> </tr>"));

            }

            rubricTB.Controls.Add(new LiteralControl("</tr>"));

        }
    }
}
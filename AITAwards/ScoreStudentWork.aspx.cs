using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class ScoreStudentWork : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

                if (Request.QueryString["categoryID"] != null)
                {
                    int categoryID = 0;
                    try
                    {
                        categoryID = int.Parse(Request.QueryString["categoryID"]);
                    }
                    catch (Exception)
                    {
                        Response.Redirect("AllCategories.aspx");
                    }
                    InitializePage(categoryID);
                }
                else
                {
                    Response.Redirect("AllCategories.aspx");
                }
            
        }

        protected void InitializePage(int categoryID)
        {
            List<ProjectDetail> lstProject = new List<ProjectDetail>();
            List<int> lstCriteriaID = new List<int>();
            //TODO Setting EventID
            IJudgeDatabase judgeDatabase = new JudgeDB();
            lstProject = judgeDatabase.GetListOfProjectByCategory(categoryID);

            lstCriteriaID = judgeDatabase.GetCriteriaIDByCategory(categoryID);
            float totalScore = judgeDatabase.GetTotalScoreByCategoryID(lstCriteriaID);
            
            contentControl.Controls.Clear();
            contentControl.Controls.Add(new LiteralControl("<div class='row padding-10'>"));
            ImageButton imageButton;


            for (int i = 1; i < lstProject.Count + 1; i++)
            {
                imageButton = new ImageButton();
                imageButton.ID = "project" + lstProject[i - 1].ProjectID;
                double avgScore = judgeDatabase.GetTotalScoreByProjectID(lstProject[i -1].ProjectID);
                //TODO Change Image
                imageButton.ImageUrl = "Images/Projects/pre_" + lstProject[i - 1].CategoryID + "/" + lstProject[i - 1].PathFile;

                //System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(imageButton.ImageUrl));

                //if (image.Height > image.Width)
                //{
                //    imageButton.CssClass = "rounded image-wa";
                //}
                //else
                //{
                //    imageButton.CssClass = "rounded image-ha";
                //}

                imageButton.CssClass = "rounded image-ha";

                imageButton.Click += ImageButton_Click;

                contentControl.Controls.Add(new LiteralControl("<div class='col text-center'>"));
                contentControl.Controls.Add(new LiteralControl("<div class='card' style='width: 14rem;'>"));
                contentControl.Controls.Add(imageButton);
                contentControl.Controls.Add(new LiteralControl("<div class='card-body'><h5 class='card-title'>"));
                contentControl.Controls.Add(new LiteralControl(avgScore + " / " + totalScore));
                contentControl.Controls.Add(new LiteralControl("</h5></div>"));
                contentControl.Controls.Add(new LiteralControl("</div>"));
                contentControl.Controls.Add(new LiteralControl("</div>"));

                if (i % 3 == 0)
                {
                    contentControl.Controls.Add(new LiteralControl("</div>"));
                    contentControl.Controls.Add(new LiteralControl("<div class='row padding-10'>"));
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
            Response.Redirect("StudentWorkDetail.aspx?projectid=" + imageID);
        }

    }
}
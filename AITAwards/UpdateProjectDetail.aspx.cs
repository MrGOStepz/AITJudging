using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class UpdateProjectDetail : System.Web.UI.Page
    {
        private int _projectID;
        private ProjectDetail _projectDetail;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            alertControl.Visible = false;

            //if (AppSession.GetUserProfile() != null)
            //{
            //    UserProfile userProfile = new UserProfile();
            //    userProfile = AppSession.GetUserProfile();

            //    //2 = Judge
            //    if (userProfile.UserLevel != 1)
            //        Response.Redirect("Login.aspx");
            //}
            //else
            //{
            //    Response.Redirect("Login.aspx");
            //}

            if (Request.QueryString["projectID"] != null)
            {
                int projectID = 0;
                try
                {
                    projectID = int.Parse(Request.QueryString["projectID"]);
                    _projectID = projectID;
                }
                catch (Exception)
                {
                    Response.Redirect("UpdateProject.aspx");
                }
            }

            if (!IsPostBack)
            {
                InitializePage();
            }
        }

        private void InitializePage()
        {
            IAdminDatabase adminDB = new AdminDB();

            List<AITEvent> aitEvent = new List<AITEvent>();
            List<AITCategories> lstAitCategories = new List<AITCategories>();
            List<TypeFileDetail> lstTypeFile = new List<TypeFileDetail>();

            aitEvent = adminDB.GetListEvent();
            lstTypeFile = adminDB.GetTypeFileDetail();

            ddlEvent.Items.Clear();
            ddlCategory.Items.Clear();
            ddlTypeID.Items.Clear();

            ProjectDetail projectDetail = new ProjectDetail();
            projectDetail = adminDB.GetProjectDetailByID(_projectID);
            _projectDetail = new ProjectDetail();
            _projectDetail = projectDetail;
            for (int i = 0; i < lstTypeFile.Count; i++)
            {
                ddlTypeID.Items.Insert(i, new ListItem(lstTypeFile[i].TypeFileName, lstTypeFile[i].TypeFileID.ToString()));
            }


            for (int i = 0; i < aitEvent.Count; i++)
            {
                ddlEvent.Items.Insert(i, new ListItem(aitEvent[i].Name, aitEvent[i].EventID.ToString()));
            }

            lstAitCategories = new List<AITCategories>();
            lstAitCategories = adminDB.GetListCategoryByEventID(int.Parse(projectDetail.EventID.ToString()));

            for (int i = 0; i < lstAitCategories.Count; i++)
            {
                ddlCategory.Items.Insert(i, new ListItem(lstAitCategories[i].Name, lstAitCategories[i].CategoryID.ToString()));
            }


            ddlCategory.SelectedValue = projectDetail.CategoryID.ToString();
            ddlTypeID.SelectedValue = projectDetail.TypeFileID.ToString();
            txtTitle.Text = projectDetail.Name;
            txtDescription.Text = projectDetail.Description;

            if (ddlTypeID.SelectedValue == "2")
            {
                txtURL.Text = projectDetail.PathFile;
                fileUpload.Enabled = false;
            }
            else if(ddlTypeID.SelectedValue == "1")
            {
                txtURL.Enabled = false;
                txtURL.Text = projectDetail.PathFile;
                
            }

            

        }

        private void ShowAlert(string text, bool error)
        {
            if (error)
            {
                lbAlert.Text = text;
                alertControl.Visible = true;
                alertControl.Attributes.Remove("class");
                alertControl.Attributes.Add("class", "alert alert-danger");
            }
            else
            {
                lbAlert.Text = text;
                alertControl.Visible = true;
                alertControl.Attributes.Remove("class");
                alertControl.Attributes.Add("class", "alert alert-success");
            }
        }

        protected void Menu_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            switch (linkButton.ID)
            {
                case "lbtnAddEvent":
                    Response.Redirect("AddEvent.aspx");
                    break;
                case "lbtnUpdateEvent":
                    Response.Redirect("UpdateEvent.aspx");
                    break;
                case "lbtnAddCategory":
                    Response.Redirect("AddCategory.aspx");
                    break;
                case "lbtnUpdateCategory":
                    Response.Redirect("UpdateCategory.aspx");
                    break;
                case "lbtnInviteJudge":
                    Response.Redirect("InvitationJudge.aspx");
                    break;
                default:
                    break;
            }
        }

        protected void btnSearchCategories_Click(object sender, EventArgs e)
        {
            IAdminDatabase adminDB = new AdminDB();

            List<AITCategories> lstAitCategories = new List<AITCategories>();
            lstAitCategories = adminDB.GetListCategoryByEventID(int.Parse(ddlEvent.SelectedValue));

            ddlCategory.Items.Clear();
            for (int i = 0; i < lstAitCategories.Count; i++)
            {
                ddlCategory.Items.Insert(i, new ListItem(lstAitCategories[i].Name, lstAitCategories[i].CategoryID.ToString()));
            }
        }

        protected void btnUpdateProject_Click(object sender, EventArgs e)
        {
            IAdminDatabase adminDB = new AdminDB();
            ProjectDetail projectDetail = new ProjectDetail();
            projectDetail = adminDB.GetProjectDetailByID(_projectID);
            _projectDetail = new ProjectDetail();
            _projectDetail = projectDetail;

            if (txtTitle.Text == "")
            {
                alertControl.Visible = true;
                ShowAlert("Please fill up the title name!", true);
                return;
            }

            //AITCategories aitCategories = new AITCategories();

            projectDetail.Name = txtTitle.Text;
            projectDetail.Description = txtDescription.Text;
            projectDetail.CategoryID = int.Parse(ddlCategory.SelectedValue);

            string projectFilename;
            string preProjectFilename;

            if (ddlTypeID.SelectedValue == "1")
            {
                if (fileUpload.HasFile)
                {
                    try
                    {
                        projectFilename = String.Format("{0}_{1}", DateTime.Now.Ticks, fileUpload.FileName);

                        string subPath = "Images/Projects/" + int.Parse(ddlCategory.SelectedValue);
                        bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                        fileUpload.SaveAs(Server.MapPath("Images/Projects/" + int.Parse(ddlCategory.SelectedValue) + "/" + projectFilename));
                        projectDetail.PathFile = projectFilename;
                        projectDetail.TypeFileID = 1;
                    }
                    catch (Exception ex)
                    {
                        ShowAlert("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, true);
                        return;
                    }
                }
                else
                {
                    projectDetail.PathFile = _projectDetail.PathFile;
                    projectDetail.TypeFileID = _projectDetail.TypeFileID;
                }
            
            }
            else if (ddlTypeID.SelectedValue == "2")
            {
                projectDetail.TypeFileID = 2;
                projectDetail.PathFile = txtURL.Text;
            }

            //Preview Image
            if (fileUpload1.HasFile)
            {
                try
                {
                    preProjectFilename = String.Format("{0}_{1}", DateTime.Now.Ticks, fileUpload.FileName);

                    string subPath = "Images/Projects/pre_" + int.Parse(ddlCategory.SelectedValue);
                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                    fileUpload1.SaveAs(Server.MapPath("Images/Projects/pre_" + int.Parse(ddlCategory.SelectedValue) + "/" + preProjectFilename));
                    projectDetail.PreImage = preProjectFilename;
                }
                catch (Exception ex)
                {
                    ShowAlert("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, true);
                    return;
                }
            }
            else
            {
                projectDetail.PreImage = _projectDetail.PreImage;
            }

            if (adminDB.UpdateProject(projectDetail) > 0)
            {
                ShowAlert("Update Project Complete!", false);
            }
            else
            {
                ShowAlert("Something wrong!", true);
            }
        }
    }
}
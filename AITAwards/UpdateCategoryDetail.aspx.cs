using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class UpdateCategoryDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            alertControl.Visible = false;

            if (!IsPostBack)
            {
                if (Request.QueryString["eventID"] != null)
                {
                    int eventID = 0;
                    try
                    {
                        eventID = int.Parse(Request.QueryString["eventID"]);
                    }
                    catch (Exception)
                    {
                        Response.Redirect("../AdminJudge.aspx");
                    }
                    InitializePage();
                }
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


        private void InitializePage()
        {
            IAdminDatabase adminDB = new AdminDB();

            List<AITEvent> aitEvent = new List<AITEvent>();
            List<AITCategories> lstAitCategories = new List<AITCategories>();
            aitEvent = adminDB.GetListEvent();

            ddlUpdateEvent.Items.Clear();
            ddlCategories.Items.Clear();

            for (int i = 0; i < aitEvent.Count; i++)
            {
                ddlUpdateEvent.Items.Insert(i, new ListItem(aitEvent[i].Name, aitEvent[i].EventID.ToString()));
            }
            lstAitCategories = new List<AITCategories>();
            lstAitCategories = adminDB.GetListCategoryByEventID(int.Parse(Request.QueryString["eventID"]));
            ddlUpdateEvent.SelectedValue = lstAitCategories[0].EventID.ToString();
            for (int i = 0; i < lstAitCategories.Count; i++)
            {
                ddlCategories.Items.Insert(i, new ListItem(lstAitCategories[i].Name, lstAitCategories[i].CategoryID.ToString()));
            }

            string ttemp = ddlCategories.SelectedValue;

            if (!IsPostBack)
            {
                ListItem lstItem = new ListItem();
                lstItem = ddlCategories.SelectedItem;
                txtCategory.Text = lstItem.Text;
            }

            if (lstAitCategories.Count > 0)
                txtImagePath.Text = lstAitCategories[0].PathFile;
            else
                txtImagePath.Text = "CategoryImage.png";

            ttemp = ddlCategories.SelectedValue;
        }

        protected void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            if (txtCategory.Text == "")
            {
                alertControl.Visible = true;
                ShowAlert("Please fill up the event name!", true);
                return;
            }

            AITCategories aitCategory = new AITCategories();

            aitCategory.CategoryID = AppSession.GetCategoryID();
            aitCategory.EventID = int.Parse(ddlUpdateEvent.SelectedValue);
            aitCategory.Name = txtCategory.Text;

            if (fileUpload.HasFile)
            {
                try
                {
                    string filename = String.Format("{0}_{1}", DateTime.Now.Ticks, fileUpload.FileName);
                    fileUpload.SaveAs(Server.MapPath("Images/Categories/") + filename);
                    aitCategory.PathFile = filename;
                }
                catch (Exception ex)
                {
                    ShowAlert("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, true);
                    return;
                }
            }
            else
            {
                aitCategory.PathFile = txtImagePath.Text;
            }

            IAdminDatabase adminDB = new AdminDB();
            if (adminDB.UpdateCategory(aitCategory) > 0)
            {
                ShowAlert("Update Category Complete!", false);
            }
            else
            {
                ShowAlert("Something wrong!", true);
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string ttemp = ddlCategories.SelectedValue;

            AITCategories aitCategories = new AITCategories();
            IAdminDatabase adminDB = new AdminDB();
            aitCategories = adminDB.GetCategoryByCategoryID(int.Parse(ddlCategories.SelectedValue));

            ListItem lstItem = new ListItem();
            lstItem = ddlCategories.SelectedItem;
            txtCategory.Text = lstItem.Text;

            ddlUpdateEvent.SelectedValue = aitCategories.EventID.ToString();
            txtImagePath.Text = aitCategories.PathFile;
            AppSession.SetCategoryID(aitCategories.CategoryID);
        }
    }
}
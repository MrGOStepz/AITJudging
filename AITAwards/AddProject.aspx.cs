using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class AddProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            alertControl.Visible = false;

            if (!IsPostBack)
            {
                IAdminDatabase adminDB = new AdminDB();

                List<AITEvent> aitEvent = new List<AITEvent>();
                aitEvent = adminDB.GetListEvent();

                ddlEvent.Items.Clear();
                for (int i = 0; i < aitEvent.Count; i++)
                {
                    ddlEvent.Items.Insert(i, new ListItem(aitEvent[i].Name, aitEvent[i].EventID.ToString()));
                }
            }
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (txtCategoryName.Text == "")
            {
                alertControl.Visible = true;
                ShowAlert("Please fill up the event name!", true);
                return;
            }

            AITCategories aitCategories = new AITCategories();

            aitCategories.Name = txtCategoryName.Text;
            aitCategories.EventID = int.Parse(ddlEvent.SelectedValue);


            if (fileUpload.HasFile)
            {
                try
                {
                    string filename = String.Format("{0}_{1}", DateTime.Now.Ticks, fileUpload.FileName);
                    fileUpload.SaveAs(Server.MapPath("../Images/Categories/") + filename);
                    aitCategories.PathFile = filename;
                }
                catch (Exception ex)
                {
                    ShowAlert("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, true);
                    return;
                }
            }
            else
            {
                aitCategories.PathFile = "CategoryImage.png";
            }

            IAdminDatabase adminDB = new AdminDB();
            if (adminDB.AddCategory(aitCategories) > 0)
            {
                ShowAlert("Add Category Complete!", false);
                txtCategoryName.Text = "";
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
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class ManageJudgeDetail : System.Web.UI.Page
    {
        private List<int> _lstCbID = new List<int>();
        private int _categoryID;
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



            if (ddlCategories.SelectedValue == "")
            {
                IAdminDatabase adminDB = new AdminDB();

                List<AITCategories> lstAitCategories = new List<AITCategories>();

                lstAitCategories = new List<AITCategories>();
                if (Request.QueryString["eventID"] == null)
                {
                    Response.Redirect("ManageJudge.aspx");
                }
                lstAitCategories = adminDB.GetListCategoryByEventID(int.Parse(Request.QueryString["eventID"]));
                ReloadUser(lstAitCategories[0].CategoryID);
            }
            else
                ReloadUser(int.Parse(ddlCategories.SelectedValue));

        }

        protected void Menu_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            switch (linkButton.ID)
            {
                case "lbtnAddEvent":
                    Response.Redirect("AddEvent.aspx");
                    break;
                case "lbtnManageJudge":
                    Response.Redirect("ManageJudge.aspx");
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
                    Response.Redirect("InviteJudge.aspx");
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

            ddlCategories.Items.Clear();

            lstAitCategories = new List<AITCategories>();
            lstAitCategories = adminDB.GetListCategoryByEventID(int.Parse(Request.QueryString["eventID"]));

            for (int i = 0; i < lstAitCategories.Count; i++)
            {
                ddlCategories.Items.Insert(i, new ListItem(lstAitCategories[i].Name, lstAitCategories[i].CategoryID.ToString()));
            }

        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            _categoryID = int.Parse(ddlCategories.SelectedValue);
            ReloadUser(int.Parse(ddlCategories.SelectedValue));
        }

        protected void btnApproveJudge_Click(object sender, EventArgs e)
        {
            List<JudgeCategory> lstJudgeCat = new List<JudgeCategory>();
            JudgeCategory judgeCategory = new JudgeCategory();
            IJudgeDatabase judgeDB = new JudgeDB();

            List<int> lstJudgeID = new List<int>();
            DatabaseHandle dbHandle = new DatabaseHandle();
            lstJudgeID = dbHandle.GetListJudgeID(int.Parse(ddlCategories.SelectedValue));
            for (int i = 0; i < lstJudgeID.Count; i++)
            {
                CheckBox checkbox = (CheckBox)tbodyControl.FindControl("cb" + lstJudgeID[i]);
                if (checkbox.Checked == true)
                {
                    judgeCategory = new JudgeCategory();
                    judgeCategory.CategoryID = int.Parse(ddlCategories.SelectedValue);
                    judgeCategory.UserID = lstJudgeID[i];
                    lstJudgeCat.Add(judgeCategory);
                }
            }

            if (judgeDB.AddJudgeByCategory(lstJudgeCat) > 0)
            {
                ShowAlert("Add Judge Complete!", false);
            }
            else
            {
                ShowAlert("Something Error!", true);
            }

            ReloadUser(_categoryID);
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

        private void ReloadUser(int categoryID)
        {
            DatabaseHandle dbHandle = new DatabaseHandle();

            List<UserProfile> lstUserProfile = new List<UserProfile>();
            string json = dbHandle.GetListJudge(int.Parse(ddlCategories.SelectedValue));
            lstUserProfile = JsonConvert.DeserializeObject<List<UserProfile>>(json);
            tbodyControl.Controls.Clear();
            _lstCbID = new List<int>();
            for (int i = 0; i < lstUserProfile.Count; i++)
            {
                tbodyControl.Controls.Add(new LiteralControl("<tr>"));
                tbodyControl.Controls.Add(new LiteralControl("<td>"));
                tbodyControl.Controls.Add(new LiteralControl(lstUserProfile[i].UserID.ToString()));
                tbodyControl.Controls.Add(new LiteralControl("</td>"));
                tbodyControl.Controls.Add(new LiteralControl("<td>"));
                tbodyControl.Controls.Add(new LiteralControl(lstUserProfile[i].UserName.ToString()));
                tbodyControl.Controls.Add(new LiteralControl("</td>"));
                tbodyControl.Controls.Add(new LiteralControl("<td>"));
                tbodyControl.Controls.Add(new LiteralControl(lstUserProfile[i].Email.ToString()));
                tbodyControl.Controls.Add(new LiteralControl("</td>"));
                tbodyControl.Controls.Add(new LiteralControl("<td>"));
                CheckBox checkBox = new CheckBox();
                checkBox.ID = "cb" + lstUserProfile[i].UserID;

                checkBox.Checked = false;

                _lstCbID.Add(lstUserProfile[i].UserID);
                tbodyControl.Controls.Add(checkBox);
                tbodyControl.Controls.Add(new LiteralControl("</td>"));
                tbodyControl.Controls.Add(new LiteralControl("</tr>"));

            }
        }

    }
}
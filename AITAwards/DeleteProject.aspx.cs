using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class DeleteProject : System.Web.UI.Page
    {
        private List<int> _lstBtnID = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            alertControl.Visible = false;
            //if (AppSession.GetUserProfile() != null)
            //{
            //    UserProfile userProfile = new UserProfile();
            //    userProfile = AppSession.GetUserProfile();

            //    //1 = Admin
            //    if (userProfile.UserLevel != 1)
            //        Response.Redirect("Login.aspx");

            //    InitializPage();

            //}
            //else
            //{
            //    Response.Redirect("Login.aspx");
            //}


            InitializPage();

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

        private void InitializPage()
        {
            DatabaseHandle dbHandle = new DatabaseHandle();
            IAdminDatabase adminDB = new AdminDB();

            List<ProjectDetail> lstProjectDetail = new List<ProjectDetail>();
            string json = adminDB.GetListOfProject();
            lstProjectDetail = JsonConvert.DeserializeObject<List<ProjectDetail>>(json);
            tbodyControl.Controls.Clear();
            _lstBtnID = new List<int>();
            for (int i = 0; i < lstProjectDetail.Count; i++)
            {
                tbodyControl.Controls.Add(new LiteralControl("<tr>"));
                tbodyControl.Controls.Add(new LiteralControl("<td>"));
                tbodyControl.Controls.Add(new LiteralControl(lstProjectDetail[i].ProjectID.ToString()));
                tbodyControl.Controls.Add(new LiteralControl("</td>"));
                tbodyControl.Controls.Add(new LiteralControl("<td>"));
                tbodyControl.Controls.Add(new LiteralControl(lstProjectDetail[i].Name.ToString()));
                tbodyControl.Controls.Add(new LiteralControl("</td>"));
                tbodyControl.Controls.Add(new LiteralControl("<td>"));
                tbodyControl.Controls.Add(new LiteralControl(lstProjectDetail[i].Description.ToString()));
                tbodyControl.Controls.Add(new LiteralControl("</td>"));
                tbodyControl.Controls.Add(new LiteralControl("<td>"));
                tbodyControl.Controls.Add(new LiteralControl(lstProjectDetail[i].CategoryName.ToString()));
                tbodyControl.Controls.Add(new LiteralControl("</td>"));
                tbodyControl.Controls.Add(new LiteralControl("<td>"));

                Button button = new Button();
                button.ID = "btn" + lstProjectDetail[i].ProjectID;
                button.Text = "Delete";
                button.CssClass = "btn btn-danger btn-block";
                button.Click += Button_Click;

                _lstBtnID.Add(lstProjectDetail[i].ProjectID);
                tbodyControl.Controls.Add(button);
                tbodyControl.Controls.Add(new LiteralControl("</td>"));
                tbodyControl.Controls.Add(new LiteralControl("</tr>"));
            }

        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string projectID = button.ID.Substring(3);

            IAdminDatabase adminDB = new AdminDB();
            if(adminDB.DeleteProject(int.Parse(projectID)) > 0)
            {
                ShowAlert("Delete Project Complete!", false);
                InitializPage();
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
    }
}
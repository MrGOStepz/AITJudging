using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class AdminJudge : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(AppSession.GetMenuState() != null)
            {
                LoadMenuControl(AppSession.GetMenuState());
            }
            else
            {
                AppSession.SetMenuState("dashboard");
                LoadMenuControl(AppSession.GetMenuState());
            }
        }

        protected void Menu_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            switch (linkButton.ID)
            {
                case "lbtnAddEvent":
                    AppSession.SetMenuState("lbtnAddEvent");
                    LoadMenuControl(AppSession.GetMenuState());
                    break;
                case "lbtnUpdateEvent":
                    AppSession.SetMenuState("lbtnUpdateEvent");
                    AppSession.SetUpdateEventState(null);
                    AppSession.SetUpdateEventID(-1);
                    AppSession.SetBreadCrumbState("UpdateEventControl");
                    LoadMenuControl(AppSession.GetMenuState());
                    break;
                case "lbtnAddCategory":
                    AppSession.SetMenuState("lbtnAddCategory");
                    LoadMenuControl(AppSession.GetMenuState());
                    break;
                case "lbtnUpdateCategory":
                    AppSession.SetMenuState("lbtnUpdateCategory");
                    LoadMenuControl(AppSession.GetMenuState());
                    break;
                case "lbtnInviteJudge":
                    AppSession.SetMenuState("lbtnInviteJudge");
                    LoadMenuControl(AppSession.GetMenuState());
                    break;

                default:
                    break;
            }
        }

        private void LoadMenuControl(string controlID)
        {
            phControl.Controls.Clear();

            switch (controlID)
            {
                case "lbtnAddEvent":   
                    AddEventControl addEventControl = (AddEventControl)LoadControl("UserControl/AddEventControl.ascx");
                    addEventControl.ID = "addEventControl";
                    phControl.Controls.Add(addEventControl);
                    break;
                case "lbtnUpdateEvent":
                    UpdateEventControl updateEventControl = (UpdateEventControl)LoadControl("UserControl/UpdateEventControl.ascx");
                    updateEventControl.ID = "updateEventControl";
                    phControl.Controls.Add(updateEventControl);
                    break;
                case "lbtnAddCategory":
                    AddCategoryControl addCategoryControl = (AddCategoryControl)LoadControl("UserControl/AddCategoryControl.ascx");
                    addCategoryControl.ID = "addCategoryControl";
                    phControl.Controls.Add(addCategoryControl);
                    break;
                case "lbtnUpdateCategory":
                    UpdateCategoryControl updateCategoryControl = (UpdateCategoryControl)LoadControl("UserControl/UpdateCategoryControl.ascx");
                    updateCategoryControl.ID = "updateCategoryControl";
                    phControl.Controls.Add(updateCategoryControl);
                    break;
                case "lbtnInviteJudge":
                    InviteJudgeControl inviteJudgeControl = (InviteJudgeControl)LoadControl("UserControl/InviteJudgeControl.ascx");
                    inviteJudgeControl.ID = "updateEventControl";
                    phControl.Controls.Add(inviteJudgeControl);
                    break;
                default:
                    break;
            }
        }

    }
}
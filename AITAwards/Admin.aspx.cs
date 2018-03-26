using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class Admin : System.Web.UI.Page
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
                default:
                    break;
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class BreadCrumbControl : System.Web.UI.UserControl
    {
        public string State;

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (State)
            {
                case "UpdateEventControl":
                    lbBCTitile.Text = "Event";
                    break;
                case "UpdateEventDetailControl":
                    UpdateEventDetailControl();
                    break;
                default:
                    break;
            }
        }

        private void UpdateEventDetailControl()
        {
            LinkButton linkButton = new LinkButton();
            linkButton.ID = "updateEventDetail";
            linkButton.Text = "Event";
            linkButton.Click += LinkButton_Click;

            breadcrumbControl.Controls.Clear();
            lbBCTitile.Text = "Update Event";

            breadcrumbControl.Controls.Add(new LiteralControl("<li>"));
            breadcrumbControl.Controls.Add(linkButton);
            breadcrumbControl.Controls.Add(new LiteralControl("</li>"));
        }

        private void LinkButton_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            switch (linkButton.Text)
            {
                case "Event":
                    AppSession.SetUpdateEventState(null);
                    break;
                default:
                    break;
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class UpdateCategoryControl : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (AppSession.GetUpdateCategoryState() == "EditCategory")
            {
                divControl.Controls.Clear();

                PlaceHolder phControl = new PlaceHolder();
                UpdateCategoryDetailControl updateCategoryDetailControl = (UpdateCategoryDetailControl)LoadControl("UpdateCategoryDetailControl.ascx");
                updateCategoryDetailControl.ID = "updateDetailControl";
                updateCategoryDetailControl.EventID = AppSession.GetUpdateEventID();
                phControl.Controls.Add(updateCategoryDetailControl);
                divControl.Controls.Add(phControl);
                return;
            }

            InitializeBreadCrumb();
            InitializePage();
        }

        private void InitializeBreadCrumb()
        {
            phCreadCrumb.Controls.Clear();
            BreadCrumbControl breadCrumbControl = (BreadCrumbControl)LoadControl("BreadCrumbControl.ascx");
            breadCrumbControl.ID = "breadCrumbControl";
            breadCrumbControl.State = AppSession.GetBreadCrumbState();
            phCreadCrumb.Controls.Add(breadCrumbControl);
        }

        private void InitializePage()
        {
            List<AITEvent> lstAitEvent = new List<AITEvent>();
            AITEvent aitEvent = new AITEvent();

            IAdminDatabase adminDB = new AdminDB();
            lstAitEvent = adminDB.GetListEvent();

            for (int i = 0; i < lstAitEvent.Count; i++)
            {
                ImageButton image = new ImageButton();
                image.ID = "img" + lstAitEvent[i].EventID;
                image.ImageUrl = "../Images/Event/" + lstAitEvent[i].PathFile;
                image.CssClass = "image-background";

                image.Click += Image_Click;

                divControl.Controls.Add(new LiteralControl("<div class='col-lg-3'>"));
                divControl.Controls.Add(new LiteralControl("<div class='social-box facebook'>"));
                divControl.Controls.Add(image);
                divControl.Controls.Add(new LiteralControl("</div>"));
                divControl.Controls.Add(new LiteralControl("</div>"));
            }
        }

        private void Image_Click(object sender, ImageClickEventArgs e)
        {
            int eventID;
            ImageButton imageButton = sender as ImageButton;
            try
            {
                eventID = int.Parse(imageButton.ID.Substring(3));
            }
            catch (Exception)
            {
                eventID = -1;
            }


            divControl.Controls.Clear();

            PlaceHolder phControl = new PlaceHolder();
            UpdateCategoryDetailControl categoryDetailControl = (UpdateCategoryDetailControl)LoadControl("UpdateCategoryDetailControl.ascx");
            categoryDetailControl.ID = "categoryDetailControl";
            categoryDetailControl.EventID = eventID;
            AppSession.SetUpdateEventID(eventID);
            AppSession.SetUpdateCategoryState("EditCategory");

            phControl.Controls.Add(categoryDetailControl);
            divControl.Controls.Add(phControl);

            AppSession.SetBreadCrumbState("UpdateEventDetailControl");

        }

    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class UpdateCategoryDetailControl : System.Web.UI.UserControl
    {
        public int EventID;
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializePage();
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
            lstAitCategories = adminDB.GetListCategoryByEventID(EventID);
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
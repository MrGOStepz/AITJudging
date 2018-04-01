using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class UpdateEventDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            alertControl.Visible = false;

            if (!IsPostBack)
            {
                if (Request.QueryString["eventID"] != null)
                {
                    int EventID = 0;
                    try
                    {
                        EventID = int.Parse(Request.QueryString["eventID"]);
                    }
                    catch (Exception)
                    {
                        Response.Redirect("../AdminJudge.aspx");
                    }

                    IAdminDatabase adminDB = new AdminDB();
                    AITEvent aitEvent = new AITEvent();
                    aitEvent = adminDB.GetEventDetailByEventID(EventID);
                    if (aitEvent != null)
                    {
                        txtEventName.Text = aitEvent.Name;
                        chkActive.Checked = (aitEvent.IsActive == 1) ? true : false;
                        txtAddress.Text = aitEvent.Address;

                        DateTime tempDatetime = new DateTime();
                        tempDatetime = aitEvent.StartAt;
                        txtStartDate.Text = tempDatetime.ToLongDateString();

                        tempDatetime = new DateTime();
                        tempDatetime = aitEvent.EndAt;
                        txtEndDate.Text = tempDatetime.ToLongDateString();

                        txtImagePath.Text = aitEvent.PathFile;
                    }

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

        protected void btnUpdateEvent_Click(object sender, EventArgs e)
        {
            if (txtEventName.Text == "")
            {
                alertControl.Visible = true;
                ShowAlert("Please fill up the event name!", true);
                return;
            }

            AITEvent aitEvent = new AITEvent();

            aitEvent.EventID = int.Parse(Request.QueryString["eventID"]);
            aitEvent.Name = txtEventName.Text;
            aitEvent.StartAt = (calStart.SelectedDate > DateTime.MinValue) ? calStart.SelectedDate : Convert.ToDateTime(txtStartDate.Text);
            aitEvent.EndAt = (calEnd.SelectedDate > DateTime.MinValue) ? calEnd.SelectedDate : Convert.ToDateTime(txtEndDate.Text);
            aitEvent.Address = txtAddress.Text;
            aitEvent.IsActive = (chkActive.Checked == true) ? 1 : 0;


            if (fileUpload.HasFile)
            {
                try
                {
                    string filename = String.Format("{0}_{1}", DateTime.Now.Ticks, fileUpload.FileName);
                    fileUpload.SaveAs(Server.MapPath("Images/Event/") + filename);
                    aitEvent.PathFile = filename;
                }
                catch (Exception ex)
                {
                    ShowAlert("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, true);
                    return;
                }
            }
            else
            {
                aitEvent.PathFile = txtImagePath.Text;
            }

            IAdminDatabase adminDB = new AdminDB();
            if (adminDB.UpdateEvent(aitEvent) > 0)
            {
                ShowAlert("Update Event Complete!", false);
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

        protected void calStart_SelectionChanged(object sender, EventArgs e)
        {
            txtStartDate.Text = calStart.SelectedDate.ToLongDateString();
        }

        protected void calEnd_SelectionChanged(object sender, EventArgs e)
        {
            txtEndDate.Text = calEnd.SelectedDate.ToLongDateString();
        }

    }
}
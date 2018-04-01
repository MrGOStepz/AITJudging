using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class InviteJudge : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            if(txtEmail.Text == "")
            {
                ShowAlert("Fill up email!", false);
                return;
            }
            try
            {
                SmtpClient smtpClient = new SmtpClient("mail.wealthyworld.com.au", 25);

                smtpClient.Credentials = new System.Net.NetworkCredential("judging@wealthyworld.com.au", "judging!1234");
                MailMessage mail = new MailMessage("judging@wealthyworld.com.au", txtEmail.Text);

                //Setting From , To and CC
                //mail.From = new MailAddress("judging@wealthyworld.com.au");
                //mail.To.Add(new MailAddress("gokungzzz@hotmail.com"));

                string GUID = Guid.NewGuid().ToString();
                DatabaseHandle dbHanddle = new DatabaseHandle();
                if (dbHanddle.AddNewUser(GUID, Guid.NewGuid().ToString(), txtEmail.Text, GUID) > 0)
                {

                    mail.Subject = "Invitation AIT Judge";
                    mail.Body = "http://judging.wealthyworld.com.au/Register.aspx?key=" + GUID;

                    smtpClient.Send(mail);

                    ShowAlert("Send Email complete!", false);
                }
                else
                {
                    ShowAlert("Something wrong!", true);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine();
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace AITAwards
{
    public partial class InviteJudgeControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    MailMessage mailMsg = new MailMessage();

            //    // To
            //    mailMsg.To.Add(new MailAddress("5612@ait.nsw.edu.au", "5612"));

            //    // From
            //    mailMsg.From = new MailAddress("gokungzzz@sendgrid.com", "GOkung");

            //    // Subject and multipart/alternative Body
            //    mailMsg.Subject = "subject";


            //    // Init SmtpClient and send
            //    SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
            //    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("gokungzzz", "G4856162651O");
            //    smtpClient.Credentials = credentials;

            //    smtpClient.Send(mailMsg);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            try
            {

                SmtpClient smtpClient = new SmtpClient("mail.wealthyworld.com.au", 25);

                smtpClient.Credentials = new System.Net.NetworkCredential("judging@wealthyworld.com.au", "judging!1234");
                //smtpClient.UseDefaultCredentials = false;
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.EnableSsl = false;
                MailMessage mail = new MailMessage("judging@wealthyworld.com.au", "5612@ait.nsw.edu.au");

                //Setting From , To and CC
                //mail.From = new MailAddress("judging@wealthyworld.com.au");
                //mail.To.Add(new MailAddress("gokungzzz@hotmail.com"));
                mail.Subject = "Test Subject";
                mail.Body = "Body Test";

                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
        }
    }
}
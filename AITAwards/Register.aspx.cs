using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {

            if (txtUserName.Text == "")
            {
                lbAlerts.Visible = true;
                lbAlerts.Text = "Please fill up username";
            }
            else if (txtPassword.Text == "")
            {
                lbAlerts.Visible = true;
                lbAlerts.Text = "Please fill up password";
            }
            else if (txtCMPassword.Text == "")
            {
                lbAlerts.Visible = true;
                lbAlerts.Text = "Please fill up Confirm password";
            }
            else if (txtPassword.Text != txtCMPassword.Text)
            {
                lbAlerts.Visible = true;
                lbAlerts.Text = "Password doesn't match";
            }
            else
            {
                //if (wb.RegisterUser(txtUserName.Text, txtPassword.Text, txtEmail.Text) > 0)
                //{

                //    UserLoginProfile userLoginProfile = new UserLoginProfile();
                //    int userID = wb.GetUserIDbyUserName(txtUserName.Text);
                //    userLoginProfile.UserID = userID;
                //    userLoginProfile.UserName = txtUserName.Text;

                //    txtUserName.Text = "";
                //    lbAlerts.Visible = false;

                //    Response.Redirect("Catagories.aspx");

                //}
                //else
                //{
                //    lbAlerts.Visible = true;
                //    lbAlerts.Text = "There is something wrong.";
                //}


            }
        }
    }
}
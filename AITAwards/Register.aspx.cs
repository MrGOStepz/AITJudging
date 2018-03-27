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
            alertControl.Visible = false;
            if (Request.QueryString["key"] != null)
            {
                string GUID = Request.QueryString["key"];
                DatabaseHandle dbHandle = new DatabaseHandle();
  
                if(dbHandle.CheckUserKey(GUID) < 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Key is incorrect!')", true);
                    Response.Redirect("Index.aspx");
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Key is incorrect!')", true);
                Response.Redirect("Index.aspx");
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {

            if (txtUserName.Text == "")
            {
                ShowAlert("Please fill up username", true);
            }
            else if (txtEmail.Text == "")
            {
                ShowAlert("Please fill up Email", true);
            }
            else if (txtPassword.Text == "")
            {
                ShowAlert("Please fill up password", true);
            }
            else if (txtConfirmPassword.Text == "")
            {
                ShowAlert("Please fill up Confirm password", true);
            }
            else if (txtPassword.Text != txtConfirmPassword.Text)
            {
                ShowAlert("Password doesn't match", true);
            }
            else
            {
                DatabaseHandle dbHandle = new DatabaseHandle();
                if (dbHandle.AddNewUser(txtUserName.Text, txtPassword.Text, txtEmail.Text) > 0)
                    ShowAlert("Register complete!",false);
                else
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

    }
}
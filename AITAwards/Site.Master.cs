using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AppSession.GetUserProfile() != null)
            {
                btnLogin.Text = "Logout";
            }
            else
            {
                btnLogin.Text = "Login";
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (AppSession.GetUserProfile() != null)
            {
                Session.Clear();
                Response.Redirect("Index.aspx");
                
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            
        }
    }
}
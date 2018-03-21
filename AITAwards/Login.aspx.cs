﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (AppSession.GetUserProfile() != null)
                {
                    UserProfile userProfile = new UserProfile();
                    userProfile = AppSession.GetUserProfile();
                    if (userProfile.UserLevel != 1)
                        Response.Redirect("Categories.aspx");
                    else
                        Response.Redirect("Admin.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            DatabaseHandle dbHandle = new DatabaseHandle();
            UserProfile userProfile = new UserProfile();
            MD5 md5 = new MD5();

            string password = md5.EncodePassword(txtPassword.Text);

            userProfile = dbHandle.LoginbyUserAndPassword(txtUserName.Text, password);

            if(userProfile != null)
            {
                AppSession.SetUserProfile(userProfile);
                AppSession.SetUserID(userProfile.UserID);

                if (userProfile.UserLevel > 0)
                {
                    if(userProfile.UserLevel == 1)
                        Response.Redirect("Admin.aspx");
                    else if(userProfile.UserLevel == 2)
                        Response.Redirect("Categories.aspx");
                }
                else
                {
                    lbAlerts.Text = "User or Password is incorrect!";
                    lbAlerts.Visible = true;
                }

            }
            else
            {
                lbAlerts.Text = "Something wrong!";
                lbAlerts.Visible = true;
            }
        }
        
    }
}
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="AITAwards.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
    <div class="col justify-content-center">
          <div id="login-overlay" class="modal-dialog">
        <div class="modal-content">
            <div class="modal-body">
                <div class="row"> 
                    <div class="col-xl-12">
                        <p style="text-align: center;" class="h4">Login</p>
                        <div class="jumbotron login-padding">
                            <div class="form-group">
                                <label for="username" class="control-label">Username</label>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" title="Please enter your Username"></asp:TextBox>                                      
                            </div>

                            <div class="form-group">
                                <label for="password" class="control-label">Password</label>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" title="Please enter your password" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="password" class="control-label">Confirm Password</label>
                                <asp:TextBox ID="txtCMPassword" runat="server" CssClass="form-control" title="Please enter your Confirm password" TextMode="Password"></asp:TextBox>
                            </div>

                            <asp:Button ID="btnRegister" runat="server" Text="Register" class="btn btn-info btn-block" OnClick="btnRegister_Click"/> <br />
                            <asp:Label ID="lbAlerts" runat="server" CssClass="alert alert-danger" Visible="False"></asp:Label>
                       </div>
                   </div>
               </div>
           </div>
       </div>
    </div>
    </div>
        </div>

</asp:Content>

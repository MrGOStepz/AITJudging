<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AITAwards.Login" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<style type="text/css">
body {  
    background-image:url("Images/AITINKT.png");
    /* Full height */
    height: 100%;
        background-position: center;
    background-size: cover;
    background-repeat: no-repeat;
    background-attachment: fixed;

}


</style>

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

                            <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn btn-secondary btn-block" OnClick="btnLogin_Click"/> <br />
                             <div class="alert alert-success" role="alert" runat="server" id="alertControl" visible="false">
                                  <asp:Label ID="lbAlert" runat="server" Text=""></asp:Label>
                                </div>
                       </div>
                   </div>
               </div>
           </div>
       </div>
    </div>
    </div>
        </div>
</asp:Content>

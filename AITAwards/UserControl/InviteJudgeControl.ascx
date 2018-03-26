<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InviteJudgeControl.ascx.cs" Inherits="AITAwards.InviteJudgeControl" %>

<div class="card">
  <div class="card-header"><strong>Create Event</strong></div>
  <div class="card-body card-block">
    <div class="form-group"><label for="email" class=" form-control-label">Email</label><asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox></div>

    <asp:Button ID="btnSendEmail" runat="server" Text="Send Invate" CssClass="btn btn-primary btn-lg btn-block" OnClick="btnSendEmail_Click"/>
            <div class="alert alert-success" role="alert" runat="server" id="alertControl" visible="false">
          <asp:Label ID="lbAlert" runat="server" Text=""></asp:Label>
        </div>

  </div>
</div>

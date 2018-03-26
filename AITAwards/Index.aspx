<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="AITAwards.Index" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<style type="text/css">
body { 
    background-image:url("Images/AITINK.jpg");
    background-size: cover;
    background-repeat: repeat;

}
</style>
    
    <div class="wrapper-button">
        <asp:Button ID="btnJudging" runat="server" Text="Judging" CssClass=" button-center btn btn-primary" OnClick="btnJudging_Click"/>
    </div>

</asp:Content>

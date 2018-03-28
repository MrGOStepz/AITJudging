<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="AITAwards.Index" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<style type="text/css">
body {  
    background-image:url("Images/AITINK.jpg");
    /* Full height */
    height: 100%;
        background-position: center;
    background-size: cover;
    background-repeat: no-repeat;
    background-attachment: fixed;
}

</style>
    
        <div class="row" >
    <div class="col justify-content-center">
          <div id="login-overlay" class="modal-dialog" style="margin-top:50vh;">
        <div class="modal-content">
            <div class="modal-body">
                <div class="row"> 
                    <div class="col-xl-12">
                    <asp:Button ID="btnJudging" runat="server" Text="Judging" CssClass="btn btn-secondary btn-lg btn-block" OnClick="btnJudging_Click"/>
                    <asp:Button ID="btnCategory" runat="server" Text="Category" CssClass="btn btn-secondary btn-lg btn-block" OnClick="btnCategory_Click"/>

                   </div>
               </div>
           </div>
       </div>
    </div>
    </div>
        </div>


</asp:Content>

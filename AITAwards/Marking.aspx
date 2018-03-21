<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Marking.aspx.cs" Inherits="AITAwards.Marking" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col text-center">
            
        <asp:Image ID="imgProject" runat="server" class="rounded"/>
    </div>
            </div>

            <asp:PlaceHolder ID="phControl" runat="server"></asp:PlaceHolder>

        <div class="row">
<%--    <div class="justify-content-between"">
        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-success" OnClick="btnBack_Click"/>
        <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn btn-success" OnClick="btnNext_Click"/>
        </div>
    </div>--%>

        </div>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rubric.aspx.cs" Inherits="AITAwards.Rubric" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col text-center">
            
        <asp:Image ID="imgProject" runat="server" class="rounded"/>
    </div>
            </div>

    <div class="row">
        <table class="table table-bordered">
          <thead class="thead-light">
            <tr>
              <th scope="col">Criteria</th>
              <th scope="col">Ratings</th>
              <th scope="col">Points</th>
            </tr>
          </thead>
          <tbody id="rubricTB" runat="server">


          </tbody>
        </table>
    </div>

    <div class="text-center">
        <asp:Button ID="btnMark" runat="server" Text="Mark" CssClass="btn btn-warning" OnClick="btnMark_Click"/>
        </div>

        </div>
</asp:Content>

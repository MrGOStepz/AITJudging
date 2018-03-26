﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rubric.aspx.cs" Inherits="AITAwards.Rubric" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row padding-10">
            <div class="col text-center">
                <asp:Image ID="imgProject" runat="server" class="rounded"/>
            </div>
        <%--</div>

            
        <div class="row padding-10">--%>
            <%--if Img = land set col--%>
        <asp:Label ID="lbSetCol" runat="server" Text=""></asp:Label>

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

    <%--</div>--%>
            <%--if Img = land delete div--%>
    <asp:Label ID="lbCDiv" runat="server" Text=""></asp:Label>

        <div class="row padding-10">
        
        <div class="col-md-6 offset-md-3" style="display:flex;width:100%;">
        <asp:Button ID="btnProject" runat="server" Text="Back" CssClass="btn btn-warning" OnClick="btnProject_Click" style="flex:1;"/>
        <asp:Button ID="btnMark" runat="server" Text="Mark" CssClass="btn btn-warning" OnClick="btnMark_Click" style="flex:1;"/>
            </div>
    </div>

            <%--if Img = land add div--%>
        <asp:Label ID="lbCDiv2" runat="server" Text=""></asp:Label>
     </div>

</asp:Content>

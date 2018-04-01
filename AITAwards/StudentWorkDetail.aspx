<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentWorkDetail.aspx.cs" Inherits="AITAwards.StudentWorkDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row padding-10">
            <div class="col text-center">
                                <asp:Image ID="imgProject" runat="server" class="rounded" Visible="true"/>
                <asp:Literal ID="lrURL" runat="server" Visible="false"></asp:Literal>
            </div>

        <%--</div>

            
        <div class="row padding-10">--%>
            <%--if Img = land set col--%>


        <asp:Label ID="lbSetCol" runat="server" Text=""></asp:Label>
                       <div class="row padding-10" style="width:80%;">
                <div class="col text-center">

                        <div class="form-horizontal">
<%--                          <div class="row form-group">
                            <div class="col col-md-3"><label for="hf-name" class=" form-control-label">Name</label></div>
                            <div class="col-12 col-md-9">
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                          </div>
                          <div class="row form-group">
                            <div class="col col-md-3"><label for="hf-name" class=" form-control-label">Description</label></div>
                            <div class="col-12 col-md-9">
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                          </div>--%>

                           <div class="row form-group">
                            <div class="col col-md-3"><label for="hf-name" class=" form-control-label">Score</label></div>
                            <div class="col-12 col-md-9">
                                <asp:TextBox ID="txtScore" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                          </div>
                        </div>

                </div>
                </div>
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

            <%--if Img = land add div--%>
        <asp:Label ID="lbCDiv2" runat="server" Text=""></asp:Label>
     </div>
</asp:Content>

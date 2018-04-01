<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="AITAwards.Result" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row padding-10">
            <div class="col text-center">
            
                        <asp:Image ID="imgProject" runat="server" class="rounded" Visible="true"/>
                <asp:Literal ID="lrURL" runat="server" Visible="false"></asp:Literal>
    </div>
            </div>

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
                                <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                          </div>
                        </div>

                </div>
                </div>


    <div class="row padding-10">
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

<%--    <div class="text-center">
        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-warning" OnClick="btnBack_Click"/>
        <asp:Button ID="btnConfirm" runat="server" Text="Mark" CssClass="btn btn-warning" OnClick="btnConfirm_Click"/>
        </div>--%>

                <div class="row padding-10">
        
        <div class="col-md-6 offset-md-3" style="display:flex;width:100%;">
        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-warning" OnClick="btnBack_Click" style="flex:1;"/>
        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="btn btn-warning" OnClick="btnConfirm_Click" style="flex:1;"/>
            </div>
    </div>

        </div>


</asp:Content>

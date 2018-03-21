<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuestionControl.ascx.cs" Inherits="AITAwards.QuestionControl" %>
<div class="row">
            <table class="table table-bordered">
          <thead class="thead-light">
              <tr>
                  <asp:Label ID="lbHeader" runat="server" Text=""></asp:Label>
            </tr>
          </thead>
          <tbody id="rubricTB" runat="server">
              

          </tbody>
        </table>
    
 </div>

<div class="row">
    <div class="col-md-6 offset-md-3">
    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>

        <div class="justify-content-between"">
        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-success" OnClick="btnBack_Click"/>
        <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn btn-success" OnClick="btnNext_Click"/>
        </div>
    </div>




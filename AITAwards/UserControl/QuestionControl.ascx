<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuestionControl.ascx.cs" Inherits="AITAwards.QuestionControl" %>
<div class="row padding-10">
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

<div class="row padding-10">
    <div class="col-md-6 offset-md-3">
        Comment
    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
        </div>
    </div>

    <div class="row padding-10">
        
        <div class="col-md-6 offset-md-3" style="display:flex;width:100%;">
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-success" OnClick="btnBack_Click" style="flex:1;"/>
            <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn btn-success" OnClick="btnNext_Click" style="flex:1;"/>
            </div>
    </div>



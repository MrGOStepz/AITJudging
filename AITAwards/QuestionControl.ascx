<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuestionControl.ascx.cs" Inherits="AITAwards.QuestionControl" %>

            <div class="col text-center">
            
                        <asp:Image ID="imgProject" runat="server" class="rounded" Visible="true"/>
                                <div class="video-container" id="vdoCon" runat="server" Visible="false">
                <asp:Literal ID="lrURL" runat="server" Visible="false"></asp:Literal>
                    </div>
    </div>

<asp:Label ID="lbSetCol" runat="server" Text=""></asp:Label>

            <table class="table table-bordered" style="width:100%">
          <thead class="thead-light">
              <tr style='font-size:13px;'>
                  <asp:Label ID="lbHeader" runat="server" Text=""></asp:Label>
            </tr>
          </thead>
          <tbody id="rubricTB" runat="server">
              

          </tbody>
        </table>
    <asp:Label ID="lbCDiv" runat="server" Text=""></asp:Label>


<div class="row padding-10">
    <div class="col">
        Comment
    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
        </div>
    </div>

    <div class="row padding-10">
        
        <div class="col-md-6 offset-md-3" style="display:flex;width:100%;">
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-success" OnClick="btnBack_Click" style="flex:1;"/>
            <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn btn-success" OnClick="btnNext_Click" style="flex:1;"/>

<%--            <button type="button" onclick="return BackClick();"  style="flex:1;" class="btn btn-success">Back</button>
            <button type="button" onclick="return NextClick();"  style="flex:1;" class="btn btn-success">Next</button>--%>
            </div>
    </div>
<asp:Label ID="lbCDiv2" runat="server" Text=""></asp:Label>



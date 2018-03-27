<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateEventDetailControl.ascx.cs" Inherits="AITAwards.UpdateEventDetailControl" %>

<div class="card">
  <div class="card-header"><strong>Update Event</strong></div>
  <div class="card-body card-block">
    <div class="form-group"><label for="name" class=" form-control-label">Name</label><asp:TextBox ID="txtEventName" runat="server" CssClass="form-control" placeholder="Event name"></asp:TextBox></div>
    
    <div class="row form-group">
      <div class="col-6">
        <div class="form-group"><label for="startDate" class=" form-control-label">Start Date</label><asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" placeholder="Start Date" Enabled="False"></asp:TextBox><asp:Calendar ID="calStart" runat="server" OnSelectionChanged="calStart_SelectionChanged"></asp:Calendar>
</div>
      </div>
      <div class="col-6">
        <div class="form-group"><label for="endDate" class=" form-control-label">End Date</label><asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" placeholder="End Date" Enabled="False"></asp:TextBox><asp:Calendar ID="calEnd" runat="server" OnSelectionChanged="calEnd_SelectionChanged"></asp:Calendar></div>
      </div>
    </div>
    <div class="form-group"><label for="address" class=" form-control-label">Address</label><asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Address"></asp:TextBox></div>
      <div class="form-group">
          <label for="fileUpload" class=" form-control-label">File Upload</label>
              <asp:FileUpload ID="fileUpload" runat="server" accept=".png,.jpg,.jpeg,.gif,.tif"/>
          <div class="form-group"><label for="name" class=" form-control-label">Image name on server</label><asp:TextBox ID="txtImagePath" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox></div>
      </div>

     <div class="form-group">
          <label for="active" class="form-control-label">Active</label>
         <asp:CheckBox ID="chkActive" runat="server"/>
      </div>

    <asp:Button ID="btnUpdateEvent" runat="server" Text="Update Event" CssClass="btn btn-primary btn-lg btn-block" OnClick="btnUpdateEvent_Click"/>
            <div class="alert alert-success" role="alert" runat="server" id="alertControl" visible="false">
          <asp:Label ID="lbAlert" runat="server" Text=""></asp:Label>
        </div>

  </div>
</div>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateCategoryDetailControl.ascx.cs" Inherits="AITAwards.UpdateCategoryDetailControl" %>

<div class="breadcrumbs">
            <div class="col-sm-4">
                <div class="page-header float-left">
                    <div class="page-title">
                        <h1>Event</h1>
                    </div>
                </div>
            </div>
            <div class="col-sm-8">
                <div class="page-header float-right">
                    <div class="page-title">
                        <ol class="breadcrumb text-right">
                            <li><%--<a href="#">Dashboard</a>--%></li>
                            <li><%--<a href="#">Forms</a>--%></li>
                            <li class="active"><%--Basic--%></li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>

        <div class="content mt-3">
            <div class="animated fadeIn">


                                <div class="row">

                  <div class="col-lg-12">
                      <div class="card">
  <div class="card-header"><strong>Update Category</strong></div>
  <div class="card-body card-block">
    <div class="form-group">
        <label for="category" class=" form-control-label">Category &nbsp&nbsp</label>
        <asp:DropDownList ID="ddlCategories" runat="server" CssClass="btn btn-secondary dropdown-toggle"></asp:DropDownList>
        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" OnClick="btnEdit_Click"/>
    </div>
    <div class="form-group"><label for="name" class=" form-control-label">Name</label><asp:TextBox ID="txtCategory" runat="server" CssClass="form-control" placeholder="Category name"></asp:TextBox></div>
    
   
    <div class="form-group"><label for="event" class=" form-control-label">Event &nbsp&nbsp</label><asp:DropDownList ID="ddlUpdateEvent" runat="server" CssClass="btn btn-secondary dropdown-toggle"></asp:DropDownList></div>

      <div class="form-group">
          <label for="fileUpload" class=" form-control-label">File Upload</label>
              <asp:FileUpload ID="fileUpload" runat="server" accept=".png,.jpg,.jpeg,.gif,.tif"/>
          <div class="form-group"><label for="name" class=" form-control-label">Image name on server</label><asp:TextBox ID="txtImagePath" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox></div>
      </div>

    <asp:Button ID="btnUpdateCategory" runat="server" Text="Update Category" CssClass="btn btn-primary btn-lg btn-block" OnClick="btnUpdateCategory_Click"/>
            <div class="alert alert-success" role="alert" runat="server" id="alertControl" visible="false">
          <asp:Label ID="lbAlert" runat="server" Text=""></asp:Label>
        </div>

  </div>
</div>
                  </div>

                </div>


            </div><!-- .animated -->
        </div><!-- .content -->
<div id="divControl" runat="server">
    </div>
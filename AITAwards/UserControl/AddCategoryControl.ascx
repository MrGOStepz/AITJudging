<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddCategoryControl.ascx.cs" Inherits="AITAwards.AddCategoryControl" %>

        <div class="breadcrumbs">
            <div class="col-sm-4">
                <div class="page-header float-left">
                    <div class="page-title">
                        <h1>Category</h1>
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
  <div class="card-header"><strong>Create Category</strong></div>
  <div class="card-body card-block">
    <div class="form-group"><label for="name" class=" form-control-label">Name</label><asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" placeholder="Category name"></asp:TextBox></div>
     <div class="form-group"><label for="name" class=" form-control-label">Event &nbsp&nbsp</label><asp:DropDownList ID="ddlEvent" runat="server" CssClass="btn btn-secondary dropdown-toggle"></asp:DropDownList></div>
      <div class="form-group">
          <label for="fileUpload" class=" form-control-label">File Upload</label>
              <asp:FileUpload ID="fileUpload" runat="server" accept=".png,.jpg,.jpeg,.gif,.tif"/>
      </div>

    <asp:Button ID="btnAddCategory" runat="server" Text="Add Category" CssClass="btn btn-primary btn-lg btn-block" OnClick="btnAddCategory_Click"/>
            <div class="alert alert-success" role="alert" runat="server" id="alertControl" visible="false">
          <asp:Label ID="lbAlert" runat="server" Text=""></asp:Label>
        </div>

  </div>
</div>
                  </div>

                </div>


            </div><!-- .animated -->
        </div><!-- .content -->
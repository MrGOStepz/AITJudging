<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BreadCrumbControl.ascx.cs" Inherits="AITAwards.BreadCrumbControl" %>
        <div class="breadcrumbs">
            <div class="col-sm-4">
                <div class="page-header float-left">
                    <div class="page-title">
                        <h1><asp:Label ID="lbBCTitile" runat="server" Text=""></asp:Label></h1>
                    </div>
                </div>
            </div>
            <div class="col-sm-8">
                <div class="page-header float-right">
                    <div class="page-title">
                        <ol class="breadcrumb text-right" id="breadcrumbControl" runat="server">
<%--                            <li><a href="#">Dashboard</a></li>
                            <li><a href="#">Forms</a></li>
                            <li class="active">Basic</li>--%>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
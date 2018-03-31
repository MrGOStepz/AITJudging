<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEvent.aspx.cs" Inherits="AITAwards.AddEvent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
  
    <link rel="apple-touch-icon" href="Images/Logo/Logo.png"/>
    <link rel="shortcut icon" href="favicon.ico"/>

    <link rel="stylesheet" href="assets/css/normalize.css" />
    <link rel="stylesheet" href="assets/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="assets/css/font-awesome.min.css"/>
    <link rel="stylesheet" href="assets/css/themify-icons.css"/>
    <link rel="stylesheet" href="assets/css/flag-icon.min.css"/>
    <link rel="stylesheet" href="assets/css/cs-skin-elastic.css"/>
    <link rel="stylesheet" href="assets/scss/style.css"/>
    <link rel="stylesheet" href="Content/Site.css" type="text/css" />
    <link rel="stylesheet" href="Content/Style.css" type="text/css" />

    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700,800' rel='stylesheet' type='text/css'/>

    <title>Admin</title>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Left Panel -->

<!-- Left Panel -->

    <aside id="left-panel" class="left-panel">
        <nav class="navbar navbar-expand-sm navbar-default">

            <div class="navbar-header">
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#main-menu" aria-controls="main-menu" aria-expanded="false" aria-label="Toggle navigation">
                    <i class="fa fa-bars"></i>
                </button>
                <a class="navbar-brand" href="./"><img src="images/logo.png" alt="Logo"/></a>
                <a class="navbar-brand hidden" href="./"><img src="images/logo2.png" alt="Logo"/></a>
            </div>

            <div id="main-menu" class="main-menu collapse navbar-collapse">
                <ul class="nav navbar-nav">
                    <li><asp:LinkButton ID="lbtnDashboard" runat="server"   OnClick="Menu_Click"> <i class="menu-icon ti-email"></i>Dashboard </asp:LinkButton></li>

                    <h3 class="menu-title">Judge</h3><!-- /.menu-title -->            
                    <li><asp:LinkButton ID="lbtnInviteJudge" runat="server" OnClick="Menu_Click"> <i class="menu-icon ti-email"></i>Invite Judge </asp:LinkButton></li>


                    <h3 class="menu-title">Event</h3><!-- /.menu-title -->            
                    <li><asp:LinkButton ID="lbtnAddEvent" runat="server" OnClick="Menu_Click"> <i class="menu-icon ti-email"></i>Add Event </asp:LinkButton></li>
                    <li><asp:LinkButton ID="lbtnUpdateEvent" runat="server" OnClick="Menu_Click"> <i class="menu-icon ti-email"></i>Update Event </asp:LinkButton></li>

                    <h3 class="menu-title">Categories</h3><!-- /.menu-title -->            
                    <li><asp:LinkButton ID="lbtnAddCategory" runat="server" OnClick="Menu_Click"> <i class="menu-icon ti-email"></i>Add Category </asp:LinkButton></li>
                    <li><asp:LinkButton ID="lbtnUpdateCategory" runat="server" OnClick="Menu_Click"> <i class="menu-icon ti-email"></i>Update Category </asp:LinkButton></li>

                    <h3 class="menu-title">Rubrc</h3><!-- /.menu-title -->            
                    <li><asp:LinkButton ID="lbtnAddRubric" runat="server" OnClick="Menu_Click"> <i class="menu-icon ti-email"></i>Add Rubric </asp:LinkButton></li>
                    <li><asp:LinkButton ID="lbtnUpdateRubric" runat="server" OnClick="Menu_Click"> <i class="menu-icon ti-email"></i>Update Rubric</asp:LinkButton></li>


                </ul>
            </div><!-- /.navbar-collapse -->
        </nav>
    </aside><!-- /#left-panel -->

    <!-- Left Panel -->

    <!-- Right Panel -->

    <div id="right-panel" class="right-panel">

        <!-- Header-->
        <header id="header" class="header">

            <div class="header-menu">

                <div class="col-sm-7">
                    <%--<a id="menuToggle" class="menutoggle pull-left"><i class="fa fa fa-tasks"></i></a>--%>
                    <div class="header-left">
                        <button class="search-trigger"><i class="fa fa-search"></i></button>
                        <div class="form-inline">
                            <div class="search-form">
                                <input class="form-control mr-sm-2" type="text" placeholder="Search ..." aria-label="Search">
                                <button class="search-close" type="submit"><i class="fa fa-close"></i></button>
                            </div>
                        </div>


                    </div>
                </div>

                <div class="col-sm-5">
                    <div class="user-area dropdown float-right">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            <img class="user-avatar rounded-circle" src="images/admin.jpg" alt="User Avatar">
                        </a>

                        <div class="user-menu dropdown-menu">
                                <a class="nav-link" href="#"><i class="fa fa- user"></i>My Profile</a>
                                <a class="nav-link" href="#"><i class="fa fa- user"></i>Notifications <span class="count">13</span></a>
                                <a class="nav-link" href="#"><i class="fa fa -cog"></i>Settings</a>
                                <a class="nav-link" href="#"><i class="fa fa-power -off"></i>Logout</a>
                        </div>
                    </div>
                                        <div class="language-select dropdown" id="language-select">
                        <a class="dropdown-toggle" href="#" data-toggle="dropdown"  id="language" aria-haspopup="true" aria-expanded="true">
                            <i class="flag-icon flag-icon-us"></i>
                        </a>
                        <div class="dropdown-menu" aria-labelledby="language" >
                            <div class="dropdown-item">
                                <span class="flag-icon flag-icon-fr"></span>
                            </div>
                            <div class="dropdown-item">
                                <i class="flag-icon flag-icon-es"></i>
                            </div>
                            <div class="dropdown-item">
                                <i class="flag-icon flag-icon-us"></i>
                            </div>
                            <div class="dropdown-item">
                                <i class="flag-icon flag-icon-it"></i>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </header><!-- /header -->
        <!-- Header-->

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
  <div class="card-header"><strong>Create Event</strong></div>
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
      </div>

     <div class="form-group">
          <label for="active" class="form-control-label">Active</label>
         <asp:CheckBox ID="chkActive" runat="server"/>
      </div>

    <asp:Button ID="btnAddEvent" runat="server" Text="AddEvent" CssClass="btn btn-primary btn-lg btn-block" OnClick="btnAddEvent_Click"/>
            <div class="alert alert-success" role="alert" runat="server" id="alertControl" visible="false">
          <asp:Label ID="lbAlert" runat="server" Text=""></asp:Label>
        </div>

  </div>
</div>
                  </div>

                </div>


            </div><!-- .animated -->
        </div><!-- .content -->


    </div><!-- /#right-panel -->

    <!-- Right Panel -->


    <script src="assets/js/vendor/jquery-2.1.4.min.js"></script>
    <script src="assets/js/popper.min.js"></script>
    <script src="assets/js/plugins.js"></script>
    <script src="assets/js/main.js"></script>
    
        </form>


</body>
</html>


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageJudgeDetail.aspx.cs" Inherits="AITAwards.ManageJudgeDetail" %>


<!doctype html>
<html class="no-js" lang=""> <!--<![endif]-->
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>AIT Admin Judging Platform</title>
    <meta name="description" content="Sufee Admin - HTML5 Admin Template">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link rel="apple-touch-icon" href="apple-icon.png">
    <link rel="shortcut icon" href="favicon.ico">

    <link rel="stylesheet" href="assets/css/normalize.css">
    <link rel="stylesheet" href="assets/css/bootstrap.min.css">
    <link rel="stylesheet" href="assets/css/font-awesome.min.css">
    <link rel="stylesheet" href="assets/css/themify-icons.css">
    <link rel="stylesheet" href="assets/css/flag-icon.min.css">
    <link rel="stylesheet" href="assets/css/cs-skin-elastic.css">
    <link rel="stylesheet" href="assets/css/lib/datatable/dataTables.bootstrap.min.css">
    <link rel="stylesheet" href="assets/scss/style.css">

        <link rel="stylesheet" href="Content/Site.css" type="text/css" />
    <link rel="stylesheet" href="Content/Style.css" type="text/css" />

    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700,800' rel='stylesheet' type='text/css'>

</head>
<body>
        <!-- Left Panel -->
    <form id="form1" runat="server">
    <aside id="left-panel" class="left-panel">
        <nav class="navbar navbar-expand-sm navbar-default">

            <div class="navbar-header">
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#main-menu" aria-controls="main-menu" aria-expanded="false" aria-label="Toggle navigation">
                    <i class="fa fa-bars"></i>
                </button>
                <a class="navbar-brand" href="./"><img src="images/logo/logo2.png" width="110" height="35" alt="Logo"></a>
                <a class="navbar-brand hidden" href="./"><img src="images/logo/logo2.png" width="120" height="20"  alt="Logo"></a>
            </div>

            <div id="main-menu" class="main-menu collapse navbar-collapse">
                <ul class="nav navbar-nav">


                    <li><asp:LinkButton ID="lbtnDashboard" runat="server"   OnClick="Menu_Click"> <i class="menu-icon ti-email"></i>Dashboard </asp:LinkButton></li>

                    <h3 class="menu-title">Judge</h3><!-- /.menu-title -->            
                    <li><asp:LinkButton ID="lbtnInviteJudge" runat="server" OnClick="Menu_Click"> <i class="menu-icon ti-email"></i>Invite Judge </asp:LinkButton></li>
                    <li><asp:LinkButton ID="lbtnManageJudge" runat="server" OnClick="Menu_Click"> <i class="menu-icon ti-email"></i>Manage Judge </asp:LinkButton></li>


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
                    <a id="menuToggle" class="menutoggle pull-left"><i class="fa fa fa-tasks"></i></a>
                    <div class="header-left">
                        <button class="search-trigger"><i class="fa fa-search"></i></button>
                        <div class="form-inline">
                            <div class="search-form">
                                <input class="form-control mr-sm-2" type="text" placeholder="Search ..." aria-label="Search">
                                <button class="search-close" type="submit"><i class="fa fa-close"></i></button>
                            </div>
                        </div>

                        <div class="dropdown for-notification">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="notification" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            <i class="fa fa-bell"></i>
                            <span class="count bg-danger">0</span>
                          </button>
<%--                          <div class="dropdown-menu" aria-labelledby="notification">
                            <p class="red">#</p>
                            <a class="dropdown-item media bg-flat-color-1" href="#">
                                <i class="fa fa-check"></i>
                                <p>#</p>
                            </a>
                          </div>--%>
                        </div>

                        <div class="dropdown for-message">
                          <button class="btn btn-secondary dropdown-toggle" type="button"
                                id="message"
                                data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            <i class="ti-email"></i>
                            <span class="count bg-primary">0</span>
                          </button>
                          <div class="dropdown-menu" aria-labelledby="message">
<%--                            <p class="red">You have 4 Mails</p>
                            <a class="dropdown-item media bg-flat-color-1" href="#">
                                <span class="photo media-left"><img alt="avatar" src="images/avatar/1.jpg"></span>
                                <span class="message media-body">
                                    <span class="name float-left">Jonathan Smith</span>
                                    <span class="time float-right">Just now</span>
                                        <p>Hello, this is an example msg</p>
                                </span>
                            </a>--%>
                          </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-5">
                    <div class="user-area dropdown float-right">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            <img class="user-avatar rounded-circle" src="images/logo/logo.png" alt="User Avatar">
                        </a>

                        <div class="user-menu dropdown-menu">
<%--                                <a class="nav-link" href="#"><i class="fa fa- user"></i>My Profile</a>

                                <a class="nav-link" href="#"><i class="fa fa- user"></i>Notifications <span class="count">13</span></a>

                                <a class="nav-link" href="#"><i class="fa fa -cog"></i>Settings</a>--%>

                                <a class="nav-link" href="#"><i class="fa fa-power -off"></i>Logout</a>
                        </div>
                    </div>

                    <div class="language-select dropdown" id="language-select">
                        <a class="dropdown-toggle" href="#" data-toggle="dropdown"  id="language" aria-haspopup="true" aria-expanded="true">
                            <i class="flag-icon flag-icon-us"></i>
                        </a>
                        <div class="dropdown-menu" aria-labelledby="language" >
<%--                            <div class="dropdown-item">
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
                            </div>--%>
                        </div>
                    </div>

                </div>
            </div>

        </header><!-- /header -->
        <!-- Header-->

        <div class="breadcrumbs">
            <div class="col-md-4">
                <div class="page-header float-left">
                    <div class="page-title">
                        <h1>Judge Manage</h1>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <div class="page-header float-right">
                    <div class="page-title">
                        <ol class="breadcrumb text-right">
                            <li><a href="ManageJudge.aspx">Manage Judge</a></li>
                            <li class="active">Manage Judge Detail</li>
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
                                <asp:Button ID="btnFilter" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnFilter_Click"/>
                            </div>
                                    <div class="alert alert-success" role="alert" runat="server" id="alertControl" visible="false">
                                  <asp:Label ID="lbAlert" runat="server" Text=""></asp:Label>
                                </div>
                              </div>
                          </div>
                        </div>

                  </div>

                                <div class="row">

                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header">
                            <strong class="card-title">List User</strong>
                            <asp:Button ID="btnUpdateJudge" runat="server" Text="Approve" CssClass="btn btn-primary" OnClick="btnUpdateJudge_Click"/>
                        </div>
                        <div class="card-body">
                  <table id="bootstrap-data-table1" class="table table-striped table-bordered">
                    <thead>
                      <tr>
                        <th>User ID</th>
                        <th>User Name</th>
                        <th>Email</th>
                        <th>Check</th>
                      </tr>
                    </thead>
                      <tbody id="tbody1" runat="server">

                    </tbody>
                  </table>
                        </div>
                    </div>
                </div>


                </div>


                <div class="row">

                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header">
                            <strong class="card-title">Add User</strong>
                            <asp:Button ID="btnApproveJudge" runat="server" Text="Approve" CssClass="btn btn-primary" OnClick="btnApproveJudge_Click"/>
                        </div>
                        <div class="card-body">
                  <table id="bootstrap-data-table" class="table table-striped table-bordered">
                    <thead>
                      <tr>
                        <th>User ID</th>
                        <th>User Name</th>
                        <th>Email</th>
                        <th>Check</th>
                      </tr>
                    </thead>
                      <tbody id="tbodyControl" runat="server">

                    </tbody>
                  </table>
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


    <script src="assets/js/lib/data-table/datatables.min.js"></script>
    <script src="assets/js/lib/data-table/dataTables.bootstrap.min.js"></script>
    <script src="assets/js/lib/data-table/dataTables.buttons.min.js"></script>
    <script src="assets/js/lib/data-table/buttons.bootstrap.min.js"></script>
    <script src="assets/js/lib/data-table/jszip.min.js"></script>
    <script src="assets/js/lib/data-table/pdfmake.min.js"></script>
    <script src="assets/js/lib/data-table/vfs_fonts.js"></script>
    <script src="assets/js/lib/data-table/buttons.html5.min.js"></script>
    <script src="assets/js/lib/data-table/buttons.print.min.js"></script>
    <script src="assets/js/lib/data-table/buttons.colVis.min.js"></script>
    <script src="assets/js/lib/data-table/datatables-init.js"></script>


    <script type="text/javascript">
        $(document).ready(function() {
          $('#bootstrap-data-table-export').DataTable();
        } );
    </script>
        </form>

</body>
</html>




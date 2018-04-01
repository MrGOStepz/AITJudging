<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Marking.aspx.cs" Inherits="AITAwards.Marking" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container">
        <div class="row padding-10">
            <button type="button" onclick="return TestClick();">Total</button>
<%--            <div class="col text-center">
            
        <asp:Image ID="imgProject" runat="server" class="rounded"/>
    </div>--%>

            <asp:PlaceHolder ID="phControl" runat="server"></asp:PlaceHolder>
       </div>
        </div>

        <script>

        function TestClick() {
            PageMethods.TestClick(onSucceed, onError);
        }

        // On Success
        function onSucceed(results, currentContext, methodName) {

        }

        // On Error
        function onError(results, currentContext, methodName) {

        }

    </script>

</asp:Content>

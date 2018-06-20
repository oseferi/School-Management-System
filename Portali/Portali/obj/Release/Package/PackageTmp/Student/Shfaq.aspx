<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shfaq.aspx.cs" Inherits="Portali.Student.Shfaq" %>
<%@Register TagPrefix="meta" TagName="HeadS" Src="../Controls/HeadS.ascx" %>
<%@Register TagPrefix="meta" TagName="NavS" Src="../Controls/NavS.ascx" %>
<%@Register TagPrefix="meta" TagName="MenuS" Src="../Controls/MenuS.ascx" %>

<!DOCTYPE html>
<html lang="en">

<head runat="server">
    <meta:HeadS runat="server"></meta:HeadS>
</head>
<body class="fix-header" runat="server">
    <meta:NavS runat="server"></meta:NavS>
        <meta:MenuS runat="server"></meta:MenuS>
        <div id="page-wrapper">
            <div class="container-fluid">
                <div class="row bg-title">
                    <div class="col-lg-3 col-md-4 col-sm-4 col-xs-12">
                        <h1 id="Titull" runat="server" class="page-title"></h1> </div>
                </div>
                <div class="row" id="Permbajtje" runat="server">
                </div>  
            </div>
            </div>
    <footer class="footer text-center"> 2018 &copy; Fakulteti i Shkencave Te Natyres | Universiteti i Tiranes </footer>    
    <script src="../Assets/plugins/bower_components/sidebar-nav/dist/sidebar-nav.min.js"></script>
    <script src="../Assets/js/jquery.slimscroll.js"></script>
    <script src="../Assets/js/custom.min.js"></script>
</body>
</html>
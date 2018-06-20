<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Njoftimet.aspx.cs" Inherits="Portali.Student.Njoftimet" %>
<%@Register TagPrefix="meta" TagName="HeadS" Src="../Controls/HeadS.ascx" %>
<%@Register TagPrefix="meta" TagName="NavS" Src="../Controls/NavS.ascx" %>
<%@Register TagPrefix="meta" TagName="MenuS" Src="../Controls/MenuS.ascx" %>
<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta:HeadS runat="server"></meta:HeadS>
</head>
<body class="fix-header">
    <meta:NavS runat="server"></meta:NavS>
        <meta:MenuS runat="server"></meta:MenuS>
 <div id="page-wrapper">
            <div class="container-fluid">
                <div class="row bg-title">
                    <div class="col-lg-6 col-md-4 col-sm-4 col-xs-12">
                        <h3 id="Titull" runat="server" class="page-title">Njoftimet</h3> </div>
                </div>
     <div class="row" id="Permbajtje" runat="server">
    
                     </div>
              </div>      
            </div>
            <footer class="footer text-center"> 2018 &copy; Fakulteti i Shkencave Të Natyrës | Universiteti i Tiranës </footer>
  <script src="../Assets/plugins/bower_components/sidebar-nav/dist/sidebar-nav.min.js"></script>
    <script src="../Assets/js/jquery.slimscroll.js"></script>
    <script src="../Assets/js/custom.min.js"></script>
</body>
</html>

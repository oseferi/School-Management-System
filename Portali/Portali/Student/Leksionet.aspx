<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Leksionet.aspx.cs" Inherits="Portali.Student.Leksionet" %>
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
                        <h1 id="Titull" runat="server" class="page-title">Materiale mësimore</h1> </div>
                </div>
     
     <form id="form1" runat="server">
       <div class="col-md-12 content">
       <div class="container" style="width:auto;">
           <div class="row" id="Permbajtje" runat="server">
         </div> 
      </div> 
       </div> 
    </form>
              </div>      
            </div>
            <footer class="footer text-center"> 2018 &copy; Fakulteti i Shkencave Të Natyrës | Universiteti i Tiranës </footer>
<script src="../Assets/plugins/bower_components/sidebar-nav/dist/sidebar-nav.min.js"></script>
    <script src="../Assets/js/jquery.slimscroll.js"></script>
    <script src="../Assets/js/custom.min.js"></script>
</body>
</html>

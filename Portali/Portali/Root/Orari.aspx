<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Orari.aspx.cs" Inherits="Portali.Root.Orari" %>
<%@Register TagPrefix="meta" TagName="HeadA" Src="../Controls/HeadA.ascx" %>
<%@Register TagPrefix="meta" TagName="NavA" Src="../Controls/NavA.ascx" %>
<%@Register TagPrefix="meta" TagName="MenuA" Src="../Controls/MenuA.ascx" %>
<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta:HeadA runat="server"></meta:HeadA>
</head>
<body class="fix-header">
<meta:NavA runat="server"></meta:NavA>
        <meta:MenuA runat="server"></meta:MenuA>
     <div id="page-wrapper">
            <div class="container-fluid">
                <div class="row bg-title">
                    <div class="col-lg-6 col-md-4 col-sm-4 col-xs-12">
                        <h1 id="Titull" runat="server" class="page-title">Orari mësimor</h1> </div>
                </div>
     <div class="row" id="Permbajtje" runat="server">
     <form id="form1" runat="server">
       <div class="col-md-12 content">
       <div class="container" style="width:auto;">
           <div style="margin: 0 auto; width:100%; height:485px;">
    <object type="text/html" data="http://37.139.119.36:81/orari/"
            style="width:100%; height:100%; margin:1%;">
    </object>
</div>   
         </div> 
      </div>  
    </form>
                     </div>
              </div>      
            </div>
            <footer class="footer text-center"> 2018 &copy; Fakulteti i Shkencave Të Natyrës | Universiteti i Tiranës </footer>
<script src="../Assets/plugins/bower_components/sidebar-nav/dist/sidebar-nav.min.js"></script>
    <script src="../Assets/js/jquery.slimscroll.js"></script>
    <script src="../Assets/js/custom.min.js"></script>
</body>
</html>

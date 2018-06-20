<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Kontakt.aspx.cs" Inherits="Portali.Pedagog.Kontakt" %>
<%@Register TagPrefix="meta" TagName="HeadP" Src="../Controls/HeadP.ascx" %>
<%@Register TagPrefix="meta" TagName="NavP" Src="../Controls/NavP.ascx" %>
<%@Register TagPrefix="meta" TagName="MenuP" Src="../Controls/MenuP.ascx" %>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta:HeadP runat="server"></meta:HeadP>
</head>
<body class="fix-header">
 <meta:NavP runat="server"></meta:NavP>
 <meta:MenuP runat="server"></meta:MenuP>
 <div id="page-wrapper">
   <div class="container-fluid">
     <div class="row bg-title">
       <div class="col-lg-6 col-md-4 col-sm-4 col-xs-12">
          <h1 id="Titull" runat="server" class="page-title">Kontakt dhe Suport</h1>
       </div>
     </div>
      <div class="row" id="Permbajtje" runat="server">
       <form id="form1" runat="server">
         <div class="col-md-12 content">
           <div class="container" style="width:auto;">
            <div class="row" style="margin-left:0px;">
              <div class="col-md-4">
               <h4 style="text-align:center;">PORTALI I MENAXHIMIT - FSHN</h4>
               <hr style="margin-top: 30px;"/>
               <p><asp:Label runat="server" style="text-align:center;">KUJDES! Shumë persona mund t'ju marrin në telefon duke u shtirur sikur janë punonjës të 
                    fakultetit dhe të kerkojnë kredencialet tuaja, por mos bini pre e tyre.</asp:Label></p>
               <p><asp:Label runat="server" style="text-align:center;">Për çdo paqartesi, ankesë apo problem, ju lutemi të telefononi në numrin e telefonit 
                    ose të dergoni nje email në adresën e fakultetit.</asp:Label></p>
            </div>
            <div class="col-md-4">
             <h4 style="text-align:center;">ADRESA TË RËNDËSISHME</h4>
             <hr style="margin-top: 30px;"/>
             <ul style="text-align:center; -webkit-padding-start: 15px;">
                    <p><asp:Hyperlink runat="server" NavigateUrl="http:\\www.unitir.edu.al">UNIVERSITETI I TIRANËS</asp:Hyperlink></p>
                    <p><asp:Hyperlink runat="server" NavigateUrl="http:\\www.fshn.edu.al">FAKULTETI I SHKENCAVE TË NATYRËS</asp:Hyperlink></p>
                    <p><asp:Hyperlink runat="server" NavigateUrl="http:\\www.arsimi.gov.al">MINISTRIA E ARSIMIT</asp:Hyperlink></p>
                    <p><asp:Hyperlink runat="server" NavigateUrl="http://37.139.119.36:81/orari/">ORARI MËSIMOR - FSHN</asp:Hyperlink></p>
             </ul>
         </div>
         <div class="col-md-4">
            <h4 style="text-align:center;">KONTAKT & SUPORT</h4>
            <hr style="margin-top: 30px;"/>
                <p style="text-align:center;"><asp:Label runat="server"><i class="glyphicon glyphicon-home"></i> Blv. Zogu i Parë, Tiranë 1000</asp:Label></p>
                <p style="text-align:center;"><asp:Label runat="server"><i class="glyphicon glyphicon-envelope"></i> support@fshn.edu.al</asp:Label></p>
                <p style="text-align:center;"><asp:Label runat="server"><i class="glyphicon glyphicon-phone"></i> +355 69 68 67 66</asp:Label></p>
                <p style="text-align:center;"><asp:Label runat="server"><i class="glyphicon glyphicon-print"></i> +355 42 22 33 44</asp:Label></p>
         </div>      
      </div>
      <iframe src="https://www.google.com/maps/embed?pb=!1m16!1m12!1m3!1d5991.92797432493!2d19.81181341039996!3d41.33139645577831!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!2m1!1sfakulteti+i+shkencave!5e0!3m2!1sen!2s!4v1517178286211" 
               width="1050" height="220" frameborder="0" style="border:0" allowfullscreen></iframe>
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

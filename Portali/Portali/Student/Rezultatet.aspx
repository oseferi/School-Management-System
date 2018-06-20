<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rezultatet.aspx.cs" Inherits="Portali.Student.Rezultatet" %>
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
                        <h1 id="Titull" runat="server" class="page-title">Rezultatet e provimeve</h1> </div>
                </div>
     <div class="row" id="Permbajtje" runat="server">
       <form id="form1" runat="server">
    <div class="col-md-9 content">
       <div class="container" style="width:auto;">
           <div>
             <asp:GridView ID="GridView1" runat="server" CssClass= "table table-hover table-striped table-bordered table-condensed" >
             </asp:GridView>
           </div>    
         </div> 
      </div>  
    <div class="col-md-3 content">
       <div class="container" style="width:200px;">   
          <h2 style="text-align:center;">Llogaritni gjithashtu:</h2>
          <asp:Button ID="Button1" runat="server" OnClick="Btn1_Mes" style="padding-right:1px;padding-left:1px" CssClass="btn btn-success btn-block" Text="Mesatarja e përgjithshme" />
              <asp:Label ID="Label1" runat="server" Text=""></asp:Label><br /> 
          <asp:Button ID="Button2" runat="server" OnClick="Btn2_Krd" style="background-color: #3d545a;border-color: #333e42;" CssClass="btn btn-info btn-block" Text="Numri i krediteve" />       
              <asp:Label ID="Label2" runat="server" Text=""></asp:Label><br />
           <asp:Button ID="Button3" runat="server" OnClick="Btn3_Ngl" CssClass="btn btn-danger btn-block" Style="background: #ec1919;border: 1px solid #ec1919;" Text="Lëndët e mbartura" />
              <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
         </div> 
      </div>
    </form>
    <script type="text/javascript">
      $(document).ready(function () {
       NgjyraGrid('GridView1', 5, 4);
       });
   function NgjyraGrid(gID, cIndex, nota) {
   $("#" + gID + " td:nth-child(" + cIndex + ")").each(function () {
    if (parseInt($(this).text()) == nota) {
        $(this).parent("tr").css("color", "#e74c3c");
    }
    else $(this).parent("tr").css("color", "#5eb744");
  });
}
</script>
             </div>
              </div>      
            </div>
            <footer class="footer text-center"> 2018 &copy; Fakulteti i Shkencave Të Natyrës | Universiteti i Tiranës </footer>
    <script src="../Assets/plugins/bower_components/sidebar-nav/dist/sidebar-nav.min.js"></script>
    <script src="../Assets/js/jquery.slimscroll.js"></script>
    <script src="../Assets/js/custom.min.js"></script>
</body>
</html>



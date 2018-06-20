<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_Njoftim.aspx.cs" Inherits="Portali.Pedagog.Shto_Njoftim" %>
<%@Register TagPrefix="meta" TagName="HeadP" Src="../Controls/HeadP.ascx" %>
<%@Register TagPrefix="meta" TagName="NavP" Src="../Controls/NavP.ascx" %>
<%@Register TagPrefix="meta" TagName="MenuP" Src="../Controls/MenuP.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
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
        <h1 id="H1" runat="server" class="page-title">Publikoni një njoftim</h1> 
      </div>
     </div>
     <div class="row" id="Permbajtja" runat="server">
      <form id="form1" runat="server" CssClass="form-horizontal">
       <div class="col-md-12 content">
        <div class="container" style="width:auto;">
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
         <div class="form-group">
          <div class="col-md-7">
            Titulli i Njoftimit: <asp:TextBox ID="Titull" class="form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="Kontroll_Titulli" CssClass="text-danger" runat="server" ErrorMessage="Plotësoni titullin" Text="*" ControlToValidate="Titull"></asp:RequiredFieldValidator>
          </div>
         </div>
         <div class="form-group">
          <div class="col-md-7">
          Permbajtja: <asp:TextBox ID="Permbajtje" class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
          <asp:RequiredFieldValidator ID="Kontroll_Permbajtja" CssClass="text-danger" runat="server" ErrorMessage="Plotësoni përmbajtjen" Text="*" ControlToValidate="Permbajtje"></asp:RequiredFieldValidator>
         </div>
        </div>
        <div class="form-group">
         <div class="col-md-7">
          Status:
          <asp:DropDownList ID="Status" runat="server" class="form-control btn btn-default">
             <asp:ListItem Text="Aktive" Value="aktive"></asp:ListItem>
             <asp:ListItem Text="Draft" Value="draft"></asp:ListItem>
          </asp:DropDownList>
          <br />
          <br />
        </div>
        </div>
        <div class="form-group">
        <div class="col-md-7">
        Kursi :
        <asp:DropDownList ID="Kursi"  runat="server" class="form-control btn btn-default">
                <asp:ListItem Text="" Value=""></asp:ListItem>
        </asp:DropDownList>
            <br />
            <br />
        </div>
        </div>
        <div class="form-group">
        <div class="col-md-7">
       Ngarko dokument (opsionale):
        <asp:FileUpload ID="Dokumenti" class="btn btn-default" runat="server" />
        </div>
        </div>
        <div class="form-group">
        <div class="col-sm-offset-2 col-sm-4">
        <asp:Button ID="Button1" runat="server" style="margin-top: 15px;" class="btn btn-primary" Text="Shto Njoftim" OnClick="Shto_njoftim" />
        </div>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_Njoftime.aspx.cs" Inherits="Portali.Root.Shto_Njoftime" %>
<%@Register TagPrefix="meta" TagName="HeadA" Src="../Controls/HeadA.ascx" %>
<%@Register TagPrefix="meta" TagName="NavA" Src="../Controls/NavA.ascx" %>
<%@Register TagPrefix="meta" TagName="MenuA" Src="../Controls/MenuA.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
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
                        <h1 id="H1" runat="server" class="page-title">Publikoni një njoftim</h1> </div>
                </div>
     <div class="row" id="Permbajtja" runat="server">
     <form id="form1" runat="server" CssClass="form-horizontal"> 
    <div class="col-md-12 content">
       <div class="container" style="width:auto;">  
           <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
        <div class="col-md-7">       
        Titulli i Njoftimit:
        <asp:TextBox ID="Titull" runat="server" class="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="Kontroll_Titulli" runat="server" CssClass="text-danger" Text="*" ErrorMessage="Plotësoni Titullin" ControlToValidate="Titull"></asp:RequiredFieldValidator>
          </div>
      </div>
            <div class="form-group">
        <div class="col-md-7"> 
        Përmbajtja:
        <asp:TextBox ID="Permbajtje" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="Kontroll_Permbajtja" runat="server" Text="*" ErrorMessage="Plotësoni Përmbajtjen" CssClass="text-danger" ControlToValidate="Permbajtje"></asp:RequiredFieldValidator>
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
       Ngarko Skedar (Opsionale):
        <asp:FileUpload ID="Dokumenti" runat="server" class="form-control btn btn-default"/>
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


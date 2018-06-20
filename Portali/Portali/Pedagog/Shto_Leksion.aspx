<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_Leksion.aspx.cs" Inherits="Portali.Pedagog.Shto_Leksion" %>
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
      <h3 id="H1" runat="server" class="page-title">Leksion i ri</h3> 
     </div>
    </div>
    <div class="row" id="Permbajtje" runat="server">
     <form id="form1" runat="server">
        <div class="row" id="FushKursi" runat="server">
                            <div class="col-md-2"></div>
                            <div class="col-md-8">
                                <div class="white-box">
                                    <div class="row">
                                        <div class="col-md-1"></div>
                                        <div class="col-md-10">
                                            <asp:DropDownList ID="Kursi" runat="server" OnSelectedIndexChanged="Shfaq_Permbajtjen" AutoPostBack="true" CssClass="form-control form-control-line">
                                                <asp:ListItem Text="Zgjidh Kursin" Value="null" Selected></asp:ListItem>
                                    
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>




        <!-- ============================================================== -->
                    <div class="row" id="FushLeksion" runat="server">
                        <div class="col-md-2" ></div>
                         
                        <div class="col-md-8">
                                <div class="white-box">
                                    <div class="row">
                                        <div class="col-md-1"></div>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="TitullLeksion" runat="server" Placeholder="Titull" CssClass="form-control form-control-line"></asp:TextBox>
                                        </div>
                                     </div> 

                                    <div class="row">
                                        <div class="col-md-1"></div>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="PermbajtjeLeksioni" runat="server" CssClass="form-control form-control-line" TextMode="MultiLine" Placeholder="Permbajtja" Columns="20" Rows="10" style="resize:none;"></asp:TextBox>
                                        </div>
                                     </div>
                                     
                                    <div class="row">
                                        <div class="col-md-1"></div>
                                        <div class="col-md-10">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    </br>
                                                    <h4>Statusi:</h4>
                                                </div>
                                                <div class="col-md-3">
                                                    </br>
                                                    <asp:DropDownList ID="Statusi" runat="server" CssClass="form-control form-control-line">
                                                        <asp:ListItem Value="aktiv" Text="Aktiv"  Selected ></asp:ListItem>
                                                        <asp:ListItem Value="draft" Text="Draft" ></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-1"></div>
                                                <div class="col-md-4">
                                                    </br>
                                                    <h4>Numer Leksioni :</h4>
                                                </div>
                                                <div class="col-md-2">
                                                    </br>
                                                    <asp:TextBox TextMode="Number" CssClass="form-control form-control-line" runat="server" ID="NumerLeksioni"></asp:TextBox>
                                                </div>
                                             </div>
                                            </br>
                                            <div class="row">
                                                <div class="col-md-5">
                                                    </br>
                                                   <h4>Ngarko Dokument :</h4> 
                                                </div>
                                                <div class="col-md-1"></div>
                                                <div class="col-md-6 text-left">
                                                    </br>
                                                    <asp:FileUpload ID="Dokumenti" runat="server"  CssClass="form-control form-control-line" />
                                                    
                                                </div>
                                            </div>

                                        </div>
                                    </div><!-- Fund Fushat Shtese -->

                                    <!-- Submit Button -->
                                    <div class="row">
                                        <div class="col-md-8">
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="TitullLeksion" ErrorMessage="Ju Lutem Plotesoni Fushen e Titullit" ></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="PermbajtjeLeksioni"  ErrorMessage="Ju Lutem Plotesoni Fushen e Permbajtjes"></asp:RequiredFieldValidator>
                                            
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="NumerLeksioni" ErrorMessage="Ju Lutem Plotesoni Fushen e Numrit" ></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="Button1" runat="server" Text="Posto Leksionin" OnClick="Posto_Leksion" CssClass="btn btn-block btn-danger" />
                                           
                                        </div>
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

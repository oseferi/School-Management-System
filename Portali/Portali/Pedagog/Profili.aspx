<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profili.aspx.cs" Inherits="Portali.Pedagog.Profili" %>
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
       <h1 id="Titull" runat="server" class="page-title">Profili i pedagogut</h1>
     </div>
    </div>
    <div class="row" id="Permbajtje" runat="server">
      <div class="row-eq-height" >
                     <div class="col-md-4 col-xs-12" >
                        <div class="white-box" style="height:524px">
                            <div class="user-bg"> 
                                <div class="overlay-box">
                                    <div class="user-content" style=" margin-top: 10px;">
                                        <a href="javascript:void(0)"><asp:Image CssClass="thumb-lg img-circle" runat="server" ID="Profil_Picture" AlternateText="img"></asp:Image></a>
                                        <h3 id="Emri" class="text-white" runat="server"></h3>
                                        <h4 id="Email" runat="server" class="text-white"></h4> 
                                        <h4 id="Numer" runat="server" class="text-white" ></h4>
                                        <asp:Image ID="Image1" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="user-btm-box" style="margin-top:20px;">
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">                                       
                                        <h4>Titulli Shkencor</h4>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        <h4 id="TitullSh" runat="server"></h4>
                                    </div>
                            </div>
                                <div class="row">
                                   <div class="col-md-6 col-sm-6">
                                        <h4>Datëlindje:</h4>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        
                                        <h4 id="Datelindje" runat="server"></h4>
                                    </div>
                                </div>
                                 <div class="row">
                                   <div class="col-md-6 col-sm-6">
                                        <h4>Gjinia:</h4>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        <h4 id="Gjinia" runat="server"></h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8 col-xs-12">                                               
                    <div class="white-box" style="height:175px">
                        <div class="row">
                            <div class="col-md-12" >
                                <h2><b>Departamenti</b></h2>
                                <h3 id="Departamenti" runat="server"></h3>
                            </div>
                        </div>
                    </div>
                        <div class="white-box">
                            <form id="form1" runat="server" class="form-horizontal form-material">

                                <div class="form-group">
                                    <label class="col-md-12">Password i Ri</label>
                                    <div class="col-md-12">
                                        <asp:TextBox TextMode="Password" CssClass="form-control form-control-line" runat="server" ID="Password" placeholder="Vendos Passwordin e ri"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="Password"  runat="server" ErrorMessage="Ju Lutem Plotesoni Fushen"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-12">Konfirmo Password-in</label>
                                     <div class="col-md-12">
                                        <asp:TextBox TextMode="Password" CssClass="form-control form-control-line" runat="server" ID="Password_Confirm" placeholder="Rishkruaj Passwordin"></asp:TextBox>
                                        <asp:CompareValidator ID="Compare1"  ControlToValidate="Password" ControlToCompare="Password_Confirm" 
                                                              Type="String"  EnableClientScript="false"  Text="Passwordet nuk perputhen"  runat="server"/>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="Password_Confirm" runat="server" ErrorMessage="Ju Lutem Plotesoni Fushen"></asp:RequiredFieldValidator>
                                      </div>
                               </div>
                                <div class="form-group">
                                    <div class="col-sm-12"> 
                                        <asp:Button CssClass="btn btn-success" ID="Update" runat="server" OnClick="Ndrysho" Text="Ndrysho"></asp:Button>
                                    </div>
                                </div>
                            </form>
                        </div>

                    </div>
                        </div>
   </div>
  </div>      
 </div>
 <footer class="footer text-center"> 2018 &copy; Fakulteti i Shkencave Të Natyrës | Universiteti i Tiranës </footer>
 <script src="../Assets/plugins/bower_components/sidebar-nav/dist/sidebar-nav.min.js"></script>
 <script src="../Assets/js/jquery.slimscroll.js"></script>
 <script src="../Assets/js/custom.min.js"></script>
</body>
</html>

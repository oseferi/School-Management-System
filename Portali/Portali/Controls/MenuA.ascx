<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuA.ascx.cs" Inherits="Portali.Controls.MenuA" %>

 <div class="navbar-default sidebar" role="navigation">
            <div class="sidebar-nav slimscrollsidebar">
                <div class="sidebar-head">
                    <h3><span class="fa-fw open-close"><i class="ti-close ti-menu"></i></span> <span class="hide-menu">Administratori</span></h3>
                </div>
                <ul class="nav" id="side-menu">
                    <li style="padding: 70px 0 0;">
                        <asp:Hyperlink runat="server" NavigateUrl="../Root/Shto_Njoftime.aspx" class="waves-effect"><i class="fa fa-book fa-fw" aria-hidden="true"></i>NJOFTIME</asp:Hyperlink>
                    </li>
                    <li>
                        <asp:Hyperlink runat="server" NavigateUrl="../Root/Shto_User.aspx" class="waves-effect"><i class="fa fa-home fa-fw" aria-hidden="true"></i>PËRDORUESIT</asp:Hyperlink>
                    </li>
                    <li>
                        <asp:Hyperlink runat="server" NavigateUrl="../Root/Shto_Sezon.aspx" class="waves-effect"><i class="fa fa-mortar-board fa-fw" aria-hidden="true"></i>SEZONET AKTIVE</asp:Hyperlink>
                    </li>
                    <li>
                        <asp:Hyperlink runat="server" NavigateUrl="../Root/Orari.aspx" class="waves-effect"><i class="fa fa-clock-o fa-fw" aria-hidden="true"></i>ORARI MËSIMOR</asp:Hyperlink>
                    </li>
                </ul>
                <div class="center p-20">
                     <asp:Hyperlink runat="server" NavigateUrl="../Root/Kontakt.aspx" class="btn btn-danger btn-block waves-effect waves-light" Style="background: #ec1919;border: 1px solid #ec1919;"><i class="fa fa-info fa-fw" aria-hidden="true"></i>KONTAKTONI</asp:Hyperlink>
                </div>
            </div>
        </div>
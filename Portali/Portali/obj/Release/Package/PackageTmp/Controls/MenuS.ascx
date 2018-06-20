<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuS.ascx.cs" Inherits="Portali.Controls.MenuS" %>

        <div class="navbar-default sidebar" role="navigation">
            <div class="sidebar-nav slimscrollsidebar">
                <div class="sidebar-head">
                    <h3><span class="fa-fw open-close"><i class="ti-close ti-menu"></i></span> <span class="hide-menu">Student</span></h3>
                </div>
                <ul class="nav" id="side-menu">
                    <li style="padding: 70px 0 0;">
                        <asp:Hyperlink runat="server" NavigateUrl="../Student/Njoftimet.aspx" class="waves-effect"><i class="fa fa-home fa-fw" aria-hidden="true"></i>NJOFTIME</asp:Hyperlink>
                    </li>
                    <li>
                        <asp:Hyperlink runat="server" NavigateUrl="../Student/Leksionet.aspx" class="waves-effect"><i class="fa fa-book fa-fw" aria-hidden="true"></i>MATERIALE MËSIMORE</asp:Hyperlink>
                    </li>
                    <li>
                        <asp:Hyperlink runat="server" NavigateUrl="../Student/Regjistrim.aspx" class="waves-effect"><i class="fa fa-mortar-board fa-fw" aria-hidden="true"></i>REGJISTROHU NË SEZON</asp:Hyperlink>
                    </li>
                    <li>
                        <asp:Hyperlink runat="server" NavigateUrl="../Student/Rezultatet.aspx" class="waves-effect"><i class="fa fa-list-alt fa-fw" aria-hidden="true"></i>REZULTATE</asp:Hyperlink>
                    </li>
                    <li>
                        <asp:Hyperlink runat="server" NavigateUrl="../Student/Orari.aspx" class="waves-effect"><i class="fa fa-clock-o fa-fw" aria-hidden="true"></i>ORARI MËSIMOR</asp:Hyperlink>
                    </li>
                </ul>
                <div class="center p-20">
                     <asp:Hyperlink runat="server" NavigateUrl="../Student/Kontakt.aspx" class="btn btn-danger btn-block waves-effect waves-light" Style="background: #ec1919;border: 1px solid #ec1919;"><i class="fa fa-info fa-fw" aria-hidden="true"></i>KONTAKTONI</asp:Hyperlink>
                 </div>
            </div>
        </div>
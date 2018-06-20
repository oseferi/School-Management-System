<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavA.ascx.cs" Inherits="Portali.Controls.NavA" %>

 <div class="preloader">
        <svg class="circular" viewBox="25 25 50 50">
            <circle class="path" cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10" />
        </svg>
    </div>
        <nav class="navbar navbar-default navbar-static-top m-b-0">
            <div class="navbar-header">
                <div class="top-left-part">
                     <asp:Hyperlink runat="server" Cssclass="logo" NavigateUrl="../Root/Shto_Njoftime.aspx">
                    <b><asp:Image runat="server" ImageUrl="../Assets/img/logo_final.svg" height="30px" alt="home" class="dark-logo" /><asp:Image runat="server" height="60" style="padding-top:3px" ImageUrl="../Assets/img/logo_final.svg" alt="home" class="light-logo" /></b>
                    </asp:Hyperlink>
                </div>
                <ul class="nav navbar-top-links navbar-right pull-right" style="margin-bottom: -9px;">
                    <li>
                   <form action="../logout.aspx" method="post"><button type="submit" style="margin-right: 10px;margin-top: 13px;margin-bottom: 8px;" class="btn btn-default navbar-btn navbar-link">Dilni</button></form>
                    </li>
                    <li>
                        <asp:Hyperlink runat="server" class="profile-pic" NavigateUrl="../Root/Profili.aspx"> <asp:Image ID="Foto_Profil" runat="server" CssClass="img-circle" Width="35" Height="35" /><b class="hidden-xs"><%= Session["UserID"] %></b></asp:Hyperlink>
                    </li>
                </ul>
            </div>
        </nav>
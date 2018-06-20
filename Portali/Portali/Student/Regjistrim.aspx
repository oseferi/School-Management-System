<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Regjistrim.aspx.cs" Inherits="Portali.Student.Regjistrim" %>
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
                        <h1 id="Titull" runat="server" class="page-title">Regjistrohu në sezonin aktiv</h1> </div>
                </div>
                <div class="row" id="Permbajtje" runat="server">
     <form id="form1" runat="server">
       <div class="col-md-9 content">
       <div class="container" style="width:auto;"> 
             <div>
              <asp:Label ID="Sezoni" runat="server" Text=""></asp:Label>
             <br />
          <br />
        <asp:GridView ID="Grid1" CssClass= "table table-hover table-striped table-bordered table-condensed" runat="server" DataKeyNames="Lenda,Kredite,DataProvimit,OraProvimit,Salla" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" Visible="False">
            <Columns>
                <asp:TemplateField HeaderText="Nr">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                        <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Lënda" SortExpression="Lenda">
                    <EditItemTemplate>
                        <asp:TextBox ID="Lenda" runat="server" Text='<%# Bind("Lenda") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Lenda") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Kredite" SortExpression="NrKredite">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("NrKredite") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Kredite") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Data" SortExpression="DataProvimit">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("DataProvimit") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("DataProvimit") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ora" SortExpression="OraProvimit">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("OraProvimit") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("OraProvimit") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Salla" SortExpression="Salla">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Salla") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("Salla") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Zgjidh" ShowHeader="False">
                    <EditItemTemplate>
                        <asp:Button ID="Zgjidh" runat="server" Text="Button" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="choose" runat="server" OnClick="choose_Click">>>></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                
    </div> 
         </div> 
      </div>  
         <div class="col-md-3 content">
       <div class="container" style="width:200px;">   
          <h2 style="text-align:center;">Përzgjedhjet</h2>
            <asp:Label ID="lndregj" runat="server" style="padding-right:1px;padding-left:1px" Visible="false" Text="● Ju jeni regjistruar në: "></asp:Label>
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

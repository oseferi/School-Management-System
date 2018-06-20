<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_Nota.aspx.cs" Inherits="Portali.Pedagog.Shto_Nota" %>
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
                        <h1 id="Titull" runat="server" class="page-title">Publikoni rezultatet e provimeve</h1> </div>
                </div>
     <div class="row" id="Permbajtje" runat="server">
     <form id="form1" runat="server" CssClass="form-horizontal">
       <div class="col-md-12 content">
       <div class="container" style="width:auto;">
            <div class="form-group">
        <div class="col-md-7">
        <asp:Label ID="Sezon" runat="server" ForeColor="Red" Text=""></asp:Label>
            <br />
           <br />
            </div>
        </div>
         <div class="form-group">
        <div class="col-md-7">
        <asp:Label ID="Label2" runat="server" Text="Zgjidhni lëndën" />
        <asp:DropDownList ID="lenda" runat="server" OnSelectedIndexChanged="lenda_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem>         </asp:ListItem>
        </asp:DropDownList>
            <br />
           <br />
            </div>
        </div>
           <div class="form-group">
        <div class="col-md-6">
        <asp:GridView ID="Grid1" runat="server" CssClass= "table table-hover table-striped table-bordered table-condensed" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" AutoGenerateEditButton="true" DataKeyNames="SsnStudent" OnRowCancelingEdit="Grid1_RowCancelingEdit" OnRowEditing="Grid1_RowEditing" OnRowUpdating="Grid1_RowUpdating">
            <Columns>
                <asp:BoundField DataField="SsnStudent" HeaderText="Ssn" ReadOnly="True" SortExpression="SsnStudent" Visible="False" />
                <asp:BoundField DataField="EmerS" HeaderText="EmriStudentit" ReadOnly="True" SortExpression="EmerS" />
                <asp:BoundField DataField="MbiemerS" HeaderText="MbiemriStudentit" ReadOnly="True" SortExpression="MbiemerS" />
                <asp:TemplateField HeaderText="Nota" SortExpression="Nota" >
                    <EditItemTemplate>
                        <asp:TextBox ID="nota" runat="server" Text='<%# Bind("Nota") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1"  runat="server" Text='<%# Bind("Nota") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
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

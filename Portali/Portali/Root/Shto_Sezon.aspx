<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_Sezon.aspx.cs" Inherits="Portali.Root.Shto_Sezon" %>
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
                        <h1 id="H1" runat="server" class="page-title">Shtoni një sezon të ri</h1> </div>
                </div>
     <div class="row" id="Permbajtje" runat="server">
     <form id="form1" runat="server" cssclass="form-horizontal"> 
    <div class="col-md-12 content">
       <div class="container" style="width:auto;"> 
           <div runat="server" id="ShtoSezon">
               <asp:ValidationSummary ID="ValidationSummary1" CssClass="text-danger" runat="server" DisplayMode="BulletList" ForeColor="Red" />
            <div class="form-group">
        <div class="col-md-7">    
        <asp:Label ID="Label2" runat="server" Text="IdSezoni"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="text-danger" runat="server" ErrorMessage="Plotesoni Id E Sezonit" ToolTip="Plotesoni Id E Sezonit" Text="*" ControlToValidate="IdSez" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:TextBox ID="IdSez" runat="server" class="form-control"></asp:TextBox>
            </div>
        </div>
            <div class="form-group">
        <div class="col-md-7">
        <asp:Label ID="Label3" runat="server" Text="Emri"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="text-danger" runat="server" ErrorMessage="Plotesoni Emrin E Sezonit" ForeColor="Red" ToolTip="Plotesoni Emrin E Sezonit" Text="*" ControlToValidate="EmSez"></asp:RequiredFieldValidator>
        <asp:TextBox ID="EmSez" runat="server" class="form-control"></asp:TextBox>
            </div>
        </div>
            <div class="form-group">
        <div class="col-md-7">
        <asp:Label ID="Label4" runat="server" Text="Tipi"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="text-danger" runat="server" ErrorMessage="Zgjidhni Tipin E Sezonit" ForeColor="Red" ToolTip="Zgjidhni Tipin E Sezonit" Text="*" ControlToValidate="TipSez"></asp:RequiredFieldValidator>
        <asp:DropDownList ID="TipSez" runat="server" class="form-control btn btn-default">
            <asp:ListItem>        </asp:ListItem>
            <asp:ListItem>NORMAL</asp:ListItem>
            <asp:ListItem>I VECANTE</asp:ListItem>
        </asp:DropDownList>
            </div>
        </div>
            <div class="form-group">
        <div class="col-md-7">
        <asp:Label ID="Label5" runat="server" Text="Semestri" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="text-danger" runat="server" ErrorMessage="Zgjidhni Semestrin E Sezonit" ForeColor="Red" ToolTip="Zgjidhni Semestrin E Sezonit" Text="*" ControlToValidate="SemSez"></asp:RequiredFieldValidator>
        <asp:DropDownList ID="SemSez" runat="server" class="form-control btn btn-default">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>SEMESTRI I PARE</asp:ListItem>
            <asp:ListItem>SEMESTRI I DYTE</asp:ListItem>
            <asp:ListItem>VJETORE</asp:ListItem>
        </asp:DropDownList>
            </div>
        </div>
            <div class="form-group">
        <div class="col-md-7">
        <asp:Label ID="Label6" runat="server" Text="VitiAkademik"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="text-danger" runat="server" ErrorMessage="Zgjidhni Vitin Akademik të Sezonit" ForeColor="Red" ToolTip="Zgjidhni Vitin Akademit te Sezonit" Text="*" ControlToValidate="VitSez"></asp:RequiredFieldValidator>
        <asp:DropDownList ID="VitSez" runat="server" class="form-control btn btn-default">
            <asp:ListItem></asp:ListItem>  
            <asp:ListItem>2017-2018</asp:ListItem>
            <asp:ListItem>2018-2019</asp:ListItem>
            <asp:ListItem>2019-2020</asp:ListItem>
        </asp:DropDownList>
            </div>
        </div>
               <div class="form-group">
        <div class="col-md-offset-2 col-md-4">
        <asp:Button ID="crt" runat="server" Text="Krijo Sezon" Style="margin-top: 15px;background: #ec1919;border: 1px solid #ec1919;" class="btn btn-danger" OnClick="crt_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="orarbtn" runat="server" Text="Vendos Oraret" OnClick="orarbtn_Click" Style="margin-top: 15px;" class="btn btn-default" CausesValidation="false"/>
        </div>
        </div>
        </div>
        <div runat="server" id="Oraret" visible="false">
            <%--<asp:DropDownList ID="dega" runat="server">
                <asp:ListItem></asp:ListItem>
            </asp:DropDownList><br /><br />--%>
            <asp:Label ID="Label8" runat="server" Text="Vendosni Oraret e Provimeve Per Sezonin :"></asp:Label>
            <asp:Label ID="season" runat="server" ForeColor="Red" Text=""></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label7" runat="server" Text="Zgjidh Departamentin"></asp:Label>
            <asp:DropDownList ID="dep" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dep_SelectedIndexChanged">
                <asp:ListItem></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="back" runat="server" Text="<Kthehu" OnClick="back_Click" />
            <br />
            <br />
            <asp:GridView ID="Grid1" runat="server" OnPageIndexChanging="Grid1_PageIndexChanging" CssClass= "table table-hover table-striped table-bordered table-condensed" DataKeyNames="IdSezoni,IdKursi,IdPedag" AllowPaging="True" AllowSorting="True" OnRowEditing="Grid1_RowEditing" OnRowCancelingEdit="Grid1_RowCancelingEdit" OnRowUpdating="Grid1_RowUpdating" AutoGenerateColumns="False" AutoGenerateEditButton="True" >
                <Columns>
                    <asp:BoundField DataField="IdSezoni" HeaderText="IdSezoni" ReadOnly="True" SortExpression="IdSezoni" Visible="False" />
                    <asp:BoundField DataField="IdKursi" HeaderText="IdKursi" ReadOnly="True" SortExpression="IdKursi" Visible="False" />
                    <asp:BoundField DataField="IdPedag" HeaderText="IdPedag" ReadOnly="True" SortExpression="IdPedag" Visible="False" />
                    <asp:BoundField DataField="EmerK" HeaderText="Lenda" ReadOnly="True" SortExpression="EmerK" />
                    <asp:BoundField DataField="EmerP" HeaderText="Emri Pedagogut" ReadOnly="True" SortExpression="EmerP" />
                    <asp:BoundField DataField="MbiemerP" HeaderText="Mbiemri Pedagogut" ReadOnly="True" SortExpression="MbiemerP" />
                    <asp:TemplateField HeaderText="Salla" SortExpression="Salla">
                        <EditItemTemplate>
                            <asp:TextBox ID="Salla" runat="server" Text='<%# Bind("Salla") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Salla") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Data Provimit" SortExpression="DataProvimit">
                        <EditItemTemplate>
                            <asp:TextBox ID="Data" runat="server" Text='<%# Bind("DataProvimit") %>' TextMode="Date"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("DataProvimit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ora Provimit" SortExpression="OraProvimit">
                        <EditItemTemplate>
                            <asp:TextBox ID="Ora" runat="server" Text='<%# Bind("OraProvimit") %>' TextMode="DateTime"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("OraProvimit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
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

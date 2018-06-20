<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Portali.Login" %>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml" class="no-js">
    <head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Identifikohu | PORTALI I MENAXHIMIT - FSHN</title>
    <link rel="icon" type="image/png" href="../Assets/logofinal.png" />
    <meta name="description" content="Portali i Fakultetit te Shkencave te Natyres per cdo student e pedagog te fakultetit." />
    <meta name="keywords" content="Portal, nota, student, FSHN, pedagog, provim" />
    <meta name="author" content="Portali-FSHN" />
    <link rel="stylesheet" type="text/css" href="../Assets/css/normalize.css" />
    <link rel="stylesheet" type="text/css" href="../Assets/css/demo.css" />
    <link rel="stylesheet" type="text/css" href="../Assets/css/component.css" />
    <link rel="stylesheet" type="text/css" href="../Assets/css/cs-select.css" />
    <link rel="stylesheet" type="text/css" href="../Assets/css/cs-skin-boxes.css" />
    <script src="../Assets/js/modernizr.custom.js" type="text/javascript"></script>
</head>
<body>
    <div class="container">
        <div class="fs-form-wrap" id="fs-form-wrap">
                <div class="fs-title">
                     <h1>FAKULTETI I SHKENCAVE TË NATYRËS</h1>
                     <h3 style="color:#373f48;"><i>Identifikohu në portalin e menaxhimit</i></h3>
				</div>
				<form id="myform" class="fs-form fs-form-full" autocomplete="off" runat="server">
					<ol class="fs-fields">
						<li>
							<asp:Label runat="server" CssClass="fs-field-label fs-anim-upper" Text="Emri i Përdoruesit" />
							<asp:Textbox runat="server" CssClass="fs-anim-lower" Id="q1" Type="text" Placeholder="username" Required="required"/>
						</li>
						<li>                           
							<asp:Label runat="server" CssClass="fs-field-label fs-anim-upper" Text="Fjalëkalimi" />
							<asp:Textbox runat="server" CssClass="fs-anim-lower" Id="q2" Type="password" Placeholder="password" Required="required"/>
                        </li>
                   </ol>
                   <asp:Button runat="server" OnClick="Button1_Click" CssClass="fs-submit" Type="submit" Text="IDENTIFIKOHU" />
                   <asp:Image ID="Image1" runat="server" ImageUrl="../Assets/LOGOFINAL.PNG" ImageAlign="Right" />
                </form>
        </div>
    </div>
    <script src="../Assets/js/classie.js" type="text/javascript"></script>
    <script src="../Assets/js/selectFx.js" type="text/javascript"></script>
    <script src="../Assets/js/fullscreenForm.js" type="text/javascript"></script>
    <script type="text/javascript">
			(function() {
				var formWrap = document.getElementById( 'fs-form-wrap' );

				[].slice.call( document.querySelectorAll( 'select.cs-select' ) ).forEach( function(el) {
					new SelectFx( el, {
						stickyPlaceholder: false,
						onChange: function(val){
							document.querySelector('span.cs-placeholder').style.backgroundColor = val;
						}
					});
				} );

				new FForm( formWrap, {
					onReview : function() {
						classie.add( document.body, 'overview' );
					}
				} );
			})();
    </script>
</body>
</html>


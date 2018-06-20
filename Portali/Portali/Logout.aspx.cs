using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portali
{
    public partial class Logout : System.Web.UI.Page
    {
        //faqja sherben per te shkaterruar sesionet dhe ridrejtuar tek logini ne klikim te butonit logout
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserID"] = null;
            Response.Redirect("~/Login.aspx");
        }
    }
}
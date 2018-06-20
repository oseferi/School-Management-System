using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portali.Pedagog
{
    public partial class Orari : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else if (Session["Roli"].ToString() == "STUDENT")
            {
                Response.Redirect("~/Student/Njoftimet.aspx");
            }
            else if (Session["Roli"].ToString() == "ADMIN")
            {
                Response.Redirect("~/Root/Shto_Njoftime.aspx");
            }
        }
    }
}
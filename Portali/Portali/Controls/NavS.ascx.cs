using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portali.Controls
{
    public partial class NavS : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Foto_Profil.ImageUrl = Session["Link_Imazh"].ToString(); //per te marre linkun e fotos
        }

    }
}
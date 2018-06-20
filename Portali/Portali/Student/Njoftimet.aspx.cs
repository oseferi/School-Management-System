using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net;
using System.Text;
using System.IO;
using Portali.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;


/*------------------------------------------------------------------------------------------------
*
*Filtrimi i Njoftimeve
 *Afishohet lista e plote e njoftimeve [mbi te cilat ka akses per perdoruesi i loguar aktualisht] te cilen e gjenerojm nga API NjoftimeController.
 *Si fillim krijohet nje kerkese http GET, duke thirrur metoden MerrNjoftime, drejt Api i cili eshte ne pathin /api/Njoftime
 *Metoda MerrNjoftime pranon nje parameter:
 *                                       -string username {username i perdoruesit qe eshte loguar aktualisht dhe nga ku llogaria nga ku po gjenerohet kerkesa}
 *
 *Per te menaxhuar transferimin e te dhenave eshte krijuar nje klase model "Njoftim" e cila ka si atribute te gjitha vlerat qe ndodhen tabelen njoftime 
 *edhe gjithashtu 3 vlera shtese qe gjenerohen gjate thirrjes se procedures ne databaze.
 *Pergjigja nga API vjen si nje array objektesh njoftim, e cila eshte encoduar ne json.
 *Ne kete skedar behet konvertimi i objekteve nga formati json, ne array objektesh Njoftim
 *Pasi behet konvertimi, bridhet array, dhe per cdo element gjenerohet nje div ku do insertojm dinamikisht vlerat e secilit objekt, ne menyre qe ti shfaqim ne front-end
 * 
*---------------------*/

namespace Portali.Student
{
    public partial class Njoftimet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else if (Session["Roli"].ToString() == "PEDAGOG")
            {
                Response.Redirect("~/Pedagog/Shto_Njoftim.aspx");
            }
            else if (Session["Roli"].ToString() == "ADMIN")
            {
                Response.Redirect("~/Root/Shto_Njoftime.aspx");
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:23243/api/Njoftime/MerrNjoftime?username=" + Session["UserID"].ToString());
            request.Method = "Get";
            request.KeepAlive = true;
            request.ContentType = "appication/json";
            //request.Headers.Add("Content-Type", "appication/json");
            //request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = 0;//sepse nxjerr 411 length not specified


            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string myResponse = "";
                using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    myResponse = sr.ReadToEnd();
                }

                // Response.Write(myResponse);
                JavaScriptSerializer js = new JavaScriptSerializer();
                Njoftim[] njoftime = js.Deserialize<Njoftim[]>(myResponse);
                for (int i = 0; i < njoftime.Length; i++)
                {

                    if (Convert.ToBoolean(njoftime[i].Status) == true)
                    {



                        string njoftim = "";
                        njoftim = "<div class=\"col-md-3 text-center\">";
                        njoftim += "<a href=\"shfaq.aspx?tipi=njoftim&id=" + njoftime[i].ID.ToString() + "\" class=\"thumbnail\">";
                        njoftim += "<div class=\"white-box\" style=\"height:260px\" >";
                        njoftim += "<div class=\"row\" style=\"height:120px\">";
                        njoftim += "<h3>";
                        if (njoftime[i].Titulli.Length>60)
                        {
                            string titulli = njoftime[i].Titulli;
                            njoftim += titulli.Substring(0, 65)+"..";
                        }
                        else
                        {
                           njoftim +=  njoftime[i].Titulli;
                        }

                        njoftim += "<br /></div><!--row-->";
                        njoftim += "<div class=\"row\" style=\"height:100px\">";
                      //  njoftim += "<small class=\"text-muted\">" + njoftime[i].Emer_Pedagogu + " " + njoftime[i].Mbiemer_Pedagogu + "</small>";
                        njoftim +=  njoftime[i].Emer_Pedagogu + " " + njoftime[i].Mbiemer_Pedagogu ;
                        njoftim += "</br>";
                        njoftim += "<img width=\"30 px\" height=\"auto\" src=\"../Assets/img/profil.png\" class=\"circle\" />";
                        njoftim += "</div><!-- row i mesit-->";
                        njoftim += "<div class=\"row\">";
                        njoftim += "<div class=\"col-md-1 text-right\"><span class=\"meta__date\"><i class=\"fa fa-calendar-o\"></i> </span></div>";
                        DateTime dt = Convert.ToDateTime(njoftime[i].Data);
                        njoftim += "<div class=\"col-md-9\">"+dt.ToShortDateString()+"</div>";
                        njoftim += "</div><!--row 3-->";
                        njoftim += " </div></a>";
                        njoftim += "</div>";

                        Permbajtje.InnerHtml = Permbajtje.InnerHtml + njoftim;
                    }
                }

                response.Close();

            }

            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        Response.Write("<script>alert('HTTP Status Code: " + (int)response.StatusCode + "')</script>");
                    }
                    else
                    {
                        // no http status code available
                    }
                }
                else
                {
                    // no http status code available
                }
            }


        }

    }       
}
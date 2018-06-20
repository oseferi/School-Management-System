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






namespace Portali.Student
{
    public partial class Shfaq : System.Web.UI.Page
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
            else
            {
                if (Request.QueryString["Tipi"] == null || Request.QueryString["Tipi"] == null)
                {
                    Response.Write("<script>alert('Ju lutem specifikoni parametrat')</script>");

                }
                else
                {
                    string tipi = Request.QueryString["Tipi"].ToString();
                    int ID = Convert.ToInt32(Request.QueryString["ID"]);

                    switch (tipi)
                    {
                        case "leksion":
                            Afisho_Leskion(ID);
                            break;
                        case "seminar":
                            Afisho_Seminar(ID);
                            break;
                        case "njoftim":
                            Afisho_Njoftim(ID);
                            break;
                        default:
                            Response.Write("<script>alert('Ju lutem specifikoni tipin')</script>");
                            break;
                    }

                }
            }

             
           
        }

        protected void Afisho_Leskion(int id)
        {
            HttpWebRequest requestLeksion = (HttpWebRequest)WebRequest.Create("http://localhost:23243/api/Leksione/MerrLeksion?id="+id.ToString()+"&username="+Session["UserID"].ToString());//
            requestLeksion.Method = "Get";
            requestLeksion.KeepAlive = true;
            requestLeksion.ContentType = "appication/json";
            requestLeksion.ContentLength = 0;//sepse nxjerr 411 length not specified

            HttpWebResponse responseLeksion = (HttpWebResponse)requestLeksion.GetResponse();
            string myResponseLeksion = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(responseLeksion.GetResponseStream()))
            {
                myResponseLeksion = sr.ReadToEnd();
            }

            // Response.Write(myResponse);
            JavaScriptSerializer js = new JavaScriptSerializer();
            Leksion leksioni = js.Deserialize<Leksion>(myResponseLeksion);
            if (Convert.ToBoolean(leksioni.Status) == true)
            {

                
                string permbajtje = "";
         permbajtje="<div class=\"row\">";
			permbajtje+="<div class=\"col-md-1\"></div>";
			permbajtje+="<div class=\"col-md-10\">";
			    permbajtje+="<div class=\"white-box\">";
					permbajtje+="<div class=\"row\">";
						permbajtje+="<div class=\"col-md-4\"></div>";
                        permbajtje += "<div class=\"col-md-4 text-center\"><i><h4>" + leksioni.Emer_Kursi + "</h4></i></div>";
						permbajtje+="</div>";
					permbajtje+="<div class=\"row\">";
						permbajtje+="<div class=\"col-md-2\"></div>";
						permbajtje+="<div class=\"col-md-8 text-center\">";
							permbajtje+="<h1 class=\"display-4\">"+leksioni.Titulli+"</h2></br>";
                            permbajtje += "<img width=\"30 px\" height=\"auto\" src=\"../Assets/img/profil.png\" class=\"circle\" /></br>";
							permbajtje+="<span>Nga :<b>"+leksioni.Emer_Pedagogu+" "+leksioni.Mbiemer_Pedagogu+"</b> <span>";
						permbajtje+="</div>";
					permbajtje+="</div>";
					permbajtje+="<div class=\"row\">";
                        permbajtje+="<div class=\"col-md-1\"></div>";
						
                        DateTime dt = Convert.ToDateTime(leksioni.Data);
                        permbajtje += "<div class=\"col-md-2\">Data :" + dt.ToShortDateString() + "</div>";
						//permbajtje+="<div class=\"col-md-2\"></div>";
						permbajtje+="<div class=\"col-md-6\"></div>";
                        permbajtje += "<div class=\"col-md-2 text-right\">Lekioni Nr: <b>" + leksioni.Numer_Leksioni + "</b></div>";
						//permbajtje+="<div class=\"col-md-1\"><b>"+leksioni.Numer_Leksioni+"</b></div>";
					permbajtje+="</div></br></br>";

                    permbajtje+="<div class=\"row\">";
						permbajtje+="<div class=\"col-md-1\"></div>";
						permbajtje+="<div class=\"col-md-10\">";
							permbajtje+=leksioni.Permbajtja;
						permbajtje+="</div>";
					permbajtje+="</div>";
					permbajtje+="<div class=\"row\">";
						permbajtje+="<div class=\"col-md-9\"></div>";
						permbajtje+="<div class=\"col-md-3\">";
                        if(!String.IsNullOrEmpty(leksioni.Link_dokumenti.ToString())){
							permbajtje+="<a class=\"btn btn-info\" href=\""+leksioni.Link_dokumenti+"\">Kliko per Dokumentin</a>";
                        }
						permbajtje+="</div>";
					permbajtje+="</div >";
				permbajtje+="</div>	";
			permbajtje+="</div>";
	permbajtje+="</div>	";			
							
                /*
                permbajtje ="<div class=\"row\">";
                permbajtje += "<div class=\"col-md-2\"></div>";
                permbajtje += "<div class=\"col-md-8\">";
                permbajtje += "<div class=\"white-box\" >";
                permbajtje ="<div class=\"row\">";
                permbajtje += "<div class=\"col-md-1\"></div>";
                permbajtje += "<div class=\"col-md-10\" text-center\">";
                permbajtje += "<i><h4>"+leksioni.Emer_Kursi+"</h4></i>";
                permbajtje += "<h3 class=\"text-center\">";
                permbajtje += leksioni.Titulli;
                permbajtje += "<br />";
                permbajtje += "<small class=\"text-muted\">" + leksioni.Emer_Pedagogu + " " + leksioni.Mbiemer_Pedagogu + "</small>";
                permbajtje += "</h3>";
                permbajtje += "<img width=\"30 px\" height=\"auto\" src=\"../Assets/img/profil.png\" class=\"circle\" />";
                //permbajtje += "Data"+leksioni.Data.ToString();
                permbajtje += "</div><!-- row -->";
                permbajtje += "<div class=\"row\">";
                permbajtje += "<p>" + leksioni.Permbajtja + "</p>";
                permbajtje += "</div>";
                permbajtje += " </div></a>";
                permbajtje += "</div>";
                permbajtje += "</div><!--row1-->";*/
                Permbajtje.InnerHtml = permbajtje;
            }

            responseLeksion.Close(); 

        }

        protected void Afisho_Seminar(int id)
        {

        }

        protected void Afisho_Njoftim(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:23243/api/Njoftime/MerrNjoftim?id="+id.ToString()+"&username="+Session["UserID"].ToString());//
            request.Method = "Get";
            request.KeepAlive = true;
            request.ContentType = "appication/json";
            request.ContentLength = 0;//sepse nxjerr 411 length not specified

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string myResponse = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                myResponse = sr.ReadToEnd();
            }

            // Response.Write(myResponse);
            JavaScriptSerializer js = new JavaScriptSerializer();
            Njoftim njoftimi = js.Deserialize<Njoftim>(myResponse);
            if (Convert.ToBoolean(njoftimi.Status) == true)
            {
                string permbajtje = "";
                permbajtje = "<div class=\"row\">";
                permbajtje += "<div class=\"col-md-1\"></div>";
                permbajtje += "<div class=\"col-md-10\">";
                permbajtje += "<div class=\"white-box\">";
                permbajtje += "<div class=\"row\">";
                permbajtje += "<div class=\"col-md-4\"></div>";
                permbajtje += "<div class=\"col-md-4 text-center\"><i><h4>" + njoftimi.Emer_Kursi + "</h4></i></div>";
                permbajtje += "</div>";
                permbajtje += "<div class=\"row\">";
                permbajtje += "<div class=\"col-md-2\"></div>";
                permbajtje += "<div class=\"col-md-8 text-center\">";
                permbajtje += "<h1 class=\"display-4\">" + njoftimi.Titulli + "</h1></br>";
                permbajtje += "<img width=\"30 px\" height=\"auto\" src=\"../Assets/img/profil.png\" class=\"circle\" /></br>";
                permbajtje += "<span>Nga :<b>" + njoftimi.Emer_Pedagogu + " " + njoftimi.Mbiemer_Pedagogu + "</b> <span>";
                permbajtje += "</div>";
                permbajtje += "</div>";
                permbajtje += "<div class=\"row\">";
                permbajtje += "<div class=\"col-md-1\"></div>";
               // permbajtje += "<div class=\"col-md-1\">Data :</div>";
                DateTime dt = Convert.ToDateTime(njoftimi.Data);
                permbajtje += "<div class=\"col-md-2\">Data :" + dt.ToShortDateString() +"</div>";
                //permbajtje += "<div class=\"col-md-2\">" "</div>";
                //permbajtje+="<div class=\"col-md-6\"></div>";
                //permbajtje += "<div class=\"col-md-6\">Lekioni Nr: <b>" + leksioni.Numer_Leksioni + "</b></div>";
                //permbajtje+="<div class=\"col-md-1\"><b>"+leksioni.Numer_Leksioni+"</b></div>";
                permbajtje += "</div></br></br>";

                permbajtje += "<div class=\"row\">";
                permbajtje += "<div class=\"col-md-1\"></div>";
                permbajtje += "<div class=\"col-md-10\" style=\"font-size:1.5em\">";
                permbajtje += njoftimi.Permbajtja;
                permbajtje += "</div>";
                permbajtje += "</div>";
                permbajtje += "<div class=\"row\">";
                permbajtje += "<div class=\"col-md-9\"></div>";
                permbajtje += "<div class=\"col-md-3\">";
                /*if(leksioni.Link_dokumenti!=null){
                    permbajtje+="<a class=\"btn btn-info\" href=\""+leksioni.Link_dokumenti+"\">Kliko per Dokumentin</a>";
                }*/
                permbajtje += "</div>";
                permbajtje += "</div >";
                permbajtje += "</div>	";
                permbajtje += "</div>";
                permbajtje += "</div>	";		
    
                Permbajtje.InnerHtml = permbajtje;

            }
            
            response.Close(); 
        }



    }
}
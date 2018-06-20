using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;



namespace Portali.Student
{

    public partial class Leksionet : System.Web.UI.Page
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
            if (Request.QueryString["ID"] == null)
            {
                Afisho_Kurset(Session["UserID"].ToString());
            }
            else
            {
                Afisho_Leksione(Request.QueryString["ID"].ToString(), Session["UserID"].ToString());
            }
        }

        protected void Afisho_Kurset(string username)
        {
            string connectionstring = WebConfigurationManager.ConnectionStrings["UniversityConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                string queryprocedure ="FiltroKurse_NgaLeksionet";
                SqlCommand cmd = new SqlCommand(queryprocedure, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string permbajtje = "";
                        permbajtje +="<div class=\"col-sm-4\">";
                        permbajtje +="<a href=\"?ID="+reader["IdKursi"]+"\">";
                        permbajtje += "<div class=\"white-box\">";
                        permbajtje += "<h4>" + reader["Emer_Kursi"]+"</h4>";
                        permbajtje += "</div>";
                        permbajtje += "</a>";
                        permbajtje += "</div>";
                        Permbajtje.InnerHtml = Permbajtje.InnerHtml + permbajtje;
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('"+ex.Message+"')</script>");
                }
            }
        
        }



        protected void Afisho_Leksione(string idkursi, string username)
        {
            string connectionstring = WebConfigurationManager.ConnectionStrings["UniversityConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                string queryprocedure = "FiltroLeksioneKursi";
                SqlCommand cmd = new SqlCommand(queryprocedure, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idkursi", idkursi);
                cmd.Parameters.AddWithValue("@User", username);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            string permbajtje = "";
                            permbajtje += "<div class=\"col-sm-4\">";
                            permbajtje += "<a href=\"Shfaq.aspx?tipi=leksion&ID=" + reader["IdLeksioni"] + "\">";
                            permbajtje += "<div class=\"white-box\">";
                            permbajtje += "<h4>" + reader["Titulli"] + "</h4><br/>";
                            permbajtje += "<h1>" + reader["NumerLeksioni"] + "</h1>";
                            //permbajtje += 
                            permbajtje += "</div>";
                            permbajtje += "</a>";
                            permbajtje += "</div>";
                            Permbajtje.InnerHtml = Permbajtje.InnerHtml + permbajtje;
                        }
                    }
                    else
                    {
                        string permbajtje = "";
                        permbajtje += "<div class=\"col-sm-1\"></div>";
                        permbajtje += "<div class=\"col-sm-10\">";
                        permbajtje += "<div class=\"white-box\" style=\"height:80%\">";
                        permbajtje += "<h4>Nuk Ka leksione per kete Kurs</h4>";
                        permbajtje += "</div><!-- whitebox -->";
                        permbajtje += "</div><!-- col-sm-8 -->";
                        Permbajtje.InnerHtml = Permbajtje.InnerHtml + permbajtje;
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('"+ex.Message+"')</script>");
                }
            }
        }
    }
}
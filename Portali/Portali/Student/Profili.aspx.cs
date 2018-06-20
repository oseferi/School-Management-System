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
    public partial class Profili : System.Web.UI.Page
    {
        protected string querystring = WebConfigurationManager.ConnectionStrings["UniversityConnectionString"].ConnectionString;
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

            //Profil_Picture.ImageUrl = Session["Link_Imazh"].ToString();
            Profil_Picture.ImageUrl = Session["Link_Imazh"].ToString();
            using (SqlConnection conn = new SqlConnection(querystring))
            {
                string querySelect = "SELECT  NrStudenti,SsnStudent,EmerS,MbiemerS,AtesiS,DitelindjeS,GjiniaS,Vendlindje,Vendbanim,EmailS,TelefonS,VitiS,GrupiS,Departament.EmerD as EmerDepartamenti,Dega.EmerD as EmerDege FROM (Student  INNER JOIN Departament ON Student.IdDep = Departament.IdDep) INNER JOIN Dega ON Student.IdDega = Dega.IdDege WHERE SsnStudent =@SSN";
                SqlCommand cmd = new SqlCommand(querySelect, conn);
                cmd.Parameters.AddWithValue("@SSN", Session["SSN"].ToString());

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Emri.InnerText = reader["EmerS"].ToString() + " " + reader["AtesiS"].ToString() + " " + reader["MbiemerS"].ToString();
                        Email.InnerText = reader["EmailS"].ToString();
                        Numer.InnerText = reader["TelefonS"].ToString();
                        Viti.InnerHtml = "<b>Viti </b> " + reader["VitiS"].ToString();
                        Grupi.InnerHtml = "<b>Grupi </b> " + reader["GrupiS"].ToString();
                        Gjinia.InnerText = reader["GjiniaS"].ToString();
                        VendLindje.InnerText = reader["VendLindje"].ToString();
                        VendBanimi.InnerText = reader["Vendbanim"].ToString();
                        Departamenti.InnerHtml = ShkonjaEPareMadhe(reader["EmerDepartamenti"].ToString().ToLower());//Vlera ne db eshte me kapitale, bejm konvertimin
                        Dega.InnerHtml = ShkonjaEPareMadhe(reader["EmerDege"].ToString().ToLower());//Vlera ne db eshte me kapitale bejm konvertimin
                        DateTime dt = reader.GetDateTime(5);//akeson elementin e peste te reader meqenese nuk e akseson dot me emer kolone
                        Datelindje.InnerText = dt.ToShortDateString();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "')</script>");
                }
            }
        }
        protected void Ndrysho(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (IsPostBack)
                {
                    string password = Password.Text;
                    using (SqlConnection connec = new SqlConnection(querystring))
                    {
                        string queryProcedure = "NderroPassword";
                        SqlCommand comman = new SqlCommand(queryProcedure, connec);
                        comman.CommandType = CommandType.StoredProcedure;
                        comman.Parameters.AddWithValue("@pUser", Session["UserID"].ToString());
                        comman.Parameters.AddWithValue("@pPassword", password);
                        comman.Parameters.Add("@responseMessage", SqlDbType.Char, 30);
                        comman.Parameters["@responseMessage"].Direction = ParameterDirection.Output;

                        try
                        {
                            connec.Open();
                            comman.ExecuteNonQuery();
                            string message = (string)comman.Parameters["@responseMessage"].Value;
                            if (message.Replace(" ", "") != "Success")
                            {
                                Response.Write("<script>alert('Ndodhi nje gabim!')</script>");
                                Response.Write("<script>alert('" + message + "')</script>");
                            }
                            else
                            {
                                Response.Write("<script>alert('Passwordi u Nderrua me sukses')</script>");
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<script>alert('" + ex.Message + "')</script>");
                        }
                    }

                }
            }

        }

        /*
         * Funksion qe kthen nje string me shkronjen e pare kapitale

         */
        static string ShkonjaEPareMadhe(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }


    }
}
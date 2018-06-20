using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.IO;


namespace Portali.Pedagog
{
    public partial class Shto_Njoftim : System.Web.UI.Page
    {
        private static string constr = WebConfigurationManager.ConnectionStrings["UniversityConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else if (Session["Roli"].ToString() == "STUDENT")
            {
                Response.Redirect("~/Student/Shto_Njoftim.aspx");
            }
            else if (Session["Roli"].ToString() == "ADMIN")
            {
                Response.Redirect("~/Root/Shto_Njoftime.aspx");
            }
            Gjej_Kurset();
        }

        protected void ClearFields()
        {
            Titull.Text = "";
            Permbajtje.Text = "";
        }

        /*
        *Ketu Behet Terheqja e vlerave nga forma ne front end
        *Behet kontrolli per statusin e njoftimit
        *Behet kontrolli per dokumentin e ngarkuar
        *Nese Kemi dokument te ngarkuar atehere gjenerohet linku i cili i bashkangjitet permbajtjes
        */
        protected void Shto_njoftim(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (IsPostBack)
                {
                    using (SqlConnection connection = new SqlConnection(constr))
                    {
                        string Perdoruesi = Session["UserID"].ToString();
                        string Insert_Procedure = "ShtoNjoftim";
                        string Titull_Njoftimi = Titull.Text.ToString();
                        string Permbajtje_Njoftimi = Permbajtje.Text.ToString();

                        /*------------------------------------------------------------------------------------------------
                        *
                        *Kontrolli i Statusit
                        *---------------------*/
                        bool Status_Njoftimi = false;
                        if (Status.SelectedValue.ToString() == "aktive")
                        {
                            Status_Njoftimi = true;
                        }

                        /*------------------------------------------------------------------------------------------------
                        *
                        *Kontrolli i Dokumentit
                        *---------------------*/
                        string link_dokumenti = "";
                        if (Dokumenti.HasFile)
                        {
                            if (Validim_Skedari(Dokumenti.PostedFile, Dokumenti, Perdoruesi) == "Error Ne Ngarkim")
                            {
                                Response.Write("<script>alert('Ndodhi nje problem gjate ngarkimit te dokumenti, ju lutem provoni perseri pas disa castesh. Nese problemi vazhdon ju lutem kontaktoni administratorin')</script>");
                            }
                            else if (Validim_Skedari(Dokumenti.PostedFile, Dokumenti, Perdoruesi) == "Tipi i dokumentit nuk lejohet")
                            {
                                Response.Write("<script>alert('Tipi i dokumentit nuk lejohet, ju lutem ngarkoni vetem tipet e lejuara te dokumenteve. Nese problemi vazhdon ju lutem kontaktoni administratorin')</script>");
                            }
                            else
                            {
                                link_dokumenti = Validim_Skedari(Dokumenti.PostedFile, Dokumenti, Perdoruesi);
                            }
                            //Response.Write(Validim_Skedari(Dokumenti.PostedFile, Dokumenti, Perdoruesi));
                        }
                        if (link_dokumenti != "")
                        {
                            Permbajtje_Njoftimi += "</br>Klikoni <a href=\"" + link_dokumenti + "\"> ketu </a> per te shkarkuar dokumentin";
                        }


                        /*------------------------------------------------------------------------------------------------
                        *
                        *Veprimet Me Databazen
                        *---------------------*/

                        SqlCommand komande = new SqlCommand(Insert_Procedure, connection);
                        komande.CommandType = CommandType.StoredProcedure;
                        komande.Parameters.AddWithValue("@Titulli", Titull_Njoftimi);
                        komande.Parameters.AddWithValue("@Permbajtja", Permbajtje_Njoftimi);
                        komande.Parameters.Add("@DataN", SqlDbType.DateTime);
                        komande.Parameters["@DataN"].Value = DateTime.Now;
                        komande.Parameters.Add("@Statusi", SqlDbType.Bit);
                        komande.Parameters["@Statusi"].Value = Status_Njoftimi;
                        komande.Parameters.AddWithValue("@SsnP", Session["SSN"].ToString());// Do behet funksion i cili terheq ssn e perdoruesit
                        if (Kursi.SelectedValue != null)
                        {
                            komande.Parameters.AddWithValue("@IdKursi", Kursi.SelectedValue.ToString());
                        }



                        komande.Parameters.Add("@responseMessage", SqlDbType.Char, 30);
                        komande.Parameters["@responseMessage"].Direction = ParameterDirection.Output;
                        connection.Open();
                        komande.ExecuteNonQuery();
                        string pergjigje = komande.Parameters["@responseMessage"].Value.ToString();


                        if (pergjigje.Replace(" ", "") != "Success")//Ne DB eshte ruajtur varchar(250) keshtu qe kthen hapesira boshe
                        {
                            Response.Write("<script>alert('Ndodhi nje problem provoni perseri pas pak castesh ose kontaktoni administratorin e faqes0.');</script>");
                            ClearFields();
                        }
                        else
                        {
                            Response.Write("<script>alert('Njoftimi u shtua me sukses');</script>");
                            ClearFields();
                        }

                    }
                }
                else
                {
                    Response.Write("<script>alert('Ndodhi nje problem provoni perseri pas pak castesh ose kontaktoni administratorin e faqes1.');</script>");
                    ClearFields();
                }
            }
            else
            {
                Response.Write("<script>alert('Ndodhi nje problem provoni perseri pas pak castesh ose kontaktoni administratorin e faqes2.');</script>");
                ClearFields();
            }
        }
        /*------------------------------------------------------------------------------------------------
               *
               *Funksioni i Validimit te skedarit
               *Merr si parameter: kontrollin e skedarit, skedarin dhe username-in)
                *Kontrollon :
                *          -Nese ka skedar te ngarkuar
                *          -Nese skedari ben pjese ne listen tipeve te skedareve te lejuar
                *          -Kerkon nese skedari ekziston tek pathi qe paracaktojm
                *Kthen pergjigje nje string i cili eshte pathi i plote i dokumentit ne direktorin ku eshte ngarkuar;
                *Emerimi i File:
                *     Koha + file extension [formati i kohes : _MMddyyyy_HHmmss]
               *---------------------*/

        private string Validim_Skedari(HttpPostedFile httpPostedFile, System.Web.UI.WebControls.FileUpload FileUpload1, string User)
        {
            string Pathi_Dokumenteve = Server.MapPath("~/Uploads/Njoftime/" + User + "/");
            DirectoryInfo di = Directory.CreateDirectory(Pathi_Dokumenteve);
            bool fileOK = false; //Tregon Vlefshmerin e dokumentit
            string extension = "";


            if (FileUpload1.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                String[] allowedExtensions = { ".doc", ".png", ".jpeg", ".jpg", ".pdf" };


                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                        extension = fileExtension;
                    }
                }
            }

            if (fileOK)
            {
                try
                {
                    string filepath = Pathi_Dokumenteve + DateTime.UtcNow.ToString("_MMddyyyy_HHmmss") + extension;
                    FileUpload1.PostedFile.SaveAs(filepath);
                    return filepath;
                }
                catch (Exception ex)
                {
                    return "Error Ne Ngarkim";
                    //return ex.Message;
                }
            }
            else
            {
                return "Tipi i dokumentit nuk lejohet";
            }


        }


        /*------------------------------------------------------------------------------------------------
        *
        *Funksion Gjej Kurset
         *Gjeneron element liste duke kerkuar ne databaze me ssn e perdoruesit dhe ja shtop drop down listes [Kursi]
        *---------------------*/
        protected void Gjej_Kurset()
        {
            using (SqlConnection lidhje = new SqlConnection(constr))
            {
                string query = "SELECT IdKursi, EmerK FROM Kursi WHERE SsnP = @SSN";
                SqlCommand komande = new SqlCommand(query, lidhje);
                komande.Parameters.AddWithValue("@SSN", Session["SSN"].ToString());
                lidhje.Open();
                SqlDataReader Lexues = komande.ExecuteReader();
                while (Lexues.Read())
                {
                    Kursi.Items.Add(new ListItem(Lexues["EmerK"].ToString(), Lexues["IdKursi"].ToString()));

                }

            }

        }
    }

}
                    
                    
           
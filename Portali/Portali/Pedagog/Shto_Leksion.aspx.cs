using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.IO;//Directory info

namespace Portali.Pedagog
{
    public partial class Shto_Leksion : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                Gjej_Kurset();
                FushLeksion.Style["display"] = "none";//Fsheh Fushat 
            }
        }

        protected void Gjej_Kurset()
        {
            using (SqlConnection lidhje = new SqlConnection(constr))
            {
                string query = "SELECT IdKursi, EmerK FROM KURSI WHERE SsnP = @SSN";
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


        protected void Shfaq_Permbajtjen(object sender, EventArgs e)
        {
            if (Kursi.SelectedValue.ToString() != "null")
            {
                string idKursi = Kursi.SelectedValue.ToString();

                using (SqlConnection con = new SqlConnection(constr))
                {
                    string querySelect = "SELECT TOP 1 NumerLeksioni FROM Leksione WHERE IdKursi =@idkursi ORDER BY NumerLeksioni DESC";
                    SqlCommand cmd = new SqlCommand(querySelect, con);
                    cmd.Parameters.AddWithValue("@idkursi", idKursi);
                    try
                    {
                        con.Open();
                        int numerLeksion = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                        NumerLeksioni.Text = numerLeksion.ToString();
                        FushKursi.Style["display"] = "none";
                        FushLeksion.Style["display"] = "block";
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Na vjen keq! Serveri nuk eshte ne gjendje ti sherbej kerkeses suaj per momentin')</script>");
                        Response.Write("<script>alert('" + ex.Message + "')</script>");
                        //Response.Redirect("Shto_Njoftim.aspx");
                    }

                }
            }
        }

        protected void Posto_Leksion(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (IsPostBack)
                {
                    using (SqlConnection connection = new SqlConnection(constr))
                    {
                        string Perdoruesi = Session["UserID"].ToString();
                        string Insert_Procedure = "ShtoLeksion";
                        string Titull_Leksion = TitullLeksion.Text.ToString();
                        string Permbajtje_Leksion = PermbajtjeLeksioni.Text.ToString();

                        /*------------------------------------------------------------------------------------------------
                        *
                        *Kontrolli i Statusit
                        *---------------------*/
                        bool Status_Leksioni = false;
                        if (Statusi.SelectedValue.ToString() == "aktiv")
                        {
                            Status_Leksioni = true;
                        }                        /*------------------------------------------------------------------------------------------------
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
                                Response.Write("<script>confirm('Tipi i dokumentit nuk lejohet, ju lutem ngarkoni vetem tipet e lejuara te dokumenteve. Nese problemi vazhdon ju lutem kontaktoni administratorin')</script>");
                            }
                            else
                            {
                                link_dokumenti = Validim_Skedari(Dokumenti.PostedFile, Dokumenti, Perdoruesi);
                            }
                            //Response.Write(Validim_Skedari(Dokumenti.PostedFile, Dokumenti, Perdoruesi));
                        }



                        /*------------------------------------------------------------------------------------------------
                        *
                        *Veprimet Me Databazen
                        *---------------------*/

                        SqlCommand komande = new SqlCommand(Insert_Procedure, connection);
                        komande.CommandType = CommandType.StoredProcedure;
                        komande.Parameters.Add("@Numerleksioni", SqlDbType.Int);
                        komande.Parameters["@Numerleksioni"].Value = Convert.ToInt16(NumerLeksioni.Text);
                        komande.Parameters.AddWithValue("@Titulli", Titull_Leksion);
                        komande.Parameters.AddWithValue("@Permbajtja", Permbajtje_Leksion);
                        komande.Parameters.Add("@DataN", SqlDbType.DateTime);
                        komande.Parameters["@DataN"].Value = DateTime.Now;
                        komande.Parameters.Add("@Statusi", SqlDbType.Bit);
                        komande.Parameters["@Statusi"].Value = Status_Leksioni;
                        komande.Parameters.AddWithValue("@SsnP", Session["SSN"].ToString());// Do behet funksion i cili terheq ssn e perdoruesit
                        komande.Parameters.AddWithValue("@IdKursi", Kursi.SelectedValue.ToString());
                        komande.Parameters.AddWithValue("@link_dokumenti", link_dokumenti);

                        komande.Parameters.Add("@responseMessage", SqlDbType.Char, 30);
                        komande.Parameters["@responseMessage"].Direction = ParameterDirection.Output;

                        connection.Open();
                        komande.ExecuteNonQuery();
                        string pergjigje = komande.Parameters["@responseMessage"].Value.ToString();


                        if (pergjigje.Replace(" ", "") != "Success")//Ne DB eshte ruajtur varchar(250) keshtu qe kthen hapesira boshe
                        {
                            Response.Write("<script>alert('Ndodhi nje problem provoni perseri pas pak castesh ose kontaktoni administratorin e faqes0.');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Njoftimi u shtua me sukses');</script>");
                            Pastro_Fushat();
                        }

                    }
                }
                else
                {
                    Response.Write("<script>alert('Ndodhi nje problem provoni perseri pas pak castesh ose kontaktoni administratorin e faqes1.');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Ndodhi nje problem provoni perseri pas pak castesh ose kontaktoni administratorin e faqes2.');</script>");
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
         *Kthen pergjigje nje string i cili eshte link i dokumentit ne direktorin ku eshte ngarkuar;
         *Emerimi i File:
         *     Koha + file extension [formati i kohes : _MMddyyyy_HHmmss]
        *---------------------*/

        private string Validim_Skedari(HttpPostedFile httpPostedFile, System.Web.UI.WebControls.FileUpload FileUpload1, string User)
        {
            string Pathi_Dokumenteve = Server.MapPath("../Uploads/Leksione/" + User + "/");
            DirectoryInfo di = Directory.CreateDirectory(Pathi_Dokumenteve);//Klase qe menaxhon pathet. Nese pathi ekziston nuk en asnje ndryshim ,ne te kundert krijon te gjith folderat e nevojshem
            bool fileOK = false; //Tregon Vlefshmerin e dokumentit.Fillimisht eshte false meqenese nuk eshte kryer validimi
            string extension = "";


            if (FileUpload1.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                String[] allowedExtensions = { ".doc", ".docx", ".xlsx", ".png", ".jpeg", ".jpg", ".pdf", ".zip", ".rar" };


                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                        extension = fileExtension;
                    }
                }


                if (fileOK)
                {
                    try
                    {

                        string name = DateTime.UtcNow.ToString("_MMddyyyy_HHmmss") + extension;//Gjenerohet emri me te cilin do te ruhet skedari
                        string filepath = Pathi_Dokumenteve + name;//Pathi i plote fizik 
                        FileUpload1.PostedFile.SaveAs(filepath);//Behet ngarkimi i skedarit ne pathin e specifikuar
                        string link_skedari = "../Uploads/Leksione/" + User + "/" + name; //Krijohet linku i cili do te perdoret me vone ne front end
                        return link_skedari;
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
            else
            {
                return "";
            }
        }

        protected void Pastro_Fushat()
        {
            TitullLeksion.Text = string.Empty;
            PermbajtjeLeksioni.Text = string.Empty;
            NumerLeksioni.Text = string.Empty;
            Kursi.Items.Clear();
            Kursi.Items.Add(new ListItem("Zgjidh Kursin", "null"));
            FushKursi.Style["display"] = "block";
            FushLeksion.Style["display"] = "none";
            Gjej_Kurset();
        }
    }
}
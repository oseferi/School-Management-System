using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Configuration;
using Portali.Models;
using System.Data.SqlClient;
using System.Web.Http.Description;
using System.Threading.Tasks;
using System.Data;



namespace Portali.Controllers
{
    public class NjoftimeController : ApiController
    {
        [AcceptVerbs("GET")]
        // [AcceptVerbs("GET", "POST")] Do te implementohet ne 2.0
        public IHttpActionResult MerrNjoftime(string username)
        {


            string connectionString = WebConfigurationManager.ConnectionStrings["UniversityConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))//Nese perdorim Using, mbyllja e lidhjes behet automatikisht ne fund te setit te instruksioneve
            {

                string query = "FiltroNjoftime";
                SqlCommand komande = new SqlCommand(query, con);
                komande.CommandType = CommandType.StoredProcedure;
                komande.Parameters.AddWithValue("@User", username);

                try
                {
                    con.Open();// *Behet Hapja e lidhje pasi komanda eshte ndertuar 


                    SqlDataReader Reader = komande.ExecuteReader();//Krijojm Readerin qe do ekzekutoj querin dhe do lexoj vlerat


                    List<Njoftim> Njoftime = new List<Njoftim>(); //Krijojm Arrayn e Njoftimeve

                    while (Reader.Read())
                    {

                        Njoftime.Add(new Njoftim(Reader[0].ToString(),
                                                 Reader[1].ToString(),
                                                 Reader[2].ToString(),
                                                 Reader[3].ToString(),
                                                 Reader[4].ToString(),
                                                 Reader[5].ToString(),
                                                 Reader[6].ToString(),
                                                 Reader[7].ToString(),
                                                 Reader[8].ToString(),
                                                 Reader[9].ToString()));
                    }
                    return this.Ok(Njoftime);
                }
                catch (Exception e)

                {

                    return Content(HttpStatusCode.BadRequest, e);
                }

            }
        }



        [AcceptVerbs("GET")]
        public IHttpActionResult MerrNjoftimetEReja(int id, string username)
        {


            string connectionString = WebConfigurationManager.ConnectionStrings["UniversityConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))//Nese perdorim Using, mbyllja e lidhjes behet automatikisht ne fund te setit te instruksioneve
            {

                string query = "FiltroNjoftimetEReja";
                SqlCommand komande = new SqlCommand(query, con);
                komande.CommandType = CommandType.StoredProcedure;
                komande.Parameters.Add("@Idnjoftimi", SqlDbType.Int);
                komande.Parameters["@Idnjoftimi"].Value = id;
                komande.Parameters.AddWithValue("@User", username);

                try
                {
                    con.Open();// *Behet Hapja e lidhje pasi komanda eshte ndertuar 


                    SqlDataReader Reader = komande.ExecuteReader();//Krijojm Readerin qe do ekzekutoj querin dhe do lexoj vlerat

                    if (!Reader.HasRows)
                    {
                        return this.StatusCode(HttpStatusCode.NotFound);
                    }


                    List<Njoftim> Njoftime = new List<Njoftim>(); //Krijojm Arrayn e Njoftimeve

                    while (Reader.Read())
                    {

                        Njoftime.Add(new Njoftim(Reader[0].ToString(),
                                                 Reader[1].ToString(),
                                                 Reader[2].ToString(),
                                                 Reader[3].ToString(),
                                                 Reader[4].ToString(),
                                                 Reader[5].ToString(),
                                                 Reader[6].ToString(),
                                                 Reader[7].ToString(),
                                                 Reader[8].ToString(),
                                                 Reader[9].ToString()));
                    }
                    return this.Ok(Njoftime);
                }
                catch (Exception e)
                {

                    return Content(HttpStatusCode.BadRequest, e);
                }

            }
        }



        [AcceptVerbs("GET")]
        public IHttpActionResult MerrNjoftim(int id, string username)
        {


            string connectionString = WebConfigurationManager.ConnectionStrings["UniversityConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))//Nese perdorim Using, mbyllja e lidhjes behet automatikisht ne fund te setit te instruksioneve
            {

                string query = "FiltroNjoftim";
                SqlCommand komande = new SqlCommand(query, con);
                komande.CommandType = CommandType.StoredProcedure;
                komande.Parameters.Add("@Idnjoftimi", SqlDbType.Int);
                komande.Parameters["@Idnjoftimi"].Value = id;
                komande.Parameters.AddWithValue("@User", username);

                try
                {
                    con.Open();// *Behet Hapja e lidhje pasi komanda eshte ndertuar 


                    SqlDataReader Reader = komande.ExecuteReader();//Krijojm Readerin qe do ekzekutoj querin dhe do lexoj vlerat
                    if (!Reader.HasRows)
                    {
                        return this.StatusCode(HttpStatusCode.Forbidden);//Do te thot qe nuk ka akses ose nuk ekziston njoftimi
                    }

                    Njoftim Objekt_Njoftimi = new Njoftim();

                    while (Reader.Read())
                    {

                        Objekt_Njoftimi.ID = Reader[0].ToString();
                        Objekt_Njoftimi.Titulli = Reader[1].ToString();
                        Objekt_Njoftimi.Permbajtja = Reader[2].ToString();
                        Objekt_Njoftimi.Data = Reader[3].ToString();
                        Objekt_Njoftimi.Status = Reader[4].ToString();
                        Objekt_Njoftimi.SSN_P = Reader[5].ToString();
                        Objekt_Njoftimi.IdKursi = Reader[6].ToString();
                        Objekt_Njoftimi.Emer_Kursi = Reader[7].ToString();
                        Objekt_Njoftimi.Emer_Pedagogu = Reader[8].ToString();
                        Objekt_Njoftimi.Mbiemer_Pedagogu = Reader[9].ToString();
                    }
                    return this.Ok(Objekt_Njoftimi);
                }
                catch (Exception e)
                {

                    return Content(HttpStatusCode.BadRequest, e);
                }
            }
        }

    }
}

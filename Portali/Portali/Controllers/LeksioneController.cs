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
    public class LeksioneController : ApiController
    {
        [AcceptVerbs("GET")]
        public IHttpActionResult MerrLeksione(string username)
        {
            string connectionstring = WebConfigurationManager.ConnectionStrings["UniversityConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query_procedure = "FiltroLeksione";
                SqlCommand cmd = new SqlCommand(query_procedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@User", username);

                con.Open();
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Leksion> Leksione = new List<Leksion>();//Krijom nje liste objektesh leksione te cilen do ta perdorim per ruajtur te dhenat qe do merrren nga databaza

                    while (reader.Read())
                    {
                        Leksione.Add(new Leksion(reader["IdLeksioni"].ToString(),
                                                 reader["NumerLeksioni"].ToString(),
                                                 reader["Titulli"].ToString(),
                                                 reader["Permbajtja"].ToString(),
                                                 reader["Data"].ToString(),
                                                 reader["Statusi"].ToString(),
                                                 reader["SsnP"].ToString(),
                                                 reader["IdKursi"].ToString(),
                                                 reader["Link_Dokumenti"].ToString(),
                                                 reader["Emer_Kursi"].ToString(),
                                                 reader["Emer_Pedagogu"].ToString(),
                                                 reader["Mbiemer_Pedagogu"].ToString()));
                    }
                    return this.Ok(Leksione);
                }//end try

                catch (Exception ex)
                {
                    return Content(HttpStatusCode.BadRequest, ex);
                }
            }//End using

        }//End Function


        [AcceptVerbs("GET")]
        public IHttpActionResult MerrLeksionetEReja(int Id, string username)
        {
            string connectionstring = WebConfigurationManager.ConnectionStrings["UniversityConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query_procedure = "FiltroLeksionetEReja";
                SqlCommand cmd = new SqlCommand(query_procedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Ideksioni", SqlDbType.Int);
                cmd.Parameters["@Idleksioni"].Value = Id;
                cmd.Parameters.AddWithValue("@User", username);

                con.Open();
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Leksion> Leksione = new List<Leksion>();//Krijom nje liste objektesh leksione te cilen do ta perdorim per ruajtur te dhenat qe do merrren nga databaza

                    while (reader.Read())
                    {
                        Leksione.Add(new Leksion(reader["IdLeksioni"].ToString(),
                                                 reader["NumerLeksioni"].ToString(),
                                                 reader["Titulli"].ToString(),
                                                 reader["Permbajtja"].ToString(),
                                                 reader["Data"].ToString(),
                                                 reader["Statusi"].ToString(),
                                                 reader["SsnP"].ToString(),
                                                 reader["IdKursi"].ToString(),
                                                 reader["Link_Dokumenti"].ToString(),
                                                 reader["Emer_Kursi"].ToString(),
                                                 reader["Emer_Pedagogu"].ToString(),
                                                 reader["Mbiemer_Pedagogu"].ToString()));
                    }
                    return this.Ok(Leksione);
                }//end try

                catch (Exception ex)
                {
                    return Content(HttpStatusCode.BadRequest, ex);
                }
            }//End using
        }



        [AcceptVerbs("GET")]
        public IHttpActionResult MerrLeksion(int Id, string username)
        {
            string connectionstring = WebConfigurationManager.ConnectionStrings["UniversityConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query_procedure = "FiltroLeksion";
                SqlCommand cmd = new SqlCommand(query_procedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Idleksioni", SqlDbType.Int);
                cmd.Parameters["@Idleksioni"].Value = Id;
                cmd.Parameters.AddWithValue("@User", username);

                con.Open();
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    Leksion Leksioni = new Leksion();//Krijom nje objekt leksion te cilin do ta perdorim per ruajtur te dhenat qe do merrren nga databaza

                    while (reader.Read())
                    {
                        Leksioni.ID = reader["IdLeksioni"].ToString();
                        Leksioni.Numer_Leksioni = reader["NumerLeksioni"].ToString();
                        Leksioni.Titulli = reader["Titulli"].ToString();
                        Leksioni.Permbajtja = reader["Permbajtja"].ToString();
                        Leksioni.Data = reader["Data"].ToString();
                        Leksioni.Status = reader["Statusi"].ToString();
                        Leksioni.SSN_P = reader["SsnP"].ToString();
                        Leksioni.IdKursi = reader["IdKursi"].ToString();
                       // Leksioni.Link_dokumenti = reader["Link_Dokumenti"].ToString();
                        Leksioni.Link_dokumenti = reader[8].ToString();
                        Leksioni.Emer_Kursi = reader["Emer_Kursi"].ToString();
                        Leksioni.Emer_Pedagogu = reader["Emer_Pedagogu"].ToString();
                        Leksioni.Mbiemer_Pedagogu = reader["Mbiemer_Pedagogu"].ToString();
                    }
                    return this.Ok(Leksioni);
                }//end try

                catch (Exception ex)
                {
                    return Content(HttpStatusCode.BadRequest, ex);
                }

            }


        }
    }
}


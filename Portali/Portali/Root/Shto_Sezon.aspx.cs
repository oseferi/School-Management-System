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
using System.Collections.Specialized;
using System.Configuration;

namespace Portali.Root
{
    public partial class Shto_Sezon : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["UniversityConnectionString"].ConnectionString;
        SqlDataReader reader;
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
            else if (Session["Roli"].ToString() == "STUDENT")
            {
                Response.Redirect("~/Student/Njoftimet.aspx");
            }

            if (!IsPostBack)
            {
                FillDep();
            }
        }

        protected void FillDep()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "Select Distinct EmerD From Departament";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dep.Items.Add(new ListItem(reader["EmerD"].ToString())); //shton emrat e Departamenteve ne dropdownlist
                    }
                    reader.Close();
                }
                else
                {
                    Response.Write("Nuk ka asnje departament te rregjistruar");
                }
            }
        }
        protected void FillGrid()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                if (dep.SelectedIndex == 0)// nqs nk ka zgjedhur asnje departament do ia shfaqim te gjitha oraret e lendeve ne t kundert vtm ato per departamentin e caktuar 
                {
                    string query = "select S.IdSezoni,O.IdKursi,O.IdPedag,K.EmerK,P.EmerP,P.MbiemerP,O.Salla,O.DataProvimit,O.OraProvimit "
                    + "from Sezoni AS S inner join Orari As O on S.IdSezoni=O.IdSezon inner join Kursi as K on O.IdKursi=K.IdKursi inner join Pedagog As P "
                    + "On K.SsnP=P.SsnPedagog inner join Departament As Dep On P.IdDep= Dep.IdDep "
                    + "where Salla Is NUll AND DataProvimit Is Null AND OraProvimit IS Null";

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    Grid1.DataSource = ds;
                    Grid1.DataBind();
                }
                else
                {
                    string query = "select S.IdSezoni,O.IdKursi,O.IdPedag,K.EmerK,P.EmerP,P.MbiemerP,O.Salla,O.DataProvimit,O.OraProvimit "
                    + "from Sezoni AS S inner join Orari As O on S.IdSezoni=O.IdSezon inner join Kursi as K on O.IdKursi=K.IdKursi inner join Pedagog As P "
                    + "On K.SsnP=P.SsnPedagog inner join Departament As Dep On P.IdDep= Dep.IdDep "
                    + "where Salla Is NUll AND DataProvimit Is Null AND OraProvimit IS Null AND Dep.EmerD=@em";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@em", dep.SelectedValue);
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);//ure midis DataSouce Dhe DataSetit/Ben lidhjen
                    DataSet ds = new DataSet(); //mban rekorde,tabela
                    ad.Fill(ds);
                    Grid1.DataSource = ds;
                    Grid1.DataBind();
                }
            }
        }
        protected void ClearFields()
        {
            IdSez.Text = "";
            EmSez.Text = "";
            TipSez.SelectedIndex = 0;
            SemSez.SelectedIndex = 0;
            VitSez.SelectedIndex = 0;
        }

        protected void ShfaqSezon()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "Select Emri From Sezoni Where Statusi=1";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        season.Text = reader["Emri"].ToString(); //labeli me id season merr emrin e sezonit aktiv
                    }
                    reader.Close();
                }
                else
                {
                    season.Text = "Nuk ka Sezon aktiv per Momentin/Shtoni nje nqs eshte momenti i duhur";
                }

            }
        }

        protected void crt_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                //Ndryshon Statusin e Sezonit Aktiv ne 0
                string query = "Update Sezoni Set Statusi=0 Where Statusi=1";
                SqlCommand cmd = new SqlCommand(query, con);

                //Shton Sezonin Ne DB me status 1,pra esht sezon aktiv
                string query2 = "Insert Into Sezoni(IdSezoni,Emri,Tipi,Semestri,VitiAkademik,Statusi) Values (@ids,@ems,@tip,@sem,@vit,@stat)";
                int status = 1;
                SqlCommand cmd2 = new SqlCommand(query2, con);
                cmd2.Parameters.AddWithValue("@ids", IdSez.Text);
                cmd2.Parameters.AddWithValue("@ems", EmSez.Text);
                cmd2.Parameters.AddWithValue("@tip", TipSez.Text);
                cmd2.Parameters.AddWithValue("@sem", SemSez.Text);
                cmd2.Parameters.AddWithValue("@vit", VitSez.Text);
                cmd2.Parameters.AddWithValue("@stat", status);
                con.Open();

                cmd.ExecuteNonQuery(); // Ekzekuton Komanden e Pare

                if (cmd2.ExecuteNonQuery() > 0) //Nqs Ekzekutohet me sukses shtimi ne tabelen Sezoni
                {



                    //shfaq Lendet E Procedures tek oraret
                    SqlCommand proc = new SqlCommand("Oraret", con);
                    proc.CommandType = CommandType.StoredProcedure;
                    reader = proc.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())   //Lexon vlerat e kthyera nga procedura Oraret(selecti) //Eshte Perdorur MultipleActiveResultSets="true" Ne web.config Per kte gje
                        {
                            string idK = reader["IdKursi"].ToString();
                            string ssnp = reader["SsnPedagog"].ToString();

                            string insertquery = "Insert Into Orari(IdSezon,IdKursi,IdPedag) Values(@ids,@idk,@idp)";  // Shton lendet e kthyera tek tabela Orari
                            SqlCommand ins = new SqlCommand(insertquery, con);
                            ins.Parameters.AddWithValue("@ids", IdSez.Text);
                            ins.Parameters.AddWithValue("@idk", idK);
                            ins.Parameters.AddWithValue("@idp", ssnp);

                            ins.ExecuteNonQuery();
                        }
                        reader.Close();
                    }
                    else
                    {
                        Response.Write("<script language=javascript>alert('ERROR');</script>");
                    }
                }
                else
                {
                    Response.Write("<script language=javascript>alert('ERROR Ne Shtimin e Sezonit');</script>");
                }
                ClearFields();
                ShfaqSezon();
                Oraret.Visible = true;
                //orarbtn.Visible = false;
                ShtoSezon.Visible = false;
            }
        }

        protected void orarbtn_Click(object sender, EventArgs e)
        {

            ShtoSezon.Visible = false;//ben te padukshem div- in e pare
            ShfaqSezon();
            Oraret.Visible = true; //shfaq divin e dyte
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Oraret.Visible = false;
            ShtoSezon.Visible = true;
        }

        protected void dep_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShfaqSezon();
            FillGrid();
            
        }

        protected void Grid1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void Grid1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Grid1.EditIndex = -1;
            FillGrid();
        }

        protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string curr_season = Grid1.DataKeys[e.RowIndex].Values[0].ToString();
            string curr_kurs = Grid1.DataKeys[e.RowIndex].Values[1].ToString();
            string curr_pedag = Grid1.DataKeys[e.RowIndex].Values[2].ToString();

            TextBox Salla = (TextBox)Grid1.Rows[e.RowIndex].FindControl("Salla");
            TextBox Data = (TextBox)Grid1.Rows[e.RowIndex].FindControl("Data");
            TextBox Ora = (TextBox)Grid1.Rows[e.RowIndex].FindControl("Ora");
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "Update Orari Set Salla=@sall,DataProvimit=@dt,OraProvimit=@or Where IdSezon=@ids AND IdKursi=@idk AND IdPedag=@idp";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@sall", Salla.Text);
                cmd.Parameters.AddWithValue("@dt", Data.Text);
                cmd.Parameters.AddWithValue("@or", Ora.Text);
                cmd.Parameters.AddWithValue("@ids", curr_season);
                cmd.Parameters.AddWithValue("@idk", curr_kurs);
                cmd.Parameters.AddWithValue("@idp", curr_pedag);
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    Response.Write("<script language=javascript>alert('U Shtua Me Sukses');</script>");
                    FillGrid();
                }
                else
                {
                    Response.Write("<script language=javascript>alert('Gabim Gjate Ndryshimit e Orarit');</script>");
                }
            }
        }

        protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            Grid1.DataBind();
        }
    }
}
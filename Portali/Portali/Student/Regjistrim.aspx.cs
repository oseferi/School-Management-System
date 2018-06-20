using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;
using System.Web.Configuration;

namespace Portali.Student
{
    public partial class Regjistrim : System.Web.UI.Page
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
            else if (Session["Roli"].ToString() == "ADMIN")
            {
                Response.Redirect("~/Root/Shto_Njoftime.aspx");
            }
            if (!IsPostBack)
            {
                AfishoSezon();
                AfishoGrid();
                LendeTeRegj();  
            }
        }

        protected void AfishoSezon()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string EmerSezoni = "";
                string query = "Select IdSezoni,Emri From Sezoni where Statusi=1";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Session["Sezoni"] = reader["IdSezoni"].ToString(); //.GetValues(0).ToString();
                    EmerSezoni = reader["Emri"].ToString(); //.GetValues(1).ToString();
                }

                if (string.IsNullOrEmpty(EmerSezoni))
                {
                    Response.Write("Nuk ka sezon aktiv për tu regjistruar!");
                }
                else
                {
                    Sezoni.Text = EmerSezoni;
                    Grid1.Visible = true;
                }
            }
        }
        protected void LendeTeRegj()
        {
            string query = "Select EmerK from Kursi As K inner join StudentKursiSezoni As sks on K.IdKursi=sks.IdKursi where sks.SsnStudent=@ssn AND sks.Nota Is Null";
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ssn", Session["SSN"].ToString());
                lndregj.Visible = true;
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lndregj.Text = lndregj.Text + "<br/>" + reader["EmerK"].ToString();
                }
                reader.Close();
            }
        }
        protected void AfishoGrid()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {

                SqlCommand cmd = new SqlCommand("ShfaqLende", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ssn", Session["SSN"].ToString());
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                Grid1.DataSource = ds;
                Grid1.DataBind();
            }
        }



        protected void choose_Click(object sender, EventArgs e)
        {

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            int index = row.RowIndex; 
            string lenda = Grid1.DataKeys[index].Values[0].ToString();
            int kredite = Convert.ToInt32(Grid1.DataKeys[index].Values[1].ToString());
            string dataprovimit = Grid1.DataKeys[index].Values[2].ToString();
            string ora = Grid1.DataKeys[index].Values[3].ToString();
            string salla = Grid1.DataKeys[index].Values[4].ToString();

            using (SqlConnection con = new SqlConnection(cs))
            {

                string query2 = "Select Top 1 IdKursi From Kursi Where EmerK=@em";

                string query = "Select * From StudentKursiSezoni Where SsnStudent=@ssn AND IdKursi=@IdK";

                SqlCommand cmd = new SqlCommand(query2, con);
                cmd.Parameters.AddWithValue("@em", lenda);
                string IdKurs = "";
                SqlCommand cmd2 = new SqlCommand(query, con);

                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    IdKurs = reader.GetValue(0).ToString();
                }
                reader.Close();
                cmd2.Parameters.AddWithValue("@ssn", Session["SSN"].ToString());
                cmd2.Parameters.AddWithValue("@IdK", IdKurs);
                reader = cmd2.ExecuteReader();
                if (reader.HasRows)//Kontrollon nqs ka qen i Rregjistruar me pare dhe i ndryshohen vlerat me aktualet nqs po, dhe nqs jo shtohet ne tabele
                {

                    reader.Close();
                    string updatequery = "Update StudentKursiSezoni Set IdSezoni=@IdS,Nota=@not,DataP=@Dp,OraP=@Op,Vendndodhja=@Vp where SsnStudent=@ssn AND IdKursi=@IdK";
                    SqlCommand cmdup = new SqlCommand(updatequery, con);
                    cmdup.Parameters.AddWithValue("@IdS", Session["Sezoni"].ToString());
                    cmdup.Parameters.AddWithValue("@not", DBNull.Value);
                    cmdup.Parameters.AddWithValue("@Dp", dataprovimit);
                    cmdup.Parameters.AddWithValue("@Op", ora);
                    cmdup.Parameters.AddWithValue("@Vp", salla);
                    cmdup.Parameters.AddWithValue("@ssn", Session["SSN"].ToString()); 
                    cmdup.Parameters.AddWithValue("@IdK", IdKurs);
                    cmdup.ExecuteNonQuery();
                }
                else
                {

                    reader.Close();
                    string insertquery = "Insert Into StudentKursiSezoni(SsnStudent,IdKursi,IdSezoni,DataP,OraP,Vendndodhja) Values (@ssn,@IdK,@IdS,@Dp,@Op,@Vp)";
                    SqlCommand cmdins = new SqlCommand(insertquery, con);
                    cmdins.Parameters.AddWithValue("@ssn", Session["SSN"].ToString());
                    cmdins.Parameters.AddWithValue("@IdK", IdKurs);
                    cmdins.Parameters.AddWithValue("@IdS", Session["Sezoni"].ToString());
                    cmdins.Parameters.AddWithValue("@Dp", dataprovimit);
                    cmdins.Parameters.AddWithValue("@Op", ora);
                    cmdins.Parameters.AddWithValue("@Vp", salla);

                    cmdins.ExecuteNonQuery();
                }
                AfishoGrid();
                LendeTeRegj();
            }
        }
    }
}
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

namespace Portali.Pedagog
{
    
    public partial class Shto_Nota : System.Web.UI.Page
    {
         string cs = ConfigurationManager.ConnectionStrings["UniversityConnectionString"].ConnectionString;
         SqlDataReader reader;
         string idkurs;
        string Semester;
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
                AfishoSezon();
                FillDDL();
            }
        }
        protected void lenda_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void AfishoSezon()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string EmerSezoni = ""; 
                string query = "Select IdSezoni,Emri,Semestri From Sezoni where Statusi=1"; //per te afishuar sezonin aktiv
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Session["Sezoni"] = reader["IdSezoni"].ToString(); //.GetValues(0).ToString();
                    EmerSezoni = reader["Emri"].ToString(); //.GetValues(1).ToString();
                    Semester = reader["Semestri"].ToString();
                }
                reader.Close();
                if (string.IsNullOrEmpty(EmerSezoni))
                {
                    Response.Write("Nuk ka Sezon aktiv per tu regjistruar");
                }
                else
                {
                    Sezon.Text = EmerSezoni;
                    Grid1.Visible = true;
                }
            }
        }
        protected void FillDDL()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {   
                if (Semester != "VJETORE")
                {
                    string query = "Select EmerK From Kursi Where SsnP=@ssn And Semestri=@sem";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ssn", Session["SSN"].ToString());
                    cmd.Parameters.AddWithValue("@sem", Semester);
                    con.Open();
                    reader = cmd.ExecuteReader();
                }
                else
                {
                    string query = "Select EmerK From Kursi Where SsnP=@ssn";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ssn", Session["SSN"].ToString());
                    con.Open();
                    reader = cmd.ExecuteReader();
                }
                //con.Open();
                //reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lenda.Items.Add(new ListItem(reader["EmerK"].ToString())); //shton emrat e kurseve ne dropdownlist
                    }
                    reader.Close();
                }
                else
                {
                    Response.Write("Nuk jeni titullar ne asnje lende");
                }
            }
        }
        protected void FillGrid()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "select Distinct s.SsnStudent,EmerS,MbiemerS,Nota from Kursi As k inner join StudentKursiSezoni as sks on k.IdKursi=sks.IdKursi inner join Student as s on sks.SsnStudent=s.SsnStudent where sks.IdSezoni=@sez AND EmerK=@em AND Nota Is Null And SsnP=@ssn";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@sez", Session["Sezoni"].ToString());
                cmd.Parameters.AddWithValue("@ssn", Session["SSN"].ToString()); 
                cmd.Parameters.AddWithValue("@em", lenda.SelectedValue);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                Grid1.DataSource = ds;
                Grid1.DataBind();
            }
        }

        protected void Grid1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Grid1.EditIndex = -1;
            FillGrid();
        }

        protected void Grid1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ssn = Grid1.DataKeys[e.RowIndex].Values[0].ToString();
            TextBox nota = (TextBox)Grid1.Rows[e.RowIndex].FindControl("nota");
            using (SqlConnection con = new SqlConnection(cs))
            {
                string selectquery = "Select * From Kursi Where EmerK=@em"; //select IdKursi ? 
                SqlCommand selcmd = new SqlCommand(selectquery, con);
                selcmd.Parameters.AddWithValue("@em", lenda.SelectedValue);
                con.Open();
                reader = selcmd.ExecuteReader();
                while (reader.Read()) //if(reader.HasRows nuk funksiononte
                {
                    idkurs = reader["IdKursi"].ToString();

                }
                reader.Close();
                string query = "Update StudentKursiSezoni Set Nota=@not where SsnStudent=@ssn And IdSezoni=@ids AND IdKursi=@idk";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@not", nota.Text);
                cmd.Parameters.AddWithValue("@ssn", ssn);
                cmd.Parameters.AddWithValue("@ids", Session["Sezoni"].ToString());
                cmd.Parameters.AddWithValue("@idk", idkurs);

                cmd.ExecuteNonQuery();
                FillGrid();
            }

        }

    }
}
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

namespace Portali.Student
{
    public partial class Rezultatet : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["UniversityConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else if (Session["Roli"].ToString()=="PEDAGOG")
            {
                Response.Redirect("~/Pedagog/Shto_Njoftim.aspx");
            }
            else if (Session["Roli"].ToString()== "ADMIN")
            {
                Response.Redirect("~/Root/Shto_Njoftime.aspx");
            }
            AfishoGrid();
        }
        protected void AfishoGrid()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "Select EmerK AS LËNDA, NrKredite AS KREDITE  ,Convert(varchar, DataP) AS DATA, OraP AS ORA, Nota AS NOTA From Kursi As K inner join StudentKursiSezoni As Sk On K.IdKursi=Sk.IdKursi where SsnStudent =@ssn And Nota IS NOT NULL Order By DataP Desc";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ssn", Session["SSN"].ToString());
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
         }
        protected void Btn1_Mes(object sender, EventArgs e)
        {
            using (SqlConnection noc = new SqlConnection(cs))
            { 
            string query1 = "SELECT AVG(CAST(Nota AS Decimal(4,2))) From StudentKursiSezoni Where SsnStudent =@ssn And Nota IS NOT NULL And Nota>4";
            SqlCommand command = new SqlCommand(query1, noc);
            noc.Open();
                command.Parameters.AddWithValue("@ssn", Session["SSN"].ToString());
                double mes = Convert.ToDouble(command.ExecuteScalar());
            Label1.Text = "● Mesatarja juaj është: " + mes + ".";
            noc.Close();
            }
        }
        protected void Btn2_Krd(object sender, EventArgs e) {
            using (SqlConnection ocn = new SqlConnection(cs))
            {
                string query2 = "SELECT SUM(NrKredite) From StudentKursiSezoni AS SKS INNER JOIN Kursi AS K ON K.IdKursi=SKS.IdKursi Where SsnStudent =@ssn And Nota IS NOT NULL And Nota>4";
                SqlCommand cmd = new SqlCommand(query2, ocn);
                ocn.Open();
                cmd.Parameters.AddWithValue("@ssn", Session["SSN"].ToString());
                int nrk = Convert.ToInt32(cmd.ExecuteScalar());
                Label2.Text = "● Ju keni gjithsej " + nrk + " kredite.";
                ocn.Close();
            }
        }
        protected void Btn3_Ngl(object sender, EventArgs e) {
            using (SqlConnection cno = new SqlConnection(cs))
            {
                string query3 = "SELECT COUNT(DISTINCT EmerK) From StudentKursiSezoni AS SKS INNER JOIN Kursi AS K ON K.IdKursi=SKS.IdKursi Where SsnStudent =@ssn And Nota IS NOT NULL And Nota=4";
                SqlCommand com = new SqlCommand(query3, cno);
                cno.Open();
                com.Parameters.AddWithValue("@ssn", Session["SSN"].ToString());
                int nrmb = Convert.ToInt32(com.ExecuteScalar());
                Label3.Text = "● Ju keni gjithsej " + nrmb + " lëndë të mbartura.";
                cno.Close();
            }
        }
    }
}

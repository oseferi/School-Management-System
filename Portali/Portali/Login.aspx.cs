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

namespace Portali
{
    public partial class Login : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["UniversityConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Roli"] != null && Session["UserId"]!=null) //nese eshte i loguar
            {

                if (Session["Roli"].ToString() == "STUDENT")
                {
                    Response.Redirect("~/Student/Njoftimet.aspx");
                }
                else if (Session["Roli"].ToString() == "PEDAGOG")
                {
                    Response.Redirect("~/Pedagog/Shto_Njoftim.aspx");
                }
                else if (Session["Roli"].ToString() == "ADMIN")
                {
                    Response.Redirect("~/Root/Shto_Njoftime.aspx");
                }
            }
        }
        protected void ClearFields()
        {
            q1.Text = "";
            q2.Text = "";
        }

        protected void Button1_Click(object sender, EventArgs e) //metoda per thirrjen e procedures login user
        {
            string user = q1.Text;
            string pass = q2.Text;
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("loginUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUser", user);
            cmd.Parameters.AddWithValue("@pPassword", pass);
            cmd.Parameters.Add("@responseMessage", SqlDbType.Char, 30);
            cmd.Parameters["@responseMessage"].Direction = ParameterDirection.Output;
            string query = "Select Ssn,Roli,Link_Imazh From Users where Username=@un";
            SqlCommand select = new SqlCommand(query, con);
            select.Parameters.AddWithValue("@un", user);
            con.Open();
            cmd.ExecuteNonQuery();
            string message = (string)cmd.Parameters["@responseMessage"].Value;

            if (message == "User authentication succesful ")
            {
                SqlDataReader reader = select.ExecuteReader();

                Session["UserID"] = user;
                while (reader.Read()) //krijimi i sesioneve
                {
                    Session["SSN"] = reader["Ssn"];
                    Session["Roli"] = reader.GetValue(1);
                    Session["Link_Imazh"] = reader["Link_Imazh"];
                }
                if (Session["Roli"].ToString() == "STUDENT")
                {
                    Response.Redirect("~/Student/Njoftimet.aspx");
                }
                else if (Session["Roli"].ToString() == "PEDAGOG")
                {
                    Response.Redirect("~/Pedagog/Shto_Njoftim.aspx");
                }
                else if (Session["Roli"].ToString() == "ADMIN")
                {
                    Response.Redirect("~/Root/Shto_Njoftime.aspx");
                }
                con.Close();
            }
            else
            {
                con.Close();
                ClearFields();
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('KUJDES! Plotësoni saktë kredencialet tuaja!')</script>");
            }

        }
    }
}
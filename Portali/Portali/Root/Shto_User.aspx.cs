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
    public partial class Shto_User : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["UniversityConnectionString"].ConnectionString;
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
            passwordi.Attributes["type"] = "password";
        }
 
        protected void ClearFields()
        {
            un.Text = "";
            passwordi.Text = "";
            ssn.Text = "";
            emp.Text = "";
            mbp.Text = "";
            atp.Text = "";
            dtp.Text = "";
            emailp.Text = "";
            nrp.Text = "";
            titullp.Text = "";
            emers.Text = "";
            mbs.Text = "";
            ats.Text = "";
            dts.Text = "";
            vds.Text = "";
            ems.Text = "";
            nrs.Text = "";
            vit.Text = "";
            gr.Text = "";
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            usr.Visible = false;

            if (rol.SelectedValue == "PEDAGOG")
            {
                pedag.Visible = true;

            }
            else if(rol.SelectedValue=="STUDENT")
            {
                student.Visible = true;
            }
            else //Shtohet Admini ne db
            {
                if(String.IsNullOrEmpty(un.Text) && String.IsNullOrEmpty(ssn.Text))
                {
                    ClearFields();
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('KUJDES! Plotësoni të gjitha fushat!')</script>");
                    usr.Visible = true;
                }
                else
                {
                    //string msg = AddUser();
                    SqlConnection con = new SqlConnection(cs);
                    SqlCommand cmd = new SqlCommand("ShtoUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pUser", un.Text);
                    cmd.Parameters.AddWithValue("@pPassword",passwordi.Text);
                    cmd.Parameters.AddWithValue("@pSSN", ssn.Text);
                    cmd.Parameters.AddWithValue("@pRoli", rol.SelectedValue);
                    cmd.Parameters.Add("@responseMessage", SqlDbType.Char,7);
                    cmd.Parameters["@responseMessage"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    string message = (string)cmd.Parameters["@responseMessage"].Value;
                    if(message == "Success")
                    {
                    
                        Response.Redirect("LoginPage.aspx");
                        con.Close();
                    }
                    else
                    {
                        con.Close();     
                        ClearFields();
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('KUJDES! Përdoruesi është i regjistruar njëherë!')</script>");
                        usr.Visible = true;
                    }
                }
            }
        }//Fund funksioni

        protected void Button1_Click(object sender, EventArgs e) //shton ne tab users dhe pedag
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlTransaction transaction;
            transaction=con.BeginTransaction("ShtimTab");
         
            try
            {
               // SqlCommand cmd1 = con.CreateCommand();   
                SqlCommand cmd1 = new SqlCommand("ShtoUser", con);
                SqlCommand cmd2 = new SqlCommand("ShtoPedagog", con);
               
                cmd1.CommandType = CommandType.StoredProcedure;
             
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd1.Transaction = transaction;
                cmd2.Transaction = transaction;

                cmd1.Parameters.AddWithValue("@pUser", un.Text);
                cmd1.Parameters.AddWithValue("@pPassword",passwordi.Text);
                cmd1.Parameters.AddWithValue("@pSSN", ssn.Text);
                cmd1.Parameters.AddWithValue("@pRoli", rol.SelectedValue);
                cmd1.Parameters.Add("@responseMessage", SqlDbType.Char, 7);
                cmd1.Parameters["@responseMessage"].Direction = ParameterDirection.Output;
                
                
                cmd2.Parameters.AddWithValue("@pSsnPedagog", ssn.Text);
                cmd2.Parameters.AddWithValue("@pEmerP", emp.Text);
                cmd2.Parameters.AddWithValue("@pMbiemerP", mbp.Text);
                cmd2.Parameters.AddWithValue("@pAtesiP", atp.Text);
                cmd2.Parameters.AddWithValue("@pDitelindjeP", dtp.Text);
                if (F.Checked)
                {
                    cmd2.Parameters.AddWithValue("@pGjiniaP", F.Text);
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@pGjiniaP", M.Text);
                }

                cmd2.Parameters.AddWithValue("@pEmailP", emailp.Text);
                cmd2.Parameters.AddWithValue("@pTelefonP", nrp.Text);
                cmd2.Parameters.AddWithValue("@pTitulli", titullp.Text);
                var departament="";
                switch (dep.SelectedValue)
                {
                    case "DEPARTAMENTI I BIOLOGJISË":
                        departament="BI0404";
                        break;
                    case "DEPARTAMENTI I INFORMATIKËS":
                        departament="IT0101";
                            break;
                    case "DEPARTAMENTI I KIMISË INDUSTRIALE":
                        departament="KI0202";
                        break;
                    case "DEPARTAMENTI I MATEMATIKËS":
                        departament="MA0303";
                        break;
                }
                cmd2.Parameters.AddWithValue("@pIdDep", departament);
                cmd2.Parameters.Add("@responseMessage", SqlDbType.Char, 7);
                cmd2.Parameters["@responseMessage"].Direction = ParameterDirection.Output;

                
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                string msg1 = (string)cmd1.Parameters["@responseMessage"].Value;
                string msg2 = (string)cmd2.Parameters["@responseMessage"].Value;

               if ((msg1 == "Success") && (msg2 == "Success"))
               {
                   transaction.Commit();
               }
              
            } //end try
           catch(SqlException sqlEx)
            {
                transaction.Rollback();
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Shtimi i pedagogut në bazën e të dhënave dështoi! Provoni përsëri!')</script>");
                
            }
           finally
            {
                con.Close();
                ClearFields();
                Response.Redirect("~/Root/Shto_User.aspx");
                System.Threading.Thread.Sleep(3000);
                
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlTransaction transaction;
            transaction=con.BeginTransaction("ShtimStud");

            try
            {
                // SqlCommand cmd1 = con.CreateCommand();   
                SqlCommand cmd1 = new SqlCommand("ShtoUser", con);
                SqlCommand cmd2 = new SqlCommand("ShtoStudent", con);

                cmd1.CommandType = CommandType.StoredProcedure;
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd1.Transaction = transaction;
                cmd2.Transaction = transaction;

                cmd1.Parameters.AddWithValue("@pUser", un.Text);
                cmd1.Parameters.AddWithValue("@pPassword", passwordi.Text);
                cmd1.Parameters.AddWithValue("@pSSN", ssn.Text);
                cmd1.Parameters.AddWithValue("@pRoli", rol.SelectedValue);
                cmd1.Parameters.Add("@responseMessage", SqlDbType.Char, 7);
                cmd1.Parameters["@responseMessage"].Direction = ParameterDirection.Output;

                cmd2.Parameters.AddWithValue("@sSsnStudent", ssn.Text);
                cmd2.Parameters.AddWithValue("@sEmerS", emers.Text);
                cmd2.Parameters.AddWithValue("@sMbiemerS", mbs.Text);
                cmd2.Parameters.AddWithValue("@sAtesiS", ats.Text);
                cmd2.Parameters.AddWithValue("@sDitelindjeS", dts.Text);

                if (Fe.Checked)
                {
                    cmd2.Parameters.AddWithValue("@sGjiniaS", Fe.Text);
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@sGjiniaS", Ma.Text);
                }
                cmd2.Parameters.AddWithValue("@sDateRegjistrimi", drs.Text);
                cmd2.Parameters.AddWithValue("@sVendlindje", vds.Text);
                cmd2.Parameters.AddWithValue("@sVendbanim", vbs.Text);
                cmd2.Parameters.AddWithValue("@sEmailS", ems.Text);
                cmd2.Parameters.AddWithValue("@sTelefonS", nrs.Text);
                cmd2.Parameters.AddWithValue("@sVitiS", vit.Text);
                cmd2.Parameters.AddWithValue("@sGrupiS", gr.Text);

                var departaments = "";
                switch (deps.SelectedValue)
                {
                    case "DEPARTAMENTI I BIOLOGJISË":
                        departaments = "BI0404";
                        break;
                    case "DEPARTAMENTI I INFORMATIKËS":
                        departaments = "IT0101";
                        break;
                    case "DEPARTAMENTI I KIMISË INDUSTRIALE":
                        departaments = "KI0202";
                        break;
                    case "DEPARTAMENTI I MATEMATIKËS":
                        departaments = "MA0303";
                        break;
                }
                cmd2.Parameters.AddWithValue("@sIdDep", departaments);

                var deg = "";
                switch (dega.SelectedValue)
                {
                    case "BACHELOR NË BIOLOGJI":
                        deg = "BIBBI0101";
                        break;
                    case "BACHELOR NË INFORMATIKË":
                        deg = "ITBIN0101";
                        break;
                    case "BACHELOR NË TEKNOLOGJI INFORMACIONI DHE KOMUNIKIMI":
                        deg = "ITBTI0202";
                        break;
                    case "BACHELOR NË KIMI DHE TEKNOLOGJI USHQIMORE":
                        deg = "KIBKT0202";
                        break;
                    case "MASTER I SHKENCAVE NË KIMI INDUSTRIALE":
                        deg = "KIMKI0101";
                        break;
                    case "MASTER PROFESIONAL NË MATEMATIKË TË APLIKUAR":
                        deg = "MAMMA0101";
                        break;
                }
                cmd2.Parameters.AddWithValue("@sIdDega", deg);

                cmd2.Parameters.Add("@responseMessage", SqlDbType.Char, 7);
                cmd2.Parameters["@responseMessage"].Direction = ParameterDirection.Output;

                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                string msg1 = (string)cmd1.Parameters["@responseMessage"].Value;
                string msg2 = (string)cmd2.Parameters["@responseMessage"].Value;

                if ((msg1 == "Success") && (msg2 == "Success"))
                {
                    transaction.Commit();
                }
            } //end try
            catch (SqlException sqlEx)
            {
                transaction.Rollback();
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Shtimi i studentit në bazën e të dhënave dështoi! Provoni përsëri!')</script>");
            }
            finally
            {
                con.Close();
                ClearFields();
                Response.Redirect("~/Root/ShtoUser.aspx");
                System.Threading.Thread.Sleep(3000);

            }
        }
        protected void AfishoGrid()
        {
            SqlConnection con = new SqlConnection(cs);
            if (DDL1.SelectedValue == "Student")
            {
                SqlCommand cmd = new SqlCommand("SelectStud", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@em", txt1.Text);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                Grid2.DataSource = ds;
                Grid2.DataBind();
            }
            else
            {
                SqlCommand cmd = new SqlCommand("SelectPedag", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@em", txt1.Text);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                Grid1.DataSource = ds;
                Grid1.DataBind();
            }

        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            usr.Visible = false;
            Grid1.Visible = false;
            Grid2.Visible = false;
            if (DDL1.SelectedValue == "Student")
            {
                Grid2.Visible = true;

            }
            else
            {
                Grid1.Visible = true;
            }
            AfishoGrid();
        }

        protected void Grid2_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void Grid2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from StudentKursi where SsnStudent=@sn;delete from StudentKursiSezoni where SsnStudent=@sn;Delete From Student Where SsnStudent=@sn;Delete From Users Where Ssn=@sn";
            //String query = "Delete From Users Where Ssn=@sn";
            SqlCommand cmd = new SqlCommand(query, con);
            //SqlCommand cmd2 = new SqlCommand(query2, con);
            //string idp = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string ssn = Grid2.DataKeys[e.RowIndex].Values[1].ToString();

            cmd.Parameters.AddWithValue("@sn", ssn);
            // cmd2.Parameters.AddWithValue("@sn", ssn);
            con.Open();
            // cmd2.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            con.Close();
            AfishoGrid();
        }

        protected void Grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "Delete From Users Where Ssn=@sn";
            // String query2 = "Delete From Pedagog Where SsnPedagog=@sn";
            SqlCommand cmd = new SqlCommand(query, con);
            // SqlCommand cmd2 = new SqlCommand(query2, con);
            //string idp = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string ssn = Grid1.DataKeys[e.RowIndex].Values[1].ToString();

            cmd.Parameters.AddWithValue("@sn", ssn);
            // cmd2.Parameters.AddWithValue("@sn", ssn);
            con.Open();
            //cmd2.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            con.Close();
            AfishoGrid();
        }

        protected void Grid1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //GridViewRow row = (GridViewRow)Grid1.Rows[e.RowIndex];
            TextBox USERNAME = (TextBox)Grid1.Rows[e.RowIndex].FindControl("USERNAME");
            TextBox SSN = (TextBox)Grid1.Rows[e.RowIndex].FindControl("SSN");
            TextBox EMRI = (TextBox)Grid1.Rows[e.RowIndex].FindControl("EMRI");
            TextBox ATESIA = (TextBox)Grid1.Rows[e.RowIndex].FindControl("ATESIA");
            TextBox MBIEMRI = (TextBox)Grid1.Rows[e.RowIndex].FindControl("MBIEMRI");
            TextBox EMAIL = (TextBox)Grid1.Rows[e.RowIndex].FindControl("EMAIL");
            TextBox NRKONTAKTI = (TextBox)Grid1.Rows[e.RowIndex].FindControl("NRKONTAKTI");
            TextBox TITULLI = (TextBox)Grid1.Rows[e.RowIndex].FindControl("TITULLI");
            TextBox DEPARTAMENTI = (TextBox)Grid1.Rows[e.RowIndex].FindControl("DEPARTAMENTI");
            //int index = Convert.ToInt32(Grid1.EditIndex.ToString());
            //string usrnma = Grid1[index].Cells["USERNAME"].Value.ToString();
            // string test = USERNAME.Text;
            string username = Grid1.DataKeys[e.RowIndex].Values[0].ToString();
            string ssn = Grid1.DataKeys[e.RowIndex].Values[1].ToString();
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlTransaction transaction;
            transaction = con.BeginTransaction("UpdtPedag");
            try
            {
                string query = "Update Users Set Username=@un,Ssn=@snn Where Username=@usr";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Transaction = transaction;

                string query2 = "Update Pedagog Set SsnPedagog=@snn,EmerP=@em,MbiemerP=@mb,AtesiP=@at,EmailP=@email,TelefonP=@tel,Titulli=@tit,IdDep=@dep Where SsnPedagog=@ssn";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                cmd2.CommandType = CommandType.Text;
                cmd2.Transaction = transaction;

                cmd.Parameters.AddWithValue("@un", USERNAME.Text);
                cmd.Parameters.AddWithValue("@snn", SSN.Text);
                cmd.Parameters.AddWithValue("@usr", username);

                cmd2.Parameters.AddWithValue("@snn", SSN.Text);
                cmd2.Parameters.AddWithValue("@em", EMRI.Text);
                cmd2.Parameters.AddWithValue("@mb", MBIEMRI.Text);
                cmd2.Parameters.AddWithValue("@at", ATESIA.Text);
                cmd2.Parameters.AddWithValue("@email", EMAIL.Text);
                cmd2.Parameters.AddWithValue("@tel", NRKONTAKTI.Text);
                cmd2.Parameters.AddWithValue("@tit", TITULLI.Text);

                string departaments = DEPARTAMENTI.Text;
                switch (departaments)
                {
                    case "DEPARTAMENTI I BIOLOGJISË":
                        departaments = "BI0404";
                        break;
                    case "DEPARTAMENTI I INFORMATIKËS":
                        departaments = "IT0101";
                        break;
                    case "DEPARTAMENTI I KIMISË INDUSTRIALE":
                        departaments = "KI0202";
                        break;
                    case "DEPARTAMENTI I MATEMATIKËS":
                        departaments = "MA0303";
                        break;

                }
                cmd2.Parameters.AddWithValue("@dep", departaments);
                cmd2.Parameters.AddWithValue("@ssn", ssn);

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                transaction.Commit();

                //if (cmd.ExecuteNonQuery()>0 && cmd2.ExecuteNonQuery() > 0)
                //{
                //    transaction.Commit();
                //}
            }
            catch (SqlException sqlEx)
            {
                transaction.Rollback();
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Modifikimi në bazën e të dhënave dështoi! Provoni përsëri!')</script>");

            }
            finally
            {
                con.Close();
                AfishoGrid();
                //Response.Redirect("~/Root/ShtoUser.aspx");

            }
        }

        protected void Grid2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox USERNAME = (TextBox)Grid2.Rows[e.RowIndex].FindControl("USERNAMES");
            TextBox SSN = (TextBox)Grid2.Rows[e.RowIndex].FindControl("SSNS");
            TextBox EMRI = (TextBox)Grid2.Rows[e.RowIndex].FindControl("EMRIS");
            TextBox ATESIA = (TextBox)Grid2.Rows[e.RowIndex].FindControl("ATESIAS");
            TextBox MBIEMRI = (TextBox)Grid2.Rows[e.RowIndex].FindControl("MBIEMRIS");
            TextBox EMAIL = (TextBox)Grid2.Rows[e.RowIndex].FindControl("EMAILS");
            TextBox VITI = (TextBox)Grid2.Rows[e.RowIndex].FindControl("VITIS");
            TextBox GRUPI = (TextBox)Grid2.Rows[e.RowIndex].FindControl("GRUPIS");
            TextBox DEPARTAMENTI = (TextBox)Grid2.Rows[e.RowIndex].FindControl("DEPARTAMENTIS");

            string username = Grid2.DataKeys[e.RowIndex].Values[0].ToString();
            string ssn = Grid2.DataKeys[e.RowIndex].Values[1].ToString();
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlTransaction transaction;
            transaction = con.BeginTransaction("UpdtStudent");

            try
            {
                string query = "Update Users Set Username=@un,Ssn=@sn Where Username=@usr";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Transaction = transaction;

                string query2 = "Update Student Set SsnStudent=@sn,EmerS=@em,MbiemerS=@mb,AtesiS=@at,EmailS=@email,VitiS=@vs,GrupiS=@gs,IdDep=@dep Where SsnStudent=@ssn";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                cmd2.CommandType = CommandType.Text;
                cmd2.Transaction = transaction;

                cmd.Parameters.AddWithValue("@un", USERNAME.Text);
                cmd.Parameters.AddWithValue("@sn", SSN.Text);
                cmd.Parameters.AddWithValue("@usr", username);

                cmd2.Parameters.AddWithValue("@sn", SSN.Text);
                cmd2.Parameters.AddWithValue("@em", EMRI.Text);
                cmd2.Parameters.AddWithValue("@mb", MBIEMRI.Text);
                cmd2.Parameters.AddWithValue("@at", ATESIA.Text);
                cmd2.Parameters.AddWithValue("@email", EMAIL.Text);
                cmd2.Parameters.AddWithValue("@vs", VITI.Text);
                cmd2.Parameters.AddWithValue("@gs", GRUPI.Text);

                string departaments = DEPARTAMENTI.Text;
                switch (departaments)
                {
                    case "DEPARTAMENTI I BIOLOGJISË":
                        departaments = "BI0404";
                        break;
                    case "DEPARTAMENTI I INFORMATIKËS":
                        departaments = "IT0101";
                        break;
                    case "DEPARTAMENTI I KIMISË INDUSTRIALE":
                        departaments = "KI0202";
                        break;
                    case "DEPARTAMENTI I MATEMATIKËS":
                        departaments = "MA0303";
                        break;

                }
                cmd2.Parameters.AddWithValue("@dep", departaments);
                cmd2.Parameters.AddWithValue("@ssn", ssn);

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                transaction.Commit();

                //if (cmd.ExecuteNonQuery()>0 && cmd2.ExecuteNonQuery() > 0)
                //{
                //    transaction.Commit();
                //}
            }
            catch (SqlException sqlEx)
            {
                transaction.Rollback();
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Modifikimi në bazën e të dhënave dështoi! Provoni përsëri!')</script>");
            }
            finally
            {
                con.Close();
                AfishoGrid();
                //Response.Redirect("~/Root/Shto_User.aspx");

            }
        }


        protected void Grid1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Grid1.EditIndex = -1;
            AfishoGrid();
        }

        protected void Grid2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Grid2.EditIndex = -1;
            AfishoGrid();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Grid1.Visible = false;
            Grid2.Visible = false;
            usr.Visible = true;
        }
    }
        
}
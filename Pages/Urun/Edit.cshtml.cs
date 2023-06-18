using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Verifinalodev.Pages.Musteri;

namespace Verifinalodev.Pages.Urun
{
    public class Index1Model : PageModel
    {
        public UrunInfo ListInfo = new UrunInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String urunid = Request.Query["urunid"];
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "SELECT * FROM Urun WHERE urunid=@urunid";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@urunid", urunid);
                        using SqlDataReader reader = command.ExecuteReader();
                        {
                            if (reader.Read())
                            {
                                ListInfo.urunid = "" + reader.GetInt32(0);
                                ListInfo.uname = reader.GetString(1);
                                ListInfo.urunstok = reader.GetString(2);
                                ListInfo.urunkat = reader.GetString(3);
                                ListInfo.urunkatid = reader.GetString(4);
                                ListInfo.uruntanim = reader.GetString(5);
                                ListInfo.uruntarih = reader.GetDateTime(6).ToString();
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            ListInfo.urunid = Request.Form["mid"];
            ListInfo.uname = Request.Form["mname"];
            ListInfo.urunstok = Request.Form["email"];
            ListInfo.urunkat = Request.Form["email"];
            ListInfo.urunkatid = Request.Form["email"];
            ListInfo.uruntanim = Request.Form["phone"];
            ListInfo.uruntarih = Request.Form["madres"];

            if (ListInfo.mname == "" | ListInfo.mid == "" | ListInfo.phone == "" | ListInfo.madres == "")
            {
                errorMessage = "Veriler boþ olamaz";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Musteri" +
                        "SET mname=@mname, email=@email,"
                        + "phone=@phone, madres=@madres WHERE mid=@mid";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@mid", UrunInfo.mid);
                        command.Parameters.AddWithValue("@mname", UrunInfo.mname);
                        command.Parameters.AddWithValue("@email", UrunInfo.email);
                        command.Parameters.AddWithValue("@phone", UrunInfo.phone);
                        command.Parameters.AddWithValue("@madres", UrunInfo.madres);
                        command.ExecuteNonQuery();
                        successMessage = "Urun bilgileri güncellendi";
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Urun/Index");
        }
    }
}

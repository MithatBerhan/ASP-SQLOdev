using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Verifinalodev.Pages.Musteri
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String mid = Request.Query["mid"];
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "SELECT * FROM Musteri WHERE mid=@mid";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@mid", mid);
                        using SqlDataReader reader = command.ExecuteReader();
                        {
                            if (reader.Read())
                            {
                                clientInfo.mid = "" + reader.GetInt32(0);
                                clientInfo.mname = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.tarih = reader.GetDateTime(4).ToString();
                                clientInfo.madres = reader.GetString(5);
                            }
                        }
                    }

                }   

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        { 
            clientInfo.mid = Request.Form["mid"];
            clientInfo.mname = Request.Form["mname"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.madres = Request.Form["madres"];

            if(clientInfo.mname == "" | clientInfo.mid == "" | clientInfo.phone == "" | clientInfo.madres == ""  )
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
                        command.Parameters.AddWithValue("@mid", clientInfo.mid);
                        command.Parameters.AddWithValue("@mname", clientInfo.mname);
                        command.Parameters.AddWithValue("@email",clientInfo.email);
                        command.Parameters.AddWithValue("@phone",clientInfo.phone);
                        command.Parameters.AddWithValue("@madres", clientInfo.madres);
                        command.ExecuteNonQuery();
                        successMessage = "Müþteri bilgileri güncellendi";
                    }
                }

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Musteri/Index");
        }
    }
}

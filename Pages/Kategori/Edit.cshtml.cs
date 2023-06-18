using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Verifinalodev.Pages.Musteri;

namespace Verifinalodev.Pages.Kategori
{
    public class EditModel : PageModel
    {
        public KatInfo KatInfo = new KatInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String kid = Request.Query["kid"];
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "SELECT * FROM Kategori WHERE kid=@kid";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@kid", kid);
                        using SqlDataReader reader = command.ExecuteReader();
                        {
                            if (reader.Read())
                            {
                                KatInfo.kname = reader.GetString(0);
                                KatInfo.kattanim = reader.GetString(1);
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
            KatInfo.kname = Request.Form["kname"];
            KatInfo.kattanim = Request.Form["kattanim"];


            if (KatInfo.kname == "" | KatInfo.kattanim == "" )
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
                        command.Parameters.AddWithValue("@kname", KatInfo.kname);
                        command.Parameters.AddWithValue("@kattanim", KatInfo.kattanim);
                        command.ExecuteNonQuery();
                        successMessage = "Kategori bilgileri güncellendi";
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Kategori/Index");
        }
    }
}

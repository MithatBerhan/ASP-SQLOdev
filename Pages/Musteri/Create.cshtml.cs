using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Verifinalodev.Pages.Musteri;

namespace Verifinalodev.Pages.Musteri
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.mname = Request.Form["mname"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.madres = Request.Form["madres"];

            if (clientInfo.mname == null || clientInfo.email == null || clientInfo.phone == null || clientInfo.madres == null)
            {
                errorMessage = "Lütfen tüm alanlarý doldurunuz.";
                return;
            }
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Musteri (mname,email,phone,madres) VALUES (@mname,@email,@phone,@madres)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@mname", clientInfo.mname);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@madres", clientInfo.madres);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch(Exception ex)
            {
                errorMessage = "Müþteri eklenirken bir hata oluþtu. Hata:" + ex.ToString();
                return;
            }


            clientInfo.mname = ""; clientInfo.email = ""; clientInfo.phone = ""; clientInfo.madres = "";
            successMessage = "Müþteri baþarýyla eklendi.";

            Response.Redirect("/Musteri/Index");
        }
    }
}

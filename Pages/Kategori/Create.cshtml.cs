using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Verifinalodev.Pages.Musteri;

namespace Verifinalodev.Pages.Kategori
{
    public class CreateModel : PageModel
    {
        public KatInfo KatInfo = new KatInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            KatInfo.kname = Request.Form["kname"];
            KatInfo.kattanim = Request.Form["kattanim"];

            if (KatInfo.kname == null || KatInfo.kattanim == null)
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
                    String sql = "INSERT INTO Kategori (kname,kattanim) VALUES (@kname,@kattanim)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@kname", KatInfo.kname);
                        command.Parameters.AddWithValue("@kattanim", KatInfo.kattanim);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = "Bir hata oluþtu. Hata:" + ex.ToString();
                return;
            }


            KatInfo.kname = ""; KatInfo.kattanim = "";
            successMessage = "Kategori baþarýyla eklendi.";

            Response.Redirect("/Kategori/Index");
        }
    }
}

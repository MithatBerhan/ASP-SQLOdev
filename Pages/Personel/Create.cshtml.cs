using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Verifinalodev.Pages.Musteri;

namespace Verifinalodev.Pages.Personel
{
    public class CreateModel : PageModel
    {

            public PerInfo PerInfo = new PerInfo();
            public String errorMessage = "";
            public String successMessage = "";
            public void OnGet()
            {
            }

            public void OnPost()
            {
                PerInfo.pname = Request.Form["pname"];
                PerInfo.pemail = Request.Form["pemail"];
                PerInfo.pphone = Request.Form["pphone"];
                PerInfo.ptarih = Request.Form["ptarih"];

                if (PerInfo.pname == null || PerInfo.pemail == null || PerInfo.pphone == null || PerInfo.ptarih == null)
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
                        String sql = "INSERT INTO Personel (pname,pemail,pphone,ptarih) VALUES (@pname,@pemail,@pphone,@ptarih)";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@pname", PerInfo.pname);
                            command.Parameters.AddWithValue("@pemail", PerInfo.pemail);
                            command.Parameters.AddWithValue("@pphone", PerInfo.pphone);
                            command.Parameters.AddWithValue("@ptarih", PerInfo.ptarih);
                            command.ExecuteNonQuery();
                        }
                    }

                }
                catch (Exception ex)
                {
                    errorMessage = "Müþteri eklenirken bir hata oluþtu. Hata:" + ex.ToString();
                    return;
                }


                PerInfo.pname = ""; PerInfo.pemail = ""; PerInfo.pphone = ""; PerInfo.ptarih = "";
                successMessage = "Personel baþarýyla eklendi.";

                Response.Redirect("/Personel/Index");
            }
        }
}

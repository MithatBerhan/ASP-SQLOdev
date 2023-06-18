using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Verifinalodev.Pages.Musteri;

namespace Verifinalodev.Pages.Personel
{
    public class EditModel : PageModel
    {
        public PerInfo PerInfo = new PerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String pid = Request.Query["pid"];
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "SELECT * FROM Personel WHERE pid=@pid";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@pid", pid);
                        using SqlDataReader reader = command.ExecuteReader();
                        {
                            if (reader.Read())
                            {
                                PerInfo.pid = "" + reader.GetInt32(0);
                                PerInfo.pname = reader.GetString(1);
                                PerInfo.pemail = reader.GetString(2);
                                PerInfo.pphone = reader.GetString(3);
                                PerInfo.ptarih = reader.GetDateTime(4).ToString();
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
            PerInfo.pid = Request.Form["mid"];
            PerInfo.pname = Request.Form["mname"];
            PerInfo.pemail = Request.Form["email"];
            PerInfo.pphone = Request.Form["phone"];
            PerInfo.ptarih = Request.Form["madres"];

            if (PerInfo.pname == "" | PerInfo.pid == "" | PerInfo.pphone == "" | PerInfo.ptarih == "")
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
                    String sql = "UPDATE Personel" +
                        "SET pname=@pname, pemail=@pemail,"
                        + "pphone=@pphone, ptarih=@ptarih WHERE pid=@pid";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@mid", PerInfo.pid);
                        command.Parameters.AddWithValue("@mname", PerInfo.pname);
                        command.Parameters.AddWithValue("@email", PerInfo.pemail);
                        command.Parameters.AddWithValue("@phone", PerInfo.pphone);
                        command.Parameters.AddWithValue("@madres", PerInfo.ptarih);
                        command.ExecuteNonQuery();
                        successMessage = "Personel bilgileri güncellendi";
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Personel/Index");
        }
    }
}

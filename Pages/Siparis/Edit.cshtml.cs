using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Verifinalodev.Pages.Musteri;

namespace Verifinalodev.Pages.Siparis
{
    public class EditModel : PageModel
    {
        public SipInfo SipInfo = new SipInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String sipid = Request.Query["sipid"];
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "SELECT * FROM Siparis WHERE sipid=@sipid";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@sipid",sipid);
                        using SqlDataReader reader = command.ExecuteReader();
                        {
                            if (reader.Read())
                            {
                                SipInfo.sipid = "" + reader.GetInt32(0);
                                SipInfo.musid = reader.GetString(1);
                                SipInfo.sipadres = reader.GetString(2);
                                SipInfo.sipicerik = reader.GetString(3);
                                SipInfo.siptanim = reader.GetDateTime(4).ToString();
                                SipInfo.siptarih = reader.GetString(5);
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
            SipInfo.sipid = Request.Form["sipid"];
            SipInfo.musid = Request.Form["musid"];
            SipInfo.sipadres = Request.Form["sipadres"];
            SipInfo.sipicerik = Request.Form["sipicerik"];
            SipInfo.siptanim = Request.Form["siptanim"];
            SipInfo.siptarih = Request.Form["siptarih"];

            if (SipInfo.sipid == "" | SipInfo.musid == "" | SipInfo.sipicerik == "" | SipInfo.sipadres == ""| SipInfo.siptanim == ""| SipInfo.siptarih == "")
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
                        command.Parameters.AddWithValue("@sipid", SipInfo.sipid);
                        command.Parameters.AddWithValue("@musid", SipInfo.musid);
                        command.Parameters.AddWithValue("@sipadres", SipInfo.sipadres);
                        command.Parameters.AddWithValue("@sipicerik", SipInfo.sipicerik);
                        command.Parameters.AddWithValue("@siptanim", SipInfo.siptanim);
                        command.Parameters.AddWithValue("@siptarih", SipInfo.siptarih);
                        command.ExecuteNonQuery();
                        successMessage = "Müþteri bilgileri güncellendi";
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Siparis/Index");
        }
    }
}

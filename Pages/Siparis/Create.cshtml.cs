using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Verifinalodev.Pages.Musteri;

namespace Verifinalodev.Pages.Siparis
{
    public class CreateModel : PageModel
    {
        public SipInfo SipInfo = new SipInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            SipInfo.sipid = Request.Form["sipid"];
            SipInfo.musid = Request.Form["musid"];
            SipInfo.sipadres = Request.Form["sipadres"];
            SipInfo.sipicerik = Request.Form["sipicerik"];
            SipInfo.siptanim = Request.Form["siptanim"];
            SipInfo.siptarih = Request.Form["siptarih"];

            if (SipInfo.musid == null || SipInfo.sipicerik == null || SipInfo.sipadres == null || SipInfo.siptanim == null)
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
                    String sql = "INSERT INTO Siparis (musid,sipadres,sipicerik,siptanim) VALUES (@musid,@sipadres,@sipicerik,@siptanim)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@musid", SipInfo.musid);
                        command.Parameters.AddWithValue("@sipadres", SipInfo.sipicerik);
                        command.Parameters.AddWithValue("@sipicerik", SipInfo.sipicerik);
                        command.Parameters.AddWithValue("@siptanim", SipInfo.siptanim);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = "Siparis eklenirken bir hata oluþtu. Hata:" + ex.ToString();
                return;
            }


            SipInfo.musid = ""; SipInfo.sipadres = ""; SipInfo.sipicerik = ""; SipInfo.siptanim = "";
            successMessage = "Siparis baþarýyla eklendi.";

            Response.Redirect("/Siparis/Index");
        }
    }
}

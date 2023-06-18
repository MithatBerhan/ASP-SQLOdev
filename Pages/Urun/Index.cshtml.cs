using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Verifinalodev.Pages.Urun
{
    public class IndexModel : PageModel
    {
        public List<UrunInfo> ListUrun = new List<UrunInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "SELECT * FROM Urun";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using SqlDataReader reader = command.ExecuteReader();
                        {
                            while (reader.Read())
                            {
                                UrunInfo client = new UrunInfo();
                                client.urunid = "" + reader.GetInt32(0);
                                client.uname = reader.GetString(1);
                                client.urunstok = reader.GetString(2);
                                client.urunkat = reader.GetString(3);
                                client.urunkatid = reader.GetString(4);
                                client.uruntanim = reader.GetString(5);
                                client.uruntarih = reader.GetDateTime(6).ToString();
                                ListUrun.Add(client);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }
        }
    }

    public class UrunInfo
    {
        public string urunid;
        public String uname;
        public String urunstok;
        public String urunkat;
        public String urunkatid;
        public String uruntanim;
        public String uruntarih;
    }
}


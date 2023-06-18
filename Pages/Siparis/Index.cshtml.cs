using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Verifinalodev.Pages.Siparis
{
    public class IndexModel : PageModel
    {
        public List<SipInfo> ListSip = new List<SipInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "SELECT * FROM Siparis";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using SqlDataReader reader = command.ExecuteReader();
                        {
                            while (reader.Read())
                            {
                                SipInfo client = new SipInfo();
                                client.sipid = "" + reader.GetInt32(0);
                                client.musid = reader.GetString(1);
                                client.sipadres = reader.GetString(2);
                                client.sipicerik = reader.GetString(3);
                                client.siptanim = reader.GetString(4);
                                client.siptarih = reader.GetDateTime(5).ToString();
                                ListSip.Add(client);
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

    public class SipInfo
    {
        public String sipid;
        public String musid;
        public String sipadres;
        public String sipicerik;
        public String siptanim;
        public String siptarih;
        
    }
}


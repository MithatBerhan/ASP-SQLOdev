using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Verifinalodev.Pages.Kategori
{
    public class IndexModel : PageModel
    {
        public List<KatInfo> ListKat = new List<KatInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "SELECT * Kategori";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using SqlDataReader reader = command.ExecuteReader();
                        {
                            while (reader.Read())
                            {
                                KatInfo client = new KatInfo();
                                client.kid = "" + reader.GetInt32(0);
                                client.kname = reader.GetString(1);
                                client.kattanim = reader.GetString(2);
                                ListKat.Add(client);
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

    public class KatInfo
    {
        public String kid;
        public String kname;
        public String kattanim;
       
    }
}


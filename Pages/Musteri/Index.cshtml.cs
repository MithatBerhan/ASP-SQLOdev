using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Verifinalodev.Pages.Musteri
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> ListClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True"; 
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "SELECT * FROM Musteri";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using SqlDataReader reader = command.ExecuteReader();
                        {
                            while (reader.Read())
                            {
                                ClientInfo client = new ClientInfo();
                                client.mid = "" + reader.GetInt32(0);
                                client.mname = reader.GetString(1);
                                client.email = reader.GetString(2);
                                client.phone = reader.GetString(3);
                                client.tarih = reader.GetDateTime(4).ToString();
                                client.madres = reader.GetString(5);
                                ListClients.Add(client);
                            }
                        }
                    }
                    
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public String mid;
        public String mname;
        public String email;
        public String phone;
        public String tarih;
        public String madres;
    }

}

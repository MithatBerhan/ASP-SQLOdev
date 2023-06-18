using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Verifinalodev.Pages.Personel
{
    public class IndexModel : PageModel
    {
        public List<PerInfo> ListPersonel = new List<PerInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "SELECT * FROM Personel";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using SqlDataReader reader = command.ExecuteReader();
                        {
                            while (reader.Read())
                            {
                                PerInfo Per = new PerInfo();
                                Per.pid = "" + reader.GetInt32(0);
                                Per.pname = reader.GetString(1);
                                Per.pemail = reader.GetString(2);
                                Per.pphone = reader.GetString(3);
                                Per.ptarih = reader.GetDateTime(4).ToString();
                                ListPersonel.Add(Per);
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

    public class PerInfo
    {
        public String pid;
        public String pname;
        public String pemail;
        public String pphone;
        public String ptarih;
    }
}


using Microsoft.AspNetCore.Identity;

namespace part_1.Models
{
    public class on_load
    {

        public string name { get; set; }
        public string roles { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public connect conn = new connect();


        public on_load()
        {
            roles = "lecture";
            try
            {

                using (System.Data.SqlClient.SqlConnection connects = new System.Data.SqlClient.SqlConnection(conn.connecting()))
                {
                    connects.Open();

                    string query = "select * from active;"; int count = 0;
                    using (System.Data.SqlClient.SqlCommand checks = new System.Data.SqlClient.SqlCommand(query, connects))
                    {

                        using (System.Data.SqlClient.SqlDataReader loads = checks.ExecuteReader())
                        {

                            while (loads.Read())
                            {
                                count++;
                               name =loads["name"].ToString();
                                role = loads["status"].ToString();
                                email = loads["username"].ToString();
                            }

                            loads.Close();
                        }


                    }

                    //if (count>0)
                    //{
                    //    return RedirectToAction("status","Home");
                    //}
                    connects.Close();

                }

            }
            catch (IOException e)
            {

            }

        }

    }
}

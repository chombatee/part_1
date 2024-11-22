using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace part_1.Models
{




    public class loginModel
    {
        //get and set the values
        public string Username { get; set; }
        public string Password { get; set; }
        public string WorkType { get; set; }

    }


    public class claim
    {

        public string username { get; set; }
        public string module { get; set; }
        public string claim_date { get; set; }
        public string period { get; set; }
        public string hour_rate { get; set; }
        public string hours_worked { get; set; }
        public string supporting_document { get; set; }
        public string description { get; set; }

        public string name { get; set; }

        public string email { get; set; }
        public string role { get; set; }
        public connect conn = new connect();
        public IFormFile PdfDocument { get; set; }// Use IFormFile for file uploads

        public claim()
        {

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
                                name = loads["name"].ToString();
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


        public string store(string username, string module, string claim_date, string period, string hour_rate, string hours_worked, string description,string filename,string filepath)
        {
            string message = "";
            string total = "" + double.Parse(hour_rate) * double.Parse(hours_worked);
            //total = "" + (double.Parse(total));
            try
            {
                string query = "insert into claims values('" + username + "','" + module + "','" + claim_date + "','" + period + "','" + hour_rate + "','" + hours_worked + "','" + description + "','" + total + "','"+filename+"','"+filepath+"','pending');";

                using (System.Data.SqlClient.SqlConnection connects = new System.Data.SqlClient.SqlConnection(conn.connecting()))
                {
                    connects.Open();


                    using (System.Data.SqlClient.SqlCommand checks = new System.Data.SqlClient.SqlCommand(query, connects))
                    {





                        checks.ExecuteNonQuery();


                        message = "done";

                    }     connects.Close();
                }

            }
            catch (IOException e)
            {

            }


return message;


     }   
      

      
    }

    public class get_cliams
    {
        // Initialize ArrayLists when the class is created
        public ArrayList username { get; set; } = new ArrayList();
        public ArrayList module { get; set; } = new ArrayList();
        public ArrayList claim_date { get; set; } = new ArrayList();
        public ArrayList period { get; set; } = new ArrayList();
        public ArrayList hour_rate { get; set; } = new ArrayList();
        public ArrayList hours_worked { get; set; } = new ArrayList();
        public ArrayList supporting_document { get; set; } = new ArrayList();
        public ArrayList description { get; set; } = new ArrayList();
        public ArrayList total { get; set; } = new ArrayList();
        public ArrayList no { get; set; } = new ArrayList();
        public ArrayList status { get; set; } = new ArrayList();

        public connect conn = new connect();
        public get_cliams()
        {










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
                                username.Add(loads["username"].ToString());
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








            try
            {

                using (System.Data.SqlClient.SqlConnection connects = new System.Data.SqlClient.SqlConnection(conn.connecting()))
                {
                    connects.Open();

                    string query = "select * from claims where username='" + username[0] + "'"; int count = 0;
                    username.Clear();
                    using (System.Data.SqlClient.SqlCommand checks = new System.Data.SqlClient.SqlCommand(query, connects))
                    {

                        using (System.Data.SqlClient.SqlDataReader loads = checks.ExecuteReader())
                        {

                            while (loads.Read())
                            {
                                count++;


                                no.Add("" + count);

                                username.Add(loads["username"].ToString());
                                module.Add(loads["module"].ToString());
                                claim_date.Add(loads["claim_date"].ToString());
                                period.Add(loads["claim_period"].ToString());
                                hour_rate.Add(loads["hour_rate"].ToString());
                                hours_worked.Add(loads["hour_worked"].ToString());
                                supporting_document.Add(loads["filename"].ToString());
                                description.Add(loads["description"].ToString());
                                total.Add("R" + loads["total"].ToString());
                                status.Add(loads["status"].ToString());
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



    public class loads_all
    {
        // Initialize ArrayLists when the class is created
        public ArrayList username { get; set; } = new ArrayList();
        public ArrayList module { get; set; } = new ArrayList();
        public ArrayList claim_date { get; set; } = new ArrayList();
        public ArrayList period { get; set; } = new ArrayList();
        public ArrayList hour_rate { get; set; } = new ArrayList();
        public ArrayList hours_worked { get; set; } = new ArrayList();
        public ArrayList supporting_document { get; set; } = new ArrayList();
        public ArrayList description { get; set; } = new ArrayList();
        public ArrayList total { get; set; } = new ArrayList();
        public ArrayList no { get; set; } = new ArrayList();
        public ArrayList id { get; set; } = new ArrayList();


        public string ids { get; set; }
        public string on { get; set; }

        public ArrayList status { get; set; } = new ArrayList();

        public connect conn = new connect();
        public loads_all()
        {

















            try
            {

                using (System.Data.SqlClient.SqlConnection connects = new System.Data.SqlClient.SqlConnection(conn.connecting()))
                {
                    connects.Open();

                    string query = "select * from claims where status='pending'"; int count = 0;
                    username.Clear();
                    using (System.Data.SqlClient.SqlCommand checks = new System.Data.SqlClient.SqlCommand(query, connects))
                    {

                        using (System.Data.SqlClient.SqlDataReader loads = checks.ExecuteReader())
                        {

                            while (loads.Read())
                            {
                                count++;


                                no.Add("" + count);

                                username.Add(loads["username"].ToString());
                                module.Add(loads["module"].ToString());
                                claim_date.Add(loads["claim_date"].ToString());
                                period.Add(loads["claim_period"].ToString());
                                hour_rate.Add(loads["hour_rate"].ToString());
                                hours_worked.Add(loads["hour_worked"].ToString());
                                supporting_document.Add(loads["filename"].ToString());
                                description.Add(loads["description"].ToString());
                                total.Add("R" + loads["total"].ToString());
                                status.Add(loads["status"].ToString());
                                id.Add(loads["id"].ToString());

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


        public string login(string email, string role, string password)
        {



            try
            {
                using (System.Data.SqlClient.SqlConnection connects = new System.Data.SqlClient.SqlConnection(conn.connecting()))
                {
                    connects.Open();

                    //username = "connecting";


                    string query = "select * from users where email='" + email + "' and password='" + password + "' and role='" + role + "'";

                    using (System.Data.SqlClient.SqlCommand runs = new System.Data.SqlClient.SqlCommand(query, connects))
                    {

                        using (System.Data.SqlClient.SqlDataReader get_user = runs.ExecuteReader())
                        {



                            if (get_user.HasRows)
                            {
                                string names = "";

                                while (get_user.Read())
                                {
                                    names = get_user["full_name"].ToString();
                                }


                                get_user.Close();
                                string update = "update active set username='" + role + "',name='" + names + "',status='" + role + "'";
                                using (System.Data.SqlClient.SqlCommand updates = new System.Data.SqlClient.SqlCommand(update, connects))


                                    //
                                    updates.ExecuteNonQuery();


                                //Redirect after successful login
                                return "login successfully";
                            }
                            else
                            {

                                //Redirect after successful login
                                return "login not successfully";
                            }

                        }


                    }


                    //
                    connects.Close();
                }
            }
            catch (IOException e)
            {
                return "error " + e.Message;
            }

        }



    }


}

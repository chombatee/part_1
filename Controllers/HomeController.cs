using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using part_1;
using part_1.Models;
using System.Collections;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;
using static part_1.Models.get_cliams;

namespace part_1.Controllers
{
    public class HomeController : Controller
    {

        public connect conn = new connect();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult claims()
        {
            claim all = new claim();

            return View(all);
        }

        [HttpPost]
        public IActionResult claiming(IFormFile file, claim claims)
        {

            string username = claims.username;
            string module = claims.module;
            string claim_date = claims.claim_date;
            string period = claims.period;
            string hour_rate = claims.hour_rate;
            string hours_worked = claims.hours_worked;
            string description = claims.description;
            string documents = claims.supporting_document;

            //check file

            string file_found = "no";
            string filename = "none";
            string filePath = "";
            string folderPath = "";
            if (file != null && file.Length > 0)
            {

                file_found = "yes";
                // Get the file name
                filename = Path.GetFileName(file.FileName);
                // Define the folder path (pdf folder)
                folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdf");
                // Ensure the pdf folder exists
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                // Define the full path where the file will be saved
                filePath = Path.Combine(folderPath, filename);
                // Save the file to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    Console.WriteLine("File " + filename + " is sccessully uploaded. ");
                    //  under " + claim_id.id);


                }
            }


            string message = claims.store(username, module, claim_date, period, hour_rate, hours_worked, description, filename, filePath);

            //ViewBag.Message("done");

            //then open connction

            return RedirectToAction("claims", "Home");
        }

        public IActionResult approve()
        {

            loads_all all = new loads_all();
            return View(all);
        }
        public IActionResult status()
        {
            get_cliams all = new get_cliams();


            return View(all);
        }
        public IActionResult profile()
        {

            on_load all = new on_load();

            return View(all);
        }
        public IActionResult all_dash()
        {

            on_load load = new on_load();

            return View(load);
        }
        public IActionResult Dashboard()
        {


            on_load all = new on_load();

            return View(all);
        }




        [HttpPost]
        public IActionResult Login(loginModel model)
        {
            if (ModelState.IsValid)
            {
                // Logic to handle login (e.g., authentication)
                string username = model.Username;
                string password = model.Password;
                string workType = model.WorkType;

                try
                {
                    using (System.Data.SqlClient.SqlConnection connects = new System.Data.SqlClient.SqlConnection(conn.connecting()))
                    {
                        connects.Open();

                        //username = "connecting";


                        string query = "select * from users where email='" + username + "' and password='" + password + "' and role='" + workType + "'";

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
                                    string update = "update active set username='" + username + "',name='" + names + "',status='" + workType + "'";
                                    using (System.Data.SqlClient.SqlCommand updates = new System.Data.SqlClient.SqlCommand(update, connects))


                                        //
                                        updates.ExecuteNonQuery();


                                    //Redirect after successful login
                                    return RedirectToAction("Dashboard", "Home");
                                }
                                else
                                {

                                    //Redirect after successful login
                                    return RedirectToAction("index", "Home");
                                }

                            }


                        }


                        //
                        connects.Close();
                    }
                }
                catch (IOException e)
                {
                    username = "error " + e.Message;
                }

            }

            // Return the view with validation errors if the model is invalid
            return View(model);
        }


        [HttpPost]
        public IActionResult logout()
        {

            try
            {
                using (System.Data.SqlClient.SqlConnection connects = new System.Data.SqlClient.SqlConnection(conn.connecting()))
                {
                    connects.Open();









                    string update = "update active set username='no',name='no',status='no'";
                    using (System.Data.SqlClient.SqlCommand updates = new System.Data.SqlClient.SqlCommand(update, connects))
                    {


                        //
                        updates.ExecuteNonQuery();


                        //Redirect after successful login
                        return RedirectToAction("Index", "Home");

                    }


                    connects.Close();
                }





                //


            }
            catch (IOException e)
            {

            }



            //Redirect after successful login
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult approves(loads_all all)
        {
            try
            {
                using (System.Data.SqlClient.SqlConnection connects = new System.Data.SqlClient.SqlConnection(conn.connecting()))
                {
                    connects.Open();

                    string status = all.on == "yes" ? "approved" : "rejected";
                    Console.WriteLine(status);

                    string id = all.ids.Replace(")", "");

                    
                    // Attempt to parse all.ids to an integer
                    if (int.TryParse(id, out int claimId))
                    {
                     
                        // Use parameterized query to prevent SQL injection
                        string update = "UPDATE claims SET status = @status WHERE id = @id";

                        using (System.Data.SqlClient.SqlCommand updates = new System.Data.SqlClient.SqlCommand(update, connects))
                        {
                            // Add parameters to avoid SQL injection
                            updates.Parameters.AddWithValue("@status", status);
                            updates.Parameters.AddWithValue("@id", claimId);

                            // Execute the update query
                            updates.ExecuteNonQuery();

                            // Redirect after successful update
                            return RedirectToAction("approve", "Home");
                        }
                    }
                    else
                    {
                        
                        return RedirectToAction("approve", "Home", new { error = "Invalid claim ID format" });
                    }
                }
            }
            catch (IOException e)
            {
             
            }

            return RedirectToAction("approve", "Home");
        }



        [HttpPost]
        public IActionResult Registers(registers Register)
        {

            try
            {
                using (System.Data.SqlClient.SqlConnection connects = new System.Data.SqlClient.SqlConnection(conn.connecting()))
                {
                    connects.Open();









                    string query = "INSERT INTO [dbo].[users] (username, full_name, email, role, module, password) " +
                "VALUES('" + Register.Username + "','" + Register.FullName + "','" + Register.Email + "','" + Register.Role + "','" + Register.Module + "','" + Register.Password + "');";

                    using (System.Data.SqlClient.SqlCommand updates = new System.Data.SqlClient.SqlCommand(query, connects))
                    {


                        //
                        updates.ExecuteNonQuery();


                        //Redirect after successful register
                        return RedirectToAction("Index", "Home");

                    }

                }
            }
            catch (IOException e)
            {

            }



            //Redirect after successful login
            return RedirectToAction("Register", "Home");

        }
    }
}

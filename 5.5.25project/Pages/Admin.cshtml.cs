using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Server_temp.Pages
{

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public decimal Balance { get; set; }
        public string FullName { get; set; }
        public int BirthYear { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }

        public string Role { get; set; }
    }
    public class Admin : PageModel
    {
        [TempData]
        public int? inId { get; set; }

        [TempData]
        public string? inUsername { get; set; }

        [TempData]
        public string? inFullname { get; set; }

        [TempData]
        public int? birthyear { get; set; }

        [TempData]
        public string? inEmail { get; set; }

        [TempData]
        public string? inGender { get; set; }

        [TempData]
        public string? inRole { get; set; }

        [TempData]
        public string? inchoice { get; set; }

        public string filter { get; set; } = "WHERE";



        private readonly ILogger<Admin> _logger;
        private readonly IConfiguration _configuration;
        


        public List<User> Users { get; set; } = new List<User>();
        public Admin(ILogger<Admin> logger , IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration; 
        }

        public void OnGet()
        {
            string connectionString = _configuration.GetConnectionString("MyConnection");

            if (string.IsNullOrEmpty(inUsername) &&
            string.IsNullOrEmpty(inFullname) &&
            string.IsNullOrEmpty(inEmail) &&
            string.IsNullOrEmpty(inGender) &&
            string.IsNullOrEmpty(inRole) &&
            inId == null &&
            birthyear == null)
            {
                filter = "WHERE 1=1";
            }
            else if (inchoice == "or")
            {
                if (inId != null)
                {
                    filter += $" Id = {inId} OR";
                }
                if (!string.IsNullOrEmpty(inUsername))
                {
                    filter += $" Username LIKE '%{inUsername}%' OR";
                }
                if (!string.IsNullOrEmpty(inFullname))
                {
                    filter += $" FullName LIKE '%{inFullname}%' OR";
                }
                if (birthyear != null)
                {
                    filter += $" BirthYear = {birthyear} OR";
                }
                if (!string.IsNullOrEmpty(inEmail))
                {
                    filter += $" Email LIKE '%{inEmail}%' OR";
                }
                if (!string.IsNullOrEmpty(inGender))
                {
                    filter += $" gender LIKE '%{inGender}%' OR";
                }
                if (!string.IsNullOrEmpty(inRole))
                {
                    filter += $" Role LIKE '%{inRole}%' OR";
                }
            }
            else
            {
                if (inId != null)
                {
                    filter += $" Id = {inId} AND";
                }
                if (!string.IsNullOrEmpty(inUsername))
                {
                    filter += $" Username LIKE '%{inUsername}%' AND";
                }
                if (!string.IsNullOrEmpty(inFullname))
                {
                    filter += $" FullName LIKE '%{inFullname}%' AND";
                }
                if (birthyear != null)
                {
                    filter += $" BirthYear = {birthyear} AND";
                }
                if (!string.IsNullOrEmpty(inEmail))
                {
                    filter += $" Email LIKE '%{inEmail}%' AND";
                }
                if (!string.IsNullOrEmpty(inGender))
                {
                    filter += $" gender = '{inGender}' AND";
                }
                if (!string.IsNullOrEmpty(inRole))
                {
                    filter += $" Role = '{inRole}' AND";
                }



            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                if (inchoice == "or")
                {
                    filter = filter.TrimEnd(' ', 'O', 'R');
                }
                else
                {
                    filter = filter.TrimEnd(' ', 'A', 'N', 'D');
                }
                string query = $"SELECT Id, Username, balance, FullName, Email, BirthYear, gender, Role  FROM UPCB_Users {filter}";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader UserData = cmd.ExecuteReader())
                    {
                        while (UserData.Read())
                        {
                            Users.Add(new User
                            {
                                Id = UserData.GetInt32(0),
                                Username = UserData.GetString(1),
                                Balance = UserData.GetDecimal(2),
                                FullName = UserData.GetString(3),
                                Email = UserData.GetString(4),
                                BirthYear = UserData.GetInt32(5),
                                Gender = UserData.GetString(6),
                                Role = UserData.GetString(7)
                            });
                        }
                    }
                }


            }
            inId = null;
            inUsername = null;
            inFullname = null;
            birthyear = null;
            inEmail = null;
            inGender = null;
            inRole = null;
            filter = "WHERE";
        }

        public IActionResult OnPost(int? Id, string? Username, int Increment, bool isDelete = false, bool isFilter = false)
        {
            if (isDelete)
            {
                string connectionString = _configuration.GetConnectionString("MyConnection");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string deleteUserSkinsQuery = "DELETE FROM User_Skins WHERE id = @Id;";
                    using (SqlCommand cmdSkin = new SqlCommand(deleteUserSkinsQuery, conn))
                    {
                        cmdSkin.Parameters.AddWithValue("@Id", Id);
                        cmdSkin.ExecuteNonQuery();
                    }

                    string deleteUserQuery = "DELETE FROM UPCB_Users WHERE Username = @Username";
                    using (SqlCommand cmd = new SqlCommand(deleteUserQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", Username);
                        if (Username == HttpContext.Session.GetString("Username"))
                        {
                            cmd.ExecuteNonQuery();
                            HttpContext.Session.Clear();
                            return RedirectToPage("/Sign_In");
                        }
                        cmd.ExecuteNonQuery();
                    }

                    return RedirectToPage();
                }
            }
            else if (isFilter)
            {
                if (int.TryParse(Request.Form["Id"], out int parsedId))
                    inId = parsedId;
                else
                    inId = null;

                if (!string.IsNullOrWhiteSpace(Request.Form["Username"]))
                    inUsername = Request.Form["Username"];
                else
                    inUsername = null;

                if (!string.IsNullOrWhiteSpace(Request.Form["FullName"]))
                    inFullname = Request.Form["FullName"];
                else
                    inFullname = null;

                if (int.TryParse(Request.Form["Birthyear"], out int parsedBirthyear))
                    birthyear = parsedBirthyear;
                else
                    birthyear = null;

                if (!string.IsNullOrWhiteSpace(Request.Form["Email"]))
                    inEmail = Request.Form["Email"];
                else
                    inEmail = null;

                if (!string.IsNullOrWhiteSpace(Request.Form["Gender"]))
                    inGender = Request.Form["Gender"];
                else
                    inGender = null;

                if (!string.IsNullOrWhiteSpace(Request.Form["Role"]))
                    inRole = Request.Form["Role"];
                else
                    inRole = null;

                if (!string.IsNullOrWhiteSpace(Request.Form["choice"]))
                    inchoice = Request.Form["choice"];
                else
                    inchoice = null;

                return RedirectToPage();
            }
            else
            {
                string connectionString = _configuration.GetConnectionString("MyConnection");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE UPCB_Users SET balance = balance + @Increment WHERE Username = @Username";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", Username);
                        cmd.Parameters.AddWithValue("@Increment", Increment);

                        cmd.ExecuteNonQuery();
                    }
                    return RedirectToPage();
                }
            }
        }

    }
}
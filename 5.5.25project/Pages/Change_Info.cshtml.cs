using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Server_temp.Pages
{
    public class Change_Info : PageModel
    {

        [BindProperty]
        public int option { get; set; }

        [BindProperty]
        public string? oldEmail { get; set; }

        [BindProperty]
        public string? email { get; set; }

        [BindProperty]
        public string? username { get; set; }

        [BindProperty]
        public string? oldPassword { get; set; }

        [BindProperty]
        public string? password { get; set; }

        [BindProperty]
        public string? oldUsername { get; set; }
        [BindProperty]
        public string? fullName { get; set; }
        [BindProperty]
        public int? birthYear { get; set; }
        [BindProperty]
        public string? gender { get; set; }

        

        
        public string? Message = "";

        public string sqlQuery = "AND Password = @Password";

        public string updQUery = "";



        private readonly ILogger<Change_Info> _logger;
        private readonly IConfiguration _configuration;

        public Change_Info(ILogger<Change_Info> logger , IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult OnPost(string? Value)
        {
            if (Value == "choose")
            {
                return Page();
            }
            else
            {
                int? Id = HttpContext.Session.GetInt32("Id");

                if (option != 1 && option != 2 && option != 3 && option != 4 && option != 5 && option != 6)
                {
                    Message = "Please select a valid option.";
                    return Page();
                }

                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(oldUsername))
                {
                    sqlQuery += " AND Username = @Username";
                    updQUery = "Username = @newUsername";
                }


                if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(oldEmail))
                {
                    sqlQuery += " AND Email = @Email";
                    updQUery = "Email = @newEmail";
                }

                if (!string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(oldPassword))
                {
                    
                    updQUery = "Password = @newPassword";
                }

                if (!string.IsNullOrWhiteSpace(fullName))
                {
                    updQUery = "FullName = @newFullName";
                }
                if (birthYear.HasValue)
                {
                    updQUery = "BirthYear = @newBirthYear";
                }
                if (!string.IsNullOrEmpty(gender))
                {
                    updQUery = "Gender = @newGender";
                }
                

                string connectionString = _configuration.GetConnectionString("MyConnection");

                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand checkCmd = new SqlCommand(
                    $"SELECT COUNT(*) FROM UPCB_Users WHERE Id = @Id {sqlQuery}", 
                    conn
                );
                checkCmd.Parameters.AddWithValue("@Username", oldUsername ?? string.Empty);
                checkCmd.Parameters.AddWithValue("@Email", oldEmail ?? string.Empty);
                checkCmd.Parameters.AddWithValue("@Password", oldPassword ?? string.Empty);
                checkCmd.Parameters.AddWithValue("@Id", Id);
                int count = (int)checkCmd.ExecuteScalar();
                
                if (count == 0 && option != 4 && option !=5 && option != 6)
                {
                    Message = "No matching record found.";
                    return Page();
                }

                if ((string.IsNullOrEmpty(password) || password.Length < 8 || password.Length > 32) && option == 3)
                {
                    Message = "Password must be at least 8 characters and less than 32 characters.";
                    return Page();
                }
                if ((string.IsNullOrEmpty(email) || email.Length < 5 || email.Contains(" ") || email.Length > 40) && option == 1)
                {
                    Message = "Email must be 5-40 characters long and cannot contain spaces.";
                    return Page();
                }
                if ((string.IsNullOrEmpty(username) || username.Length < 5 || username.Contains(" ") || username.Length > 20) && option == 2)
                {
                    Message = "Username must be 5-20 characters long and cannot contain spaces.";
                    return Page();
                }
                if ((string.IsNullOrEmpty(fullName) || fullName.Length < 3 || fullName.All(c => c == ' ') || fullName.Length > 40) && option == 4)
                {
                    Message = "Full name must be 3-40 characters long and cannot contain spaces only.";
                    return Page();
                }
                if ((birthYear < 1900 || birthYear > DateTime.Now.Year) && option == 5)
                {
                    Message = "Birth year must be between 1900 and the current year.";
                    return Page();
                }
                if (string.IsNullOrEmpty(gender) && option == 6)
                {
                    Message = "Choose A gender.";
                    return Page();
                }
                if (string.IsNullOrWhiteSpace(updQUery))
                    {
                        Message = "Nothing to update.";
                        return Page();
                    }

                SqlCommand updCmd = new SqlCommand(
                    $"UPDATE UPCB_Users SET {updQUery} WHERE Id = @Id ", 
                    conn
                );
                updCmd.Parameters.AddWithValue("@newUsername", username ?? string.Empty);
                updCmd.Parameters.AddWithValue("@newEmail", email ?? string.Empty);
                updCmd.Parameters.AddWithValue("@newPassword", password ?? string.Empty);
                updCmd.Parameters.AddWithValue("@newFullName", fullName ?? string.Empty);
                updCmd.Parameters.AddWithValue("@newBirthYear", birthYear.HasValue ? (object)birthYear.Value : DBNull.Value);
                updCmd.Parameters.AddWithValue("@newGender", gender ?? string.Empty);

                updCmd.Parameters.AddWithValue("@Id", Id);
                
                int rowsAffected = updCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    if (option == 1)
                    {
                        Message = "Email changed successfully.";
                    }
                    else if (option == 2)
                    {
                        Message = "Username changed successfully.";
                        HttpContext.Session.SetString("Username", username);
                    }
                    else if (option == 3)
                    {
                        Message = "Password changed successfully.";
                    }
                    else if (option == 4)
                    {
                        Message = "Full name changed successfully.";
                    }
                    else if (option == 5)
                    {
                        Message = "Birth year changed successfully.";
                    }
                    else if (option == 6)
                    {
                        Message = "Gender changed successfully.";
                    }
                }
                    else
                    {
                        Message = "Update failed.";
                    }
            
                return Page();
            }
            
        }
        
        
    
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Server_temp.Pages;

public class Sign_InModel : PageModel
{

    public string? inUsername;

    public string? inPassword;

    public string? inFullName;

    public string? inEmail;

    public int? inBirthYear;

    public string? inGender;

    public string? inRole;

    public string? Message = "";
    

    private readonly ILogger<Sign_InModel> _logger;
    private readonly IConfiguration _configuration;

   

    public Sign_InModel(ILogger<Sign_InModel> logger, IConfiguration configuration) 
    {
        _logger = logger;
        _configuration = configuration; 
    }

    public void OnGet()
    {
        
    }

    public IActionResult OnPost()
    {
        inUsername = Request.Form["Username"];
        inPassword = Request.Form["Password"];
        inFullName = Request.Form["FullName"];
        inEmail = Request.Form["Email"];
        inBirthYear = int.Parse(Request.Form["BirthYear"]);
        inGender = Request.Form["gender"];

        string connectionString = _configuration.GetConnectionString("MyConnection");

        using SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();
        
        SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM UPCB_Users WHERE Username = @Username OR Email = @Email", conn);
        checkCmd.Parameters.AddWithValue("@Username", inUsername);
        checkCmd.Parameters.AddWithValue("@Email", inEmail);

        int count = (int)checkCmd.ExecuteScalar();
        if (count > 0)
        {
            Message += "Username or email already exists. Please choose a different one.";
        }
        else if (inPassword.Length < 8 || inPassword.Length > 32 )
        {
            Message += "Password must be at least 8 characters long and less then 32 characters.\n";
        }
        else if (inUsername.Length < 5 || inUsername.Contains(" ") || inUsername.Length > 40 )
        {
            Message += "Username must be at least 5 characters long and cannot contain spaces and less then 40 characters.\n";
        }
        else if (inFullName.Length < 3 || inFullName.All(c => c == ' ') || inFullName.Length > 40)
        {
            Message += "Full name must be at least 3 characters long, cannot be empty / only spaces, and less than 40 characters. \n";
        }
        else if (inBirthYear < 1900 || inBirthYear > DateTime.Now.Year)
        {
            Message += "Birth year must be between 1900 and the current year.\n";
        }
        else
        {
            string insertQuery = "INSERT INTO UPCB_Users (Username, Password, FullName, Email, BirthYear, Gender) " +
                                       "VALUES (@Username, @Password, @FullName, @Email, @BirthYear, @Gender)" +
                                       "SELECT SCOPE_IDENTITY();";

            using (SqlCommand insertCommand = new SqlCommand(insertQuery, conn))
            {
                insertCommand.Parameters.AddWithValue("@Username", inUsername);
                insertCommand.Parameters.AddWithValue("@Password", inPassword);
                insertCommand.Parameters.AddWithValue("@FullName", inFullName);
                insertCommand.Parameters.AddWithValue("@Email", inEmail);
                insertCommand.Parameters.AddWithValue("@BirthYear", inBirthYear);
                insertCommand.Parameters.AddWithValue("@Gender", inGender);

                var newUserId = Convert.ToInt32(insertCommand.ExecuteScalar());

                // int rowsAffected = insertCommand.ExecuteNonQuery();

                if (newUserId > 0)
                {
                    string SkinsQuery = "INSERT INTO User_Skins (Id) VALUES (@Id)";
                    using (SqlCommand skinsCommand = new SqlCommand(SkinsQuery, conn))
                    {
                        skinsCommand.Parameters.AddWithValue("@Id", newUserId);
                        skinsCommand.ExecuteNonQuery();
                    }



                    SqlCommand roleCmd = new SqlCommand("SELECT Role FROM UPCB_Users WHERE Username = @Username", conn);
                    roleCmd.Parameters.AddWithValue("@Username", inUsername);

                    var role = (string)roleCmd.ExecuteScalar();

                    SqlCommand IdCmd = new SqlCommand("SELECT Id FROM UPCB_Users WHERE Username = @Username", conn);
                    IdCmd.Parameters.AddWithValue("@Username", inUsername);

                    var Id = (int)IdCmd.ExecuteScalar();


                    HttpContext.Session.SetString("Role", role);
                    HttpContext.Session.SetString("Username", inUsername);
                    HttpContext.Session.SetInt32("Id", Id);

                    Message = "Account created successfully!";
                    Application.IncrementVisitor();
                    return new JsonResult(new { success = true, redirectUrl = "/Index" });

                }
                else
                {
                    Message = "Failed to create account.";
                    return new JsonResult(new { success = false, message = Message });
                }
            }
        }
        return new JsonResult(new { success = false, message = Message });
        
    }
}

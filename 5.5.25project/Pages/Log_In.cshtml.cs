using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Server_temp.Pages
{
    public class Log_InModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public Log_InModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JsonResult OnPost()
        {
            string username = Request.Form["Username"];
            string password = Request.Form["Password"];

            string connectionString = _configuration.GetConnectionString("MyConnection");

            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand checkCmd = new SqlCommand(
                "SELECT COUNT(*) FROM UPCB_Users WHERE Username = @Username AND Password = @Password", conn);
            checkCmd.Parameters.AddWithValue("@Username", username);
            checkCmd.Parameters.AddWithValue("@Password", password);

            int count = (int)checkCmd.ExecuteScalar();

            if (count > 0)
            {
                SqlCommand roleCmd = new SqlCommand("SELECT Role FROM UPCB_Users WHERE Username = @Username", conn);
                SqlCommand idCmd = new SqlCommand("SELECT Id FROM UPCB_Users WHERE Username = @Username", conn);
                roleCmd.Parameters.AddWithValue("@Username", username);
                idCmd.Parameters.AddWithValue("@Username", username);

                var role = (string)roleCmd.ExecuteScalar();
                var id = (int)idCmd.ExecuteScalar();

                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetString("Username", username);
                HttpContext.Session.SetInt32("Id", id);

                Application.IncrementVisitor();
                return new JsonResult(new { success = true, redirectUrl = "/Index" });
            }

            return new JsonResult(new { success = false, message = "Invalid username or password." });
        }
    }
}

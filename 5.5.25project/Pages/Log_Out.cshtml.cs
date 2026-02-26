using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Server_temp.Pages;

public class Log_OutModel : PageModel
{
    private readonly ILogger<Log_OutModel> _logger;

    public Log_OutModel(ILogger<Log_OutModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        HttpContext.Session.Clear();
        return RedirectToPage("/Log_In");
    }
    public void OnPost()
    {

    }

}

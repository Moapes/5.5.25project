using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server_temp.Pages;

public class IndexModel : PageModel
{
    
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public string? Username { get; private set; }
    public IActionResult OnGet()
    {
        Username = HttpContext.Session.GetString("Username");

        if (string.IsNullOrEmpty(Username))
        {
            return RedirectToPage("/Sign_In");
        }
        else
        {
            return Page();
        }
    }

    public void OnPost()
    {

    }

}
 
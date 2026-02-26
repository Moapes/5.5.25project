using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server_temp.Pages;

public class Open_CasesModel : PageModel
{
    private readonly ILogger<Open_CasesModel> _logger;

    public Open_CasesModel(ILogger<Open_CasesModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}

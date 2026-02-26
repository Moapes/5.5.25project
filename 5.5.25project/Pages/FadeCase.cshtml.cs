using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server_temp.Pages;

namespace Server_temp.Pages;

public class FadeCaseModel : PageModel
{
    private readonly ILogger<FadeCaseModel> _logger;
    private readonly UserHelper _userHelper; // Injected UserHelper

    public FadeCaseModel(ILogger<FadeCaseModel> logger, UserHelper userHelper)
    {
        _logger = logger;
        _userHelper = userHelper; // Assign injected UserHelper
    }

    private readonly Random _random = new Random();

    public class Skin
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public double Probability { get; set; }
    }

    public List<Skin> Skins { get; set; } = new List<Skin>
    {
        new Skin { Name = "AWP Fade", ImagePath = "/Case_Skins/Fade_Skins/AWP_Fade.png", Probability = 0.00056682 },
        new Skin { Name = "Butterfly Fade", ImagePath = "/Case_Skins/Fade_Skins/BF_Fade.png", Probability = 0.00012532 },
        new Skin { Name = "Huntsman Fade", ImagePath = "/Case_Skins/Fade_Skins/Hunts_M_Fade.png", Probability = 0.00119030 },
        new Skin { Name = "R8 Revolver Fade", ImagePath = "/Case_Skins/Fade_Skins/R_Fade.png", Probability = 0.00724820 },
        new Skin { Name = "Flip Knife Fade", ImagePath = "/Case_Skins/Fade_Skins/Flip_M_Fade.png", Probability = 0.00102029 },
        new Skin { Name = "Glock-18 Fade", ImagePath = "/Case_Skins/Fade_Skins/GLOCK_Fade.png", Probability = 0.00029154 },
        new Skin { Name = "M4A1-S Fade", ImagePath = "/Case_Skins/Fade_Skins/M4S_Fade.png", Probability = 0.00101604 },
        new Skin { Name = "M9 Bayonet Fade", ImagePath = "/Case_Skins/Fade_Skins/M9_Fade.png", Probability = 0.00027221 },
        new Skin { Name = "MAC-10 Amber Fade", ImagePath = "/Case_Skins/Fade_Skins/MAC_A_Fade.png", Probability = 0.02985727 },
        new Skin { Name = "MAC-10 Fade", ImagePath = "/Case_Skins/Fade_Skins/MAC_Fade.png", Probability = 0.01066797 },
        new Skin { Name = "Nomad Fade", ImagePath = "/Case_Skins/Fade_Skins/Nomad_Fade.png", Probability = 0.00050601 },
        new Skin { Name = "P2000 Amber Fade", ImagePath = "/Case_Skins/Fade_Skins/P20_A_Fade.png", Probability = 0.10430905 },
        new Skin { Name = "R8 Amber Fade", ImagePath = "/Case_Skins/Fade_Skins/R_A_Fade.png", Probability = 0.17128644 },
        new Skin { Name = "USP-S Fade", ImagePath = "/Case_Skins/Fade_Skins/SD_Fade.png", Probability = 0.00190266 },
        new Skin { Name = "Skeleton Knife Fade", ImagePath = "/Case_Skins/Fade_Skins/Skel_Fade.png", Probability = 0.00029220 },
        new Skin { Name = "Sawed-Off Amber Fade", ImagePath = "/Case_Skins/Fade_Skins/SO_A_Fade.png", Probability = 0.43586282 },
        new Skin { Name = "SSG 08 Amber Fade", ImagePath = "/Case_Skins/Fade_Skins/SSG_A_Fade.png", Probability = 0.14883121 },
        new Skin { Name = "UMP-45 Fade", ImagePath = "/Case_Skins/Fade_Skins/UMP_Fade.png", Probability = 0.00638543 },
        new Skin { Name = "Ursus Fade", ImagePath = "/Case_Skins/Fade_Skins/Ursus_M_Fade.png", Probability = 0.00137037 },
        new Skin { Name = "MP9 Fade", ImagePath = "/Case_Skins/Fade_Skins/MP9_Fade.png", Probability = 0.07699785 }
    };

    public string SelectedSkinImagePath { get; set; } = "/More_Images/Q_Mark.png";

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

    public IActionResult OnPost()
    {
        Username = HttpContext.Session.GetString("Username");
        var balance = _userHelper.getMoneyFromUser(Username); 

        

        if (balance < 80.00m)
        {
            TempData["AlertMessage"] = "You don't have enough money to open this case.";
            return Page();
            
        }
        else
        {
            
            var selectedSkin = GetRandomSkin();
            var isFull = _userHelper.checkIfInventoryIsFull((int)HttpContext.Session.GetInt32("Id"), selectedSkin.ImagePath);
            SelectedSkinImagePath = selectedSkin?.ImagePath ?? "/More_Images/Q_Mark.png";

            if (isFull)
            {
                SelectedSkinImagePath = "/More_Images/Q_Mark.png";
                TempData["AlertMessage"] = "Your inventory is full. Please remove some skins before opening a new case.";
                return Page();
            }
            else{
                _userHelper.changeMoneyToUser(Username, -80.00m);
                return Page();
            }
            

            return Page();
        }

    }

    private Skin GetRandomSkin()
    {
        double roll = _random.NextDouble();
        double cumulative = 0.0;

        foreach (var skin in Skins.OrderBy(s => s.Probability))
        {
            cumulative += skin.Probability;
            if (roll <= cumulative)
            {
                return skin;
            }
        }

        return Skins.Last();
    }

    public string? Username { get; private set; }
}
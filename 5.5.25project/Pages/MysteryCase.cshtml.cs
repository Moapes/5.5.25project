using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server_temp.Pages;

namespace Server_temp.Pages;

public class MysteryCaseModel : PageModel
{
    private readonly ILogger<MysteryCaseModel> _logger;

    private readonly UserHelper _userHelper;

    public MysteryCaseModel(ILogger<MysteryCaseModel> logger, UserHelper userHelper)
    {
        _logger = logger;
        _userHelper = userHelper;
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
        new Skin { Name = "AK47 Bloodsport", ImagePath = "/Case_Skins/Mystery_Skins/AK_BLS.png", Probability = 0.0011 },
        new Skin { Name = "AWP Sand Mesh", ImagePath = "/Case_Skins/Mystery_Skins/AWP_SM.png", Probability = 0.1222 },
        new Skin { Name = "CZ75 Distress", ImagePath = "/Case_Skins/Mystery_Skins/CZ_DSTRS.png", Probability = 0.0439 },
        new Skin { Name = "CZ75 Polemic", ImagePath = "/Case_Skins/Mystery_Skins/CZ_PLMR.png", Probability = 0.0213 },
        new Skin { Name = "CZ75 Tigers", ImagePath = "/Case_Skins/Mystery_Skins/CZ_TGRS.png", Probability = 0.0523 },
        new Skin { Name = "Glock 18 Oxide Blaze", ImagePath = "/Case_Skins/Mystery_Skins/GLOCK_OB.png", Probability = 0.0132 },
        new Skin { Name = "Glock 18 Vogue", ImagePath = "/Case_Skins/Mystery_Skins/GLOCK_VG.png", Probability = 0.0087 },
        new Skin { Name = "M4A1-S Nightmare", ImagePath = "/Case_Skins/Mystery_Skins/M4S_NTMR.png", Probability = 0.0039 },
        new Skin { Name = "MAC-10 Disco Tech", ImagePath = "/Case_Skins/Mystery_Skins/MAC_DT.png", Probability = 0.0152 },
        new Skin { Name = "MAG-7 Heat", ImagePath = "/Case_Skins/Mystery_Skins/MAG_HEAT.png", Probability = 0.0617 },
        new Skin { Name = "Negev Tatter", ImagePath = "/Case_Skins/Mystery_Skins/NVJ_TT.png", Probability = 0.0010 },
        new Skin { Name = "P2000 Obsidian", ImagePath = "/Case_Skins/Mystery_Skins/P20_OBSDN.png", Probability = 0.0299 },
        new Skin { Name = "P2000 Urban Hazard", ImagePath = "/Case_Skins/Mystery_Skins/P20_UH.png", Probability = 0.0109 },
        new Skin { Name = "P90 Freight", ImagePath = "/Case_Skins/Mystery_Skins/P90_FRT.png", Probability = 0.0199 },
        new Skin { Name = "P250 Cassette", ImagePath = "/Case_Skins/Mystery_Skins/P250_CSTE.png", Probability = 0.0880 },
        new Skin { Name = "P250 Franklin", ImagePath = "/Case_Skins/Mystery_Skins/P250_FRNK.png", Probability = 0.0324 },
        new Skin { Name = "P250 Nevermore", ImagePath = "/Case_Skins/Mystery_Skins/P250_NVRMR.png", Probability = 0.0271 },
        new Skin { Name = "SG553 Orange Rail", ImagePath = "/Case_Skins/Mystery_Skins/SG_OR.png", Probability = 0.2156 },
        new Skin { Name = "Tec-9 Avalanche", ImagePath = "/Case_Skins/Mystery_Skins/TEC_AVLNC.png", Probability = 0.0295 }
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

        

        if (balance < 30.00m)
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
                Console.WriteLine("Selected skin: " + selectedSkin.Name);
                _userHelper.changeMoneyToUser(Username, -30.00m);
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
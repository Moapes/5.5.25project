using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server_temp.Pages;

namespace Server_temp.Pages;

public class HyperCaseModel : PageModel
{
    private readonly ILogger<HyperCaseModel> _logger;

    private readonly UserHelper _userHelper;

    public HyperCaseModel(ILogger<HyperCaseModel> logger, UserHelper userHelper)
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
        new Skin { Name = "M9 Bayonet Knife Lore", ImagePath = "/Case_Skins/Hyper_Skins/M9_Lore.png", Probability = 0.005 },
        new Skin { Name = "Bowie Knife Slaughter", ImagePath = "/Case_Skins/Hyper_Skins/Bowie_Slter.png", Probability = 0.005 },
        new Skin { Name = "AWP Hyper Beast", ImagePath = "/Case_Skins/Hyper_Skins/AWP_HB", Probability = 0.01 },
        new Skin { Name = "M4A4-S Hyper Beast", ImagePath = "/Case_Skins/Hyper_Skins/M4S_HB.png", Probability = 0.015 },
        new Skin { Name = "Nova Hyper Beast", ImagePath = "/Case_Skins/Hyper_Skins/NOVA_HB.png", Probability = 0.015 },
        new Skin { Name = "Five Seven Hyper Beast", ImagePath = "/Case_Skins/Hyper_Skins/FS_HB.png", Probability = 0.015 },
        new Skin { Name = "AK47 Elite Build", ImagePath = "/Case_Skins/Hyper_Skins/AK_EB.png", Probability = 0.04 },
        new Skin { Name = "MAC 10 Ocianic", ImagePath = "/Case_Skins/Hyper_Skins/MAC_OCNC.png", Probability = 0.045 },
        new Skin { Name = "Five Seven Monkey Buisness", ImagePath = "/Case_Skins/Hyper_Skins/FS_MB.png", Probability = 0.045 },
        new Skin { Name = "Five Seven Violent Daimyo", ImagePath = "/Case_Skins/Hyper_Skins/FS_VD.png", Probability = 0.045 },
        new Skin { Name = "Glock 17 Wasteland Rebel", ImagePath = "/Case_Skins/Hyper_Skins/GLOCK_WR.png", Probability = 0.045 },
        new Skin { Name = "M4A4-S Leaded Glass", ImagePath = "/Case_Skins/Hyper_Skins/M4S_LG.png", Probability = 0.045 },
        new Skin { Name = "MP7 Armour Core", ImagePath = "/Case_Skins/Hyper_Skins/MP7_AC.png", Probability = 0.05 },
        new Skin { Name = "P250 Iron Clad", ImagePath = "/Case_Skins/Hyper_Skins/P250_IC.png", Probability = 0.05 },
        new Skin { Name = "P250 Volcanic", ImagePath = "/Case_Skins/Hyper_Skins/P250_VLNC.png", Probability = 0.05 },
        new Skin { Name = "PP Bizon High Roller", ImagePath = "/Case_Skins/Hyper_Skins/PP_HR.png", Probability = 0.06 },
        new Skin { Name = "PP Bizon Harvester", ImagePath = "/Case_Skins/Hyper_Skins/PP_HRVSTR.png", Probability = 0.06 },
        new Skin { Name = "SG Aerial", ImagePath = "/Case_Skins/Hyper_Skins/SG_Aerial.png", Probability = 0.06 },
        new Skin { Name = "Sawed Off Origami", ImagePath = "/Case_Skins/Hyper_Skins/SO_ORGM.png", Probability = 0.06 },
        new Skin { Name = "Desert Eagle Bronze Deco", ImagePath = "/Case_Skins/Hyper_Skins/D_BD.png", Probability = 0.055 }
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



        if (balance < 45.00m)
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
            else
            {
                Console.WriteLine("Selected skin: " + selectedSkin.Name);
                _userHelper.changeMoneyToUser(Username, -45.00m);
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

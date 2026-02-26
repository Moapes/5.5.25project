using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server_temp.Pages;

namespace Server_temp.Pages;

public class VIPCaseModel : PageModel
{
    private readonly ILogger<VIPCaseModel> _logger;
    private readonly UserHelper _userHelper;

    public VIPCaseModel(ILogger<VIPCaseModel> logger, UserHelper userHelper)
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
        new Skin { Name = "AK47 Asimov", ImagePath = "/Case_Skins/VIP_Skins/AK_ASMV.png", Probability = 0.005 },
        new Skin { Name = "AK47 Neon Rider", ImagePath = "/Case_Skins/VIP_Skins/AK_NR.png", Probability = 0.007 },
        new Skin { Name = "AK47 Predator", ImagePath = "/Case_Skins/VIP_Skins/AK_PD.png", Probability = 0.02 },
        new Skin { Name = "AK47 Redline", ImagePath = "/Case_Skins/VIP_Skins/AK_RDLN.png", Probability = 0.03 },
        new Skin { Name = "AWP Electric Blue", ImagePath = "/Case_Skins/VIP_Skins/AWP_EB.png", Probability = 0.04 },
        new Skin { Name = "AWP Fever", ImagePath = "/Case_Skins/VIP_Skins/AWP_FV.png", Probability = 0.05 },
        new Skin { Name = "AWP Neo Noir", ImagePath = "/Case_Skins/VIP_Skins/AWP_NN.png", Probability = 0.03 },
        new Skin { Name = "Desert Eagle Crimson", ImagePath = "/Case_Skins/VIP_Skins/D_CR.png", Probability = 0.02 },
        new Skin { Name = "Five-SeveN Angry Mob", ImagePath = "/Case_Skins/VIP_Skins/FS_AM.png", Probability = 0.08 },
        new Skin { Name = "Huntsman Safari Mesh", ImagePath = "/Case_Skins/VIP_Skins/HNTSMN_S.png", Probability = 0.07 },
        new Skin { Name = "M4A4 Asiimov", ImagePath = "/Case_Skins/VIP_Skins/M4_ASMV.png", Probability = 0.004 },
        new Skin { Name = "M4A4 Black Pearl", ImagePath = "/Case_Skins/VIP_Skins/M4_BK.png", Probability = 0.10 },
        new Skin { Name = "M4A1-S Decimator", ImagePath = "/Case_Skins/VIP_Skins/M4_DS.png", Probability = 0.10 },
        new Skin { Name = "M4A1-S Cyrex", ImagePath = "/Case_Skins/VIP_Skins/M4S_CF.png", Probability = 0.02 },
        new Skin { Name = "M9 Bayonet Black Wax", ImagePath = "/Case_Skins/VIP_Skins/M9_BW.png", Probability = 0.002 },
        new Skin { Name = "MAG-7 Turtle", ImagePath = "/Case_Skins/VIP_Skins/MG_TRTL.png", Probability = 0.05 },
        new Skin { Name = "M4A4 Dark Water", ImagePath = "/Case_Skins/VIP_Skins/M4_DK.png", Probability = 0.06 },
        new Skin { Name = "Negev Fade", ImagePath = "/Case_Skins/VIP_Skins/NVJ_FADE.png", Probability = 0.03 },
        new Skin { Name = "Negev Man-o'-War", ImagePath = "/Case_Skins/VIP_Skins/NVJ_MF.png", Probability = 0.01 },
        new Skin { Name = "Talon BF", ImagePath = "/Case_Skins/VIP_Skins/TALON_BF.png", Probability = 0.03 }
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

        

        if (balance < 210.00m)
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
                _userHelper.changeMoneyToUser(Username, -210.00m);
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


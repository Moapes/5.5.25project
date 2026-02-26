using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;

namespace Server_temp.Pages
{
    public class Money_Farm : PageModel
    {
        [TempData]
        public int inMoney_Clicked { get; set; }
        private readonly ILogger<Money_Farm> _logger;

        private readonly IConfiguration _configuration;

        private readonly UserHelper _userHelper;

        public Money_Farm(ILogger<Money_Farm> logger, IConfiguration configuration, UserHelper userHelper)
        {
            _userHelper = userHelper;
            _logger = logger;
            _configuration = configuration;
        }


        public void OnGet()
        {
        }
        public void OnPost()
        {
            inMoney_Clicked += 1;

            int? Id = HttpContext.Session.GetInt32("Id");
            string Username = HttpContext.Session.GetString("Username");
            string connectionString = _configuration.GetConnectionString("MyConnection");

            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            if (inMoney_Clicked >= 50)
            {
                inMoney_Clicked -= 50;
                _userHelper.changeMoneyToUser(Username, 100.00m);
            }
        
        }


            
        }
}

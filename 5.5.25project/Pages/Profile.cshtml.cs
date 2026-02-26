using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Collections.Generic;
using Server_temp.Pages;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Server_temp.Pages;

public class Skin
{
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public double Price { get; set; }
}


public class ProfileModel : PageModel
{
    public int inSkins_In_Invertory { get; set; }
    
    private readonly ILogger<ProfileModel> _logger;
    private readonly IConfiguration _configuration;

    public ProfileModel(ILogger<ProfileModel> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public List<Skin> userSkins { get; set; } = new List<Skin>();
    public int? Id { get; set; }
    public void OnGet()
    {
        Id = HttpContext.Session.GetInt32("Id");

        string connectionString = _configuration.GetConnectionString("MyConnection");

        using SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();

        string query = "SELECT * FROM User_Skins WHERE id = @Id;";
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@Id", Id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                if (reader.Read())
                {
                    
                    for (int i = 1; i < reader.FieldCount; i++) 
                    {
                        string skinPath = reader.GetString(i);

                        if (skinPath == "Empty")
                            continue;

                        inSkins_In_Invertory += 1; 
                        string imagePath = skinPath;

                        string nameQuery = "SELECT Name FROM Skins WHERE ImagePath = @ImagePath";
                        string priceQuery = "SELECT Price FROM Skins WHERE ImagePath = @ImagePath";

                        string name;
                        double price;

                        using (SqlConnection innerConn = new SqlConnection(connectionString))
                        {
                            innerConn.Open();

                            using (SqlCommand nameCmd = new SqlCommand(nameQuery, innerConn))
                            {
                                nameCmd.Parameters.AddWithValue("@ImagePath", imagePath);
                                name = (string)nameCmd.ExecuteScalar();
                            }

                            using (SqlCommand priceCmd = new SqlCommand(priceQuery, innerConn))
                            {
                                priceCmd.Parameters.AddWithValue("@ImagePath", imagePath);
                                price = Convert.ToDouble(priceCmd.ExecuteScalar());
                            }
                        }

                        userSkins.Add(new Skin
                        {
                            Name = name,
                            ImagePath = imagePath,
                            Price = price
                        });
                    }
                }
            }
        }
    }

    public IActionResult OnPost(string skinpath)
    {

        Id = HttpContext.Session.GetInt32("Id");

        string connectionString = _configuration.GetConnectionString("MyConnection");

        using SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();

        string query = "SELECT * FROM User_Skins WHERE id = @Id";
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@Id", Id);
            using (SqlDataReader UserSkins = cmd.ExecuteReader())
            {

                if (UserSkins.Read())
                {

                
                    for (int i = 1; i < UserSkins.FieldCount; i++)
                    {
                        if (string.IsNullOrEmpty(skinpath))
                        {
                            string imagePath = UserSkins.GetString(i);
                            if (imagePath == "Empty")
                                continue;
                            using SqlConnection NewConn = new SqlConnection(connectionString);
                            NewConn.Open();


                            string getPriceQuery = "SELECT Price FROM Skins WHERE ImagePath = @ImagePath";
                            using (SqlCommand getPriceCmd = new SqlCommand(getPriceQuery, NewConn))
                            {

                                getPriceCmd.Parameters.AddWithValue("@ImagePath", imagePath);
                                object result = getPriceCmd.ExecuteScalar();

                                if (result == null || result == DBNull.Value) //check if there was a result (null is if there was no match so returned null
                                //and DBNull.Value is if the column was found but it was null)
                                {
                                    return RedirectToPage();
                                }

                                double skinPrice = Convert.ToDouble(result);
                                string updateBalanceQuery = "UPDATE UPCB_Users SET balance = balance + @Price WHERE Id = @Id";
                                using (SqlCommand updateBalanceCmd = new SqlCommand(updateBalanceQuery, NewConn))
                                {
                                    updateBalanceCmd.Parameters.AddWithValue("@Price", skinPrice);
                                    updateBalanceCmd.Parameters.AddWithValue("@Id", Id);
                                    updateBalanceCmd.ExecuteNonQuery();
                                }
                            }



                            string updateQuery = $"UPDATE User_Skins SET Skin{i} = 'Empty' WHERE id = @Id";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, NewConn))
                            {
                                updateCmd.Parameters.AddWithValue("@Id", Id);
                                updateCmd.ExecuteNonQuery();
                            }
                            




                        }
                        if (UserSkins.GetString(i) == skinpath)
                        {
                            using SqlConnection NewConn = new SqlConnection(connectionString);
                            NewConn.Open();
                            string updateQuery = $"UPDATE User_Skins SET Skin{i} = 'Empty' WHERE id = @Id";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, NewConn))
                            {
                                updateCmd.Parameters.AddWithValue("@Id", Id);
                                updateCmd.ExecuteNonQuery();
                            }

                            string getPriceQuery = "SELECT Price FROM Skins WHERE ImagePath = @ImagePath";
                            using (SqlCommand getPriceCmd = new SqlCommand(getPriceQuery, NewConn))
                            {
                                getPriceCmd.Parameters.AddWithValue("@ImagePath", skinpath);
                                object result = getPriceCmd.ExecuteScalar();

                                if (result == null || result == DBNull.Value) //check if there was a result (null is if there was no match so returned null
                                //and DBNull.Value is if the column was found but it was null)
                                {
                                    return RedirectToPage(); 
                                }

                                double skinPrice = Convert.ToDouble(result);
                                string updateBalanceQuery = "UPDATE UPCB_Users SET balance = balance + @Price WHERE Id = @Id";
                                using (SqlCommand updateBalanceCmd = new SqlCommand(updateBalanceQuery, NewConn))
                                {
                                    updateBalanceCmd.Parameters.AddWithValue("@Price", skinPrice);
                                    updateBalanceCmd.Parameters.AddWithValue("@Id", Id);
                                    updateBalanceCmd.ExecuteNonQuery();
                                }
                            }




                            return RedirectToPage();
                        }
                    }
                }
            }

            return RedirectToPage();
            
        }

    }


}
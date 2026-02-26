using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Server_temp.Pages
{
    public class UserHelper
    {
        private readonly IConfiguration _configuration;

        public UserHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public decimal getMoneyFromUser(string Username)
        {
            string connectionString = _configuration.GetConnectionString("MyConnection");

            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql_string = "SELECT balance FROM UPCB_Users WHERE Username = @Username";

            SqlCommand checkCmd = new SqlCommand(sql_string ,conn);
            checkCmd.Parameters.AddWithValue("@Username", Username);

            object result = checkCmd.ExecuteScalar();

            decimal balance = Convert.ToDecimal(result);
            return balance;


        }

        public void changeMoneyToUser(string Username, decimal money)
        {
            string connectionString = _configuration.GetConnectionString("MyConnection");

            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql_string = "UPDATE UPCB_Users SET balance = balance + @money WHERE Username = @Username";

            SqlCommand checkCmd = new SqlCommand(sql_string, conn);
            checkCmd.Parameters.AddWithValue("@money", money);
            checkCmd.Parameters.AddWithValue("@Username", Username);

            checkCmd.ExecuteNonQuery();
        }

        public bool checkIfInventoryIsFull(int Id, string skinPath)
        {
            string connectionString = _configuration.GetConnectionString("MyConnection");

            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql_string = "SELECT * FROM User_Skins WHERE id = @Id";

            SqlCommand checkCmd = new SqlCommand(sql_string, conn);
            checkCmd.Parameters.AddWithValue("@id", Id);

            using SqlDataReader reader = checkCmd.ExecuteReader();

            if (reader.Read())
            {
                
                for (int i = 1; i < reader.FieldCount; i++) 
                {
                    
                    if (reader.GetString(i).Trim().Equals("Empty", StringComparison.OrdinalIgnoreCase))
                    {
                        
                        string columnName = reader.GetName(i); 

                        reader.Close();

                        
                        string updateSql = $"UPDATE User_Skins SET {columnName} = @skinPath WHERE id = @Id";
                        SqlCommand updateCmd = new SqlCommand(updateSql, conn);
                        updateCmd.Parameters.AddWithValue("@skinPath", skinPath);
                        updateCmd.Parameters.AddWithValue("@id", Id);

                        updateCmd.ExecuteNonQuery();

                        return false; 
                    }
                }
            }

            return true; 


        }

        // public decimal getPriceFromPath(string path)
        // {}


        // }
        
    }
}
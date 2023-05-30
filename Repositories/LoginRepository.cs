using Microsoft.Data.SqlClient;
using SimpleStocks.Utils;
using SimpleStocks.Models.UserLogin;
using SimpleStocks.Interfaces;

namespace SimpleStocks.Repositories
{
    public class LoginRepository : BaseRepository, ILoginRepository
    {
        private readonly IConfiguration _configuration;
        public LoginRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public LoginResponse LoginWithCredentials(LoginRequest loginRequest)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT TOP 1 [StockUser].Id AS Id, [StockUser].UserName AS UserName, [StockUser].Email AS Email, [StockUser].FirstName AS FirstName, [StockUser].LastName AS LastName, [StockUser].IsAdmin AS IsAdmin, 
                                                     [StockUser].AddressLineOne AS AddressLineOne, [StockUser].AddressLineTwo AS AddressLineTwo, [StockUser].City AS City,
                                                    [StockUser].State AS State, [StockUser].Zip AS Zip
                                                     FROM[StockUser]
                                                      INNER JOIN[Login] ON[StockUser].Id = [Login].UserId
                                                     WHERE [Login].PasswordHash = @PasswordHash AND [StockUser].Email = @Email";

                    cmd.Parameters.AddWithValue("@PasswordHash", loginRequest.PasswordHash);
                    //cmd.Parameters.AddWithValue("@Id", credentials.Id);
                    cmd.Parameters.AddWithValue("@Email", loginRequest.Email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            LoginResponse loginResponse = new LoginResponse
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                UserName = DbUtils.GetString(reader, "UserName"),
                                Email = DbUtils.GetString(reader, "Email"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName"),
                                IsAdmin = DbUtils.GetBoolean(reader, "IsAdmin"),
                                AddressLineOne = DbUtils.GetString(reader, "AddressLineOne"),
                                AddressLineTwo = DbUtils.GetString(reader, "AddressLineTwo"),
                                City = DbUtils.GetString(reader, "City"),
                                State = DbUtils.GetString(reader, "State"),
                                Zip = DbUtils.GetInt(reader, "Zip")
                            };
                            reader.Close();
                            return loginResponse;
                        }
                    }

                    return null;
                }
            }
        }

        
        public void UpdateCredentials(string Email, string PasswordHash)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE [LOGIN] SET [PasswordHash] = @passwordHash WHERE [Email] = @Email;";

                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@PasswordHash", PasswordHash);

                        cmd.ExecuteNonQuery();

                        Console.WriteLine("credentials successfully updated");
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


        }

    }
}

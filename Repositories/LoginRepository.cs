using Microsoft.Data.SqlClient;
using SimpleStocks.Utils;
using SimpleStocks.Models.UserLogin;
using SimpleStocks.Interfaces;
using System.Runtime.CompilerServices;

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
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
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
                                Zip = DbUtils.GetString(reader, "Zip")
                            };
                            reader.Close();
                            return loginResponse;
                        }
                    }

                    return null;
                }
            }
        }


        //public void UpdateCredentials(LoginRequest loginRequest, string NewPasswordHash)
        //{
        //    try
        //    {
        //        using (var conn = Connection)
        //        {
        //            conn.Open();

        //            using (var cmd = conn.CreateCommand())
        //            {
        //                cmd.CommandText = @"SELECT COUNT(*) FROM LOGIN WHERE Email = @OldEmail AND PasswordHash = @OldPasswordHash";

        //                cmd.Parameters.AddWithValue("@OldEmail", loginRequest.Email);
        //                cmd.Parameters.AddWithValue("@OldPasswordHash", loginRequest.PasswordHash);

        //                var recordCount = (int)cmd.ExecuteScalar();

        //                Console.WriteLine(recordCount);

        //                if (recordCount > 0)
        //                {
        //                    var validPassword = LoginWithCredentials(loginRequest);

        //                    Console.WriteLine(validPassword);

        //                    if (validPassword != null)
        //                    {
        //                        cmd.CommandText = @"UPDATE [LOGIN] SET [PasswordHash] = @NewPasswordHash WHERE Email = @Email;";

        //                        cmd.Parameters.Clear();
        //                        cmd.Parameters.AddWithValue("@PasswordHash", NewPasswordHash);
        //                        cmd.Parameters.AddWithValue("@Email", loginRequest.Email);

        //                        cmd.ExecuteNonQuery();

        //                        Console.WriteLine($"this is the run sql {cmd.CommandText}");

        //                        foreach (SqlParameter P in cmd.Parameters) 
        //                        {
        //                            Console.WriteLine($"{P.ParameterName} has a value of {P.Value}");
        //                        } 

        //                        Console.WriteLine("credentials successfully updated");
        //                    } else 
        //                    {
        //                        Console.WriteLine("this password is not valid in the system");
        //                    }
        //                } else 
        //                {

        //                    Console.WriteLine("the email password combo is not valid");

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.Message);
        //    }


        //}


        public void UpdateCredentials(LoginRequest loginRequest)
        {

            using (var conn = Connection)
            {
                conn.Open();

                using(var  cmd = conn.CreateCommand()) 
                {
                    cmd.CommandText = @"UPDATE [Login] SET [PasswordHash] = @PasswordHash, [Email] = @Email  
                                      WHERE Email = @Email;";

                    cmd.Parameters.AddWithValue("@PasswordHash", loginRequest.PasswordHash);
                    cmd.Parameters.AddWithValue("@Email", loginRequest.Email);

                    cmd.ExecuteNonQuery();
                }
            }
             
        }

    }
} 

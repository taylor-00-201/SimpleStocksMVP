using SimpleStocks.Models;
using Microsoft.Data.SqlClient;
using SimpleStocks.Utils;
using SimpleStocks.Models.UserLogin;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;
using SimpleStocks.Interfaces;

namespace SimpleStocks.Repositories
{
    public class StockUserRepository : BaseRepository, IStockUserRepository
    {
        private readonly IConfiguration _configuration;
        public StockUserRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }


        public List<StockUser> GetAllStockUsers()
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT [Id], [UserName], [Email], [FirstName], [LastName], [IsAdmin], [AddressLineOne], [AddressLineTwo], [City], [State], [Zip]
FROM [SimpleStocks].dbo.StockUser";

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            var StockUsers = new List<StockUser>();

                            while (reader.Read())
                            {
                                var User = new StockUser
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

                                StockUsers.Add(User);
                            }


                            conn.Close();
                            return StockUsers;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                Console.WriteLine("A Connection Error Occoured: " + Ex.Message);
                return null;
            }
        }

        public StockUser GetStockUserById(int id)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT [Id], [UserName], [Email], [FirstName], [LastName], [IsAdmin], [AddressLineOne], [AddressLineTwo], [City], [State], [Zip]
                        FROM [SimpleStocks].dbo.StockUser
                        WHERE Id = @id";

                        cmd.Parameters.AddWithValue(@"Id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                var User = new StockUser
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    UserName = DbUtils.GetString(reader, "UserName"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    FirstName = DbUtils.GetString(reader, "FirstName"),
                                    LastName = DbUtils.GetString(reader, "LastName"),
                                    IsAdmin = DbUtils.GetBoolean(reader, "IsAdmin")
                                };

                                return User;
                            }


                            conn.Close();
                        }

                        return null;
                    }
                }
            }
            catch (Exception Ex)
            {

                Console.WriteLine("A Connection Error Occoured: " + Ex.Message);
                return null;
            }
        }

        public void RegisterUser(RegisterUser registerUser)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [StockUser] ([UserName], [Email], [FirstName], [LastName], [IsAdmin], [AddressLineOne], [AddressLineTwo], [City], [State], [Zip])
    VALUES (@UserName, @Email, @FirstName, @LastName, @IsAdmin, @AddressLineOne, @AddressLineTwo, @City, @State, @Zip);

    DECLARE @NewStockUserId INT;
    SET @NewStockUserId = SCOPE_IDENTITY();

    INSERT INTO [Login] (UserID, PasswordHash, Email) 
    VALUES (@NewStockUserId, @PasswordHash, @Email);

    INSERT INTO [BankAccounts] ([UserId], [Balance])
    VALUES (@NewStockUserId, @Balance);
"
;

                    cmd.Parameters.AddWithValue("@UserName", registerUser.UserName);
                    cmd.Parameters.AddWithValue("@Email", registerUser.Email);
                    cmd.Parameters.AddWithValue("@FirstName", registerUser.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", registerUser.LastName);
                    cmd.Parameters.AddWithValue("@IsAdmin", false);
                    cmd.Parameters.AddWithValue("@PasswordHash", registerUser.PasswordHash);
                    cmd.Parameters.AddWithValue("@AddressLineOne", registerUser.AddressLineOne);
                    cmd.Parameters.AddWithValue("@AddressLineTwo", registerUser.AddressLineTwo);
                    cmd.Parameters.AddWithValue("@City", registerUser.City);
                    cmd.Parameters.AddWithValue("@State", registerUser.State);
                    cmd.Parameters.AddWithValue("@Zip", registerUser.Zip);

                    Random random = new Random();
                    int userBalance = random.Next(1, 1000000);
                    cmd.Parameters.AddWithValue("@Balance", userBalance);
                    cmd.ExecuteNonQuery();


                }

                conn.Close ();
            }
        }


        //public StockUser UpdateUser(UpdateStockUserModel userModel)
        //{
        //    try
        //    {
        //        using (SqlConnection conn = Connection)
        //        {
        //            conn.Open();

        //            using (SqlCommand cmd = conn.CreateCommand())
        //            {
        //                cmd.CommandText = @"UPDATE dbo.[StockUser] 
        //                              SET [UserName] = @UserName,
        //                                  [Email] = @Email,
        //                                  [FirstName] = @FirstName,
        //                                  [LastName] = @LastName
        //                                  WHERE Id = @Id
        //                                  UPDATE dbo.[Login]
        //                                  SET[Email] = @Email,
        //                                  [PasswordHash] = @PasswordHash
        //                                  WHERE UserId = @Id AND PasswordHash = @PasswordHash";

        //                cmd.Parameters.AddWithValue("@UserName", userModel.UserName);
        //                cmd.Parameters.AddWithValue("@Email", userModel.Email);
        //                cmd.Parameters.AddWithValue("@FirstName", userModel.FirstName);
        //                cmd.Parameters.AddWithValue("@LastName", userModel.LastName);
        //                cmd.Parameters.AddWithValue("@PasswordHash", userModel.PasswordHash);

        //                int affectedRows = cmd.ExecuteNonQuery();
        //                Console.WriteLine($"affected rows {affectedRows}");


        //            }
        //            return null;
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //       Console.WriteLine("The user was unabe to be updated " + Ex.Message);
        //    }
        //}

        public void DeleteUserById(int UserId)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM BankAccounts WHERE UserId = @UserId
                                        DELETE FROM Transactions WHERE UserId = @UserId
                                        DELETE FROM [Order] WHERE UserId = @UserId
                                        DELETE FROM UserAssets WHERE UserId = @UserId
                                        DELETE FROM Login WHERE UserId = @UserId
                                        DELETE FROM StockUser WHERE Id = @UserId";

                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }
            }
            catch (Exception Ex)
            {

                Console.WriteLine("There was an error deleteing the selected user: " + Ex.Message);
            }
        }
    }
}

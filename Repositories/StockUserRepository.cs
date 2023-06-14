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
                        cmd.CommandText = @"SELECT [Id], [UserName], [Email], [FirstName], [LastName], [IsAdmin], [AddressLineOne], [AddressLineTwo], [City], [State], [Zip], [Balance]
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
                                    Zip = DbUtils.GetString(reader, "Zip"),
                                    Balance = DbUtils.GetDecimal(reader, "Balance")
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
                        cmd.CommandText = @"SELECT [Id], [UserName], [Email], [FirstName], [LastName], [IsAdmin], [AddressLineOne], [AddressLineTwo], [City], [State], [Zip], [Balance]
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
                                    IsAdmin = DbUtils.GetBoolean(reader, "IsAdmin"),
                                    AddressLineOne = DbUtils.GetString(reader, "AddressLineOne"),
                                    AddressLineTwo = DbUtils.GetString(reader, "AddressLineTwo"),
                                    City = DbUtils.GetString(reader, "City"),
                                    State = DbUtils.GetString(reader, "State"),
                                    Zip = DbUtils.GetString(reader, "Zip"),
                                    Balance = DbUtils.GetDecimal(reader, "Balance")
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
                    cmd.CommandText = @"INSERT INTO [StockUser] ([UserName], [Email], [FirstName], [LastName], [IsAdmin], [AddressLineOne], [AddressLineTwo], [City], [State], [Zip], [Balance])
    VALUES (@UserName, @Email, @FirstName, @LastName, @IsAdmin, @AddressLineOne, @AddressLineTwo, @City, @State, @Zip, @Balance);

    DECLARE @NewStockUserId INT;
    SET @NewStockUserId = SCOPE_IDENTITY();

    INSERT INTO [Login] (UserID, PasswordHash, Email) 
    VALUES (@NewStockUserId, @PasswordHash, @Email);
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

                conn.Close();
            }
        }


        public void UpdateUser(UpdateStockUserModel userModel, int Id)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                                         

                                          UPDATE dbo.[Login]
                                          SET [Email] = @Email, [PasswordHash] = @PasswordHash
                                          WHERE UserId = @Id               
   
                                          UPDATE dbo.[StockUser] 
                                      SET [UserName] = @UserName,
                                          [Email] = @Email,
                                          [FirstName] = @FirstName,
                                          [LastName] = @LastName,
                                          [IsAdmin] = 0,
                                          [AddressLineOne] = @AddressLineOne,
                                          [AddressLineTwo] = @AddressLineTwo,
                                          [City] = @City,
                                          [State] = @State,
                                          [Zip] = @Zip
                                          WHERE Id = @Id

                                         ";

                        //ALTER TABLE Login CHECK CONSTRAINT FK__Login__Email__3D14070F;

                        //ALTER TABLE Login NOCHECK CONSTRAINT FK__Login__Email__3D14070F;

                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.Parameters.AddWithValue("@UserName", userModel.UserName);
                        cmd.Parameters.AddWithValue("@Email", userModel.Email);
                        cmd.Parameters.AddWithValue("@FirstName", userModel.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", userModel.LastName);
                        cmd.Parameters.AddWithValue("@PasswordHash", userModel.PasswordHash);
                        cmd.Parameters.AddWithValue("@AddressLineOne", userModel.AddressLineOne);
                        cmd.Parameters.AddWithValue("@AddressLineTwo", userModel.AddressLineTwo);
                        cmd.Parameters.AddWithValue("@City", userModel.City);
                        cmd.Parameters.AddWithValue("@State", userModel.State);
                        cmd.Parameters.AddWithValue("@Zip", userModel.Zip);
                        cmd.ExecuteNonQuery();

                    }

                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("The user was unabe to be updated " + Ex.Message);
            }


        }

        public void UpdateUserToAdmin(int Id)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                                          UPDATE dbo.[StockUser] 
                                      SET [IsAdmin] = 1
                                      WHERE Id = @Id;";

                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.ExecuteNonQuery();

                    }

                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("The user was unabe to be updated " + Ex.Message);
            }


        }

        public void DeleteUserById(int UserId)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                                        DELETE FROM Transactions WHERE UserId = @UserId
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

        public void AddToBankAccount(BankAccounts bankAccount)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE [StockUser] SET [Balance] = [Balance] + @Balance WHERE [Id] = @UserId";
                    cmd.Parameters.AddWithValue("@UserId", bankAccount.UserId);
                    cmd.Parameters.AddWithValue("@Balance", bankAccount.Balance);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SubtractFromBankAccount(BankAccounts bankAccount)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE [StockUser] SET [Balance] = [Balance] - @Balance WHERE [Id] = @UserId";
                    cmd.Parameters.AddWithValue("@UserId", bankAccount.UserId);
                    cmd.Parameters.AddWithValue("@Balance", bankAccount.Balance);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

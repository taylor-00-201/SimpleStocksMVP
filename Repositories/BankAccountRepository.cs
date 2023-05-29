//using Microsoft.Data.SqlClient;
//using SimpleStocks.Models;
//using SimpleStocks.Models.UserLogin;
//using SimpleStocks.Repositories;

//namespace SimpleStocks.Repositories
//{
//    public class BankAccountRepository : BaseRepository
//    {
//        private readonly IConfiguration _configuration;
//        public BankAccountRepository(IConfiguration configuration) : base(configuration)
//        {
//            _configuration = configuration;
//        }
//public void AddBankAccount(int AccountNumber, int RoutingNumber, string BankName)
//        {
//            try
//            {
//                using (SqlConnection conn = Connection) 
//                {
//                    conn.Open();

//                    using(SqlCommand cmd = conn.CreateCommand()) 
//                    {
//                        cmd.CommandText = @"INSERT INTO [BankAccounts] ([UserId], [Balance], [AccountNUmber], [RoutingNumber], [BankName])
//                  VALUES (
//                    @UserId, @Balance, @AccountNumeber, @RoutingNumber, @BankName)"
//                        ;



//                    cmd.Parameters.AddWithValue("@UserId", );
//                    Random random = new Random();
//                    int userBalance = random.Next(1, 1000000);
//                    cmd.Parameters.AddWithValue("@Balance",  userBalance);
//                    cmd.Parameters.AddWithValue("@AccountNumber", AccountNumber);
//                    cmd.Parameters.AddWithValue("@RoutingNumber",  RoutingNumber);
//                    cmd.Parameters.AddWithValue("@BankName",  BankName);

//                    }
//                }
//            }
//            catch (Exception Ex)
//            {

//                throw;
//            }
        
//        }
//    }
//}

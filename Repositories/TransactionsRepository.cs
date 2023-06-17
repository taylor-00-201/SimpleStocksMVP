using Microsoft.Data.SqlClient;
using SimpleStocks.Interfaces;
using SimpleStocks.Models;
using SimpleStocks.Utils;
using System.Collections.Generic;
using System.Data.Common;
using System.Transactions;

namespace SimpleStocks.Repositories
{
    public class TransactionsRepository : BaseRepository, ITransactionsRepository
    {
        private readonly IConfiguration _configuration;

        public TransactionsRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }



        public List<Transactions> ReturnTransactionsByUserIdBuy(int UserId)
        {
            List<Transactions> transactionList = new List<Transactions>();
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Transactions 
                                      WHERE UserId = @UserId AND TransactionType = @TransactionType;";
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TransactionType", "Buy");


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var transaction = new Transactions
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                UserId = DbUtils.GetInt(reader, "UserId"),
                                TransactionType = DbUtils.GetString(reader, "TransactionType"),
                                Quantity = DbUtils.GetInt(reader, "Quantity"),
                                AssetId = DbUtils.GetInt(reader, "AssetId"),
                                DateTime = DbUtils.GetDateTime(reader, "DateTime"),
                                Amount = DbUtils.GetDecimal(reader, "Amount")
                            };

                            transactionList.Add(transaction);

                        }
                    }

                    conn.Close();
                }

            }

            if (transactionList.Count == 0)
            {
                throw new Exception("There was a problem with the database");
            }

            return transactionList;
        }

        public List<Transactions> ReturnTransactionsByUserIdSell(int UserId)
        {
            List<Transactions> transactionList = new List<Transactions>();
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Transactions 
                                      WHERE UserId = @UserId AND TransactionType = @TransactionType;";
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TransactionType", "Sell");


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var transaction = new Transactions
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                UserId = DbUtils.GetInt(reader, "UserId"),
                                TransactionType = DbUtils.GetString(reader, "TransactionType"),
                                Quantity = DbUtils.GetInt(reader, "Quantity"),
                                AssetId = DbUtils.GetInt(reader, "AssetId"),
                                DateTime = DbUtils.GetDateTime(reader, "DateTime"),
                                Amount = DbUtils.GetDecimal(reader, "Amount")
                            };

                            transactionList.Add(transaction);

                        }
                    }

                    conn.Close();
                }

            }

            if (transactionList.Count == 0)
            {
                throw new Exception("There was a problem with the database");
            }

            return transactionList;
        }

        public List<Transactions> ReturnTransactionsByUserId(int UserId)
        {
            List<Transactions> transactionList = new List<Transactions>();
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Transactions 
                                      WHERE UserId = @UserId;";
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var transaction = new Transactions
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                UserId = DbUtils.GetInt(reader, "UserId"),
                                TransactionType = DbUtils.GetString(reader, "TransactionType"),
                                Quantity = DbUtils.GetInt(reader, "Quantity"),
                                AssetId = DbUtils.GetInt(reader, "AssetId"),
                                DateTime = DbUtils.GetDateTime(reader, "DateTime"),
                                Amount = DbUtils.GetDecimal(reader, "Amount")
                            };

                            transactionList.Add(transaction);

                        }
                    }

                    conn.Close();
                }

            }

            if (transactionList.Count == 0)
            {
                Console.WriteLine("There was a problem with the database");
            }

            return transactionList;
        }


        public void ProcessTransaction(Transactions transaction)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO Transactions ([UserId], [TransactionType], [Quantity], [AssetId], [DateTime], [Amount]) 
                                       Values (@UserId, @TransactionType, @Quantity, @AssetId, @DateTime, @Amount);";

                        cmd.Parameters.AddWithValue("@UserId", transaction.UserId);
                        cmd.Parameters.AddWithValue("@TransactionType", transaction.TransactionType);
                        cmd.Parameters.AddWithValue("@Quantity", transaction.Quantity);
                        cmd.Parameters.AddWithValue("@AssetId", transaction.AssetId);
                        cmd.Parameters.AddWithValue("@DateTime", transaction.DateTime);
                        cmd.Parameters.AddWithValue("@Amount", transaction.Amount);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception Ex)
            {

                Console.WriteLine($"an error occoured: {Ex.Message}");
            }
        }

        public void UpdateTransaction(Transactions transaction, int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE dbo.[Transactions]
                                 SET  TransactionType = @TransactionType,
                                      Quantity = @Quantity,
                                      AssetId = @AssetId,
                                      DateTime = @DateTime,
                                      Amount = @Amount
                                      Where Id = @Id;";

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@TransactionType", transaction.TransactionType);
                    cmd.Parameters.AddWithValue("@Quantity", transaction.Quantity);
                    cmd.Parameters.AddWithValue("@AssetId", transaction.AssetId);
                    cmd.Parameters.AddWithValue("@DateTime", transaction.DateTime);
                    cmd.Parameters.AddWithValue("@Amount", transaction.Amount);
                    var foundTransaction = (int)cmd.ExecuteNonQuery();


                    if (foundTransaction == 0)
                    {
                        throw new Exception("the record was not found");
                    }
                }
            }
        }


        public void DeleteTransaction(int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Transactions WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("@Id", Id);
                   var count = (int)cmd.ExecuteNonQuery();


                    if (count == 0) 
                    {
                        throw new Exception("the record was not found");
                    }
                }
            }
        }







    }
}
 
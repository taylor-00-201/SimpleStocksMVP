using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.Data.SqlClient;
using SimpleStocks.Interfaces;
using SimpleStocks.Models;
using SimpleStocks.Models.UserLogin;

namespace SimpleStocks.Repositories
{
    public class AssetRepository : BaseRepository, IAssetRepository
    {
        private readonly IConfiguration _configuration;

        public AssetRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public void AddAsset(Asset asset)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO Assets ([symbol], [Name], [CurrentPrice]) VALUES (@symbol, @Name, @CurrentPrice);";
                        cmd.Parameters.AddWithValue("@symbol", asset.Symbol);
                        cmd.Parameters.AddWithValue("@Name", asset.Name);
                        cmd.Parameters.AddWithValue("@CurrentPrice", asset.CurrentPrice);
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }

        public void RemoveAsset(int Id)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM ASSETS ([symbol], [Name], [CurrentPrice) VALUES (@symbol, @Name, @CurrentPrice) 
                                          WHERE Id = @Id;";
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }

        //public void UpdateAsset()
        //{

        //}

    }
}

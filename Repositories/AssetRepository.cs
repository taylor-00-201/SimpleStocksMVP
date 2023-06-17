using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using SimpleStocks.Interfaces;
using SimpleStocks.Models;
using SimpleStocks.Models.UserLogin;
using SimpleStocks.Utils;

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

        public Asset GetAssetById(int AssetId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Assets WHERE Id = @AssetId";
                    cmd.Parameters.AddWithValue("@AssetId", AssetId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var asset = new Asset
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Symbol = DbUtils.GetString(reader, "symbol"),
                                Name = DbUtils.GetString(reader, "Name"),
                                CurrentPrice = DbUtils.GetDecimal(reader, "CurrentPrice")
                            };

                            return asset;

                        }
                        else
                        {
                            throw new Exception("the asset does not exist");
                        }

                    }
                }
            }
        }

        public Asset GetAssetByName(string Name)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Assets WHERE Name = @Name";
                    cmd.Parameters.AddWithValue("@Name", Name);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var asset = new Asset
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Symbol = DbUtils.GetString(reader, "symbol"),
                                Name = DbUtils.GetString(reader, "Name"),
                                CurrentPrice = DbUtils.GetDecimal(reader, "CurrentPrice")
                            };

                            return asset;

                        }
                        else
                        {
                            throw new Exception("the asset does not exist");
                        }

                    }
                }
            }
        }

        public Asset GetAssetBySymbol(string Symbol)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Assets WHERE Symbol = @Symbol";
                    cmd.Parameters.AddWithValue("@Symbol", Symbol);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var asset = new Asset
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Symbol = DbUtils.GetString(reader, "symbol"),
                                Name = DbUtils.GetString(reader, "Name"),
                                CurrentPrice = DbUtils.GetDecimal(reader, "CurrentPrice")
                            };

                            return asset;

                        }
                        else
                        {
                            throw new Exception("the asset does not exist");
                        }

                    }
                }
            }
        }

        public List<Asset> GetAllAssets()
        {
            List<Asset> assets = new List<Asset>();
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Assets";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var asset = new Asset
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Symbol = DbUtils.GetString(reader, "symbol"),
                                Name = DbUtils.GetString(reader, "Name"),
                                CurrentPrice = DbUtils.GetDecimal(reader, "CurrentPrice")
                            };

                            assets.Add(asset);


                        }
                    }
                }

                if (assets.Count == 0)
                {
                    Console.WriteLine("No assets found");
                }
            }
            return assets;
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
                        cmd.CommandText = @"DELETE FROM ASSETS WHERE Id = @Id;";
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

        public void UpdateAsset(Asset asset, int Id)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE dbo.ASSETS 
                                          SET [Symbol] = @Symbol,
                                              [Name] = @Name,
                                              [CurrentPrice] = @CurrentPrice
                                              WHERE Id = @Id;";
                        cmd.Parameters.AddWithValue("@Id", Id);
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

    }
}

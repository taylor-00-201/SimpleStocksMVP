using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleStocks.Repositories;


namespace SimpleStocks.Services
{
    public class ProcessTransactions
    {
        public static string AddAnAssetEntryPoint(string values) 
        {
            try
            {   
                JObject value = JObject.Parse(values);
                string? AssetId = value["Id"].ToString();
                string? Symbol = value["symbol"].ToString();
                string? name = value["name"].ToString();
                string? price = value["currentPrice"].ToString();

            }
            catch (Exception)
            {

                throw;
            }
            
            
            return string.Empty;
        }

        private static string AddTransation()
        {



            return string.Empty;
        }

        private static string ModifyUserBlance()
        {



            return string.Empty;
        }


        private static string AddAsset() 
        {



            return string.Empty;
        }
    }
}

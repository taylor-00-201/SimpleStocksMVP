//using Microsoft.AspNetCore.Authentication;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using SimpleStocks.Interfaces;
//using SimpleStocks.Repositories;
//using SimpleStocks.Models;


//namespace SimpleStocks.Services
//{
//    public class ProcessTransactions
//    {

//        private readonly IAssetRepository _assetRepository;
//        private readonly IStockUserRepository _stockUserRepository;
//        private readonly ITransactionsRepository _transactionsRepository;

//        public ProcessTransactions(IAssetRepository assetRepository, IStockUserRepository stockUserRepository, ITransactionsRepository transactionsRepository) 
//        {
//            _assetRepository = assetRepository;
//            _stockUserRepository = stockUserRepository;
//            _transactionsRepository = transactionsRepository;
//        }
        
//        public string AddAnAssetEntryPoint(string values) 
//        {
//            try
//            {   
//                JObject value = JObject.Parse(values);
//                string? AssetId = value["Id"].ToString();
//                string? symbol = value["symbol"].ToString();
//                string? Name = value["Name"].ToString();
//                string? Price = value["CurrentPrice"].ToString();
                

//                Transactions transactions = new Transactions 
//                {

//                };
//                _transactionsRepository.ProcessTransaction(transactions);

//                BankAccounts bankAccounts = new BankAccounts 
//                {

//                };



//                if (transactions.TransactionType == "Buy")
//                {
//                    _stockUserRepository.SubtractFromBankAccount(bankAccounts);
//                }
//                else 
//                {
//                    _stockUserRepository.AddToBankAccount(bankAccounts);
//                }


//                Asset asset = new Asset 
//                {
//                    Id = AssetId, 
//                    Symbol = symbol, 
//                    Name = Name,
//                    CurrentPrice = Price
//                }
//                _assetRepository.UpdateAsset(asset, AssetId)

//            }
//            catch (Exception Ex)
//            {

//                Console.WriteLine(Ex.Message);
//            }
            
            
//            return string.Empty;
//        }
//    }
//}

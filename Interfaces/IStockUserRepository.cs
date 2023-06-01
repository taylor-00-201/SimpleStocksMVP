using SimpleStocks.Models;
using SimpleStocks.Models.UserLogin;

namespace SimpleStocks.Interfaces
{
    public interface IStockUserRepository
    {
        void AddToBankAccount(BankAccounts bankAccount);
        void DeleteUserById(int UserId);
        List<StockUser> GetAllStockUsers();
        StockUser GetStockUserById(int id);
        void RegisterUser(RegisterUser registerUser);
        void SubtractFromBankAccount(BankAccounts bankAccount);
        StockUser UpdateUser(UpdateStockUserModel userModel);
    }
}
using SimpleStocks.Models;
using SimpleStocks.Models.UserLogin;

namespace SimpleStocks.Interfaces
{
    public interface IStockUserRepository
    {
        void DeleteUserById(int UserId);
        List<StockUser> GetAllStockUsers();
        StockUser GetStockUserById(int id);
        void RegisterUser(RegisterUser registerUser);
       // StockUser UpdateUser(UpdateStockUserModel userModel);
    }
}
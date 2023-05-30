using SimpleStocks.Models.UserLogin;

namespace SimpleStocks.Interfaces
{
    public interface ILoginRepository
    {
        LoginResponse LoginWithCredentials(LoginRequest loginRequest);
        void UpdateCredentials(string Email, string PasswordHash);
    }
}
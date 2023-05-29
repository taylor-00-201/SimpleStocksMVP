using SimpleStocks.Models.UserLogin;

namespace SimpleStocks.Interfaces
{
    public interface ILoginRepository
    {
        LoginResponse LoginWithCredentials(LoginRequest loginRequest);
        void UpdateCredentialsAdmin(string Email, string PasswordHash);
    }
}